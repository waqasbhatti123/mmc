using FOS.DataLayer;
using FOS.Setup;
using FOS.Web.UI.Common;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace FOS.Web.UI.Controllers.API
{
    public class GetOrdersDeleteController : ApiController
    {

        FOSDataModel db = new FOSDataModel();

        public Result<SuccessResponse> Get(int JobID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
                {



                JobsDetail retailerObj = new JobsDetail();
                
                //ADD New Retailer 
                retailerObj = db.JobsDetails.Where(u => u.JobID == JobID).FirstOrDefault();
             
                    retailerObj.Status = false;

                    db.SaveChanges();

                return new Result<SuccessResponse>
                {
                    Data = null,
                    Message = "Deleted Successfully",
                    ResultType = ResultType.Success,
                    Exception = null,
                    ValidationErrors = null


                };
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Deleted API Failed");
                return new Result<SuccessResponse>
                {
                    Data = null,
                    Message = "Deleted API Failed",
                    ResultType = ResultType.Exception,
                    Exception = ex,
                    ValidationErrors = null
                };
            }
        

        }


    }
}