﻿using FOS.DataLayer;
using FOS.Web.UI.Common;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using System.Web.Http;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Net.Http.Headers;

namespace FOS.Web.UI.Controllers.API
{
    public class OrderForExcelController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        [HttpPost]
        public Result<SuccessResponse> Post(OrderSummery rm)
        {
            // Retailer retailerObj = new Retailer();
            try
            {
                Microsoft.Reporting.WebForms.LocalReport ReportViewer1 = new Microsoft.Reporting.WebForms.LocalReport();

                DateTime Todate = DateTime.Parse(rm.DateTo);
                DateTime newDate = Todate.AddDays(1);
                DateTime FromDate = DateTime.Parse(rm.DateFrom);

                string DateTO = Todate.ToString("dd-MM-yyyy");
                string FromTO = FromDate.ToString("dd-MM-yyyy");

                if (rm.Status == "Daily")

                {
                    var number = "";
                   

                        try
                        {
                            decimal linesperbill = 0;
                            int? total = 0;
                            //List<Sp_OrderSummaryGrandTotal_Result> gt = db.Sp_OrderSummaryGrandTotal(FromDate, newDate, rm.DistributorID, rm.RangeID, rm.SaleOfficerID).ToList();
                           


                            // List<Sp_OrderSummeryReportNotSoldItem1_1_Result> NotItems = db.Sp_OrderSummeryReportNotSoldItem1_1(FromDate, newDate, rm.DistributorID, rm.RangeID, rm.SaleOfficerID).ToList();
                            List<Sp_OrderForPDFinMMCTest_Result> result = db.Sp_OrderForPDFinMMCTest(FromDate, newDate, rm.DistributorID, rm.RangeID, rm.SaleOfficerID).ToList();
                          List<sp_BrandAndItemWiseReport_Result> result2 = db.sp_BrandAndItemWiseReport(FromDate, newDate, 0, rm.SaleOfficerID,0,0, rm.RangeID).ToList();
                        //List<Sp_OurBrandForPDFinMMC_Result> result2 = db.Sp_OurBrandForPDFinMMC(FromDate, newDate, rm.DistributorID, rm.RangeID, rm.SaleOfficerID).ToList();
                        List<Sp_FollowUpVisitsDailyForMMC_Result> result3 = db.Sp_FollowUpVisitsDailyForMMC(FromDate, newDate, rm.DistributorID, rm.RangeID, rm.SaleOfficerID).ToList();
                        if (result.Count > 0)
                            {

                                string dealername = "";
                                string CityName = "";
                                var dealer = db.Dealers.Where(u => u.ID == rm.DistributorID).FirstOrDefault();

                                dealername = dealer.ShopName;
                                CityName = dealer.City.Name;




                                string SoName = "";
                                var SO = db.SaleOfficers.Where(u => u.ID == rm.SaleOfficerID).FirstOrDefault();

                                SoName = SO.Name;

                                string RangeName = "";
                                var range = db.MainCategories.Where(u => u.MainCategID == rm.RangeID).FirstOrDefault();

                                RangeName = range.MainCategDesc;

                            var totalVisitsToday = db.JobsDetails.Where(x => x.SalesOficerID == rm.SaleOfficerID && x.JobDate >= FromDate && x.JobDate <= newDate && x.Status == true).ToList();

                            var listi = totalVisitsToday.Count();

                            DateTime? firstRecord = totalVisitsToday.Where(x => x.SalesOficerID == rm.SaleOfficerID && x.JobDate >= FromDate && x.JobDate <= newDate).Select(x => x.JobDate).FirstOrDefault();
                            DateTime? lastRecord = totalVisitsToday.Where(x => x.SalesOficerID == rm.SaleOfficerID && x.JobDate >= FromDate && x.JobDate <= newDate).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();


                            var ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == rm.SaleOfficerID && x.JobDate >= FromDate && x.JobDate <= newDate  && x.VisitPurpose == "Ordering" && x.Status == true
                            ).Select(x => x.ID).Count();

                            var FollowUpsShops = totalVisitsToday.Where(x => x.SalesOficerID == rm.SaleOfficerID && x.JobDate >= FromDate && x.JobDate <= newDate  && x.VisitPurpose == "FollowupVisit" && x.Status == true
                           ).Select(x => x.ID).Count();

                            TimeSpan? difference = (lastRecord - firstRecord);
                            var format = difference;
                            string test = difference.HasValue ? difference.Value.ToString(@"hh\:mm") : string.Empty;

                            var SOID = db.SaleOfficers.Where(x => x.ID == rm.SaleOfficerID).Select(x => x.SORoleID).FirstOrDefault()
;
                            if (SOID == 1)
                            {
                                ReportParameter[] prm = new ReportParameter[10];
                                prm[0] = new ReportParameter("DistributorName", dealername);
                                prm[1] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                                prm[2] = new ReportParameter("SOName", SoName);

                                prm[3] = new ReportParameter("DateTo", DateTO);
                                prm[4] = new ReportParameter("DateFrom", FromTO);
                                prm[5] = new ReportParameter("CityName", CityName);
                                prm[6] = new ReportParameter("TotalVisitsToday", listi.ToString());
                                prm[7] = new ReportParameter("ProductiveShops", ProductiveShops.ToString());
                                prm[8] = new ReportParameter("TodayWorkingTime", test);
                                prm[9] = new ReportParameter("FollowUps", FollowUpsShops.ToString());
                                ReportViewer1.ReportPath = HttpContext.Current.Server.MapPath("~\\Views\\Reports\\MMCOrders.rdlc");
                                ReportViewer1.EnableExternalImages = true;
                                ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
                                ReportDataSource dt2 = new ReportDataSource("DataSet2", result2);
                                ReportDataSource dt3 = new ReportDataSource("DataSet3", result3);
                                //ReportDataSource dt4 = new ReportDataSource("DataSet4", result3);
                                ReportViewer1.SetParameters(prm);
                                ReportViewer1.DataSources.Clear();
                                ReportViewer1.DataSources.Add(dt1);
                                ReportViewer1.DataSources.Add(dt2);
                                ReportViewer1.DataSources.Add(dt3);
                                //ReportViewer1.DataSources.Add(dt4);

                            }
                            else
                            {

                                ReportParameter[] prm = new ReportParameter[10];
                                prm[0] = new ReportParameter("DistributorName", dealername);
                                prm[1] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                                prm[2] = new ReportParameter("SOName", SoName);

                                prm[3] = new ReportParameter("DateTo", DateTO);
                                prm[4] = new ReportParameter("DateFrom", FromTO);
                                prm[5] = new ReportParameter("CityName", CityName);
                                prm[6] = new ReportParameter("TotalVisitsToday", listi.ToString());
                                prm[7] = new ReportParameter("ProductiveShops", ProductiveShops.ToString());
                                prm[8] = new ReportParameter("TodayWorkingTime", test);
                                prm[9] = new ReportParameter("FollowUps", FollowUpsShops.ToString());
                                ReportViewer1.ReportPath = HttpContext.Current.Server.MapPath("~\\Views\\Reports\\MMCOrdersRSM.rdlc");
                                ReportViewer1.EnableExternalImages = true;
                                ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
                                ReportDataSource dt2 = new ReportDataSource("DataSet2", result2);
                                //ReportDataSource dt3 = new ReportDataSource("DataSet3", result2);
                                //ReportDataSource dt4 = new ReportDataSource("DataSet4", result3);
                                ReportViewer1.SetParameters(prm);
                                ReportViewer1.DataSources.Clear();
                                ReportViewer1.DataSources.Add(dt1);
                                ReportViewer1.DataSources.Add(dt2);
                                //ReportViewer1.DataSources.Add(dt3);
                                //ReportViewer1.DataSources.Add(dt4);
                            }
                            ReportViewer1.Refresh();
                            Warning[] warnings;
                            string[] streamids;
                            string mimeType;
                            string encoding;
                            string extension;
                            byte[] bytes = ReportViewer1.Render("PDF", null, out mimeType,
                                    out encoding, out extension, out streamids, out warnings);
                            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                                // HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                                using (MemoryStream memoryStream = new MemoryStream(bytes))
                                {

                                    memoryStream.Seek(0, SeekOrigin.Begin);

                                    memoryStream.Close();

                                    if (rm.Type == "Email")
                                    {

                                        MailMessage mm = new MailMessage("GPCInfo786@gmail.com", rm.Email);
                                        mm.Subject = "Order PDF";
                                        mm.Body = " RetailerOrder PDF Attachment";
                                        mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "RetailerDailyOrder.pdf"));
                                        mm.IsBodyHtml = true;
                                        SmtpClient smtp = new SmtpClient();
                                        smtp.Host = "smtp.gmail.com";
                                        smtp.EnableSsl = true;
                                        NetworkCredential NetworkCred = new NetworkCredential();
                                        NetworkCred.UserName = "GPCInfo786@gmail.com";
                                        NetworkCred.Password = "harry11223344";
                                        smtp.UseDefaultCredentials = true;
                                        smtp.Credentials = NetworkCred;
                                        smtp.Port = 587;
                                        smtp.Send(mm);
                                        //HttpResponseMessage response6 = new HttpResponseMessage(HttpStatusCode.OK);
                                        //string url = "Email sent successfully";
                                        //response6.Content = new StringContent(url);
                                        //return response6;
                                    }
                                    else if (rm.Type == "Download")
                                    {
                                        SuccessResponse d = new SuccessResponse();
                                        string fname = "D" + DateTime.Now.ToString("ddMMyyyyHHss");
                                        System.IO.File.WriteAllBytes(HttpContext.Current.Server.MapPath("~") + "/PDF/" + fname + ".pdf", bytes);
                                        HttpResponseMessage response2 = new HttpResponseMessage(HttpStatusCode.OK);
                                        d.data = "http://mmc.workforcepk.com/" + "\\PDF\\" + fname + ".pdf";
                                        return new Result<SuccessResponse>
                                        {
                                            Data = d,
                                            Message = "Downloaded",
                                            ResultType = ResultType.Success,
                                            Exception = null,
                                            ValidationErrors = null
                                        };

                                    }
                                    else
                                    {
                                        var fileStream = new FileStream(@"~/Images/SchoolPictures/", FileMode.Create);
                                        var pdfWriter2 = PdfWriter.GetInstance(pdfDoc, fileStream);

                                    }
                                }

                            }
                            else
                            {
                                return new Result<SuccessResponse>
                                {
                                    Data = null,
                                    Message = "No data Present today for this Distributor",
                                    ResultType = ResultType.Success,
                                    Exception = null,

                                };

                            }

                        }
                        catch (Exception ex)
                        {
                            return new Result<SuccessResponse>
                            {
                                Data = null,
                                Message = ex.InnerException.Message,
                                ResultType = ResultType.Success,
                                Exception = null,

                            };
                        }

                   

                }







                else if (rm.Status == "Weekly")
                {
                    try
                    {
                        decimal? total = 0;
                        List<Sp_OrderSummeryReportInExcelRangeWiseWeeklyReport_Result> result = db.Sp_OrderSummeryReportInExcelRangeWiseWeeklyReport(FromDate, newDate, 6, rm.SaleOfficerID).ToList();

                        foreach (var item in result)
                        {
                            total += item.Subtotal;
                        }
                        string SoName = "";
                        List<SaleOfficer> SO = db.SaleOfficers.Where(u => u.ID == rm.SaleOfficerID).ToList();
                        foreach (var SOS in SO)
                        {
                            SoName = SOS.Name;
                        }
                        string RangeName = "";
                        List<MainCategory> Region = db.MainCategories.Where(u => u.MainCategID == rm.RangeID).ToList();
                        foreach (var SOS in Region)
                        {
                            RangeName = SOS.MainCategDesc;
                        }

                        ReportParameter[] prm = new ReportParameter[5];

                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("SOName", SoName);
                   
                        prm[2] = new ReportParameter("DateTo", DateTO);
                        prm[3] = new ReportParameter("DateFrom", FromTO);
                        prm[4] = new ReportParameter("LineTotal", total.ToString());
                        ReportViewer1.ReportPath = HttpContext.Current.Server.MapPath("~\\Views\\Reports\\WeeklyReport.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);
                        ReportViewer1.Refresh();
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;
                        byte[] bytes = ReportViewer1.Render("PDF", null, out mimeType,
                                out encoding, out extension, out streamids, out warnings);
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        // HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        using (MemoryStream memoryStream = new MemoryStream(bytes))
                        {

                            memoryStream.Seek(0, SeekOrigin.Begin);

                            memoryStream.Close();

                            if (rm.Type == "Email")
                            {

                                MailMessage mm = new MailMessage("GPCInfo786@gmail.com", rm.Email);
                                mm.Subject = "Order PDF";
                                mm.Body = " RetailerOrder PDF Attachment";
                                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "RetailersWeeklyOrder.pdf"));
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential();
                                NetworkCred.UserName = "GPCInfo786@gmail.com";
                                NetworkCred.Password = "harry11223344";
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);

                            }
                            else if (rm.Type == "Download")
                            {
                                SuccessResponse d = new SuccessResponse();
                                string fname = "W" + DateTime.Now.ToString("ddMMyyyyHHss");
                                System.IO.File.WriteAllBytes(HttpContext.Current.Server.MapPath("~") + "/PDF/" + fname + ".pdf", bytes);
                                HttpResponseMessage response2 = new HttpResponseMessage(HttpStatusCode.OK);
                                d.data = "http://panda.workforcepk.com/" + "\\PDF\\" + fname + ".pdf";
                                return new Result<SuccessResponse>
                                {
                                    Data = d,
                                    Message = "Email Sent Successfully",
                                    ResultType = ResultType.Success,
                                    Exception = null,
                                    ValidationErrors = null
                                };
                            }
                            else
                            {
                                var fileStream = new FileStream(@"~/Images/SchoolPictures/", FileMode.Create);
                                var pdfWriter2 = PdfWriter.GetInstance(pdfDoc, fileStream);

                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        return new Result<SuccessResponse>
                        {
                            Data = null,
                            Message = "Something Went Wrong",
                            ResultType = ResultType.Failure,
                            Exception = null,

                        };
                    }
                }
                else if (rm.Status == "Monthly")
                {
                    try
                    {
                        decimal? total = 0;
                        List<Sp_OrderSummeryReportInExcelRangeWiseWeeklyReport_Result> result = db.Sp_OrderSummeryReportInExcelRangeWiseWeeklyReport(FromDate, newDate, 6, rm.SaleOfficerID).ToList();
                        foreach (var item in result)
                        {
                            total += item.Subtotal;
                        }
                        string SoName = "";
                        List<SaleOfficer> SO = db.SaleOfficers.Where(u => u.ID == rm.SaleOfficerID).ToList();
                        foreach (var SOS in SO)
                        {
                            SoName = SOS.Name;
                        }
                        string RangeName = "";
                        List<MainCategory> Region = db.MainCategories.Where(u => u.MainCategID == rm.RangeID).ToList();
                        foreach (var SOS in Region)
                        {
                            RangeName = SOS.MainCategDesc;
                        }

                        ReportParameter[] prm = new ReportParameter[5];
                     
                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("SOName", SoName);
                        prm[2] = new ReportParameter("DateTo", DateTO);
                        prm[3] = new ReportParameter("DateFrom", FromTO);
                        prm[4] = new ReportParameter("LineTotal", total.ToString());
                        ReportViewer1.ReportPath = HttpContext.Current.Server.MapPath("~\\Views\\Reports\\MonthlyReport.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);
                        ReportViewer1.Refresh();
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;
                        byte[] bytes = ReportViewer1.Render("PDF", null, out mimeType,
                                out encoding, out extension, out streamids, out warnings);
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        // HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        using (MemoryStream memoryStream = new MemoryStream(bytes))
                        {

                            memoryStream.Seek(0, SeekOrigin.Begin);

                            memoryStream.Close();

                            if (rm.Type == "Email")
                            {

                                MailMessage mm = new MailMessage("GPCInfo786@gmail.com", rm.Email);
                                mm.Subject = "Order PDF";
                                mm.Body = " RetailerOrder PDF Attachment";
                                mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "RetailerMonthlyOrder.pdf"));
                                mm.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential();
                                NetworkCred.UserName = "GPCInfo786@gmail.com";
                                NetworkCred.Password = "harry11223344";
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mm);

                            }
                            else if (rm.Type == "Download")
                            {
                                SuccessResponse d = new SuccessResponse();
                                string fname = "M" + DateTime.Now.ToString("ddMMyyyyHHss");
                                System.IO.File.WriteAllBytes(HttpContext.Current.Server.MapPath("~") + "/PDF/" + fname + ".pdf", bytes);
                                HttpResponseMessage response2 = new HttpResponseMessage(HttpStatusCode.OK);
                                d.data = "http://panda.workforcepk.com/" + "\\PDF\\" + fname + ".pdf";
                                return new Result<SuccessResponse>
                                {
                                    Data = d,
                                    Message = "Email Sent Successfully",
                                    ResultType = ResultType.Success,
                                    Exception = null,
                                    ValidationErrors = null
                                };
                            }
                            else
                            {
                                var fileStream = new FileStream(@"~/Images/SchoolPictures/", FileMode.Create);
                                var pdfWriter2 = PdfWriter.GetInstance(pdfDoc, fileStream);

                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        return new Result<SuccessResponse>
                        {
                            Data = null,
                            Message = "Something Went Wrong",
                            ResultType = ResultType.Failure,
                            Exception = null,

                        };
                    }


                }



                return new Result<SuccessResponse>
                {
                    Data = null,
                    Message = "There is an issue with your internet. Kindly Try again",
                    ResultType = ResultType.Success,
                    Exception = null,

                };





            }

            catch (Exception ex)
            {


                return new Result<SuccessResponse>
                {
                    Data = null,
                    Message = "Something Went Wrong",
                    ResultType = ResultType.Failure,
                    Exception = null,

                };

            }

        }


    }



}



public class SuccessResponse
{
    public string data { get; set; }
}
public class OrderSummery
{
    public OrderSummery()
    {
        CompititorInformation = new List<CompititorInfoModel>();
    }
    public int RetailerID { get; set; }
    public string ShopName { get; set; }
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public int DistributorID { get; set; }
    public int RangeID { get; set; }
    public int SaleOfficerID { get; set; }
    public string Email { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }

    public List<CompititorInfoModel> CompititorInformation { get; set; }



}

public class CompititorInfoModel
{
    public int SaleOfficerID { get; set; }
    public int RetailerID { get; set; }

    public int SylabusID { get; set; }
}
