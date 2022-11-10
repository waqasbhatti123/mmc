using FOS.Setup;
using FOS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FOS.DataLayer;
using FluentValidation.Results;
using FOS.Web.UI.Models;
using FOS.Web.UI.Common.CustomAttributes;


using FOS.Web.UI.DataSets;
using CrystalDecisions.CrystalReports.Engine;
using Shared.Diagnostics.Logging;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Data.Entity.Core.Objects;
using System.Net;

namespace FOS.Web.UI.Controllers
{
    public class ReportsController : Controller
    {
        FOSDataModel db = new FOSDataModel();


    
    #region FOS Wise Date/Month Wise Intake Delivered Report-1A
    [HttpGet]
        public ActionResult FosDateWiseReport()
        {
            ReportsInputData obj = new ReportsInputData();
            ManagePainter objPainter = new ManagePainter();
            obj.Territories = objPainter.GetTerritoryNamesList();
            obj.FosNames = objPainter.getFosLov(0).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
            var customer = objPainter.GetCityList(1);
            obj.PainterCityNames = customer;
            return View(obj);
        }
        #endregion

        #region FOS Wise Date/Month Wise Intake Report
        [HttpGet]
        public ActionResult FosDateMonthWiseIntakeReport()
        {
            ReportsInputData obj = new ReportsInputData();
            ManagePainter objPainter = new ManagePainter();
            obj.Territories = objPainter.GetTerritoryNamesList();
            obj.FosNames = objPainter.getFosLov(0).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
            var customer = objPainter.GetCityList(1);
            obj.PainterCityNames = customer;
            return View(obj);
        }
        #endregion

        #region City Wise Date Wise Intake Delivered Report
        [HttpGet]
        public ActionResult CityDateWiseRetailerReport()
        {
            ReportsInputData obj = new ReportsInputData();
            ManagePainter objPainter = new ManagePainter();
            obj.Territories = objPainter.GetTerritoryNamesList();
            obj.FosNames = objPainter.getFosLov(0).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
            var customer = objPainter.GetCityList(1);
            obj.PainterCityNames = customer;
            return View(obj);
        }
        #endregion

        #region City Wise Month Wise Intake Delivered Report
        [HttpGet]
        public ActionResult CityDateMonthWiseIntakeReport()
        {
            ReportsInputData obj = new ReportsInputData();
            ManagePainter objPainter = new ManagePainter();
            obj.Territories = objPainter.GetTerritoryNamesList();
            obj.FosNames = objPainter.getFosLov(0).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
            var customer = objPainter.GetCityList(1);
            obj.PainterCityNames = customer;
            return View(obj);
        }
        #endregion

        #region Retail Shop Wise Date Wise Intake Delivered Report
        [HttpGet]
        public ActionResult RetailerShopDateWiseReport()
        {
            ReportsInputData obj = new ReportsInputData();
            ManagePainter objPainter = new ManagePainter();
            obj.Territories = objPainter.GetTerritoryNamesList();
            obj.FosNames = objPainter.getFosLov(0).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
            var customer = objPainter.GetAllCitiesList();
            obj.PainterCityNames = customer;
            return View(obj);
        }
        #endregion

        #region Retail Shop Wise Month Wise Intake Delivered Report
        [HttpGet]
        public ActionResult RetailerShopDateMonthWiseIntakeReport()
        {
            ReportsInputData obj = new ReportsInputData();
            ManagePainter objPainter = new ManagePainter();
            obj.Territories = objPainter.GetTerritoryNamesList();
            obj.FosNames = objPainter.getFosLov(0).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
            var customer = objPainter.GetAllCitiesList();
            obj.PainterCityNames = customer;
            return View(obj);
        }
        #endregion

        #region City Fos Wise Report
        [HttpGet]
        public ActionResult CityWiseFosReport()
        {
            ReportsInputData obj = new ReportsInputData();
            ManagePainter objPainter = new ManagePainter();
            obj.Territories = objPainter.GetTerritoryNamesList();
            obj.FosNames = objPainter.getFosLov(0).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
            var customer = objPainter.GetAllCitiesList();
            obj.PainterCityNames = customer;
            return View(obj);
        }
        #endregion

        #region City Market Retailer Wise Report
        public ActionResult CityMktRtlrWiseReport()
        {
            ReportsInputData obj = new ReportsInputData();
            ManagePainter objPainter = new ManagePainter();
            obj.Territories = objPainter.GetTerritoryNamesList();
            obj.FosNames = objPainter.getFosLov(0).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
            var customer = objPainter.GetAllCitiesList();
            obj.PainterCityNames = customer;
            return View(obj);
        }
        #endregion

        #region Helping Methods
        public JsonResult getData(string cities)
        {
            string[] mul = cities.Split(',');
            return null;
        }
        public ActionResult getCities(string TID)
        {
            int tid = 0;
            if (!string.IsNullOrEmpty(TID))
            {
                tid = Convert.ToInt32(TID);
            }
            ManagePainter objPainter = new ManagePainter();
            string Reponse = "";
            using (FOSDataModel dbContext = new FOSDataModel())
            {
                var customer = objPainter.GetCityList(tid).OrderBy(x => x.CityName);
                foreach (var p in customer)
                {
                    Reponse += @"<div class='span3 scroll' style='margin:0px;margin-left: 0px;'><input " + ((p.Selected == false) ? "" : "checked") + " id='painter" + p.ID + "' data-id='" + p.ID + "' class='pCheckBox' name='painter[]' onchange='painterSelected(this)' type='checkbox' style='margin: -1px 0 0;'><span class='lbl' style='font-size: 11px;margin-left: 5px;color: #000000;'>" + p.CityName + "</span><p style='font-size:9px;' id='cityName'>" + p.CityName + "</p></div>";
                }

            }

            return Json(new { Response = Reponse });
        }

        public ActionResult getShops(string CID)
        {
            int tid = 0;
            if (!string.IsNullOrEmpty(CID))
            {
                tid = Convert.ToInt32(CID);
            }
            ManagePainter objPainter = new ManagePainter();
            string Reponse = "";
            using (FOSDataModel dbContext = new FOSDataModel())
            {
                var customer = objPainter.GetShopsList(tid).OrderBy(x => x.CityName);
                foreach (var p in customer)
                {
                    Reponse += @"<div class='span3 scroll' style='margin:0px;margin-left: 0px;'><input " + ((p.Selected == false) ? "" : "checked") + " id='painter" + p.ID + "' data-id='" + p.ID + "' class='pCheckBox' name='painter[]' onchange='painterSelected(this)' type='checkbox' style='margin: -1px 0 0;'><span class='lbl' style='font-size: 11px;margin-left: 5px;color: #000000;'>" + p.CityName + "</span><p style='font-size:9px;' id='cityName'>" + p.CityName + "</p></div>";
                }

            }

            return Json(new { Response = Reponse });
        }

        public ActionResult getAreas(string CID)
        {
            int tid = 0;
            if (!string.IsNullOrEmpty(CID))
            {
                tid = Convert.ToInt32(CID);
            }
            ManagePainter objPainter = new ManagePainter();
            string Reponse = "";
            using (FOSDataModel dbContext = new FOSDataModel())
            {
                var customer = objPainter.GetAreaList(tid).OrderBy(x => x.CityName);
                foreach (var p in customer)
                {
                    Reponse += @"<div class='span3 scroll' style='margin:0px;margin-left: 0px;'><input " + ((p.Selected == false) ? "" : "checked") + " id='painter" + p.ID + "' data-id='" + p.ID + "' class='pCheckBox' name='painter[]' onchange='painterSelected(this)' type='checkbox' style='margin: -1px 0 0;'><span class='lbl' style='font-size: 11px;margin-left: 5px;color: #000000;'>" + p.CityName + "</span><p style='font-size:9px;' id='cityName'>" + p.CityName + "</p></div>";
                }

            }

            return Json(new { Response = Reponse });
        }

        public ActionResult getMarkets(string CID)
        {
            int tid = 0;
            if (!string.IsNullOrEmpty(CID))
            {
                tid = Convert.ToInt32(CID);
            }
            ManagePainter objPainter = new ManagePainter();
            string Reponse = "";
            using (FOSDataModel dbContext = new FOSDataModel())
            {
                var customer = objPainter.GetMarketList(tid).OrderBy(x => x.CityName);
                foreach (var p in customer)
                {
                    Reponse += @"<div class='span3 scroll' style='margin:0px;margin-left: 0px;'><input " + ((p.Selected == false) ? "" : "checked") + " id='painter" + p.ID + "' data-id='" + p.ID + "' class='pCheckBox' name='painter[]' onchange='painterSelected(this)' type='checkbox' style='margin: -1px 0 0;'><span class='lbl' style='font-size: 11px;margin-left: 5px;color: #000000;'>" + p.CityName + "</span><p style='font-size:9px;' id='cityName'>" + p.CityName + "</p></div>";
                }

            }

            return Json(new { Response = Reponse });
        }

        public ActionResult getFosNames(string TID)
        {
            int tid = 0;
            if (!string.IsNullOrEmpty(TID))
            {
                tid = Convert.ToInt32(TID);
            }
            ManagePainter objPainter = new ManagePainter();
            var customers = objPainter.getFosLov(tid).OrderBy(x => x.Name);
            var customer = objPainter.getFosLov(tid).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID
            });
            return Json(customer);
        }

        public ActionResult getCitiess(string TID)
        {
            int tid = 0;
            if (!string.IsNullOrEmpty(TID))
            {
                tid = Convert.ToInt32(TID);
            }
            ManagePainter objPainter = new ManagePainter();
            var customer = objPainter.GetCityList(tid).OrderBy(x => x.CityName);


            return Json(customer);
        }
        #endregion

        public ActionResult FOSPlanning()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            var ranges = FOS.Setup.ManageRegion.GetRangeType();
            var rangeid = ranges.FirstOrDefault();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            regionalHeadData.Insert(0, new RegionalHeadData
            {
                ID = 0,
                Name = "All"
            });

            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regionalHeadData.FirstOrDefault().ID, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);


            return View(objJob);
        }

        public ActionResult FOSPlanningReport(string StartingDate, string EndingDate, int TID, int fosid)
        {
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_FosPlanning_Result> result = objRetailers.FOSPlanningReport(start, end, TID, fosid);
                rptDataSet obj = new rptDataSet();
                DataRow dtrow;
                DataTable dtNewTable;
                dtNewTable = new DataTable();
                DataColumn dcol51 = new DataColumn("Teritory Head", typeof(System.String));
                dtNewTable.Columns.Add(dcol51);
                DataColumn dcol5 = new DataColumn("SaleOfficerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol5);
                DataColumn dcol2 = new DataColumn("DealerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol2);
                DataColumn dcol4 = new DataColumn("CityName", typeof(System.String));
                dtNewTable.Columns.Add(dcol4);
                DataColumn dcol7 = new DataColumn("ShopName", typeof(System.String));
                dtNewTable.Columns.Add(dcol7);
                DataColumn dcol6 = new DataColumn("JobDate", typeof(System.String));
                dtNewTable.Columns.Add(dcol6);

                foreach (var item in result)
                {

                    dtrow = dtNewTable.NewRow();
                    dtrow[0] = item.Name;
                    dtrow[1] = item.SaleOfficerName;
                    dtrow[2] = item.DealerName;
                    dtrow[3] = item.CityName;
                    dtrow[4] = item.ShopName;
                    dtrow[5] = item.JobDate;

                    dtNewTable.Rows.Add(dtrow);

                }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/FOSPlanning.rpt")));
                rd.SetDataSource(dtNewTable);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string type = "xls";
                if (type == "pdf")
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "FOSPlanning.pdf");
                }
                else
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/ms-excel", string.Format("FOSPlanning{0}.xls", DateTime.Now.ToShortDateString()));
                }

                return View();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                return null;
            }

        }

        public ActionResult RetailerAvgSale()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            List<MainCategories> CityObj = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }


           
            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Regions = CityObj;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);


            return View(objJob);
        }

        public void RetailerSaleAverage(int TID, int cityid, DateTime sdate, DateTime edate)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }


            try
            {


                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_RetailerAvgSale_Result> result = objRetailers.AvgSale(TID, 6, cityid, sdate, edate);

                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Shop Name\",\"Region Name\",\"City Name\",\"SaleOfficer Name\",\"Total Orders\",\"Distributor Name\",\"Total Value\",\"Average Value\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=RetailersAVGSale" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";





               
                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",


                        // retailer.Name,
                    retailer.shopname,
                        retailer.Region,
                        retailer.CityName,
                        retailer.SOName,
                        retailer.Orders,
                        retailer.rangeADealer,
                        retailer.value,
                        retailer.TotalAvgValue



                        ));
                    }

               


                Response.Write(sw.ToString());
                Response.End();



                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;

                hst.ReportName = "Retailer Average Sale";
                hst.ReportType = "AVG Sale";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();


            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

        }

        public ActionResult RetailerSummery()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }


            List<MainCategories> CityObj = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Regions = CityObj;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListByRegionIDD(regionalHeadData.FirstOrDefault().ID);


            return View(objJob);
        }

        public void RetailerSummaryRpt(int TID, int cityid, DateTime sdate, DateTime edate)
        {
            DateTime start = sdate;
            DateTime end = edate;
            DateTime final = end.AddDays(1);
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }


            try
            {


                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_Total_RetailerInformationSummery_Result> result = db.Sp_Total_RetailerInformationSummery(TID,0, cityid,start,final,6).ToList();

                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"SrNO \",\"Region\",\"City\",\"Retailers Count\",\"Distributor Count\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=RetailersSummaryRpt" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";

                int srNo = 1;

                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",

                        srNo,
                     retailer.Region,
                      retailer.City,
                    retailer.RetailersCount,
                      retailer.DealersCount,
                    
                  

                    srNo++


                    ));
                }





                Response.Write(sw.ToString());
                Response.End();



                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;

                hst.ReportName = "RetailerInformation";
                hst.ReportType = "RetailerInfo";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();


            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

        }

        public ActionResult RetailerInfo()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }


            List<MainCategories> CityObj = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Regions = CityObj;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListByRegionIDD(regionalHeadData.FirstOrDefault().ID);


            return View(objJob);
        }

        public void RetailerInformation(int TID,  int cityid, DateTime sdate, DateTime edate)
        {
            DateTime start = sdate;
            DateTime end = edate;
            DateTime final = end.AddDays(1);
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }


            try
            {
               

                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_Total_RetailerInformation1_5_Result> result = objRetailers.RetailerInfos(TID, 0, 6,cityid, start, final);

                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"SrNO \",\"Created Date\",\"ShopID\",\"Shop Name\",\"Owner Name\",\"Sales Officer Name\",\"Distributor Name\",\"Region Name\",\"City Name\",\"Area Name\",\"Address\",\"Phone1\",\"Phone2\",\"Shop Type\",\"Quota\",\"NewOROldShop\",\"LatitudeLongitude\",\"ActiveInactive\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=Retailers" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";


                //var RegionalHead = db.RegionalHeadRegions.Where(x => x.RegionID == TID).Select(x =>x.RegionHeadID).FirstOrDefault();
                //var name = db.RegionalHeads.Where(x => x.ID == RegionalHead).Select(x => x.Name).FirstOrDefault();

                int srNo = 1;

                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\"",

                            srNo,
                         retailer.lastupdate,
                          retailer.ShopID,
                        retailer.ShopName,
                          retailer.RetailerName,
                        retailer.SaleOfficerName,
                        retailer.DealerName,
                       // retailer.re,
                        retailer.RegionName,
                        retailer.City,
                        retailer.AreaName,
                        retailer.address,
                        retailer.Contact1,
                        retailer.Contact2,

                      retailer.Shoptype,
                         retailer.Quota,
                        
                        retailer.NewOrOld,
                        retailer.location,

                        retailer.ActiveInactive,

                        srNo++


                        ));
                    }
              

          


                Response.Write(sw.ToString());
                Response.End();



                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
               
                hst.ReportName = "RetailerInformation";
                hst.ReportType = "RetailerInfo";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();


            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

        }



        public ActionResult ItemSaleSummary()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }


            List<MainCategories> CityObj = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Regions = CityObj;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListByRegionIDD(regionalHeadData.FirstOrDefault().ID);


            return View(objJob);
        }

        public void ItemSummeryInfo(int TID, int RangeID, int cityid, string sdate, string edate, string ReportType)
        {
            Microsoft.Reporting.WebForms.LocalReport ReportViewer1 = new Microsoft.Reporting.WebForms.LocalReport();
            DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? DateTime.Now.ToString() : sdate);
            DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? DateTime.Now.ToString() : edate);
            DateTime final = end.AddDays(1);
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }


            try
            {
                if (ReportType == "RegionWise")
                {

                    ManageRetailer objRetailers = new ManageRetailer();
                    List<sp_ItemWisereportRegionWise_Result> result = db.sp_ItemWisereportRegionWise(start,final,TID,RangeID).ToList();

                    try
                    {


                        //ReportParameter[] prm = new ReportParameter[3];

                        //prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        //prm[1] = new ReportParameter("DateTo", edate);
                        //prm[2] = new ReportParameter("DateFrom", sdate);


                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\ItemRegionWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);

                       // ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);

                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=ItemsRegonWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }

                }
                else
                {
                    ManageRetailer objRetailers = new ManageRetailer();
                    List<sp_ItemWisereportRegionandCityWise_Result> result = db.sp_ItemWisereportRegionandCityWise(start, final, TID, cityid, RangeID).ToList();

                    try
                    {


                        //ReportParameter[] prm = new ReportParameter[3];

                        //prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        //prm[1] = new ReportParameter("DateTo", edate);
                        //prm[2] = new ReportParameter("DateFrom", sdate);


                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\ItemCityWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);

                        // ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);

                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=ItemsRegonWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }
                }


           



                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;

                hst.ReportName = "RetailerInformation";
                hst.ReportType = "RetailerInfo";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();


            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

        }


        public ActionResult StockPosition()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }


            List<MainCategories> CityObj = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Regions = CityObj;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);


            return View(objJob);
        }

        public void StockPositionReport(int TID, int RangeID, int cityid, string sdate, string edate)
        {


            try
            {
                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? DateTime.Now.ToString() : sdate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? DateTime.Now.ToString() : edate);
                DateTime final = end.AddDays(1);

                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_StockPositionReport_Result> result = objRetailers.GetStockPosition(start,final,TID,cityid,RangeID);

                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Item Name\",\"Region Name\",\"City Name\",\"Range\",\"Quantity\",\"From Date\",\"To Date\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=StockPosition" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";


           


                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",


                    // retailer.Name,
                    retailer.ItemName,
                    retailer.RegionName,
                    retailer.CityName,
                    retailer.RangeName,
                  retailer.Quantity,
                    retailer.Start,
                    retailer.EndDate



                    ));
                }
                Response.Write(sw.ToString());
                Response.End();

            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

        }


        public ActionResult StockInvoice()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }


            List<MainCategories> CityObj = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Regions = CityObj;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);


            return View(objJob);
        }

        public void StockInvoiceReport(int TID, int RangeID, int cityid, string sdate, string edate)
        {


            try
            {
                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? DateTime.Now.ToString() : sdate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? DateTime.Now.ToString() : edate);
                DateTime final = end.AddDays(1);

                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_StockInvoiceReport_Result> result = objRetailers.GetStockInvoice(start, final, TID, cityid, RangeID);

                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Item Name\",\"Region Name\",\"City Name\",\"Range\",\"Quantity\",\"From Date\",\"To Date\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=StockPosition" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";





                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",


                    // retailer.Name,
                    retailer.ItemName,
                    retailer.RegionName,
                    retailer.CityName,
                    retailer.RangeName,
                  retailer.Quantity,
                    retailer.Start,
                    retailer.EndDate



                    ));
                }
                Response.Write(sw.ToString());
                Response.End();

            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

        }

        public ActionResult ShopVisitSummery()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangeType();
            var rangeid = ranges.FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            regionalHeadData.Insert(0, new RegionalHeadData
            {
                ID = 0,
                Name = "All"
            });

            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regionalHeadData.FirstOrDefault().ID, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);

            return View(objJob);
        }

        public ActionResult ShopVisitSummeryy(string StartingDate, string EndingDate, int TID, int fosid, int dealerid, int cityid)
        {
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp__TotalShopVisitSummeryReport_Result> result = objRetailers.ShopVisitSummery(start, end, TID, fosid, dealerid, cityid);
                rptDataSet obj = new rptDataSet();
                DataRow dtrow;
                DataTable dtNewTable;
                dtNewTable = new DataTable();
                DataColumn dcol51 = new DataColumn("Teritory Head", typeof(System.String));
                dtNewTable.Columns.Add(dcol51);
                DataColumn dcol5 = new DataColumn("SaleOfficerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol5);
                DataColumn dcol3 = new DataColumn("DealerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol3);
                DataColumn dcol0 = new DataColumn("CItyName", typeof(System.String));
                dtNewTable.Columns.Add(dcol0);
                DataColumn dcol01 = new DataColumn("Zone", typeof(System.String));
                dtNewTable.Columns.Add(dcol01);
                DataColumn dcol2 = new DataColumn("TotalJobs", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol2);
                DataColumn dcol4 = new DataColumn("ShopVisited", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol4);
                DataColumn dcol7 = new DataColumn("ShopMissed", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol7);
                DataColumn dcol6 = new DataColumn("Order1kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol6);
                DataColumn dcol65 = new DataColumn("Order5kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol65);
                DataColumn dcol66 = new DataColumn("TotalOrders", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol66);
                DataColumn dcol67 = new DataColumn("Delievered1kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol67);
                DataColumn dcol68 = new DataColumn("Delievered5kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol68);
                DataColumn dcol69 = new DataColumn("TotalDelievered", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol69);


                foreach (var item in result)
                {

                    dtrow = dtNewTable.NewRow();
                    dtrow[0] = item.Name;
                    dtrow[1] = item.SaleOfficerName;
                    dtrow[2] = item.DealerName;
                    dtrow[3] = (item.CityName);
                    dtrow[4] = (item.Zone);
                    dtrow[5] = item.TotalJobs;
                    dtrow[6] = item.Done;
                    dtrow[7] = item.Pending;
                    dtrow[8] = item.Order1KG;
                    dtrow[9] = item.Order5kg;
                    dtrow[10] = item.TotalOrder;
                    dtrow[11] = item.Delevired1Kg;
                    dtrow[12] = item.Delievered5kg;
                    dtrow[13] = item.TotalDelievered;
                    dtNewTable.Rows.Add(dtrow);

                }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/ShopVisit_Summary.rpt")));
                rd.SetDataSource(dtNewTable);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string type = "xls";
                if (type == "pdf")
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "VisitSummary.pdf");
                }
                else
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/ms-excel", string.Format("VisitSummary{0}.xls", DateTime.Now.ToShortDateString()));
                }

                return View();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                return null;
            }

        }


        public ActionResult DealerInfo()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }

            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }

            List<MainCategories> CityObj = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);

            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Regions = CityObj;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);


            return View(objJob);
        }

        public void DealerInformation(int TID)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }

            try
            {

                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_DealerInformationSummery1_1_Result> result = objRetailers.DealerInfo(TID, 0, 0,6);

                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"SR NO\",\"Distributor ID\",\"Region\",\"City\",\"Distributor Name\",\"Owner Name\",\"Address\",\"Phone1\",\"Phone2\",\"ActiveInactive\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=Distributors" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";

                //   var retailers = ManageRetailer.GetRetailersForExportinExcel();
                int srNo = 1;
                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"",
                        srNo,
                    retailer.DistID,
                    // retailer.Name,
                    retailer.Region,
                    retailer.City,
                    retailer.ShopSchoolName,
                    retailer.PrincipleName,
                    retailer.Address,
                    retailer.Contact1,
                    retailer.Contact2,
                    retailer.Active,
                    
                   
                    srNo++

                    ));
                }
                Response.Write(sw.ToString());
                Response.End();

                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "DealerInformation";
                hst.ReportType = "DealerInfo";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();


            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

        }








        public ActionResult ShopVisitSummeryOneLine()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }

        public void ShopVisitSummeryOneLiner(string StartingDate, string EndingDate, int TID, int fosid)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_DealersOrderDetail_Result> result = objRetailers.ShopVisitSummeryOneLine(start, final, TID, fosid,6);
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"OrderID\",\"Zone\",\"RegionalHead Name\",\"Sales Officer Name\",\"Distributor Name\",\"Item Name\",\"Item Quantity(CTN)\",\"Item Price\",\"City Name\",\"Visited Date\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=DistributorOrders" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";

                //   var retailers = ManageRetailer.GetRetailersForExportinExcel();

                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"",
                    retailer.OrderID,
                    retailer.Zone,
                    // retailer.Name,
                    retailer.RegionalHeadName,
                    retailer.SaleOfficerName,
                    retailer.DistributorName,
                    retailer.ItemName,
                    retailer.Quantity,
                    retailer.Price,
                    retailer.CityName,
                    retailer.VisitDate

                    // retailer.CustomerType



                    ));
                }
                Response.Write(sw.ToString());
                Response.End();

                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Distributor Order Detail Report";
                hst.ReportType = "DealerOrder";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }

        public ActionResult Attandance()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.FirstOrDefault();
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }

        public void AttandanceReport(string StartingDate, string EndingDate, int TID, int fosid, int RangeID)
        {


            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_AttandaceReport_Result> result = objRetailers.Attandance(start, final, TID, fosid, RangeID);
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"RegionalHead Name\",\"Saleofficer Name\",\"Region Name\",\"City Name\",\"Type\",\"Date/Time\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=Attandance" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";



                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",

                    retailer.RegionalHead,
                    retailer.Name,
                    // retailer.Name,
                    retailer.Region,
                    retailer.City,
                    retailer.Type,
                    retailer.CreatedAt
              

                    // retailer.CustomerType



                    ));
                }
                Response.Write(sw.ToString());
                Response.End();


                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Attendance Report";
                hst.ReportType = "AttendanceReport";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();


            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }

        public ActionResult ManagersUpdate()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }


        public void ManagersLoginHistory(string StartingDate, string EndingDate, int TID, int RangeID)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_ManagersUpdateSummary_Result> result = objRetailers.ManagersSummary(start, final, TID, RangeID);
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"UserID\",\"UserName\",\"Password\",\"Fetch Date\",\"Report Type\",\"Report Name\",\"Phone No\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ManagersDailySummary" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";



                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",

                    retailer.userid,
                    retailer.username,
                    // retailer.Name,
                    retailer.Password,
                    retailer.createdon,
                    retailer.rEPORTtYPE,
                    retailer.reportname,
                    retailer.PhoneNo

                    ));
                }
                Response.Write(sw.ToString());
                Response.End();
        
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }

        public ActionResult SaleOfficerDetail()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }


        public void SaleOfficerDetailRpt( int TID)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

           
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_SaleofficerData_Result> result = db.Sp_SaleofficerData(TID, 6).ToList();
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Name\",\"RegionalHead Name\",\"Phone1\",\"Phone2\",\"Joining Date\",\"Active\",\"LeaveON\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=SoDetail" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";



                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",

                    retailer.name,
                    retailer.RegionalHeadName,
                    retailer.Phone1,
                    retailer.Phone2,
               
                    retailer.DateofJoin,
                    retailer.Active,
                    retailer.LeaveON

                    ));
                }
                Response.Write(sw.ToString());
                Response.End();
                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "SO Detail Report";
                hst.ReportType = "SO Details";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }

        public ActionResult ShopsPerformance()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Areas = ManageArea.GetAllAreaListByCityID(objJob.Cities.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }

        public void ShopsPerformanceRpt(string StartingDate, string EndingDate, int TID, int fosid, int cityId, int areaId)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();

              



                var lists = db.sp_ShopProductivity(start, final, TID, fosid,cityId,areaId).ToList();

           


                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Region Name\",\"City Name\",\"Area Name\",\"SS Name\",\"ShopID\",\"Shop Name\",\"Contact No\",\"Distributor Name\",\"Total Visits\",\"Productive\",\"Followup\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=ShopsPerformanceReport" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";


                foreach (var retailer in lists)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\"",

                    retailer.Region,
                    retailer.CityName,
                    retailer.AreaName,
                    retailer.SaleOfficerName,
                    retailer.ShopID,
                    retailer.ShopName,
                    retailer.Phone1,
                    retailer.Dealer,
                    retailer.TotalVisits,
                    retailer.ProductiveOrders,
                    retailer.NonProductive

                   


                    ));
                }

                Response.Write(sw.ToString());
                Response.End();
            
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }

        public ActionResult SSSaleSummary()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Areas = ManageArea.GetAllAreaListByCityID(objJob.Cities.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }


        public void SSSaleSummaryRpt(string StartingDate, string EndingDate, int TID, int fosid)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();

                List<KPIData> list = new List<KPIData>();
                decimal? Val = 0;
                KPIData comlist;


                var totalDaysCount = (end - start).TotalDays;

                var lists = db.sp_GetSSSalesSummary(start, final, TID, fosid, 6).ToList();

                if (lists != null)
                {
                    foreach (var items in lists)
                    {
                        comlist = new KPIData();

                        comlist.RHName = items.RegionName;
                      
                        comlist.SoName = items.SaleOfficerName;
                        comlist.totalVisits = items.TotalVisits;

                        comlist.ProductiveShops = (int)items.ProductiveOrders;
                        comlist.NonProductive = (int)items.NonProductive;
                        comlist.ProductivePer = Convert.ToDecimal(items.Productiveperage);

                        var days = db.sp_getSOWorkingDays(start, final, items.ID).Count();


                        comlist.TotalWorkingDays = days;

                        comlist.AbsentDays = totalDaysCount - comlist.TotalWorkingDays;


                        comlist.PerDayVisitShops = comlist.totalVisits / comlist.TotalWorkingDays;

                        comlist.perDayorders = comlist.ProductiveShops / comlist.TotalWorkingDays;


                        comlist.PerDayFollowups = comlist.NonProductive / comlist.TotalWorkingDays;

                       

                        var total = db.sp_BrandAndItemWiseReport(start, final, 0, items.ID, 0, 0, 6).ToList();

                        foreach (var itemss in total)
                        {
                            Val += itemss.TotalQuantity;
                        }
                        comlist.totalSale = Val;

                        var sale = Convert.ToInt32(comlist.totalSale);

                        comlist.PerDaycartons = sale / comlist.TotalWorkingDays;

                        list.Add(comlist);
                        Val = 0;
                    }



                }


                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Head Name\",\"SS Name\",\"Present Days\",\"Absent Days\",\"Total Vist Shops\",\"Productive Shops\",\"Total Followups\",\"Total Cartons\",\"Per Day Visited\",\"Per Day Order\",\"Per Day Followup\",\"Per Day Cartons\",\"Productivity %age\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=SSSaleSummaryReport" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";


                foreach (var retailer in list)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\"",

                    retailer.RHName,
                   
                    retailer.SoName,
                    retailer.TotalWorkingDays,
                    retailer.AbsentDays,
                    retailer.totalVisits,
                    retailer.ProductiveShops,
                    retailer.NonProductive,

                    retailer.totalSale,
                    retailer.PerDayVisitShops,
                    retailer.perDayorders,

                    retailer.PerDayFollowups,
                    retailer.PerDaycartons,

                    retailer.ProductivePer


                    ));
                }

                Response.Write(sw.ToString());
                Response.End();
           
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }


        public ActionResult SSDailyPerformance()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Areas = ManageArea.GetAllAreaListByCityID(objJob.Cities.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }
        public ActionResult SSVisitSummary()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Areas = ManageArea.GetAllAreaListByCityID(objJob.Cities.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }

        public void SSDailyPerformanceRpt(string StartingDate, string EndingDate, int TID, int fosid, int cityId)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();

                List<KPIData> list = new List<KPIData>();
                decimal? Val = 0;
                KPIData comlist;



                var lists = db.sp_GetKPISummarySOWise(start, final, TID, fosid, cityId).ToList();

                if (lists != null)
                {
                    foreach (var items in lists)
                    {
                        DateTime start1 = Convert.ToDateTime(items.DateofOrders);
                        //DateTime end1 = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                        DateTime final1 = start1.AddDays(1);
                        comlist = new KPIData();

                        comlist.RHName = items.RegionName;
                        comlist.RegionName = items.Region;
                        comlist.CityName = items.CityName;
                        comlist.SoName = items.SaleOfficerName;
                        comlist.totalVisits = items.TotalVisits;

                        comlist.ProductiveShops = (int)items.ProductiveOrders;
                        comlist.NonProductive = (int)items.NonProductive;

                        comlist.StartDate = items.MarketStart;
                        comlist.EndDate = items.MarketClose;

                        comlist.DateOFOrder = items.DateofOrders;


                        



                        TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                        comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);

                        var total = db.sp_BrandAndItemWiseReport(start1, final1, 0, items.ID, 0, 0, 6).ToList();

                        foreach (var itemss in total)
                        {
                            Val += itemss.TotalQuantity;
                        }
                        comlist.totalSale = Val;
                        list.Add(comlist);
                        Val = 0;
                    }



                }


                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Region\",\"City\",\"SS Name\",\"Visit Date\",\"Total Vists\",\"Productive\",\"Followups\",\"Market Start\",\"Market Close\",\"Elapse Time\",\"Total Cartons\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=SSDailyPerformanceReport" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";


                foreach (var retailer in list)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\"",

                    retailer.RegionName,
                    retailer.CityName,
                    // retailer.Name,
                    retailer.SoName,
                    retailer.DateOFOrder,
                    retailer.totalVisits,
                    retailer.ProductiveShops,
                    retailer.NonProductive,

                    retailer.StartDate,
                    retailer.EndDate,
                    retailer.ElapseTime,

                    retailer.totalSale


                    ));
                }

                Response.Write(sw.ToString());
                Response.End();
                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Retailer Order Detail Report";
                hst.ReportType = "RetailerOrder";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }
        public void SSVisitSummaryRpt(string StartingDate, string EndingDate, int TID, int fosid, int cityId, int areaId)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();

                List<KPIData> list = new List<KPIData>();
                decimal? Val = 0;
                KPIData comlist;



                var lists = db.sp_GetSSVisitSummary_New(start, final, TID, fosid, cityId, areaId).ToList();

                if (lists != null)
                {
                    foreach (var items in lists)
                    {
                        DateTime start1 = Convert.ToDateTime(items.JobDate);
                        //DateTime end1 = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                        DateTime final1 = start1.AddDays(1);
                        comlist = new KPIData();

                        comlist.RHName = items.RegionHeadName;
                        comlist.RegionName = items.RegionName;
                        comlist.CityName = items.CityName;
                        comlist.SoName = items.SaleOfficerName;
                        comlist.totalVisits = items.TotalVisits;

                        comlist.ProductiveShops = (int)items.ProductiveOrders;
                        comlist.NonProductive = (int)items.NonProductive;
                        comlist.PerDaycartons = items.TotalCartons;
                        comlist.DateOFOrder = items.JobDate;

                        comlist.StartDate = items.MarketStart;
                        comlist.EndDate = items.MarketClose;
                        comlist.AreaName = items.AreaName;

                        //comlist.DateOFOrder = items.DateofOrders;






                        TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                        comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);

                        var total = db.sp_BrandAndItemWiseReport(start1, final1, 0, items.ID, 0, 0, 6).ToList();

                        foreach (var itemss in total)
                        {
                            Val += itemss.TotalQuantity;
                        }
                        comlist.totalSale = Val;
                        list.Add(comlist);
                        Val = 0;
                    }



                }


                StringWriter sw = new StringWriter();

                sw.WriteLine("\"Region\",\"City\",\"Area Name\",\"SS Name\",\"Visit Date\",\"Total Vists\",\"Productive\",\"Followups\",\"Total Cartons\",\"Market Start\",\"Market Close\",\"Elapse Time\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=SSDailyPerformanceReport" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";


                foreach (var retailer in list)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\"",

                    retailer.RegionName,
                    retailer.CityName,
                     retailer.AreaName,
                    retailer.SoName,
                    retailer.DateOFOrder,
                    retailer.totalVisits,
                    retailer.ProductiveShops,
                    retailer.NonProductive,

                    retailer.PerDaycartons,

                    retailer.StartDate,
                    retailer.EndDate,
                    retailer.ElapseTime


                    ));
                }

                Response.Write(sw.ToString());
                Response.End();
                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Retailer Order Detail Report";
                hst.ReportType = "RetailerOrder";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }



        public ActionResult RetailerOrdersDetail()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges= FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
           var  rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });
           

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            //objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Areas = ManageArea.GetAllAreaListByCityID(objJob.Cities.FirstOrDefault().ID);
            objJob.Retailers = ManageRetailer.GetRetailerBySOIDAndCity(SaleOfficerObj.Select(s => s.ID).FirstOrDefault(), objJob.Cities.FirstOrDefault().ID, objJob.Areas.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }
        public ActionResult ShopsWiseAnalysis()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });


            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            //objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.RegionDatas = ManageRegion.GetRegions();
            objJob.Cities = ManageCity.GetCityByRegionID(objJob.RegionDatas.FirstOrDefault().ID);
            objJob.Areas = ManageArea.GetAllAreaListByCityID(objJob.Cities.FirstOrDefault().ID);
            objJob.Retailers = ManageRetailer.GetRetailerByAreaAndCity(objJob.Cities.FirstOrDefault().ID, objJob.Areas.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }
        public JsonResult GetRetailersBySOIDAndCity(int soId, int cityId, int areaId)
        {
            var result = FOS.Setup.ManageRetailer.GetRetailerBySOIDAndCity(soId, cityId, areaId);
            return Json(result);
        }
        public JsonResult GetRetailerByAreaAndCity(int cityId, int areaId)
        {
            var result = FOS.Setup.ManageRetailer.GetRetailerByAreaAndCity(cityId, areaId);
            return Json(result);
        }
        public JsonResult GetCityByRegionID(int regionId)
        {
            var result = FOS.Setup.ManageCity.GetCityByRegionID(regionId);
            return Json(result);
        }
        public JsonResult GetAllAreaListByCityID(int ID)
        {
            var result = FOS.Setup.ManageArea.GetAllAreaListByCityID(ID);
            return Json(result);
        }
        public void RetailerOrders(string StartingDate, string EndingDate, int TID, int fosid, int cityId, int areaId, int retailerId)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_RetailersOrderDetailFinal1_3_Result> result = objRetailers.RetailerVisitSummeryOneLine(start, final, TID, fosid,cityId,areaId,retailerId);
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"OrderID\",\"Date\",\"City Name\",\"Area Name\",\"ShopID\",\"Shop Name\",\"Contact No\",\"SS Name\",\"Distributor Name\",\"Item Name\",\"Order Quantity\",\"Stock Quantity\",\"OwnPurchaseRate\",\"OwnSaleRate\",\"CompititorProduct\",\"CompititorPurchaseRate\",\"CompititorSaleRate\",\"Reason For No Sale\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=RetailerOrder" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";


              foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\"\"{16}\"\"{17}\"",

                        retailer.OrderID,
                        retailer.VisitDate,
                        retailer.CityName,
                        retailer.AreaName,
                       retailer.ShopID,
                        retailer.RetailerName,
                       retailer.Phone1,
                        retailer.SaleOfficerName,
                       
                
                        retailer.DealerName,

                        retailer.OwnProductName,
                        retailer.Quantity,
                        retailer.StockQuantity,
                
                        retailer.OwnPurchaserate,
                        retailer.OwnSaleRate,
                           retailer.CompititorProductName,
                   retailer.CompititorPurchaseRate,
                   retailer.CompititorSaleRate,
                  
                        retailer.ReasonForNoSale

                        // retailer.CustomerType



                        ));
                    }
               
                Response.Write(sw.ToString());
                Response.End();
                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Retailer Order Detail Report";
                hst.ReportType = "RetailerOrder";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }
        public void ShopsWiseAnalysisDetails(string StartingDate, string EndingDate, int regionId,  int cityId, int areaId, int retailerId)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();
                //List<Sp_RetailersOrderDetailByCity_Result> result = objRetailers.RetailerOrderSummaryByCityAndRegions(start, final, regionId, cityId, areaId, retailerId);
                List<sp_GetKPISummarySOWise_New_Result> result = db.sp_GetKPISummarySOWise_New(start, final, regionId, 0,cityId, areaId, retailerId).ToList();
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"ShopID\",\"Shop Name\",\"Date\",\"OrderID\",\"Order Quantity\",\"Stock Quantity\",\"City Name\",\"Area Name\",\"Contact No\",\"SS Name\",\"Distributor Name\",\"Comp. Product Name\",\"Comp. Purchase Rate\",\"Reason For No Sale\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=RetailerOrder" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";


                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\"",

                    retailer.ShopID,
                    retailer.ShopName,
                    retailer.Date,
                    retailer.JobID,
                   retailer.OrderQty,
                    retailer.StockQty,
                   retailer.CityName,
                    retailer.AreaName,


                    retailer.ContactNumber,
                    
                    retailer.SaleOfficerName,
                    retailer.DealerName,
                    retailer.CompititorProductName,
                    retailer.CompititorPurchaseRate,
                    retailer.OtherReasonFornoSale



                    ));
                }

                Response.Write(sw.ToString());
                Response.End();
                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Retailer Order Detail Report";
                hst.ReportType = "RetailerOrder";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }


        public ActionResult LowItemsDetail()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionData = new List<RegionalHeadData>();
            regionData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            if (userID == 1)
            {
                regionData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionData = regionData;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListByRegionIDD(regionData.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }


        public void LowOrdersSOWise(string StartingDate, string EndingDate, int TID, int fosid, string Type, int RegionID, int CityID)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }

            if (Type == "SoWise")
            {
                try
                {

                    DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                    DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                    DateTime final = end.AddDays(1);
                    ManageRetailer objRetailers = new ManageRetailer();
                    List<Sp_LessItemsSoldRegionWise_Result> result = db.Sp_LessItemsSoldRegionWise(start, final, 6, TID, fosid).ToList();
                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"RegionalHead Name\",\"Sales Officer Name\",\"Item Name\",\"Quantity in PCS\",\"Price\",\"Grand Total\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=LowSalesItemSOWise" + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";


                  
                        foreach (var retailer in result)
                        {
                            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",


                            // retailer.Name,
                            retailer.RegionalHeadName,
                            retailer.SaleofficerName,
                            
                             retailer.ItemName,
                            retailer.Orders,
                            retailer.Price,
                            retailer.Total


                            ));
                        }
                 

                    Response.Write(sw.ToString());
                    Response.End();
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Low Item Sale Report SO Wise";
                    hst.ReportType = "Items Report";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
                catch (Exception exp)
                {
                    Log.Instance.Error(exp, "Report Not Working");
                    // return null;
                }
            }
            else
            {
                try
                {

                    DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                    DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                    DateTime final = end.AddDays(1);
                    ManageRetailer objRetailers = new ManageRetailer();
                    List<Sp_LessItemsSoldRegionandCityWise_Result> result = db.Sp_LessItemsSoldRegionandCityWise(start, final, 6, RegionID, CityID).ToList();
                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"Region Name\",\"City Name\",\"Item Name\",\"Quantity in PCS\",\"Price\",\"Grand Total\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=LowSalesItemRegionWise" + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";


                 
                        foreach (var retailer in result)
                        {
                            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",


                            // retailer.Name,
                            retailer.RegionName,
                            retailer.CityName,
                     
                            retailer.ItemName,
                            retailer.Orders,
                            retailer.Price,
                            retailer.Total


                            ));
                        }
                 

                    Response.Write(sw.ToString());
                    Response.End();
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Low Item Sale Report Region Wise";
                    hst.ReportType = "Items Report";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
                catch (Exception exp)
                {
                    Log.Instance.Error(exp, "Report Not Working");
                    // return null;
                }
            }
        }

        public ActionResult SOAttendance()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }

        public ActionResult StockTakingDetail()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            objJob.Range = ranges;
            return View(objJob);
        }

        public void Stocktakingorderdetails(string StartingDate, string EndingDate, int TID, int fosid)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }

            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                //  DateTime Startfinal = start.AddDays(-1);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_StockTakingDetailReport_Result> result = objRetailers.Stocktaking(start, final, TID, fosid);
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"StockID\",\"RegionalHead Name\",\"Sales Officer Name\",\"Distributor Name\",\"Item Name\",\"Quantity (IN CTN)\",\"City Name\",\"Stock Taking Date\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=StockTaking" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";

                //   var retailers = ManageRetailer.GetRetailersForExportinExcel();

                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",

                    retailer.StockID,

                    retailer.RegionalHeadName,
                    retailer.SaleOfficerName,
                    retailer.DistributorName,
                    retailer.ItemName,
                    retailer.Quantity,
                    retailer.CityName,
                    retailer.StockTakingTime



                    ));
                }
                Response.Write(sw.ToString());
                Response.End();
                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Stock taking Detail Report";
                hst.ReportType = "Stocktaking";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }










        public ActionResult ShopVisitDetail()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            regionalHeadData.Insert(0, new RegionalHeadData
            {
                ID = 0,
                Name = "All"
            });

            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regionalHeadData.FirstOrDefault().ID, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);

            return View(objJob);
        }

        public ActionResult ShopVisitDetaill(string StartingDate, string EndingDate, int TID, int fosid, int dealerid, int cityid)
        {
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp__TotalShopVisitDetail_Result> Result = objRetailers.ShopVisitDetail(start, end, TID, fosid, dealerid, cityid);

                rptDataSet obj = new rptDataSet();
                DataRow dtrow;
                DataTable dtNewTable;
                dtNewTable = new DataTable();
                DataColumn dcol5 = new DataColumn("SaleOfficerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol5);
                DataColumn dcol3 = new DataColumn("DealerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol3);
                DataColumn dcol0 = new DataColumn("CItyName", typeof(System.String));
                dtNewTable.Columns.Add(dcol0);
                DataColumn dcol2 = new DataColumn("ShopName", typeof(System.String));
                dtNewTable.Columns.Add(dcol2);
                DataColumn dcol4 = new DataColumn("JobDate", typeof(System.String));
                dtNewTable.Columns.Add(dcol4);
                DataColumn dcol7 = new DataColumn("Order1kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol7);
                DataColumn dcol6 = new DataColumn("Order5kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol6);
                DataColumn dcol65 = new DataColumn("Delievered1kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol65);
                DataColumn dcol66 = new DataColumn("Delievered5kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol66);



                foreach (var item in Result)
                {

                    dtrow = dtNewTable.NewRow();
                    dtrow[0] = item.SaleOfficerName;
                    dtrow[1] = item.DealerName;
                    dtrow[2] = (item.CityName);
                    dtrow[3] = item.ShopName;
                    dtrow[4] = item.JobDate;
                    dtrow[5] = item.Order1kg;
                    dtrow[6] = item.Order5kg;
                    dtrow[7] = item.Delevired1Kg;
                    dtrow[8] = item.Delievered5kg;

                    dtNewTable.Rows.Add(dtrow);

                }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/ShopVisit_Detail.rpt")));
                rd.SetDataSource(dtNewTable);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string type = "xls";
                if (type == "pdf")
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "ShopVisit_Detail.pdf");
                }
                else
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/ms-excel", string.Format("ShopVisit_Detail{0}.xls", DateTime.Now.ToShortDateString()));
                }

                return View();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                return null;
            }

        }




        public ActionResult MarketInfo()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            regionalHeadData.Insert(0, new RegionalHeadData
            {
                ID = 0,
                Name = "All"
            });

            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regionalHeadData.FirstOrDefault().ID, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);

            return View(objJob);
        }

        public ActionResult PresentSOReport()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            //  var objRetailer = new RetailerData();
            //objRetailer.Regions = FOS.Setup.ManageCity.GetRegionList();

            List<RegionalHeadData> objRegion = new List<RegionalHeadData>();
            IEnumerable<RegionalHeadData> obj = new List<RegionalHeadData>();
            if (userID == 1)
            {
                objRegion.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;


          // regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionaList(userID);
            regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            obj = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
                objRegion = obj.ToList();
          
            var objRetailer = new RetailerData();
            objRetailer.Regionss = objRegion;
            objRetailer.Range = ranges;
            return View(objRetailer);
        }


        public void TodayPresentSO(string StartingDate, string EndingDate, int TID)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {
                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);

                List<KPIData> list = new List<KPIData>();
                decimal? Val = 0;
                KPIData comlist;

                if (TID == 0 )
                {
                    var SaleOfficerIDs = db.SaleOfficers.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => x.ID).ToList();
                    foreach (var item in SaleOfficerIDs)
                    {
                        var totalVisitsToday = db.JobsDetails.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.Status == true).ToList();

                        if (totalVisitsToday != null)
                        {
                            comlist = new KPIData();
                            comlist.SoName = db.SaleOfficers.Where(x => x.ID == item).Select(x => x.Name).FirstOrDefault();
                            comlist.totalVisits = totalVisitsToday.Count();
                            comlist.CityName = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.Retailer.City.Name).FirstOrDefault();
                            comlist.StartDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.JobDate).FirstOrDefault();
                            comlist.EndDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();
                            comlist.ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.VisitPurpose == "Ordering" && x.Status == true
                            ).Select(x => x.ID).Count();
                            comlist.NonProductive = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.VisitPurpose == "FollowupVisit" && x.Status == true
                             ).Select(x => x.ID).Count();

                            TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                            comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);
                            var total = db.sp_BrandAndItemWiseReport(start, final, 0, item, 0, 0, 6).ToList();

                            foreach (var items in total)
                            {
                                Val += items.TotalQuantity;
                            }
                            comlist.totalSale = Val;
                            list.Add(comlist);
                        }
                    }

                }

                else if (  TID != 0)
                {
                    var SaleOfficerIDs = db.SaleOfficers.Where(x => x.RegionalHeadID == TID && x.IsActive == true && x.IsDeleted == false).Select(x => x.ID).ToList();
                    foreach (var item in SaleOfficerIDs)
                    {
                        var totalVisitsToday = db.JobsDetails.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.Status == true).ToList();

                        if (totalVisitsToday != null)
                        {
                            comlist = new KPIData();
                            comlist.SoName = db.SaleOfficers.Where(x => x.ID == item).Select(x => x.Name).FirstOrDefault();
                            comlist.totalVisits = totalVisitsToday.Count();
                            comlist.CityName = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.Retailer.City.Name).FirstOrDefault();
                            comlist.StartDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.JobDate).FirstOrDefault();
                            comlist.EndDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();
                            comlist.ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final  && x.VisitPurpose == "Ordering" && x.Status == true
                            ).Select(x => x.ID).Count();
                            comlist.NonProductive = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.VisitPurpose == "FollowupVisit" && x.Status == true
                           ).Select(x => x.ID).Count();
                            //comlist.totallines = db.Sp_TotalLinesForSummaryInDSR(item, start, final).FirstOrDefault();
                            //var finallines = Convert.ToDecimal(comlist.totallines);
                            //var finalProductiveShops = Convert.ToDecimal(comlist.ProductiveShops);

                            //if (comlist.ProductiveShops != 0)
                            //{

                            //    var Linesperbill = finallines / finalProductiveShops;

                            //    comlist.Linesperbill = Linesperbill.ToString("0.0");
                            //}

                            TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                            comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);
                            var total = db.sp_BrandAndItemWiseReport(start, final, 0, item, 0, 0, 6).ToList();

                            foreach (var items in total)
                            {
                                Val += items.TotalQuantity;
                            }
                            comlist.totalSale = Val;
                            list.Add(comlist);
                        }
                    }
                }

                var filtered = list.Where(t => t.totalVisits > 0);
                ManageRetailer objRetailers = new ManageRetailer();
                //List<spGetSalesOfficerWithLoginDate_Result> result = objRetailers.TodayPresentSalesOfficer(TID, start, final);
                // Example data
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"SrNo.\",\"AreaName\",\"SOName\",\"Total Visits\",\"Productive Visits\",\"Follow Ups\",\"Market Start Time\",\"Market End Time\",\"Elapse Time\",\"TotalSaleIncarton\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=Performance Report" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";

                //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                int srNo = 1;
                foreach (var retailer in filtered)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"",
                    srNo,
                    retailer.CityName,
                    retailer.SoName,
                    retailer.totalVisits,
                    retailer.ProductiveShops,
                    retailer.NonProductive,
                     
                      retailer.StartDate,
                       retailer.EndDate,
                        retailer.ElapseTime,
                        retailer.totalSale
                   ,
                    srNo++


                    ));
                }
                Response.Write(sw.ToString());
                Response.End();
                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Present SO Report";
                hst.ReportType = "PresentSO";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();


            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

        }

        public ActionResult MarketInformation(string StartingDate, string EndingDate, int TID, int fosid, int dealerid)
        {
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                ManageRetailer objRetailers = new ManageRetailer();
                List<sp_MarketInformation_Result> Result = objRetailers.MarketInfo(start, end, TID, fosid, dealerid);

                rptDataSet obj = new rptDataSet();
                DataRow dtrow;
                DataTable dtNewTable;
                dtNewTable = new DataTable();
                DataColumn dcol5 = new DataColumn("SaleOfficerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol5);
                DataColumn dcol3 = new DataColumn("DealerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol3);
                DataColumn dcol0 = new DataColumn("ShopName", typeof(System.String));
                dtNewTable.Columns.Add(dcol0);
                DataColumn dcol06 = new DataColumn("JobDate", typeof(System.DateTime));
                dtNewTable.Columns.Add(dcol06);
                DataColumn dcol2 = new DataColumn("Available", typeof(System.String));
                dtNewTable.Columns.Add(dcol2);
                DataColumn dcol4 = new DataColumn("Available1kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol4);
                DataColumn dcol7 = new DataColumn("Available5kg", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol7);
                DataColumn dcol6 = new DataColumn("PSO Material", typeof(System.String));
                dtNewTable.Columns.Add(dcol6);
                DataColumn dcol65 = new DataColumn("UBL Account Opened", typeof(System.String));
                dtNewTable.Columns.Add(dcol65);
                DataColumn dcol66 = new DataColumn("Broucher Avaiabe", typeof(System.String));
                dtNewTable.Columns.Add(dcol66);
                DataColumn dcol67 = new DataColumn("SMS Card Avaiable", typeof(System.String));
                dtNewTable.Columns.Add(dcol67);
                DataColumn dcol68 = new DataColumn("Shade Car Avaiable", typeof(System.String));
                dtNewTable.Columns.Add(dcol68);
                DataColumn dcol69 = new DataColumn("Display", typeof(System.String));
                dtNewTable.Columns.Add(dcol69);
                DataColumn dcol70 = new DataColumn("White 40 KG Avaiable", typeof(System.String));
                dtNewTable.Columns.Add(dcol70);
                DataColumn dcol71 = new DataColumn("Note", typeof(System.String));
                dtNewTable.Columns.Add(dcol71);
                foreach (var item in Result)
                {

                    dtrow = dtNewTable.NewRow();
                    dtrow[0] = item.SaleOfficerName;
                    dtrow[1] = item.DealerName;
                    dtrow[2] = (item.ShopName);
                    dtrow[3] = item.JobDate;
                    dtrow[4] = item.Available;
                    dtrow[5] = item.Available1kg;
                    dtrow[6] = item.Available5kg;
                    dtrow[7] = item.PSOMaterial;
                    dtrow[8] = item.UBLAccountOpened;
                    dtrow[9] = item.BroucherAvaiabe;
                    dtrow[10] = item.SmsCardAvailable;
                    dtrow[11] = item.ShadeCardAvailable;
                    dtrow[12] = item.Display;
                    dtrow[13] = item.White40KgAvailable;
                    dtrow[14] = item.Note;

                    dtNewTable.Rows.Add(dtrow);

                }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/MarketInformation.rpt")));
                rd.SetDataSource(dtNewTable);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string type = "xls";
                if (type == "pdf")
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "MarketInformation.pdf");
                }
                else
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/ms-excel", string.Format("MarketInformation{0}.xls", DateTime.Now.ToShortDateString()));
                }

                return View();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                return null;
            }

        }


        public ActionResult CityRetailerWiseReport()
        {
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<CityData> cities = new List<CityData>();
            cities = FOS.Setup.ManageRegionalHead.GetCities();
            cities.Insert(0, new CityData
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();
            RetailerObj.Insert(0, new RetailerData
            {
                ID = 0,
                Name = "All"
            });
            var objJob = new JobsData();
            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.Retailers = RetailerObj;
            objJob.Cities = cities;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");

            return View(objJob);
        }

        public ActionResult CityRetailerWiseReportExtract(int cityid, int retailerid)
        {
            try
            {

                DateTime start = DateTime.Now;
                //DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp_CityMarketRetailerInfo_Result> Result = objRetailers.CityMarketRetailerInfo(start, cityid, retailerid);

                rptDataSet obj = new rptDataSet();
                DataRow dtrow;
                DataTable dtNewTable;
                dtNewTable = new DataTable();
                DataColumn dcol5 = new DataColumn("City", typeof(System.String));
                dtNewTable.Columns.Add(dcol5);
                DataColumn dcol3 = new DataColumn("Area", typeof(System.String));
                dtNewTable.Columns.Add(dcol3);
                DataColumn dcol0 = new DataColumn("ShopName", typeof(System.String));
                dtNewTable.Columns.Add(dcol0);
                DataColumn dcol06 = new DataColumn("Category", typeof(System.String));
                dtNewTable.Columns.Add(dcol06);
                DataColumn dcol2 = new DataColumn("Previous ", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol2);
                DataColumn dcol4 = new DataColumn("Current", typeof(System.Int32));
                dtNewTable.Columns.Add(dcol4);
                DataColumn dcol7 = new DataColumn("Average", typeof(System.Decimal));
                dtNewTable.Columns.Add(dcol7);

                foreach (var item in Result)
                {

                    dtrow = dtNewTable.NewRow();
                    dtrow[0] = item.City;
                    dtrow[1] = item.Area;
                    dtrow[2] = (item.ShopName);
                    dtrow[3] = item.Category;
                    dtrow[4] = item.Previous;
                    dtrow[5] = item.Current;
                    dtrow[6] = item.Average;


                    dtNewTable.Rows.Add(dtrow);

                }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/CityRetailerWiseInfo.rpt")));
                rd.SetDataSource(dtNewTable);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string type = "xls";
                if (type == "pdf")
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "CityRetailerWiseInfo.pdf");
                }
                else
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/ms-excel", string.Format("CityRetailerWiseInfo{0}.xls", DateTime.Now.ToShortDateString()));
                }

                return View();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                return null;
            }

        }


        public ActionResult ShopBrandWiseDisplay()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            regionalHeadData.Insert(0, new RegionalHeadData
            {
                ID = 0,
                Name = "All"
            });

            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regionalHeadData.FirstOrDefault().ID, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            //objJob.VisitPlan = visitData;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
            objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);

            return View(objJob);
        }


        public ActionResult ShopBrandWiseDisplayReport(string StartingDate, string EndingDate, int TID, int fosid, int dealerid, int cityid, int display)
        {
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                ManageRetailer objRetailers = new ManageRetailer();
                List<Sp__ShopBrandWiseDisplayData_Result> result = objRetailers.ShopBrandWiseDisplayReport(start, end, TID, fosid, dealerid, cityid, display);
                rptDataSet obj = new rptDataSet();
                DataRow dtrow;
                DataTable dtNewTable;
                dtNewTable = new DataTable();
                DataColumn dcol51 = new DataColumn("Terretory Head", typeof(System.String));
                dtNewTable.Columns.Add(dcol51);
                DataColumn dcol5 = new DataColumn("SaleOfficerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol5);
                DataColumn dcol3 = new DataColumn("DealerName", typeof(System.String));
                dtNewTable.Columns.Add(dcol3);
                DataColumn dcol0 = new DataColumn("ShopName", typeof(System.String));
                dtNewTable.Columns.Add(dcol0);
                DataColumn dcol01 = new DataColumn("City", typeof(System.String));
                dtNewTable.Columns.Add(dcol01);
                DataColumn dcol2 = new DataColumn("Date", typeof(System.DateTime));
                dtNewTable.Columns.Add(dcol2);
                DataColumn dcol4 = new DataColumn("Display", typeof(System.String));
                dtNewTable.Columns.Add(dcol4);
                DataColumn dcol7 = new DataColumn("Path", typeof(System.String));
                dtNewTable.Columns.Add(dcol7);





                foreach (var item in result)
                {

                    dtrow = dtNewTable.NewRow();
                    dtrow[0] = item.Name;
                    dtrow[1] = item.SaleOfficerName;
                    dtrow[2] = item.DealerName;
                    dtrow[3] = (item.ShopName);
                    dtrow[4] = (item.CityName);
                    dtrow[5] = item.DateComplete;
                    dtrow[6] = item.Display;
                    dtrow[7] = item.Path;

                    dtNewTable.Rows.Add(dtrow);

                }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports/ShopBrandWiseDisplay.rpt")));
                rd.SetDataSource(dtNewTable);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                string type = "xls";
                if (type == "pdf")
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "ShopBrandWiseDisplay.pdf");
                }
                else
                {
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/ms-excel", string.Format("ShopBrandWiseDisplay{0}.xls", DateTime.Now.ToShortDateString()));
                }

                return View();
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                return null;
            }

        }

        public ActionResult DailyPerformanceKPI()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }



            //List<VisitPlanData> visitData = new List<VisitPlanData>();
            //visitData = FOS.Setup.ManageJobs.GetAllVisitList();

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
           objJob.Range = ranges;
            objJob.PainterCityNames = FOS.Setup.ManagePainter.GetPainterCityNamesList();
            objJob.VisitPlanEach = new Shared.Common.SelectedWeekday("0000000");
           // objJob.Cities = ManageCity.GetCityListBySOID(SaleOfficerObj.FirstOrDefault().ID);
            return View(objJob);
        }

        public void DailyPerformanceKPIDetails(string StartingDate, string EndingDate, string Type , string Typess,int RHID, int SOID, string ReportType, int RHIDD)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            Microsoft.Reporting.WebForms.LocalReport ReportViewer1 = new Microsoft.Reporting.WebForms.LocalReport();
            FOSDataModel data = new FOSDataModel();
            DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
            DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
            DateTime final = end.AddDays(1);
            //RegionWIse
            if (Type == "RegionWise")
            {
                List<sp_getRegionWiseOrdersAndFollowups1_4_Result> result = data.sp_getRegionWiseOrdersAndFollowups1_4(start, final,RHID).ToList();

                if (ReportType == "Excel")
                {
                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"SrNo.\",\"RegionName\",\"Orders\",\"FollowUps\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=RegionWise" + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";

                    //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                    int srNo = 1;
                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                        srNo,

                        retailer.Name,
                        retailer.Orders,
                        retailer.Followups,
                        srNo++


                        ));
                    }
                    Response.Write(sw.ToString());
                    Response.End();

                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report";
                    hst.ReportType = "RegionWise";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();


                }
                else
                {
                    try
                    {
                        var regionalheadName = db.RegionalHeads.Where(x => x.ID == RHID).Select(x => x.Name).FirstOrDefault();

                        ReportParameter[] prm = new ReportParameter[4];
                       
                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("DateTo", EndingDate);
                        prm[2] = new ReportParameter("DateFrom", StartingDate);
                        prm[3] = new ReportParameter("RegionalHeadName", regionalheadName);
                    
                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\RegionWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
                       
                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);
                     
                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=RegionWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }
                   

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }

                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report PDF";
                    hst.ReportType = "RegionWise";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }

            }

            //SOWise
            if (Type == "SoWise")
            {
                List<sp_getSoWiseOrdersAndFollowUps1_6_Result> result = data.sp_getSoWiseOrdersAndFollowUps1_6(start, final,RHID,6).ToList();
                if (ReportType == "Excel")
                {

                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"SrNo.\",\"RegionalHeadName\",\"SOName\",\"RetailerOrders\",\"Followups\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=SOWise" + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";

                    //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                    int srNo = 1;
                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                        srNo,
                        retailer.RegionName,
                        retailer.SaleOfficerName,
                        retailer.RetailerOrders,
                      
                        retailer.Followups,
                        srNo++


                        ));
                    }
                    Response.Write(sw.ToString());
                    Response.End();

                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report";
                    hst.ReportType = "SOWise";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        var regionalheadName = db.RegionalHeads.Where(x => x.ID == RHID).Select(x => x.Name).FirstOrDefault();

                        ReportParameter[] prm = new ReportParameter[4];

                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("DateTo", EndingDate);
                        prm[2] = new ReportParameter("DateFrom", StartingDate);
                        prm[3] = new ReportParameter("RegionalHeadName", regionalheadName);

                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\SOWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);

                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);

                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=SOWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report PDF";
                    hst.ReportType = "SOWise";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
            }

            //ItemWise
            if (Type == "ItemWise")
            {
                List<sp_getItemWiseOrdersCount1_1_Result> result = data.sp_getItemWiseOrdersCount1_1(start, final,RHID).ToList();

                if (ReportType == "Excel")
                {
                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"SrNo.\",\"Range Name\",\"Item Name\",\"Quantity\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=ItemWise" + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";

                    //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                    int srNo = 1;
                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                        srNo,
                        retailer.Maincategdesc,
                 
                        retailer.ItemName,
                        retailer.Qty,
                        srNo++


                        ));
                    }
                    Response.Write(sw.ToString());
                    Response.End();

                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report";
                    hst.ReportType = "ItemWise";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        var regionalheadName = db.RegionalHeads.Where(x => x.ID == RHID).Select(x => x.Name).FirstOrDefault();

                        ReportParameter[] prm = new ReportParameter[4];

                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("DateTo", EndingDate);
                        prm[2] = new ReportParameter("DateFrom", StartingDate);
                        prm[3] = new ReportParameter("RegionalHeadName", regionalheadName);

                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\ItemWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);

                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);

                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=ItemWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report PDF";
                    hst.ReportType = "ItemWise";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
            }
            //DistributorWise
            if (Type == "DistributorWise")
            {
                List<sp_getDistributorWiseOrdersCount1_2_Result> result = data.sp_getDistributorWiseOrdersCount1_2(start, final,RHID).ToList();

                if (ReportType == "Excel")
                {
                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"SrNo.\",\"Region\",\"ShopName\",\"Quantity\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=DistributorWise" + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";

                    //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                    int srNo = 1;
                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                        srNo,

                        retailer.Name,
                        retailer.ShopName,
                        retailer.Orders,
                        srNo++


                        ));
                    }
                    Response.Write(sw.ToString());
                    Response.End();

                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report";
                    hst.ReportType = "DistributorWise";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        var regionalheadName = db.RegionalHeads.Where(x => x.ID == RHID).Select(x => x.Name).FirstOrDefault();

                        ReportParameter[] prm = new ReportParameter[4];

                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("DateTo", EndingDate);
                        prm[2] = new ReportParameter("DateFrom", StartingDate);
                        prm[3] = new ReportParameter("RegionalHeadName", regionalheadName);

                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\DistributorWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);

                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);

                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=DistributorWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report PDF";
                    hst.ReportType = "DistributorWise";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
            }
            //NoSOPresent
            if (Type == "NoSoPresent")
            {
                List<sp_getNoSoPresentInJobOrder1_1_Result> result = data.sp_getNoSoPresentInJobOrder1_1(start, final,RHID).ToList();
                if (ReportType == "Excel")
                {

                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"SrNo.\",\"RegionalHeadName\",\"SoName\",\"Phone1\",\"Phone2\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=SoAbsent" + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";

                    //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                    int srNo = 1;
                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                        srNo,
                        retailer.RegionalHead,
                        retailer.Saleofficer,
                        retailer.Phone1,
                        retailer.Phone2,
                        srNo++


                        ));
                    }
                    Response.Write(sw.ToString());
                    Response.End();

                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report";
                    hst.ReportType = "NOSOPresent";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        var regionalheadName = db.RegionalHeads.Where(x => x.ID == RHID).Select(x => x.Name).FirstOrDefault();

                        ReportParameter[] prm = new ReportParameter[4];

                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("DateTo", EndingDate);
                        prm[2] = new ReportParameter("DateFrom", StartingDate);
                        prm[3] = new ReportParameter("RegionalHeadName", regionalheadName);

                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\AbsentSOWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);

                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);

                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=AbsentSOWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report PDF";
                    hst.ReportType = "NOSOPresent";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
            }


            if (Type == "OnlineVsOffline")
            {
                List<sp_getOnlineVsOffline1_1_Result> result = data.sp_getOnlineVsOffline1_1(start, final,RHID).ToList();

                if (ReportType == "Excel")
                {
                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"SrNo.\",\"RegionalHeadName\",\"SoName\",\"Online\",\"Offline\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=OnlineVsOffline" + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";

                    //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                    int srNo = 1;
                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                        srNo,
                        retailer.RegionalHaeadName,
                        retailer.SoName,
                        retailer.OnlineOrders,
                        retailer.OfflineOrders
                       ,
                        srNo++


                        ));
                    }
                    Response.Write(sw.ToString());
                    Response.End();
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report";
                    hst.ReportType = "OnlineVSOffline";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
                else
                {
                    var regionalheadName = "";
                    try
                    {
                        if (RHID == 0)
                        {
                            regionalheadName = "All";
                        }
                        else
                        {

                             regionalheadName = db.RegionalHeads.Where(x => x.ID == RHID).Select(x => x.Name).FirstOrDefault();
                        }
                        

                        ReportParameter[] prm = new ReportParameter[4];

                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("DateTo", EndingDate);
                        prm[2] = new ReportParameter("DateFrom", StartingDate);
                        prm[3] = new ReportParameter("RegionalHeadName", regionalheadName);

                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\OnlineOfflineWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);

                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);

                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=SOWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Performance KPI Report PDF";
                    hst.ReportType = "OnlineVSOffline";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
            }

            if (Type == "SOSummary")
            {
                if (Typess == "Daily")
                {
                    List<KPIData> list = new List<KPIData>();

                    KPIData comlist;

                    if(RHID==0 && SOID == 0)
                    {
                        var SaleOfficerIDs = db.SaleOfficers.Where(x =>  x.IsActive == true && x.IsDeleted == false).Select(x => x.ID).ToList();
                        foreach (var item in SaleOfficerIDs)
                        {
                            var totalVisitsToday = db.JobsDetails.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.Status == true).ToList();

                            if (totalVisitsToday != null)
                            {
                                comlist = new KPIData();
                                comlist.SoName = db.SaleOfficers.Where(x => x.ID == item).Select(x => x.Name).FirstOrDefault();
                                comlist.totalVisits = totalVisitsToday.Count();
                                comlist.CityName= totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.Retailer.City.Name).FirstOrDefault();
                                comlist.StartDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.JobDate).FirstOrDefault();
                                comlist.EndDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();
                                comlist.ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.VisitPurpose == "Ordering" && x.Status == true
                                ).Select(x => x.ID).Count();
                                comlist.totallines = db.Sp_TotalLinesForSummaryInDSR(item, start, final).FirstOrDefault();
                                var finallines = Convert.ToDecimal(comlist.totallines);
                                var finalProductiveShops = Convert.ToDecimal(comlist.ProductiveShops);

                                if (comlist.ProductiveShops != 0)
                                {

                                    var Linesperbill = finallines / finalProductiveShops;

                                    comlist.Linesperbill = Linesperbill.ToString("0.0");
                                }

                                TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                                comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);
                                var total = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.Status == true).Sum(i => i.OrderTotal);
                                comlist.totalSale = total;
                                list.Add(comlist);
                            }
                        }

                    }

                   else if (SOID == 0 && RHID != 0)
                    {
                        var SaleOfficerIDs = db.SaleOfficers.Where(x => x.RegionalHeadID == RHIDD && x.IsActive==true&& x.IsDeleted==false).Select(x => x.ID).ToList();
                        foreach (var item in SaleOfficerIDs)
                        {
                            var totalVisitsToday = db.JobsDetails.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.Status == true).ToList();

                            if (totalVisitsToday != null)
                            {
                                comlist = new KPIData();
                                comlist.SoName = db.SaleOfficers.Where(x => x.ID == item).Select(x => x.Name).FirstOrDefault();
                                comlist.totalVisits = totalVisitsToday.Count();
                                comlist.CityName = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.Retailer.City.Name).FirstOrDefault();
                                comlist.StartDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.JobDate).FirstOrDefault();
                                comlist.EndDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();
                                comlist.ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.JobType == "Retailer Order" && x.VisitPurpose == "Ordering" && x.Status == true
                                ).Select(x => x.ID).Count();
                                comlist.totallines = db.Sp_TotalLinesForSummaryInDSR(item, start, final).FirstOrDefault();
                                var finallines = Convert.ToDecimal(comlist.totallines);
                                var finalProductiveShops = Convert.ToDecimal(comlist.ProductiveShops);

                                if (comlist.ProductiveShops != 0)
                                {

                                    var Linesperbill = finallines / finalProductiveShops;

                                    comlist.Linesperbill = Linesperbill.ToString("0.0");
                                }

                               TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                                comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);
                                var total = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.Status == true).Sum(i => i.OrderTotal);
                                comlist.totalSale = total;
                                list.Add(comlist);
                            }
                        }
                    }
                    else
                    {
                        var totalVisitsToday = db.JobsDetails.Where(x => x.SalesOficerID == SOID && x.JobDate >= start && x.JobDate <= final && x.Status == true).ToList();

                        comlist = new KPIData();
                        comlist.SoName = db.SaleOfficers.Where(x => x.ID == SOID).Select(x => x.Name).FirstOrDefault();
                        comlist.totalVisits = totalVisitsToday.Count();
                        comlist.CityName = totalVisitsToday.Where(x => x.SalesOficerID == SOID && x.JobDate >= start && x.JobDate <= final).Select(x => x.Retailer.City.Name).FirstOrDefault();
                        comlist.StartDate = totalVisitsToday.Where(x => x.SalesOficerID == SOID && x.JobDate >= start && x.JobDate <= final).Select(x => x.JobDate).FirstOrDefault();
                        comlist.EndDate = totalVisitsToday.Where(x => x.SalesOficerID == SOID && x.JobDate >= start && x.JobDate <= final).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();
                        comlist.ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == SOID && x.JobDate >= start && x.JobDate <= final  && x.VisitPurpose == "Ordering" && x.Status == true
                        ).Select(x => x.ID).Count();
                        comlist.totallines = db.Sp_TotalLinesForSummaryInDSR(SOID, start, final).FirstOrDefault();
                        var finallines = Convert.ToDecimal(comlist.totallines);
                        var finalProductiveShops = Convert.ToDecimal(comlist.ProductiveShops);

                        if (comlist.ProductiveShops != 0)
                        {

                            var Linesperbill = finallines / finalProductiveShops;

                            comlist.Linesperbill = Linesperbill.ToString("0.0");
                        }

                        TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                        comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);
                        var total = totalVisitsToday.Where(x => x.SalesOficerID == SOID && x.JobDate >= start && x.JobDate <= final && x.Status==true).Sum(i => i.OrderTotal);
                        comlist.totalSale = total;
                        list.Add(comlist);

                    }

                    var filtered = list.Where(t => t.totalVisits > 0);

                    if (ReportType == "Excel")
                    {


                        // Example data
                        StringWriter sw = new StringWriter();

                        sw.WriteLine("\"SrNo.\",\"AreaName\",\"SOName\",\"Total Visits\",\"Productive Visits\",\"Total Lines\",\"LinesPerBill\",\"Market Start Time\",\"Market End Time\",\"Elapse Time\",\"TotalSale\"");

                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachment;filename=Performance Report" + DateTime.Now + ".csv");
                        Response.ContentType = "application/octet-stream";

                        //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                        int srNo = 1;
                        foreach (var retailer in filtered)
                        {
                            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\"",
                            srNo,
                            retailer.CityName,
                            retailer.SoName,
                            retailer.totalVisits,
                            retailer.ProductiveShops,
                            retailer.totallines,
                             retailer.Linesperbill,
                              retailer.StartDate,
                               retailer.EndDate,
                                retailer.ElapseTime,
                                retailer.totalSale
                           ,
                            srNo++


                            ));
                        }
                        Response.Write(sw.ToString());
                        Response.End();
                        ManagersLoginHst hst = new ManagersLoginHst();
                        hst.UserID = userID;
                        hst.IPAddress = remoteIpAddress;
                        hst.ReportName = "Daily Performance KPI Report";
                        hst.ReportType = "SOSummary Daily";
                        hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                        db.ManagersLoginHsts.Add(hst);
                        db.SaveChanges();
                    }
                    else
                    {
                        try
                        {


                            var regionalheadName = db.RegionalHeads.Where(x => x.ID == RHIDD).Select(x => x.Name).FirstOrDefault();
                            if (regionalheadName == null)
                            {
                                regionalheadName = "ALL";
                            }

                            ReportParameter[] prm = new ReportParameter[4];

                            prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                            prm[1] = new ReportParameter("DateTo", EndingDate);
                            prm[2] = new ReportParameter("DateFrom", StartingDate);
                            prm[3] = new ReportParameter("RegionalHeadName", regionalheadName);

                            ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\DailyKPI.rdlc");
                            ReportViewer1.EnableExternalImages = true;
                            ReportDataSource dt1 = new ReportDataSource("DataSet1", filtered);

                            ReportViewer1.SetParameters(prm);
                            ReportViewer1.DataSources.Clear();
                            ReportViewer1.DataSources.Add(dt1);

                            ReportViewer1.Refresh();



                            Warning[] warnings;
                            string[] streamIds;
                            string contentType;
                            string encoding;
                            string extension;

                            //Export the RDLC Report to Byte Array.
                            byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            //Download the RDLC Report in Word, Excel, PDF and Image formats.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            Response.AddHeader("content-disposition", "attachment;filename=Performance Report Daily" + DateTime.Now + ".Pdf");
                            Response.BinaryWrite(bytes);
                            Response.Flush();

                            Response.End();

                        }

                        catch (Exception exp)
                        {
                            Log.Instance.Error(exp, "Report Not Working");

                        }
                        ManagersLoginHst hst = new ManagersLoginHst();
                        hst.UserID = userID;
                        hst.IPAddress = remoteIpAddress;
                        hst.ReportName = "Daily Performance KPI Report PDF  ";
                        hst.ReportType = "SOSummary Daily";
                        hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                        db.ManagersLoginHsts.Add(hst);
                        db.SaveChanges();
                    }
                }


                if (Typess == "Weekly")
                {
                    List<KPIData> list = new List<KPIData>();

                    KPIData comlist;



                    var lists = db.sp_GetKPISummarySOWise(start, final, RHIDD, SOID,6).ToList();

                    if (lists != null)
                    {
                        foreach (var items in lists)
                        {
                            comlist = new KPIData();

                            comlist.RHName = items.RegionName;
                            comlist.SoName = items.SaleOfficerName;
                            comlist.totalVisits = items.TotalVisits;

                            comlist.ProductiveShops = (int)items.ProductiveOrders;
                            comlist.NonProductive = (int)items.NonProductive;
                            //comlist.totallines = items.TotalLines;
                            //var finallines = Convert.ToDecimal(comlist.totallines);
                            //var finalProductiveShops = Convert.ToDecimal(comlist.ProductiveShops);

                            //if (comlist.ProductiveShops != 0)
                            //{

                            //    var Linesperbill = finallines / finalProductiveShops;

                            //    comlist.Linesperbill = Linesperbill.ToString("0.0");
                            //}



                            comlist.ProductivePer = (decimal)items.Productiveperage;

                            //TimeSpan t = final - start;
                            //double NrOfDays = t.TotalDays;


                            var days = db.sp_getSOWorkingDays(start,final,items.ID).Count();

                            

                            var val = (double) items.ProductiveOrders / days;
                            comlist.perDayorders = System.Math.Round(val, 2); 
                            //TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                            //comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);

                            list.Add(comlist);

                        }



                    }


                    if (ReportType == "Excel")
                    {

                        // Example data
                        StringWriter sw = new StringWriter();

                        sw.WriteLine("\"SrNo.\",\"HeadName\",\"SOName\",\"Total Visits\",\"Productive Visits\",\"Non-Productive Visits\",\"Total Lines\",\"LinesPerBill\",\"Productive Percentage\",\"Per Day Orders\"");

                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachment;filename=Performance Report Weekly/Monthly" + DateTime.Now + ".csv");
                        Response.ContentType = "application/octet-stream";

                        //var retailers = ManageRetailer.GetRetailersForExportinExcel();
                        int srNo = 1;
                        foreach (var retailer in list)
                        {
                            sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"",
                            srNo,
                            retailer.RHName,
                            retailer.SoName,
                            retailer.totalVisits,
                            retailer.ProductiveShops,
                            retailer.NonProductive,
                            retailer.totallines,
                             retailer.Linesperbill,
                              retailer.ProductivePer,
                               retailer.perDayorders,


                            srNo++


                            ));
                        }
                        Response.Write(sw.ToString());
                        Response.End();
                        ManagersLoginHst hst = new ManagersLoginHst();
                        hst.UserID = userID;
                        hst.IPAddress = remoteIpAddress;
                        hst.ReportName = "Daily Performance KPI Report";
                        hst.ReportType = "SOSummary Weekly/Monthly";
                        hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                        db.ManagersLoginHsts.Add(hst);
                        db.SaveChanges();
                    }
                    else
                    {
                        try
                        {
                           

                            var regionalheadName = db.RegionalHeads.Where(x => x.ID == RHIDD).Select(x => x.Name).FirstOrDefault();

                            ReportParameter[] prm = new ReportParameter[4];

                            prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                            prm[1] = new ReportParameter("DateTo", EndingDate);
                            prm[2] = new ReportParameter("DateFrom", StartingDate);
                            prm[3] = new ReportParameter("RegionalHeadName", regionalheadName);

                            ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\Weeklymonthly.rdlc");
                            ReportViewer1.EnableExternalImages = true;
                            ReportDataSource dt1 = new ReportDataSource("DataSet1", list);

                            ReportViewer1.SetParameters(prm);
                            ReportViewer1.DataSources.Clear();
                            ReportViewer1.DataSources.Add(dt1);

                            ReportViewer1.Refresh();



                            Warning[] warnings;
                            string[] streamIds;
                            string contentType;
                            string encoding;
                            string extension;

                            //Export the RDLC Report to Byte Array.
                            byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            //Download the RDLC Report in Word, Excel, PDF and Image formats.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            Response.AddHeader("content-disposition", "attachment;filename=Performance Report Weekly/Monthly" + DateTime.Now + ".Pdf");
                            Response.BinaryWrite(bytes);
                            Response.Flush();

                            Response.End();

                        }

                        catch (Exception exp)
                        {
                            Log.Instance.Error(exp, "Report Not Working");

                        }

                        ManagersLoginHst hst = new ManagersLoginHst();
                        hst.UserID = userID;
                        hst.IPAddress = remoteIpAddress;
                        hst.ReportName = "Daily Performance KPI Report PDF";
                        hst.ReportType = "SOSummary Weekly/Monthly";
                        hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                        db.ManagersLoginHsts.Add(hst);
                        db.SaveChanges();
                    }
                }




            }
        }

        public ActionResult OrderSummaryReport()
        {
            FOSDataModel db = new FOSDataModel();
            var userID = Convert.ToInt32(Session["UserID"]);
            var objSaleOffice = new OrderSummaryReportData();
            objSaleOffice.ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
      
            var rangeid = objSaleOffice.ranges.Select(r => r.ID).FirstOrDefault();
            objSaleOffice.RegionalHead = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            var regionid = objSaleOffice.RegionalHead.FirstOrDefault();
            objSaleOffice.SaleOfficer = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regionid.ID, true);
            var regionids = objSaleOffice.ranges.FirstOrDefault();
            objSaleOffice.dealerdata = ManageDealer.GetDealersForExportinExcel(regionids.ID, regionid.ID);
            return View(objSaleOffice);

        }
        public ActionResult OrderSummaryReportForDistributor()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            FOSDataModel db = new FOSDataModel();
            var objSaleOffice = new OrderSummaryReportData();
            objSaleOffice.ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);

            var rangeid = objSaleOffice.ranges.Select(r => r.ID).FirstOrDefault();

            objSaleOffice.RegionalHead = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            var regionid = objSaleOffice.RegionalHead.FirstOrDefault();
            objSaleOffice.SaleOfficer = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regionid.ID, true);
            var regionids = objSaleOffice.ranges.FirstOrDefault();
            // objSaleOffice.CityData = ManageRegion.GetCityListForDSR(objSaleOffice.regionData.FirstOrDefault().RegionID);
            return View(objSaleOffice);

        }
        public JsonResult GetSaleOfficersRetailedtoReg(int? RegID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {


                if (RegID > 0)
                {





                    var result = ManageSaleOffice.GetAllSORelatedToRegionForDistributor(RegID).ToList();
                    return Json((result), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string a = "Select Region";
                    return Json((a), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult GetSaleOfficersRetailedtoRegForDistributor(int? RegID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {


                if (RegID > 0)
                {





                    var result = ManageSaleOffice.GetAllSORelatedToRegionForDistributor(RegID).OrderBy(x=>x.Name).ToList();
                    return Json((result), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string a = "Select Region";
                    return Json((a), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult GetCitiesRetailedtoRegForDistributor(int? RegID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {


                if (RegID > 0)
                {
                    var result = ManageSaleOffice.GetAllCitiesRelatedToRegionForDistributor(RegID).OrderBy(x => x.Name).ToList();
                    return Json((result), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string a = "Select Region";
                    return Json((a), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult GetDistributorRetailedtoSO(int? SOID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);


                DateTime dtFromToday = dtFromTodayUtc.Date;
                DateTime todate = dtFromToday.AddDays(1);
                DateTime fromdate = todate.AddDays(-30);

                if (SOID > 0)
                {
                    object[] param = { SOID };

                    var rangeid = db.SaleOfficers.Where(x => x.ID == SOID).Select(x => x.RangeID).FirstOrDefault();
                    if (rangeid == 6)
                    {


                        var result = dbContext.sp_GetDistributorListInDSR(SOID, fromdate, todate).ToList();
                        return Json((result), JsonRequestBehavior.AllowGet);
                    }
                  else if (rangeid == 7)
                    {
                        var result = dbContext.sp_GetDistributorListInDSRRangeB(SOID, fromdate, todate).ToList();
                        return Json((result), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var result = dbContext.sp_GetDistributorListInDSRRangeC(SOID, fromdate, todate).ToList();
                        return Json((result), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    string a = "Select SaleOfficer";
                    return Json((a), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        public JsonResult GetDistributorRetailedtoSOByDate(int? SOID, DateTime dtFrom, DateTime dtTo)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);


                DateTime dtFromToday = dtFromTodayUtc.Date;
                DateTime todate = dtFromToday.AddDays(1);
                DateTime fromdate = todate.AddDays(-30);
                if(dtFrom == null || dtFrom == DateTime.MinValue)
                {
                    dtFrom = dtFromTodayUtc.Date;                
                }

                if (dtTo == null || dtTo == DateTime.MinValue)
                    dtTo = todate.AddDays(-30);
                else
                    dtTo = dtTo.AddDays(1);

                if (SOID > 0)
                {
                    object[] param = { SOID };

                    var rangeid = db.SaleOfficers.Where(x => x.ID == SOID).Select(x => x.RangeID).FirstOrDefault();
                    if (rangeid == 6)
                    {


                        var result = dbContext.sp_GetDistributorListInDSR(SOID, dtFrom, dtTo).ToList();
                        return Json((result), JsonRequestBehavior.AllowGet);
                    }
                    else if (rangeid == 7)
                    {
                        var result = dbContext.sp_GetDistributorListInDSRRangeB(SOID, dtFrom, dtTo).ToList();
                        return Json((result), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var result = dbContext.sp_GetDistributorListInDSRRangeC(SOID, dtFrom, dtTo).ToList();
                        return Json((result), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    string a = "Select SaleOfficer";
                    return Json((a), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public JsonResult GetDistributorRetailedtoSOForDistributor(int? RegID,int? Cityid)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);


                DateTime dtFromToday = dtFromTodayUtc.Date;
                DateTime todate = dtFromToday.AddDays(1);
                DateTime fromdate = todate.AddDays(-30);

                if (RegID > 0)
                {
                    object[] param = { RegID };




                    var result = ManageSaleOffice.GetDistributorRetailedtoSOForDistributor(RegID,Cityid).ToList();
                    return Json((result), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string a = "Select SaleOfficer";
                    return Json((a), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public void OrderSummary(string StartingDate, string EndingDate, int? DisID, int TID, string type)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }

            FOSDataModel db = new FOSDataModel();

            DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
            DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
            DateTime final = end.AddDays(1);
            Microsoft.Reporting.WebForms.LocalReport ReportViewer1 = new Microsoft.Reporting.WebForms.LocalReport();
        
            if (type == "Daily")

            {
                var number = "";

               
                    try
                    {
                        decimal linesperbill = 0;
                        int? total = 0;
                    List<Sp_OrderForPDFinMMCTest_Result> result = db.Sp_OrderForPDFinMMCTest(start, final, DisID, 6, TID).ToList();
                    // List<Sp_OrderSummeryReportInExcel2_5_Result> result = db.Sp_OrderSummeryReportInExcel2_5(start, final, DisID, 6, TID).ToList();
                    List<sp_BrandAndItemWiseReport_Result> result2 = db.sp_BrandAndItemWiseReport(start, final, 0, TID, 0, 0, 6).ToList();
                    // List<Sp_FollowUpVisitsDaily_Result> result1 = db.Sp_FollowUpVisitsDaily(start, final, DisID, 6, TID).ToList();
                    List<Sp_FollowUpVisitsDailyForMMC_Result> result3 = db.Sp_FollowUpVisitsDailyForMMC(start, final, DisID, 6, TID).ToList();


                    string CityName = "";
                        string dealername = "";
                        var dealer = db.Dealers.Where(u => u.ID == DisID).FirstOrDefault();

                        dealername = dealer.ShopName;
                        CityName = dealer.City.Name;

                        string SoName = "";
                        var SO = db.SaleOfficers.Where(u => u.ID == TID).FirstOrDefault();

                        SoName = SO.Name;

                        string RangeName = "";
                        var range = db.MainCategories.Where(u => u.MainCategID == 6).FirstOrDefault();

                        RangeName = range.MainCategDesc;

                    var totalVisitsToday = db.JobsDetails.Where(x => x.SalesOficerID == TID && x.JobDate >= start && x.JobDate <= final && x.Status == true).ToList();

                    var listi = totalVisitsToday.Count();

                    DateTime? firstRecord = totalVisitsToday.Where(x => x.SalesOficerID == TID && x.JobDate >= start && x.JobDate <= final).Select(x => x.JobDate).FirstOrDefault();
                    DateTime? lastRecord = totalVisitsToday.Where(x => x.SalesOficerID == TID && x.JobDate >= start && x.JobDate <= final).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();


                    var ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == TID && x.JobDate >= start && x.JobDate <= final  && x.VisitPurpose == "Ordering" && x.Status == true
                    ).Select(x => x.ID).Count();

                    var FollowUpsShops = totalVisitsToday.Where(x => x.SalesOficerID == TID && x.JobDate >= start && x.JobDate <= final  && x.VisitPurpose == "FollowupVisit" && x.Status == true
                   ).Select(x => x.ID).Count();

                    TimeSpan? difference = (lastRecord - firstRecord);
                    var format = difference;
                    string test = difference.HasValue ? difference.Value.ToString(@"hh\:mm") : string.Empty;


                    ReportParameter[] prm = new ReportParameter[10];
                    prm[0] = new ReportParameter("DistributorName", dealername);
                    prm[1] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                    prm[2] = new ReportParameter("SOName", SoName);

                    prm[3] = new ReportParameter("DateTo", StartingDate);
                    prm[4] = new ReportParameter("DateFrom", StartingDate);
                    prm[5] = new ReportParameter("CityName", CityName);
                    prm[6] = new ReportParameter("TotalVisitsToday", listi.ToString());
                    prm[7] = new ReportParameter("ProductiveShops", ProductiveShops.ToString());
                    prm[8] = new ReportParameter("TodayWorkingTime", test);
                    prm[9] = new ReportParameter("FollowUps", FollowUpsShops.ToString());
                    ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\MMCOrders.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
                    ReportDataSource dt2 = new ReportDataSource("DataSet2", result2);
                    ReportDataSource dt3 = new ReportDataSource("DataSet3", result3);
                    ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);
                    ReportViewer1.DataSources.Add(dt2);
                    ReportViewer1.DataSources.Add(dt3);
                    ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=D" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Daily Sale Report";
                    hst.ReportType = "Daily Range A";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();

               

                
            }

            if (type == "Weekly")
            {
                decimal? total = 0;
                List<Sp_OrderSummeryReportInExcelRangeWiseWeeklyReport_Result> result = db.Sp_OrderSummeryReportInExcelRangeWiseWeeklyReport(start, final, 6, TID).ToList();

                foreach (var item in result)
                {
                    total += item.Subtotal;
                }
                string SoName = "";
                List<SaleOfficer> SO = db.SaleOfficers.Where(u => u.ID == TID).ToList();
                foreach (var SOS in SO)
                {
                    SoName = SOS.Name;
                }
                string RangeName = "";
                List<MainCategory> Region = db.MainCategories.Where(u => u.MainCategID == 6).ToList();
                foreach (var SOS in Region)
                {
                    RangeName = SOS.MainCategDesc;
                }

                ReportParameter[] prm = new ReportParameter[6];

                prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                prm[1] = new ReportParameter("SOName", SoName);
                prm[2] = new ReportParameter("RangeName", RangeName);
                prm[3] = new ReportParameter("DateTo", EndingDate);
                prm[4] = new ReportParameter("DateFrom", StartingDate);
                prm[5] = new ReportParameter("LineTotal", total.ToString());
                ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\WeeklyReport.rdlc");
                ReportViewer1.EnableExternalImages = true;
                ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
              
                ReportViewer1.SetParameters(prm);
                ReportViewer1.DataSources.Clear();
                ReportViewer1.DataSources.Add(dt1);
             
                ReportViewer1.Refresh();
                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                //Export the RDLC Report to Byte Array.
                byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                //Download the RDLC Report in Word, Excel, PDF and Image formats.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment;filename=W" + DateTime.Now + ".pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();

                Response.End();

                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Daily Sale Report";
                hst.ReportType = "Weekly";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
            if (type == "Monthly")
            {
                decimal? total = 0;
                List<Sp_OrderSummeryReportInExcelRangeWiseWeeklyReport_Result> result = db.Sp_OrderSummeryReportInExcelRangeWiseWeeklyReport(start, final, 6, TID).ToList();
                foreach (var item in result)
                {
                    total += item.Subtotal;
                }
                string SoName = "";
                List<SaleOfficer> SO = db.SaleOfficers.Where(u => u.ID == TID).ToList();
                foreach (var SOS in SO)
                {
                    SoName = SOS.Name;
                }
                string RangeName = "";
                List<MainCategory> Region = db.MainCategories.Where(u => u.MainCategID == 6).ToList();
                foreach (var SOS in Region)
                {
                    RangeName = SOS.MainCategDesc;
                }

                ReportParameter[] prm = new ReportParameter[6];
                prm[0] = new ReportParameter("RangeName", RangeName);
                prm[1] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                prm[2] = new ReportParameter("SOName", SoName);
                prm[3] = new ReportParameter("DateTo", EndingDate);
                prm[4] = new ReportParameter("DateFrom", StartingDate);
                prm[5] = new ReportParameter("LineTotal", total.ToString());
                ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\MonthlyReport.rdlc");
                ReportViewer1.EnableExternalImages = true;
                ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
                
                ReportViewer1.SetParameters(prm);
                ReportViewer1.DataSources.Clear();
                ReportViewer1.DataSources.Add(dt1);
            
                ReportViewer1.Refresh();
                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                //Export the RDLC Report to Byte Array.
                byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                //Download the RDLC Report in Word, Excel, PDF and Image formats.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment;filename=M" + DateTime.Now + ".pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();

                Response.End();

                ManagersLoginHst hst = new ManagersLoginHst();
                hst.UserID = userID;
                hst.IPAddress = remoteIpAddress;
                hst.ReportName = "Daily Sale Report";
                hst.ReportType = "Monthly";
                hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                db.ManagersLoginHsts.Add(hst);
                db.SaveChanges();
            }
        }


        public void OrderSummaryForDistributor(string StartingDate, string EndingDate, int? DisID, int TID)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }

            FOSDataModel db = new FOSDataModel();

            DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
            DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
            DateTime final = end.AddDays(1);
            Microsoft.Reporting.WebForms.LocalReport ReportViewer1 = new Microsoft.Reporting.WebForms.LocalReport();


            try
            {

                List<Sp_OrderSummeryReportInExcelRangeWiseForDistributor1_1_Result> result = db.Sp_OrderSummeryReportInExcelRangeWiseForDistributor1_1(start, final,  6, TID).ToList();
                
               
                string SoName = "";
                List<SaleOfficer> SO = db.SaleOfficers.Where(u => u.ID == TID).ToList();
                foreach (var SOS in SO)
                {
                    SoName = SOS.Name;
                }
                string RangeName = "";
                List<MainCategory> range = db.MainCategories.Where(u => u.MainCategID == 6).ToList();
                foreach (var SOS in range)
                {
                    RangeName = SOS.MainCategDesc;
                }

              
                ReportParameter[] prm = new ReportParameter[4];
          
                prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                prm[1] = new ReportParameter("SOName", SoName);
                prm[2] = new ReportParameter("DateTo", EndingDate);
                prm[3] = new ReportParameter("DateFrom", StartingDate);
                
                ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\Report1.rdlc");
                ReportViewer1.EnableExternalImages = true;
                ReportDataSource dt1 = new ReportDataSource("DataSet1", result);
                ReportViewer1.SetParameters(prm);
                ReportViewer1.DataSources.Clear();
                ReportViewer1.DataSources.Add(dt1);
                ReportViewer1.Refresh();



                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                //Export the RDLC Report to Byte Array.
                byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                //Download the RDLC Report in Word, Excel, PDF and Image formats.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment;filename=D" + DateTime.Now + ".Pdf");
                Response.BinaryWrite(bytes);
                Response.Flush();

                Response.End();

            }

            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

            ManagersLoginHst hst = new ManagersLoginHst();
            hst.UserID = userID;
            hst.IPAddress = remoteIpAddress;
            hst.ReportName = "Daily Sale Report For Distributor";
            hst.ReportType = "DealerSaleReport";
            hst.CreatedOn = DateTime.UtcNow.AddHours(5);
            db.ManagersLoginHsts.Add(hst);
            db.SaveChanges();


        }

        public ActionResult ShopsNotVisitedBySORpt()
        {
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.Select(r => r.ID).FirstOrDefault();
            List<RegionData> RegionObj = ManageRegion.GetRegionDataList(userID);
            var objRegion = RegionObj.FirstOrDefault();
            List<CityData> cityObj = FOS.Setup.ManageRegion.GetCitiesList(objRegion.ID);
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            var objRegionalHead = regionalHeadData.FirstOrDefault();
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(objRegionalHead.ID, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });


            var objArea = new AreaData();
            objArea.Regions = RegionObj;
            objArea.SaleOfficersFroms = SaleOfficerObj;
            objArea.RegionalHeads = regionalHeadData;
            objArea.Cities = cityObj;
            objArea.Range = ranges;
            return View(objArea);
        }
        public void ShopsNotVisitedBySORptDetail(string StartingDate, string EndingDate,int RegionID ,int CityID,int RangeID ,int intSaleOfficerIDfrom)
        {


            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            FOSDataModel data = new FOSDataModel();
            DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
            DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);

            List<sp_ShopsNotVisitedBySo1_3_Result> result = data.sp_ShopsNotVisitedBySo1_3(intSaleOfficerIDfrom, CityID, start, end,RangeID).ToList();

            StringWriter sw = new StringWriter();

            sw.WriteLine("\"SrNo.\",\"Shops\",\"Town\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ShopsNotVisited" + DateTime.Now + ".csv");
            Response.ContentType = "application/octet-stream";

            //var retailers = ManageRetailer.GetRetailersForExportinExcel();
            int srNo = 1;
            foreach (var retailer in result)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\"",
                srNo,
                retailer.ShopName,
                retailer.CityName,

                srNo++


                ));
            }
            Response.Write(sw.ToString());
            Response.End();

            ManagersLoginHst hst = new ManagersLoginHst();
            hst.UserID = userID;
            hst.IPAddress = remoteIpAddress;
            hst.ReportName = "Shops not Visited by SO Report";
            hst.ReportType = "ShopsNotVisited";
            hst.CreatedOn = DateTime.UtcNow.AddHours(5);
            db.ManagersLoginHsts.Add(hst);
            db.SaveChanges();


        }
        public ActionResult ItemsNotSoldBySoRpt()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionData> RegionObj = ManageRegion.GetRegionDataList(userID);
            var objRegion = RegionObj.FirstOrDefault();
            List<MainCategories> Ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = Ranges.FirstOrDefault();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            var objhead = regionalHeadData.FirstOrDefault();
            List<SaleOfficer> Sos = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(objhead.ID, true);
            

            var objArea = new AreaData();
            objArea.Regions = RegionObj;
            objArea.SaleOfficersFroms = Sos;
            objArea.RegionalHeads = regionalHeadData;
            objArea.Range = Ranges;
            return View(objArea);
        }
        public void ItemsNotSoldBySoRptDetail(string StartingDate, string EndingDate,int RangeID ,int intSaleOfficerIDfrom)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }


            FOSDataModel data = new FOSDataModel();
            DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
            DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);

            List<Sp_ItemsNotSoldBySo1_0_Result> result = data.Sp_ItemsNotSoldBySo1_0(intSaleOfficerIDfrom, RangeID,start, end).ToList();
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"SrNo.\",\"Range\",\"Brand\",\"ItemName\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ItemsNotSold" + DateTime.Now + ".csv");
            Response.ContentType = "application/octet-stream";

            //var retailers = ManageRetailer.GetRetailersForExportinExcel();
            int srNo = 1;
            foreach (var retailer in result)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                srNo,
                retailer.MainCategdesc,
                retailer.SubCategDesc,
                retailer.ItemName,
                srNo++


                ));
            }
            Response.Write(sw.ToString());
            Response.End();

            ManagersLoginHst hst = new ManagersLoginHst();
            hst.UserID = userID;
            hst.IPAddress = remoteIpAddress;
            hst.ReportName = "Items Not Sold By So Report";
            hst.ReportType = "ItemsNotSoldBySoRpt";
            hst.CreatedOn = DateTime.UtcNow.AddHours(5);
            db.ManagersLoginHsts.Add(hst);
            db.SaveChanges();


        }
        public JsonResult GetSaleOfficersByRegionID(int RegionHeadID)
        {
            var result = FOS.Setup.ManageCity.GetSaleOfficersByRegionID(RegionHeadID);
            return Json(result);
        }

        public JsonResult getRegionalHeadsByRegionID(int RegionID)
        {
            var result = FOS.Setup.ManageCity.getRegionalHeadsByRegionID(RegionID);
            return Json(result);
        }


        public JsonResult getCitiesRegionID(int RegionID)
        {
            var result = FOS.Setup.ManageRegion.GetCitiesList(RegionID);
            return Json(result);
        }
        #region Total Followup Report
        public ActionResult GetTotalFollowupReport()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            List<MainCategories> Ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = Ranges.FirstOrDefault();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }
            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Range = Ranges;
            return View(objJob);
        }
        public void GetAllFollowUpRetailer(string StartingDate, string EndingDate ,int HeadID, int fosid)
        {
          

            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }
            try
            {

                DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
                DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);
                DateTime final = end.AddDays(1);
                ManageRetailer objRetailers = new ManageRetailer();
                FOSDataModel db = new FOSDataModel();
                List<Sp_GetTotalFollowup1_3_Result> result = db.Sp_GetTotalFollowup1_3(start, final,HeadID,6 ,fosid).ToList();
                // Example data
                StringWriter sw = new StringWriter();

                sw.WriteLine("\"SrNo.\",\"Shop Name\",\"Head Name\",\"SO Name\",\"City Name\",\"VisitPurpose\",\"Followup Reason\",\"Date\"");

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment;filename=FollowUp" + DateTime.Now + ".csv");
                Response.ContentType = "application/octet-stream";

                //   var retailers = ManageRetailer.GetRetailersForExportinExcel();
                int SrNo = 1;
                foreach (var retailer in result)
                {
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"",
                    SrNo,
                    retailer.ShopName,
                    retailer.HeadName,
                    retailer.Saleofficer,
                    retailer.CityName,
                    retailer.FollowUp,
                    // retailer.Name,
                    retailer.FollowupReason,
                    retailer.JobDate,
                    SrNo++
                    // retailer.CustomerType



                    ));
                }
                Response.Write(sw.ToString());
                Response.End();

            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");
                // return null;
            }

            ManagersLoginHst hst = new ManagersLoginHst();
            hst.UserID = userID;
            hst.IPAddress = remoteIpAddress;
            hst.ReportName = "Follow Up Report";
            hst.ReportType = "Followup";
            hst.CreatedOn = DateTime.UtcNow.AddHours(5);
            db.ManagersLoginHsts.Add(hst);
            db.SaveChanges();


        }
        #endregion

        #region SovisitsFrequency
        public JsonResult GetSoRegionWise(int RegionID)
        {
            var result = FOS.Setup.ManageCity.GetSoRegionWise(RegionID);
            return Json(result);
        }
        public ActionResult SoVisitsFrequency()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionData> RegionObj = ManageRegion.GetRegionDataList(userID);
            var objRegion = RegionObj.FirstOrDefault();
            List<SaleOfficerData> Sos = FOS.Setup.ManageCity.GetSoRegionWise(objRegion.ID);


            var objArea = new AreaData();
            objArea.Regions = RegionObj;
            objArea.SaleOfficersFrom = Sos;

            return View(objArea);
        }
        public void SoVisitsFrequencyDetail(string StartingDate, string EndingDate, int intSaleOfficerIDfrom,int RegionID)
        {
            FOSDataModel data = new FOSDataModel();
            DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(StartingDate) ? DateTime.Now.ToString() : StartingDate);
            DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(EndingDate) ? DateTime.Now.ToString() : EndingDate);

            List<Sp_SoVisitsFrequency_Result> result = data.Sp_SoVisitsFrequency(RegionID,intSaleOfficerIDfrom, start, end).ToList();
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"SrNo.\",\"Saleofficer\",\"ShopName\",\"Date\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=SoVisitsFrequency" + DateTime.Now + ".csv");
            Response.ContentType = "application/octet-stream";

            //var retailers = ManageRetailer.GetRetailersForExportinExcel();
            int srNo = 1;
            foreach (var retailer in result)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                srNo,

                retailer.Name,
                retailer.ShopName,
                retailer.Date,
                srNo++


                ));
            }
            Response.Write(sw.ToString());
            Response.End();
        }
        #endregion SovisitsFrequency


        public ActionResult BrandWiseReport()
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            List<MainCategories> CityObj = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);

            var objRegion = CityObj.FirstOrDefault();
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            if (userID == 1)
            {
                regionalHeadData.Insert(0, new RegionalHeadData
                {
                    ID = 0,
                    Name = "All"
                });
            }

            int regId = 0;
            if (FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser() == 0)
            {
                regId = regionalHeadData.Select(r => r.ID).FirstOrDefault();
            }
            else
            {
                regId = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            }
            //List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageSaleOffice.GetAllSaleOfficerListRelatedtoregionalHeadID(0);
            List<SaleOfficer> SaleOfficerObj = FOS.Setup.ManageRegion.GetAllSOListRelatedtoregionalHeadID(regId, true);

            SaleOfficerObj.Insert(0, new SaleOfficer
            {
                ID = 0,
                Name = "All"
            });

            List<RetailerData> RetailerObj = new List<RetailerData>();

            if (SaleOfficerObj == null)
            {
                return View();
            }

            else
            {
                RetailerObj = FOS.Setup.ManageRetailer.GetAllRetailerSaleOfficerList(SaleOfficerObj.Select(s => s.ID).FirstOrDefault());
                ViewData["RetailerObj"] = RetailerObj;
            }


            ////int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
           
            List<SubCategories> SubCategory = FOS.Setup.ManageCity.GetSubCatList(objRegion.ID);
            var objRegions = SubCategory.FirstOrDefault();
            List<Items> Items = FOS.Setup.ManageCity.GetItemsList(objRegion.ID, objRegions.SubCategoryID);
        



            var objJob = new JobsData();

            objJob.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objJob.SaleOfficer = SaleOfficerObj;
            objJob.Retailers = RetailerObj;
            objJob.RegionalHead = regionalHeadData;
            objJob.Regions = CityObj;
            objJob.SubCategory = SubCategory;
            objJob.itemList = Items;


            return View(objJob);
        }


        public void BrandWiseReportInExcel(int TID, int fosid, int Regionid, int cityid,int rangeid, string sdate, string edate, string ReportType)
        {

            var userID = Convert.ToInt32(Session["UserID"]);
            var remoteIpAddress = "";
            string hostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ip in ipaddress)
            {
                remoteIpAddress = ip.ToString();
            }



            Microsoft.Reporting.WebForms.LocalReport ReportViewer1 = new Microsoft.Reporting.WebForms.LocalReport();
            DateTime start = Convert.ToDateTime(string.IsNullOrEmpty(sdate) ? DateTime.Now.ToString() : sdate);
            DateTime end = Convert.ToDateTime(string.IsNullOrEmpty(edate) ? DateTime.Now.ToString() : edate);
            DateTime final = end.AddDays(1);

            try
            {

                ManageRetailer objRetailers = new ManageRetailer();
                List<sp_BrandAndItemWiseReport_Result> result = objRetailers.BrandWisereports(start, final, Regionid,fosid,TID,cityid,rangeid);
               
                if (ReportType == "Excel")
                {
                    // Example data
                    StringWriter sw = new StringWriter();

                    sw.WriteLine("\"Regional Head Name\",\"Sales Officer Name\",\"Brand Name\",\"Item Name\",\"Quantity In PCS\",\"Quantity In CTN\"");

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", "attachment;filename=BrandWiseReport " + DateTime.Now + ".csv");
                    Response.ContentType = "application/octet-stream";

                    //var RegionalHead = db.RegionalHeadRegions.Where(x => x.RegionHeadID == Regionid).Select(x => x.RegionID).FirstOrDefault();



                    foreach (var retailer in result)
                    {
                        sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",


                       // retailer.Name,
                       retailer.HeadName,
                        retailer.SaleOfficerName,
                        retailer.Brand,
                        retailer.ItemName,
                        retailer.TotalQuantity,
                        retailer.TotalQuantity




                        ));
                    }
                    Response.Write(sw.ToString());
                    Response.End();

                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Brand Wise Report Excel";
                    hst.ReportType = "BrandReport";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();


                }

                else
                {
                    try
                    {
                        

                        ReportParameter[] prm = new ReportParameter[3];

                        prm[0] = new ReportParameter("Date", (System.DateTime.Now.ToString()));
                        prm[1] = new ReportParameter("DateTo", edate);
                        prm[2] = new ReportParameter("DateFrom", sdate);
                        

                        ReportViewer1.ReportPath = Server.MapPath("~\\Views\\Reports\\BrandWise.rdlc");
                        ReportViewer1.EnableExternalImages = true;
                        ReportDataSource dt1 = new ReportDataSource("DataSet1", result);

                        ReportViewer1.SetParameters(prm);
                        ReportViewer1.DataSources.Clear();
                        ReportViewer1.DataSources.Add(dt1);

                        ReportViewer1.Refresh();



                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        //Download the RDLC Report in Word, Excel, PDF and Image formats.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment;filename=BrandWiseReport" + DateTime.Now + ".Pdf");
                        Response.BinaryWrite(bytes);
                        Response.Flush();

                        Response.End();

                    }

                    catch (Exception exp)
                    {
                        Log.Instance.Error(exp, "Report Not Working");

                    }
                    ManagersLoginHst hst = new ManagersLoginHst();
                    hst.UserID = userID;
                    hst.IPAddress = remoteIpAddress;
                    hst.ReportName = "Brand Wise Report PDF";
                    hst.ReportType = "BrandReport";
                    hst.CreatedOn = DateTime.UtcNow.AddHours(5);
                    db.ManagersLoginHsts.Add(hst);
                    db.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Report Not Working");

            }

        }



    }
}