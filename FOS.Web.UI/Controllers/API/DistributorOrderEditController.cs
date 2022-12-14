using FOS.DataLayer;
using FOS.Web.UI.Common;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http;
using System.Web.Services.Protocols;

namespace FOS.Web.UI.Controllers.API
{
    public class DistributorOrderEditController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public Result<SuccessResponse> Post(DailyActivityRequest rm)
        {
            var JobObj = new Job();
            var JobDetail= new JobsDetail();
            var JobItem = new JobItem();

            try
            {
                var Result1 = db.JobItems.Where(x => x.JobID == rm.JobID).ToList();

                foreach (var item in Result1)
                {
                    db.JobItems.Remove(item);
                    db.SaveChanges();
                }

                var Result2 = db.JobsDetails.Where(x => x.JobID == rm.JobID).FirstOrDefault();
                db.JobsDetails.Remove(Result2);
                db.SaveChanges();


                var Result3 = db.Jobs.Where(x => x.ID == rm.JobID).FirstOrDefault();
                db.Jobs.Remove(Result3);
                db.SaveChanges();

                    JobObj.ID = db.Jobs.OrderByDescending(u => u.ID).Select(u => u.ID).FirstOrDefault() + 1;
                    DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
                    DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
                var regionalheadid = db.SaleOfficers.Where(x => x.ID == rm.SaleOfficerID).Select(x => x.RegionalHeadID).FirstOrDefault();
                JobObj.SaleOfficerID = rm.SaleOfficerID;

                      JobObj.RegionalHeadID = regionalheadid;
                   // JobObj.DistributorID = rm.ShopID;
             


                    JobObj.CreatedDate = localTime;
                   // JobOb = JobObj.SaleOfficerID;
                    JobObj.IsActive = true;
                    JobObj.IsDeleted = false;


                    JobDetail.JobID = JobObj.ID;
                    JobDetail.RegionalHeadID = regionalheadid;
                    JobDetail.SalesOficerID = JobObj.SaleOfficerID;
                    JobDetail.DealerID = rm.RetailerID;
                     JobDetail.RetailerID = 1;
                    JobDetail.JobDate = localTime;
                    JobDetail.JobType = "Distributor Order";
                    JobDetail.Status = true;
                    JobDetail.VisitPurpose = "Ordering";


                    db.Jobs.Add(JobObj);
                    db.SaveChanges();
                    db.JobsDetails.Add(JobDetail);
                    db.SaveChanges();

                        if (rm.StockItems != null && rm.StockItems.Count > 0)
                        {
                            foreach (var item in rm.StockItems)
                            {
                                db.JobItems.Add(
                                    new JobItem
                                    {
                                        JobID = JobObj.ID,
                                        ItemID = item.ItemID,
                                        Quantity = item.Quantity,
                                        Price = item.Price,
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedOn = localTime,
                                        CreatedBy = rm.SaleOfficerID,

                                    });
                                db.SaveChanges();
                            }
                        }


                return new Result<SuccessResponse>
                {
                    Data = null,
                    Message = "Distributor Order Edit Successfully",
                    ResultType = ResultType.Success,
                    Exception = null,
                    ValidationErrors = null
                };
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Distributor Order Edit Failed");
                return new Result<SuccessResponse>
                {
                    Data = null,
                    Message = "Distributor Order Edit Failed",
                    ResultType = ResultType.Exception,
                    Exception = ex,
                    ValidationErrors = null
                };
            }
        }

        public class SuccessResponse
        {

        }
        public class DailyActivityRequest
        {
            public DailyActivityRequest()
            {
                StockItems = new List<StockItemModel>();
            }
            public int JobID { get; set; }

            public int RetailerID { get; set; }
            public int SaleOfficerID { get; set; }
            public int RegionalHeadID { get; set; }
            public List<StockItemModel> StockItems { get; set; }

        }

        public class StockItemModel
        {
     
            public int ItemID { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }

        }
    }
}