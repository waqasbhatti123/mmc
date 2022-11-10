using FluentValidation.Results;
using FOS.DataLayer;
using FOS.Setup;
using FOS.Setup.Validation;
using FOS.Shared;
using FOS.Web.UI.Common.CustomAttributes;
using FOS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FOS.Web.UI.Controllers
{
    public class SaleOfficerController : Controller
    {
        FOSDataModel dbContext = new FOSDataModel();
        #region Sale Officer

        [CustomAuthorize]
        //View
        public ActionResult SaleOfficer()
        {
            // Load RegionalHead Data ...

            var objSaleOffice = new SaleOfficerData();
            objSaleOffice.RegionalHead = FOS.Setup.ManageRegionalHead.GetRegionalHeadList();
            objSaleOffice.RegionalHeadTypeData = FOS.Setup.ManageRegion.GetRegionalHeadsType();
            objSaleOffice.Cities = new List<CityData>();
            objSaleOffice.Areas = new List<Area>();
            objSaleOffice.Ranges = FOS.Setup.ManageRegion.GetRangeType();
            objSaleOffice.Designations = FOS.Setup.ManageRegion.GetDesignations();
            objSaleOffice.SOTypes = FOS.Setup.ManageRegion.GetSOTypes();
            return View(objSaleOffice);
        }

        //Insert Update Region Method...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUpdateSaleOfficer([Bind(Exclude = "TID,RegionalHead")] SaleOfficerData newSaleOfficer,string StartingDate2)
        {
            Boolean boolFlag = true;
            Boolean PhoneNumberFlag = true;
            ValidationResult results = new ValidationResult();
            try
            {

                if (newSaleOfficer != null)
                {

                    if (newSaleOfficer.ID == 0)
                    {
                        SaleOfficerValidator validator = new SaleOfficerValidator();
                        results = validator.Validate(newSaleOfficer);
                        boolFlag = results.IsValid;
                    }
                    boolFlag = true;

                    //if (newSaleOfficer.Phone1 != null)
                    //{
                    //    if (FOS.Web.UI.Common.NumberCheck.CheckSalesOfficerNumber1Exist(newSaleOfficer.ID, newSaleOfficer.Phone1 == null ? "" : newSaleOfficer.Phone1) == 1)
                    //    {
                    //        return Content("2");
                    //    }
                    //}

                    //if (newSaleOfficer.Phone2 != null)
                    //{
                    //    if (FOS.Web.UI.Common.NumberCheck.CheckSalesOfficerNumber2Exist(newSaleOfficer.ID, newSaleOfficer.Phone2 == null ? "" : newSaleOfficer.Phone2) == 1)
                    //    {
                    //        return Content("2");
                    //    }
                    //}

                    //if (newSaleOfficer.Phone1 != null && newSaleOfficer.Phone2 != null)
                    //{
                    //    if (FOS.Web.UI.Common.NumberCheck.CheckSalesOfficerNumberExist(newSaleOfficer.ID, newSaleOfficer.Phone1 == null ? "" : newSaleOfficer.Phone1, newSaleOfficer.Phone2 == null ? "" : newSaleOfficer.Phone2) == 1)
                    //    {
                    //        PhoneNumberFlag = false;
                    //    }
                    //}

                    if (PhoneNumberFlag)
                    {
                        if (boolFlag)
                        {
                            if (ManageSaleOffice.AddUpdateSaleOfficer(newSaleOfficer, StartingDate2))
                            {
                                return Content("1");
                            }
                            else
                            {
                                return Content("0");
                            }
                        }
                        else
                        {
                            IList<ValidationFailure> failures = results.Errors;
                            StringBuilder sb = new StringBuilder();
                            sb.Append(String.Format("{0}:{1}", "*** Error ***", "<br/>"));
                            foreach (ValidationFailure failer in results.Errors)
                            {
                                sb.AppendLine(String.Format("{0}:{1}{2}", failer.PropertyName, failer.ErrorMessage, "<br/>"));
                                Response.StatusCode = 422;
                                return Json(new { errors = sb.ToString() });
                            }
                        }
                    }
                    else
                    {
                        return Content("2");
                    }
                }

                return Content("0");
            }
            catch (Exception exp)
            {
                return Content("Exception : " + exp.Message);
            }
        }

        public JsonResult GetCityListByRegionHeadID(int ID)
        {
            var result = FOS.Setup.ManageCity.GetCityListByRegionHeadID(ID);
            return Json(result);
        }

        public JsonResult GetRegionalHeadAccordingToType(int RegionalHeadType)
        {
            var result = FOS.Setup.ManageSaleOffice.GetRegionalHeadAccordingToType(RegionalHeadType);
            return Json(result);
        }
        public JsonResult GetSORegions(int RegionalHeadType)
        {
            var result = FOS.Setup.ManageSaleOffice.GetRegionsofSO(RegionalHeadType);
        
            return Json(result);
        }

        public JsonResult GetSOByRegionalHeadId(int RegionalHeadId)
        {
            var result = FOS.Setup.ManageSaleOffice.GetSOByRegionalHeadId(RegionalHeadId);
            return Json(result);
        }
        

        public JsonResult GetRetailersBySOID(int soId)
        {
            var result = FOS.Setup.ManageRetailer.GetRetailerBySOID(soId);
            return Json(result);
        }
        public JsonResult GetAreaListByCityID(int ID)
        {
            var result = FOS.Setup.ManageArea.GetAreaListByCityID(ID);
            return Json(result);
        }

        public JsonResult GetAreaForSaleOfficerEdit(int ID, int CityID)
        {
            var result = FOS.Setup.ManageArea.GetAreaListByCityIDEdit(ID, CityID);
            return Json(result);
        }

        //Get All Region Method...
        public JsonResult DataHandler(DTParameters param , int RegionalHeadType , int RegionalHeadID)
        {
            try
            {
                var dtsource = new List<SaleOfficerData>();

                int regionalheadID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();

                if (regionalheadID == 0)
                {
                    dtsource = ManageSaleOffice.GetSaleOfficerListForGrid(RegionalHeadType , RegionalHeadID);
                }
                else {
                    RegionalHeadID = regionalheadID;
                    dtsource = ManageSaleOffice.GetSaleOfficerListForGrid(RegionalHeadType , RegionalHeadID);
                }

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

               

                List<SaleOfficerData> data = ManageSaleOffice.GetResult(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                foreach (var itm in data)
                {
                    if (itm.CreatedAT.HasValue)
                    {

                        itm.Createdate = Convert.ToDateTime(itm.CreatedAT).ToString("dd-MM-yyyy");
                        itm.leaveondate = Convert.ToDateTime(itm.Leaveon).ToString("dd-MM-yyyy");
                    }


                }

                int count = ManageSaleOffice.Count(param.Search.Value, dtsource, columnSearch);
                DTResult<SaleOfficerData> result = new DTResult<SaleOfficerData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }


        //Get All Region Method...
        public JsonResult GetAllData()
        {
            try
                
            {
                DateTime start1 = DateTime.UtcNow.AddHours(5);
                string cityName = "";
                DateTime start = start1.Date;
                DateTime final = start.AddDays(1);
                List<KPIData> list = new List<KPIData>();
                decimal? Val = 0;
                KPIData comlist;
                var SaleOfficerIDs = dbContext.SaleOfficers.Where(x => x.IsActive == true && x.IsDeleted == false && x.SORoleID==1).Select(x => x.ID).ToList();
                foreach (var item in SaleOfficerIDs)
                {
                    var marketstart = dbContext.SOAttendances.Where(x => x.SOID == item && x.CreatedAt >= start && x.CreatedAt <= final && x.Type == "Market start").Select(x=>x.CreatedAt).FirstOrDefault();
                    var CityID = dbContext.SOAttendances.Where(x => x.SOID == item && x.CreatedAt >= start && x.CreatedAt <= final && x.Type == "Market start").Select(x => x.CityID).FirstOrDefault();
                    var location= dbContext.SOAttendances.Where(x => x.SOID == item && x.CreatedAt >= start && x.CreatedAt <= final && x.Type == "Market start").Select(x => x.MarketStartLatlong).FirstOrDefault();
                    
                    var marketend = dbContext.SOAttendances.Where(x => x.SOID == item && x.CreatedAt >= start && x.CreatedAt <= final && x.Type == "Market close").Select(x => x.CreatedAt).FirstOrDefault();
                   var cityNames = dbContext.Cities.Where(x => x.ID == CityID).Select(x => x.Name).FirstOrDefault();
                    if (cityNames == null)
                    {
                         cityName = "NONE";
                    }
                    else
                    {
                        cityName = cityNames;
                    }
                   
                    var totalVisitsToday = dbContext.JobsDetails.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.Status == true).ToList();

                    if (totalVisitsToday != null)
                    {
                        comlist = new KPIData();
                        comlist.SoName = dbContext.SaleOfficers.Where(x => x.ID == item).Select(x => x.Name).FirstOrDefault();
                        comlist.totalVisits = totalVisitsToday.Count();
                        comlist.CityName = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.Retailer.City.Name).FirstOrDefault();
                        comlist.StartDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.JobDate).FirstOrDefault();
                        comlist.EndDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();
                        comlist.ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.VisitPurpose == "Ordering" && x.Status == true
                        ).Select(x => x.ID).Count();
                        comlist.Locations = location;
                        comlist.NonProductive = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.VisitPurpose == "FollowupVisit" && x.Status == true
                         ).Select(x => x.ID).Count();

                        TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                        comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);
                        var total = dbContext.sp_BrandAndItemWiseReport(start, final, 0, item, 0, 0, 6).ToList();

                        foreach (var items in total)
                        {
                            Val += items.TotalQuantity;
                        }
                        comlist.totalSale = Val;

                        comlist.AttendanceStart = marketstart;
                        comlist.AttendanceEnd = marketend;
                        comlist.CityName = cityName;
                        comlist.startstring = Convert.ToDateTime(comlist.StartDate).ToString("dd-MM-yyyy HH:mm:ss");
                        comlist.endstring = Convert.ToDateTime(comlist.EndDate).ToString("dd-MM-yyyy HH:mm:ss");
                        comlist.Attstartstring = Convert.ToDateTime(marketstart).ToString("dd-MM-yyyy HH:mm:ss");
                        comlist.Attendstring = Convert.ToDateTime(marketend).ToString("dd-MM-yyyy HH:mm:ss");
                        list.Add(comlist);

                        Val = 0;
                        cityName = "";
                    }
                }

                return Json(list);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public JsonResult GetAllDataPrevious()
        {
            try
            {
                DateTime start1 = DateTime.Today.AddDays(-1);
                var cityName = "";
                DateTime start = start1.Date;
                DateTime final = start.AddDays(1);
                List<KPIData> list = new List<KPIData>();
                decimal? Val = 0;
                KPIData comlist;
                var SaleOfficerIDs = dbContext.SaleOfficers.Where(x => x.IsActive == true && x.IsDeleted == false && x.SORoleID == 1).Select(x => x.ID).ToList();
                foreach (var item in SaleOfficerIDs)
                {
                    var marketstart = dbContext.SOAttendances.Where(x => x.SOID == item && x.CreatedAt >= start && x.CreatedAt <= final && x.Type == "Market start").Select(x => x.CreatedAt).FirstOrDefault();
                    var CityID = dbContext.SOAttendances.Where(x => x.SOID == item && x.CreatedAt >= start && x.CreatedAt <= final && x.Type == "Market start").Select(x => x.CityID).FirstOrDefault();
                    var location = dbContext.SOAttendances.Where(x => x.SOID == item && x.CreatedAt >= start && x.CreatedAt <= final && x.Type == "Market start").Select(x => x.MarketStartLatlong).FirstOrDefault();

                    var marketend = dbContext.SOAttendances.Where(x => x.SOID == item && x.CreatedAt >= start && x.CreatedAt <= final && x.Type == "Market close").Select(x => x.CreatedAt).FirstOrDefault();
                    var cityNames = dbContext.Cities.Where(x => x.ID == CityID).Select(x => x.Name).FirstOrDefault();
                    if (cityNames == null)
                    {
                        cityName = "NONE";
                    }
                    else
                    {
                        cityName = cityNames;
                    }
                    var totalVisitsToday = dbContext.JobsDetails.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.Status == true).ToList();

                    if (totalVisitsToday != null)
                    {
                        comlist = new KPIData();
                        comlist.SoName = dbContext.SaleOfficers.Where(x => x.ID == item).Select(x => x.Name).FirstOrDefault();
                        comlist.totalVisits = totalVisitsToday.Count();
                        comlist.CityName = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.Retailer.City.Name).FirstOrDefault();
                        comlist.StartDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).Select(x => x.JobDate).FirstOrDefault();
                        comlist.EndDate = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final).OrderByDescending(x => x.ID).Select(x => x.JobDate).FirstOrDefault();
                        comlist.ProductiveShops = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.VisitPurpose == "Ordering" && x.Status == true
                        ).Select(x => x.ID).Count();
                        comlist.Locations = location;
                        comlist.NonProductive = totalVisitsToday.Where(x => x.SalesOficerID == item && x.JobDate >= start && x.JobDate <= final && x.VisitPurpose == "FollowupVisit" && x.Status == true
                         ).Select(x => x.ID).Count();

                        TimeSpan? diff = comlist.EndDate - comlist.StartDate;

                        comlist.ElapseTime = string.Format("{0:%h} hours, {0:%m} minutes, {0:%s} seconds", diff);
                        var total = dbContext.sp_BrandAndItemWiseReport(start, final, 0, item, 0, 0, 6).ToList();

                        foreach (var items in total)
                        {
                            Val += items.TotalQuantity;
                        }
                        comlist.totalSale = Val;

                        comlist.AttendanceStart = marketstart;
                        comlist.AttendanceEnd = marketend;
                        comlist.CityName = cityName;

                        comlist.startstring = Convert.ToDateTime(comlist.StartDate).ToString("dd-MM-yyyy HH:mm:ss");
                        comlist.endstring = Convert.ToDateTime(comlist.EndDate).ToString("dd-MM-yyyy HH:mm:ss");

                        comlist.Attstartstring = Convert.ToDateTime(marketstart).ToString("dd-MM-yyyy HH:mm:ss");
                        comlist.Attendstring = Convert.ToDateTime(marketend).ToString("dd-MM-yyyy HH:mm:ss");
                        list.Add(comlist);

                        Val = 0;
                        cityName = "";
                    }
                }

                return Json(list);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        //Delete Region Method...
        public int DeleteSaleOfficer(int saleOfficerID)
        {
            return ManageSaleOffice.DeleteSaleOfficer(saleOfficerID);
        }

        #endregion Sale Officer
    
    }
}