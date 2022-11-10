using FOS.DataLayer;
using FOS.Setup;
using FOS.Shared;
using FOS.Web.UI.Common.CustomAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace FOS.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private FOSDataModel db = new FOSDataModel();
        private static int _regionalHeadID = 0;

        private static int RegionalheadID
        {
            get
            {
                if (_regionalHeadID == 0)
                {
                    _regionalHeadID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
                }

                return _regionalHeadID;
            }
        }

        [CustomAuthorize]
        public ActionResult Home(int? RangeID)
        {
            var userID = Convert.ToInt32(Session["UserID"]);
            var ranges = FOS.Setup.ManageRegion.GetRangesForAdmin(userID);
            var rangeid = ranges.FirstOrDefault();

            if (RangeID == null || RangeID == 0)
            {
                // ViewBag.rptid = "";
                ViewBag.retailers = db.Retailers.Where(x => x.IsActive == true ).Count();

                ViewBag.Distributors = db.Dealers.Where(x => x.IsActive == true).Count();


                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                var first = month.AddMonths(-1);
                var last = month.AddDays(-1);
                DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);

                DateTime dtFromToday = new DateTime(dtFromTodayUtc.Year, dtFromTodayUtc.Month, 1);
                DateTime dtToToday = dtFromToday.AddMonths(1).AddDays(-1);
                //Current Month Added Shops
                var CurrentMonthAddedShops = db.Sp_ShopAddedCount(dtFromToday, dtToToday).FirstOrDefault();
                DateTime fromdat = new DateTime(dtFromTodayUtc.Year, dtFromTodayUtc.Month, 1);
                DateTime dtFromToday1 = fromdat.AddMonths(-1);
                DateTime dtto = fromdat.AddDays(-1);
                //Pervious Month Added Shops
                var PreviousMonthAddedShops = db.Sp_ShopAddedCount(dtFromToday1, dtto).FirstOrDefault();

                // Last Month Sales
                var CurrentMonthRetailerSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.JobType == "Retailer Order").Count();



                // New Customers Today

                var CurrentMonthDistributorrSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.JobType == "Distributor Order").Count();


                //// Current Month Order Delievered
                var PreviousMonthRetailerDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.JobDate <= last && x.JobType == "Retailer Order").Count();




                var PreviousMonthDistributorDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.JobDate <= last && x.JobType == "Distributor Order").Count();




                //var ThisMonthSampleDelievered =     (from lm in db.JobsDetails
                //                                      join ji in db.JobItems on lm.JobID equals ji.JobID
                //                                      where lm.JobDate == DateTime.Today
                //                                      && lm.VisitPurpose == "Sampling"
                //                                      select ji.JobID).ToList();


                ViewBag.CurrentMonthAddedShops = CurrentMonthAddedShops;
                ViewBag.PreviousMonthAddedShops = PreviousMonthAddedShops;
                ViewBag.Lastmonthsale = CurrentMonthRetailerSale;
                ViewBag.ThisMonthSale = CurrentMonthDistributorrSale;
                ViewBag.ThisMonthSaleDone = PreviousMonthRetailerDelievered;
                ViewBag.PreviousMonthSaleDone = PreviousMonthDistributorDelievered;
                // ViewBag.TodaySaleDone = ThisMonthSampleDelievered.Count();
                ViewBag.SOPresentToday = Dashboard.SOPresenttoday().Count();
                ViewBag.SOAbsentToday = Dashboard.SOAbsenttoday().Count();
                ViewBag.FSPlanndeToday = Dashboard.FSPlannedtoday().Count();
                ViewBag.FSVisitedToday = Dashboard.FSVisitedtoday().Count();
                ViewBag.RSPlannedToday = Dashboard.RSPlannedToday().Count();
                ViewBag.RSVisitedToday = Dashboard.RSVisitedToday().Count();
                ManageRetailer objRetailers = new ManageRetailer();
                DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
                DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);




             
            }

            else
            {
                ViewBag.retailers = db.Retailers.Where(x => x.IsActive == true &&  x.RangeID==RangeID).Count();

                ViewBag.Distributors = db.Dealers.Where(x => x.IsActive == true &&  x.RangeID == RangeID).Count();

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                var first = month.AddMonths(-1);
                var last = month.AddDays(-1);
                DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);

                DateTime dtFromToday = new DateTime(dtFromTodayUtc.Year, dtFromTodayUtc.Month, 1);
                DateTime dtToToday = dtFromToday.AddMonths(1).AddDays(-1);
                //Current Month Added Shops
                var CurrentMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == RangeID  && x.LastUpdate >= dtFromToday && x.LastUpdate <= dtToToday).Count();
                DateTime fromdat = new DateTime(dtFromTodayUtc.Year, dtFromTodayUtc.Month, 1);
                DateTime dtFromToday1 = fromdat.AddMonths(-1);
                DateTime dtto = fromdat.AddDays(-1);
                //Pervious Month Added Shops
                var PreviousMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == RangeID  && x.LastUpdate >= dtFromToday1 && x.LastUpdate <= dtto).Count();

                // Last Month Sales
                var CurrentMonthRetailerSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.Retailer.RangeID==RangeID && x.JobType == "Retailer Order").Count();



                // New Customers Today

                var CurrentMonthDistributorrSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.Dealer.RangeID==RangeID && x.JobDate <= endDate && x.JobType == "Distributor Order").Count();


                //// Current Month Order Delievered
                var PreviousMonthRetailerDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.Retailer.RangeID == RangeID && x.JobDate <= last && x.JobType == "Retailer Order").Count();




                var PreviousMonthDistributorDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.Dealer.RangeID == RangeID && x.JobDate <= last && x.JobType == "Distributor Order").Count();




                //var ThisMonthSampleDelievered =     (from lm in db.JobsDetails
                //                                      join ji in db.JobItems on lm.JobID equals ji.JobID
                //                                      where lm.JobDate == DateTime.Today
                //                                      && lm.VisitPurpose == "Sampling"
                //                                      select ji.JobID).ToList();


                ViewBag.CurrentMonthAddedShops = CurrentMonthAddedShops;
                ViewBag.PreviousMonthAddedShops = PreviousMonthAddedShops;
                ViewBag.Lastmonthsale = CurrentMonthRetailerSale;
                ViewBag.ThisMonthSale = CurrentMonthDistributorrSale;
                ViewBag.ThisMonthSaleDone = PreviousMonthRetailerDelievered;
                ViewBag.PreviousMonthSaleDone = PreviousMonthDistributorDelievered;
                // ViewBag.TodaySaleDone = ThisMonthSampleDelievered.Count();
                ViewBag.SOPresentToday = Dashboard.SOPresenttodayForRange(RangeID).Count();
                ViewBag.SOAbsentToday = Dashboard.SOAbsenttoday().Count();
                ViewBag.FSPlanndeToday = Dashboard.FSPlannedtodayForRange(RangeID).Count();
                ViewBag.FSVisitedToday = Dashboard.FSVisitedtodayForRange(RangeID).Count();
                ViewBag.RSPlannedToday = Dashboard.RSPlannedTodayForRange(RangeID).Count();
                ViewBag.RSVisitedToday = Dashboard.RSVisitedTodayForRange(RangeID).Count();
                ManageRetailer objRetailers = new ManageRetailer();
                DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
                DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

                //  List<Sp_Top10CustomerVisitSOWise_Result> result = objRetailers.TopSales(localTime);
                List<Sp_Top10DistributorOrdersCityWiseGraphMonthly_Result> result1 = objRetailers.TopSales(startDate, endDate);
                List<Sp_SOVisitsTodayRangeWise_Result> SOVisits = objRetailers.SOVisitsTodayForAdmin(RangeID);
                List<Sp_SODistributorVisitsRangeWise_Result> DisVisits = objRetailers.DistributorVisitsTodayForAdmin(RangeID);
                List<Sp_Top10RetailersOrderCityWiseGraphMonthly_Result> citywise = objRetailers.TopSalesCityWise(startDate, endDate);
                List<Sp_Top30ItemsSoldMonthWise_Result> Items = objRetailers.Top30Items(startDate, endDate);
                // List<Sp_AbsentSOBarGraph_Result> PresentSO = objRetailers.TotalPresentSO();

                List<Sp_Top10OrderAmount_Result> AmountZoneWise1 = objRetailers.ToAmountZoneWise1();
                List<Sp_Top10OrderAmount_Result> AmountZoneWise2 = objRetailers.ToAmountZoneWise2();
                List<Sp_OrdersCurrentMonthQuantityWise_Result> ZoneWise1 = objRetailers.TopZoneWise1();
                List<Sp_OrdersCurrentMonthQuantityWise_Result> ZoneWise2 = objRetailers.TopZoneWise2();
                ViewBag.DataPoints = JsonConvert.SerializeObject(result1);
                ViewBag.DataPoints1 = JsonConvert.SerializeObject(citywise);
                // ViewBag.DataPoints2 = JsonConvert.SerializeObject(PresentSO);
                ViewBag.DataPoints3 = JsonConvert.SerializeObject(SOVisits);
                ViewBag.DataPoints4 = JsonConvert.SerializeObject(Items);
                ViewBag.DataPoints5 = JsonConvert.SerializeObject(AmountZoneWise1);
                ViewBag.DataPoints6 = JsonConvert.SerializeObject(AmountZoneWise2);
                ViewBag.DataPoints7 = JsonConvert.SerializeObject(ZoneWise1);
                ViewBag.DataPoints8 = JsonConvert.SerializeObject(ZoneWise2);
                ViewBag.DataPoints9 = JsonConvert.SerializeObject(DisVisits);
            }

            var objJob = new JobsData();
            objJob.Range = ranges;
            return View(objJob);
        }

        [CustomAuthorize]
        public ActionResult UserHome()
        {
            return View();
        }

        public JsonResult RetailerGraph()
        {
            RetailerGraphData result;
            if (RegionalheadID == 0)
            {
                result = FOS.Setup.Dashboard.RetailerGraph();
            }
            else
            {
                result = FOS.Setup.Dashboard.RetailerGraph(RegionalheadID);
            }
            return Json(result);
        }

        public JsonResult JobsGraph()
        {
            JobGraphData result;
            if (RegionalheadID == 0)
            {
                result = FOS.Setup.Dashboard.JobsGraph();
            }
            else
            {
                result = FOS.Setup.Dashboard.JobsGraph(RegionalheadID);
            }
            return Json(result);
        }

        public JsonResult CityGraph()
        {
            List<CityGraphData> result = FOS.Setup.Dashboard.CityGraph();
            return Json(result);
        }

        public JsonResult AreaGraph()
        {
            List<AreaGraphData> result = FOS.Setup.Dashboard.AreaGraph();
            return Json(result);
        }

        public JsonResult RegionalHeadGraph()
        {
            List<RegionalHeadGraphData> result = FOS.Setup.Dashboard.RegionalHeadGraph();
            return Json(result);
        }

        public int SalesOfficerGraph()
        {
            if (RegionalheadID == 0)
            {
                return FOS.Setup.Dashboard.SalesOfficerGraph();
            }
            else
            {
                return FOS.Setup.Dashboard.SalesOfficerGraph(RegionalheadID);
            }
        }

        public int DealerGraph()
        {
            return FOS.Setup.Dashboard.DealerGraph();
        }

        //public int GetCount()
        //{
        //    int count;
        //    var objRetailer = new RetailerData();
        //    if (RegionalheadID == 0)
        //    {
        //        count = FOS.Setup.ManageRetailer.GetPendingRetailerCountApproval();
        //    }
        //    else
        //    {
        //        count = FOS.Setup.ManageRetailer.GetPendingRetailerCountApproval(RegionalheadID);
        //    }
        //    return count;
        //}

        public int GetTotalRetailer()
        {
            int count;
            var objRetailer = new RetailerData();

            if (RegionalheadID == 0)
            {
                count = db.Retailers.Count();
            }
            else
            {
                count = db.Retailers.Where(r => r.SaleOfficer.RegionalHeadID == RegionalheadID).Count();
            }
            return count;
        }

        public int GetTotalJobs()
        {
            var objJobs = new JobsDetailData();
            int count;

            if (RegionalheadID == 0)
            {
                count = db.JobsDetails.Where(jd => jd.Job.IsDeleted == false).Count();
            }
            else
            {
                count = db.JobsDetails.Where(j => j.RegionalHeadID == RegionalheadID && j.Job.IsDeleted == false).Count();
            }
            return count;
        }

        public int GetTotalSalesOfficer()
        {
            int count;
            var objSalesOfficer = new SaleOfficerData();

            if (RegionalheadID == 0)
            {
                count = db.SaleOfficers.Count();
            }
            else
            {
                count = db.SaleOfficers.Where(s => s.RegionalHeadID == RegionalheadID).Count();
            }

            return count;
        }

        public JsonResult SoJobGraph()
        {
            List<SojobGraphData> result = FOS.Setup.Dashboard.SoJobGraph();
            return Json(result);
        }

        //public int GetCount()
        //{
        //    int count;
        //    var objRetailer = new RetailerData();

        //    count = FOS.Setup.ManageRetailer.GetDeletedRetailerCountApproval();

        //    return count;
        //}



        [CustomAuthorize]
     
        public ActionResult ManagersDashboard(int? RangeID, int? RegionalHeadID, int? ZoneID)
        {
           // var regionid = 0;
            ManageRetailer objRetailers = new ManageRetailer();
            var userID = Convert.ToInt32(Session["UserID"]);
            var ranges = FOS.Setup.ManageRegion.GetRangesRelatedToZSM(userID);
            var rangeid = ranges.FirstOrDefault();
            int RHID = FOS.Web.UI.Controllers.AdminPanelController.GetRegionalHeadIDRelatedToUser();
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            regionalHeadData = FOS.Setup.ManageRegionalHead.GetTerritorialRegionalHeadList(userID);
            var headid= regionalHeadData.FirstOrDefault();

            List<RegionalHeadData> ZoneData = new List<RegionalHeadData>();

            if (userID == 1024 || userID == 1055)
            {

                ZoneData = FOS.Setup.ManageRegionalHead.GetZonesListForDashboard();
            }
            else
            {
                ZoneData = FOS.Setup.ManageRegionalHead.GetZoneList(headid.ID);
            }

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


            if (RegionalHeadID != null && RegionalHeadID!=35 && RegionalHeadID!=5)
            {
               
                DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);

                DateTime dtFromTodayto = dtFromTodayUtc.Date;
                DateTime dtToTodayto = dtFromTodayto.AddDays(1);
                //regionid = db.RegionalHeadRegions.Where(x => x.RegionHeadID == RegionalHeadID).Select(x => x.RegionID).FirstOrDefault();
                ViewBag.retailers = db.Retailers.Where(x => x.IsActive == true &&  x.RangeID == RangeID && x.RegionID == ZoneID).Count();

                ViewBag.Distributors = db.Dealers.Where(x => x.IsActive == true &&  x.RangeID == RangeID && x.RegionID == ZoneID).Count();

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                var first = month.AddMonths(-1);
                var last = month.AddDays(-1);


                DateTime dtFromToday = new DateTime(dtFromTodayUtc.Year, dtFromTodayUtc.Month, 1);
                DateTime dtToToday = dtFromToday.AddMonths(1).AddDays(-1);
                //Current Month Added Shops
                var CurrentMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == RangeID && x.RegionID == ZoneID && x.LastUpdate >= dtFromToday && x.LastUpdate <= dtToToday).Count();
                DateTime fromdat = new DateTime(dtFromTodayUtc.Year, dtFromTodayUtc.Month, 1);
                DateTime dtFromToday1 = fromdat.AddMonths(-1);
                DateTime dtto = fromdat.AddDays(-1);
                //Pervious Month Added Shops
                var PreviousMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == RangeID && x.RegionID == ZoneID && x.LastUpdate >= dtFromToday1 && x.LastUpdate <= dtto).Count();

                // Last Month Sales
                var CurrentMonthRetailerSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.JobType == "Retailer Order" && x.Retailer.RegionID == ZoneID && x.Retailer.RangeID == RangeID).Count();



                // New Customers Today

                // var CurrentMonthDistributorrSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.JobType == "Distributor Order").Count();


                //// Current Month Order Delievered
                var PreviousMonthRetailerDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.JobDate <= last && x.JobType == "Retailer Order" && x.Retailer.RegionID == ZoneID && x.Retailer.RangeID == RangeID).Count();




                //var PreviousMonthDistributorDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.JobDate <= last && x.JobType == "Distributor Order").Count();




                //var ThisMonthSampleDelievered =     (from lm in db.JobsDetails
                //                                      join ji in db.JobItems on lm.JobID equals ji.JobID
                //                                      where lm.JobDate == DateTime.Today
                //                                      && lm.VisitPurpose == "Sampling"
                //                                      select ji.JobID).ToList();


                ViewBag.CurrentMonthAddedShops = CurrentMonthAddedShops;
                ViewBag.PreviousMonthAddedShops = PreviousMonthAddedShops;
                ViewBag.Lastmonthsale = CurrentMonthRetailerSale;
                //ViewBag.ThisMonthSale = CurrentMonthDistributorrSale;
                ViewBag.ThisMonthSaleDone = PreviousMonthRetailerDelievered;
                //ViewBag.PreviousMonthSaleDone = PreviousMonthDistributorDelievered;

                List<Sp_SOVisitsTodayForManagers_Result> SOVisits = objRetailers.SOVisitsTodayForManagers(RegionalHeadID);
             
                //ManageRetailer objRetailers = new ManageRetailer();
                DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
                DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

                //  List<Sp_Top10CustomerVisitSOWise_Result> result = objRetailers.TopSales(localTime);
                List<Sp_Top10DistributorOrdersCityWiseGraphMonthly_Result> result1 = objRetailers.TopSales(startDate, endDate);
                List<Sp_SODistributorVisitsForManagers_Result> DisVisits = objRetailers.DistributorVisitsTodayForManagers(RegionalHeadID);
                List<Sp_Top10RetailersOrderCityWiseGraphMonthly_Result> citywise = objRetailers.TopSalesCityWise(startDate, endDate);
                List<Sp_Top30ItemsSoldMonthWise_Result> Items = objRetailers.Top30Items(startDate, endDate);
                // List<Sp_AbsentSOBarGraph_Result> PresentSO = objRetailers.TotalPresentSO();

                List<Sp_Top10OrderAmountManagersDashboard_Result> AmountZoneWise1 = objRetailers.ToAmountZoneWise12(ZoneID, RangeID);
                List<Sp_Top10OrderAmountManagersDashboard_Result> AmountZoneWise2 = objRetailers.ToAmountZoneWise23(ZoneID, RangeID);
                List<Sp_OrdersCurrentMonthQuantityWiseManagersDashboard_Result> ZoneWise1 = objRetailers.TopZoneWise12(ZoneID, RangeID);
                List<Sp_OrdersCurrentMonthQuantityWiseManagersDashboard_Result> ZoneWise2 = objRetailers.TopZoneWise23(ZoneID, RangeID);
                ViewBag.DataPoints = JsonConvert.SerializeObject(result1);
                ViewBag.DataPoints1 = JsonConvert.SerializeObject(citywise);
                // ViewBag.DataPoints2 = JsonConvert.SerializeObject(PresentSO);
                ViewBag.DataPoints3 = JsonConvert.SerializeObject(SOVisits);
                ViewBag.DataPoints4 = JsonConvert.SerializeObject(Items);
                ViewBag.DataPoints5 = JsonConvert.SerializeObject(AmountZoneWise1);
                ViewBag.DataPoints6 = JsonConvert.SerializeObject(AmountZoneWise2);
                ViewBag.DataPoints7 = JsonConvert.SerializeObject(ZoneWise1);
                ViewBag.DataPoints8 = JsonConvert.SerializeObject(ZoneWise2);
                ViewBag.DataPoints9 = JsonConvert.SerializeObject(DisVisits);
            }



            //// ViewBag.TodaySaleDone = ThisMonthSampleDelievered.Count();
            //ViewBag.SOPresentToday = Dashboard.SOPresenttoday().Count();
            //ViewBag.SOAbsentToday = Dashboard.SOAbsenttoday().Count();
            //ViewBag.FSPlanndeToday = Dashboard.FSPlannedtoday().Count();
            //ViewBag.FSVisitedToday = Dashboard.FSVisitedtoday().Count();
            //ViewBag.RSPlannedToday = Dashboard.RSPlannedToday().Count();
            //ViewBag.RSVisitedToday = Dashboard.RSVisitedToday().Count();
            //ManageRetailer objRetailers = new ManageRetailer();
            //DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            //DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            //TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            //DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

            ////  List<Sp_Top10CustomerVisitSOWise_Result> result = objRetailers.TopSales(localTime);
            //List<Sp_Top10DistributorOrdersCityWiseGraphMonthly_Result> result1 = objRetailers.TopSales(startDate, endDate);
            //List<Sp_SOVisitsToday1_1_Result> SOVisits = objRetailers.SOVisitsToday();
            //List<Sp_Top10RetailersOrderCityWiseGraphMonthly_Result> citywise = objRetailers.TopSalesCityWise(startDate, endDate);
            //List<Sp_Top30ItemsSoldMonthWise_Result> Items = objRetailers.Top30Items(startDate, endDate);
            //// List<Sp_AbsentSOBarGraph_Result> PresentSO = objRetailers.TotalPresentSO();

            //List<Sp_Top10OrderAmount_Result> AmountZoneWise1 = objRetailers.ToAmountZoneWise1();
            //List<Sp_Top10OrderAmount_Result> AmountZoneWise2 = objRetailers.ToAmountZoneWise2();
            //List<Sp_OrdersCurrentMonthQuantityWise_Result> ZoneWise1 = objRetailers.TopZoneWise1();
            //List<Sp_OrdersCurrentMonthQuantityWise_Result> ZoneWise2 = objRetailers.TopZoneWise2();
            //ViewBag.DataPoints = JsonConvert.SerializeObject(result1);
            //ViewBag.DataPoints1 = JsonConvert.SerializeObject(citywise);
            //// ViewBag.DataPoints2 = JsonConvert.SerializeObject(PresentSO);
            //ViewBag.DataPoints3 = JsonConvert.SerializeObject(SOVisits);
            //ViewBag.DataPoints4 = JsonConvert.SerializeObject(Items);
            //ViewBag.DataPoints5 = JsonConvert.SerializeObject(AmountZoneWise1);
            //ViewBag.DataPoints6 = JsonConvert.SerializeObject(AmountZoneWise2);
            //ViewBag.DataPoints7 = JsonConvert.SerializeObject(ZoneWise1);
            //ViewBag.DataPoints8 = JsonConvert.SerializeObject(ZoneWise2);
            var objJob = new JobsData();
            objJob.RegionalHead = regionalHeadData;
            objJob.ZoneData = ZoneData;
            objJob.Range = ranges;
            return View(objJob);
            
        }


        [CustomAuthorize]

        public ActionResult DealersDashboard()
        {
            var CurrentMonthAddedShops = 0;
            var PreviousMonthAddedShops = 0;
            var CurrentMonthRetailerSale = 0;
            var PreviousMonthRetailerDelievered = 0;
            // var regionid = 0;
            ManageRetailer objRetailers = new ManageRetailer();
            var userID = Convert.ToInt32(Session["UserID"]);

            var DealerID = db.Users.Where(x => x.ID == userID).Select(x => x.DealerRefNo).FirstOrDefault();
            var dealerinfo = db.Dealers.Where(x => x.ID == DealerID).FirstOrDefault();


                DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);

                DateTime dtFromTodayto = dtFromTodayUtc.Date;
                DateTime dtToTodayto = dtFromTodayto.AddDays(1);
            //regionid = db.RegionalHeadRegions.Where(x => x.RegionHeadID == RegionalHeadID).Select(x => x.RegionID).FirstOrDefault();
            if (dealerinfo.RangeID == 6)
            {
                ViewBag.retailers = db.Retailers.Where(x => x.IsActive == true && x.RangeID == dealerinfo.RangeID && x.RangeADealer == dealerinfo.ID).Count();
            }
            else if(dealerinfo.RangeID == 7)
            {
                ViewBag.retailers = db.Retailers.Where(x => x.IsActive == true && x.RangeID == dealerinfo.RangeID && x.RangeBDealer == dealerinfo.ID).Count();
            }
            else
            {
                ViewBag.retailers = db.Retailers.Where(x => x.IsActive == true && x.RangeID == dealerinfo.RangeID && x.RangeCDealer == dealerinfo.ID).Count();
            }
                

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                var first = month.AddMonths(-1);
                var last = month.AddDays(-1);


                DateTime dtFromToday = new DateTime(dtFromTodayUtc.Year, dtFromTodayUtc.Month, 1);
                DateTime dtToToday = dtFromToday.AddMonths(1).AddDays(-1);
            DateTime fromdat = new DateTime(dtFromTodayUtc.Year, dtFromTodayUtc.Month, 1);
            DateTime dtFromToday1 = fromdat.AddMonths(-1);
            DateTime dtto = fromdat.AddDays(-1);
            //Current Month Added Shops
            if (dealerinfo.RangeID == 6)
            {
                 CurrentMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == dealerinfo.RangeID && x.RangeADealer == DealerID && x.LastUpdate >= dtFromToday && x.LastUpdate <= dtToToday).Count();
                 PreviousMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == dealerinfo.RangeID && x.RangeADealer == DealerID && x.LastUpdate >= dtFromToday1 && x.LastUpdate <= dtto).Count();
                 CurrentMonthRetailerSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.JobType == "Retailer Order" && x.Retailer.RangeADealer == DealerID && x.Retailer.RangeID == dealerinfo.RangeID).Count();
                 PreviousMonthRetailerDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.JobDate <= last && x.JobType == "Retailer Order" && x.Retailer.RangeADealer == DealerID && x.Retailer.RangeID == dealerinfo.RangeID).Count();
            }

           else if (dealerinfo.RangeID == 7)
            {
                CurrentMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == dealerinfo.RangeID && x.RangeBDealer == DealerID && x.LastUpdate >= dtFromToday && x.LastUpdate <= dtToToday).Count();
                PreviousMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == dealerinfo.RangeID && x.RangeBDealer == DealerID && x.LastUpdate >= dtFromToday1 && x.LastUpdate <= dtto).Count();
                CurrentMonthRetailerSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.JobType == "Retailer Order" && x.Retailer.RangeBDealer == DealerID && x.Retailer.RangeID == dealerinfo.RangeID).Count();
                PreviousMonthRetailerDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.JobDate <= last && x.JobType == "Retailer Order" && x.Retailer.RangeBDealer == DealerID && x.Retailer.RangeID == dealerinfo.RangeID).Count();
            }
            else
            {
                CurrentMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == dealerinfo.RangeID && x.RangeCDealer == DealerID && x.LastUpdate >= dtFromToday && x.LastUpdate <= dtToToday).Count();
                PreviousMonthAddedShops = db.Retailers.Where(x => x.IsActive == true && x.IsDeleted == false && x.RangeID == dealerinfo.RangeID && x.RangeCDealer == DealerID && x.LastUpdate >= dtFromToday1 && x.LastUpdate <= dtto).Count();
                CurrentMonthRetailerSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.JobType == "Retailer Order" && x.Retailer.RangeCDealer == DealerID && x.Retailer.RangeID == dealerinfo.RangeID).Count();
                PreviousMonthRetailerDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.JobDate <= last && x.JobType == "Retailer Order" && x.Retailer.RangeCDealer == DealerID && x.Retailer.RangeID == dealerinfo.RangeID).Count();
            }



            //Pervious Month Added Shops


            // Last Month Sales




            // New Customers Today

            // var CurrentMonthDistributorrSale = db.JobsDetails.Where(x => x.JobDate >= startDate && x.JobDate <= endDate && x.JobType == "Distributor Order").Count();


            //// Current Month Order Delievered





            ////var PreviousMonthDistributorDelievered = db.JobsDetails.Where(x => x.JobDate >= first && x.JobDate <= last && x.JobType == "Distributor Order").Count();




            ////var ThisMonthSampleDelievered =     (from lm in db.JobsDetails
            ////                                      join ji in db.JobItems on lm.JobID equals ji.JobID
            ////                                      where lm.JobDate == DateTime.Today
            ////                                      && lm.VisitPurpose == "Sampling"
            ////                                      select ji.JobID).ToList();


            ViewBag.CurrentMonthAddedShops = CurrentMonthAddedShops;
                ViewBag.PreviousMonthAddedShops = PreviousMonthAddedShops;
                ViewBag.Lastmonthsale = CurrentMonthRetailerSale;
                ////ViewBag.ThisMonthSale = CurrentMonthDistributorrSale;
                ViewBag.ThisMonthSaleDone = PreviousMonthRetailerDelievered;
                ////ViewBag.PreviousMonthSaleDone = PreviousMonthDistributorDelievered;

                //List<Sp_SOVisitsTodayForManagers_Result> SOVisits = objRetailers.SOVisitsTodayForManagers(RegionalHeadID);

                ////ManageRetailer objRetailers = new ManageRetailer();
                //DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
                //DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

                //TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                //DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

                ////  List<Sp_Top10CustomerVisitSOWise_Result> result = objRetailers.TopSales(localTime);
                //List<Sp_Top10DistributorOrdersCityWiseGraphMonthly_Result> result1 = objRetailers.TopSales(startDate, endDate);
                //List<Sp_SODistributorVisitsForManagers_Result> DisVisits = objRetailers.DistributorVisitsTodayForManagers(RegionalHeadID);
                //List<Sp_Top10RetailersOrderCityWiseGraphMonthly_Result> citywise = objRetailers.TopSalesCityWise(startDate, endDate);
                //List<Sp_Top30ItemsSoldMonthWise_Result> Items = objRetailers.Top30Items(startDate, endDate);
                //// List<Sp_AbsentSOBarGraph_Result> PresentSO = objRetailers.TotalPresentSO();

                //List<Sp_Top10OrderAmountManagersDashboard_Result> AmountZoneWise1 = objRetailers.ToAmountZoneWise12(ZoneID, RangeID);
                //List<Sp_Top10OrderAmountManagersDashboard_Result> AmountZoneWise2 = objRetailers.ToAmountZoneWise23(ZoneID, RangeID);
                //List<Sp_OrdersCurrentMonthQuantityWiseManagersDashboard_Result> ZoneWise1 = objRetailers.TopZoneWise12(ZoneID, RangeID);
                //List<Sp_OrdersCurrentMonthQuantityWiseManagersDashboard_Result> ZoneWise2 = objRetailers.TopZoneWise23(ZoneID, RangeID);
                //ViewBag.DataPoints = JsonConvert.SerializeObject(result1);
                //ViewBag.DataPoints1 = JsonConvert.SerializeObject(citywise);
                //// ViewBag.DataPoints2 = JsonConvert.SerializeObject(PresentSO);
                //ViewBag.DataPoints3 = JsonConvert.SerializeObject(SOVisits);
                //ViewBag.DataPoints4 = JsonConvert.SerializeObject(Items);
                //ViewBag.DataPoints5 = JsonConvert.SerializeObject(AmountZoneWise1);
                //ViewBag.DataPoints6 = JsonConvert.SerializeObject(AmountZoneWise2);
                //ViewBag.DataPoints7 = JsonConvert.SerializeObject(ZoneWise1);
                //ViewBag.DataPoints8 = JsonConvert.SerializeObject(ZoneWise2);
                //ViewBag.DataPoints9 = JsonConvert.SerializeObject(DisVisits);
          



            //// ViewBag.TodaySaleDone = ThisMonthSampleDelievered.Count();
            //ViewBag.SOPresentToday = Dashboard.SOPresenttoday().Count();
            //ViewBag.SOAbsentToday = Dashboard.SOAbsenttoday().Count();
            //ViewBag.FSPlanndeToday = Dashboard.FSPlannedtoday().Count();
            //ViewBag.FSVisitedToday = Dashboard.FSVisitedtoday().Count();
            //ViewBag.RSPlannedToday = Dashboard.RSPlannedToday().Count();
            //ViewBag.RSVisitedToday = Dashboard.RSVisitedToday().Count();
            //ManageRetailer objRetailers = new ManageRetailer();
            //DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            //DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            //TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            //DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

            ////  List<Sp_Top10CustomerVisitSOWise_Result> result = objRetailers.TopSales(localTime);
            //List<Sp_Top10DistributorOrdersCityWiseGraphMonthly_Result> result1 = objRetailers.TopSales(startDate, endDate);
            //List<Sp_SOVisitsToday1_1_Result> SOVisits = objRetailers.SOVisitsToday();
            //List<Sp_Top10RetailersOrderCityWiseGraphMonthly_Result> citywise = objRetailers.TopSalesCityWise(startDate, endDate);
            //List<Sp_Top30ItemsSoldMonthWise_Result> Items = objRetailers.Top30Items(startDate, endDate);
            //// List<Sp_AbsentSOBarGraph_Result> PresentSO = objRetailers.TotalPresentSO();

            //List<Sp_Top10OrderAmount_Result> AmountZoneWise1 = objRetailers.ToAmountZoneWise1();
            //List<Sp_Top10OrderAmount_Result> AmountZoneWise2 = objRetailers.ToAmountZoneWise2();
            //List<Sp_OrdersCurrentMonthQuantityWise_Result> ZoneWise1 = objRetailers.TopZoneWise1();
            //List<Sp_OrdersCurrentMonthQuantityWise_Result> ZoneWise2 = objRetailers.TopZoneWise2();
            //ViewBag.DataPoints = JsonConvert.SerializeObject(result1);
            //ViewBag.DataPoints1 = JsonConvert.SerializeObject(citywise);
            //// ViewBag.DataPoints2 = JsonConvert.SerializeObject(PresentSO);
            //ViewBag.DataPoints3 = JsonConvert.SerializeObject(SOVisits);
            //ViewBag.DataPoints4 = JsonConvert.SerializeObject(Items);
            //ViewBag.DataPoints5 = JsonConvert.SerializeObject(AmountZoneWise1);
            //ViewBag.DataPoints6 = JsonConvert.SerializeObject(AmountZoneWise2);
            //ViewBag.DataPoints7 = JsonConvert.SerializeObject(ZoneWise1);
            //ViewBag.DataPoints8 = JsonConvert.SerializeObject(ZoneWise2);
            var objJob = new JobsData();
            //objJob.RegionalHead = regionalHeadData;
            //objJob.ZoneData = ZoneData;
            //objJob.Range = ranges;
            return View(objJob);

        }



    }
}