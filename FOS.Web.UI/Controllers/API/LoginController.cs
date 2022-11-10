using FOS.DataLayer;
using FOS.Setup;
using FOS.Web.UI.Common;
using FOS.Web.UI.Controllers.API;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace FOS.Web.UI.Controllers.API
{
    public class LoginController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public Result<LoginResponse> Post(LoginRequest inModel)
        {
            try
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                if (inModel.UserName != null && inModel.Password != null)
                {
                    var SO = db.SaleOfficers.Where(s => s.UserName.ToLower().Equals(inModel.UserName.ToLower()) && s.Password.ToLower().Equals(inModel.Password.ToLower())).FirstOrDefault();
                    
                    if (SO != null)
                    {
                        string Token = FOS.Web.UI.Common.Token.TokenAttribute.GenerateToken(inModel.UserName, inModel.Password);
                        Token tokenObj = new Token();

                        tokenObj.SalesOfficerID = SO.ID;
                        tokenObj.TokenName = Token;
                        tokenObj.TokenAssignDate = DateTime.Now;
                        db.Tokens.Add(tokenObj);
                        db.SaveChanges();

                        LoginResponse data = new LoginResponse
                        {
                            SOID = SO.ID,
                            Name = SO.Name,
                            RoleID = 66,
                            SORoleID = SO.SORoleID,
                            SORoleName = db.SOTypes.Where(x => x.ID == SO.SORoleID).Select(x => x.Name).FirstOrDefault(),
                            RegionalHeadID = SO.RegionalHeadID,
                            Token = Token,
                            RegionID = db.RegionalHeadRegions.Where(x => x.RegionHeadID == SO.RegionalHeadID).Select(x => x.RegionID).FirstOrDefault(),
                            Region = new CommonController().GetCities(SO.RegionalHeadID),
                            SORange = SO.RangeID,
                            RetailersRelatedtoSO = new CommonController().CustomersRrelatedToSoForCheckin(SO.ID),
                            DistributorRelatedtoSO = new CommonController().DistributorRrelatedToSoForCheckin(SO.ID),
                            MainCatg = new CommonController().MainCat((int)SO.RangeID),
                            RetailerClass = new CommonController().RetailerType(),
                            RetailerType = new CommonController().RetailerType1(),
                            SORegions = new CommonController().Soregions(SO.RegionalHeadID),
                            SOCities= new CommonController().SoCities(SO.RegionalHeadID),
                            DistributorList = new CommonController().SoDistributor(SO.RegionalHeadID),
                            ReportType = new CommonController().Reporttype(),
                            SalesOfficer = new CommonController().SalesOfficers(SO.RegionalHeadID, SO.ID),
                            SalesOfficerNames = new CommonController().SalesOfficersNames(SO.ID),
                            Followupreasons= new CommonController().FollowUp(),
                            NoSaleReason = new CommonController().NoSale(),
                            RsmDcoumentbeforevisit = new CommonController().RSMDoc(),
                            CompititorBrands = new CommonController().ComBrands(),
                            MMCItems = new CommonController().MMCItemsList(),
                            SOVisitTypes = new CommonController().SOVisittypes(),
                            Retailers =db.Retailers.Where(x=>x.IsActive==true).Count(),
                            Distributors= db.Dealers.Where(x => x.IsActive == true).Count(),
                            RetailersOrders= (from lm in db.JobsDetails
                                              where lm.JobDate >= startDate
                                              && lm.JobDate <= endDate
                                              && lm.JobType == "Retailer Order"
                                              select lm).Count(),

                            DistributorsOrders= (from lm in db.JobsDetails
                                                 where lm.JobDate >= startDate
                                                 && lm.JobDate <= endDate
                                                 && lm.JobType == "Distributor Order"
                                                 select lm).Count(),


                        };
                        DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);

                        DateTime dtFromToday = dtFromTodayUtc.Date;
                        DateTime dtToToday = dtFromToday.AddDays(1);
                        AccessLog Ac = new AccessLog();
                        if (SO != null)
                        {
                            
                            var result = db.AccessLogs.Any(x => x.SaleOfficerID == SO.ID && x.LoginDate >= dtFromToday && x.LoginDate <= dtFromTodayUtc);
                            if (result == false)
                            {

                                Ac.SaleOfficerID = SO.ID;

                                Ac.LoginDate = dtFromTodayUtc;
                                Ac.Status = 1;
                                db.AccessLogs.Add(Ac);
                                db.SaveChanges();
                            }
                            else { }
                        }

                        return new Result<LoginResponse>
                        {
                            Data = data,
                            Message = "Login successful",
                            ResultType = ResultType.Success,
                            Exception = null,
                            ValidationErrors = null
                        };
                    }
                    else
                    {
                        return new Result<LoginResponse>
                        {
                            Data = null,
                            Message = "Login is not successful",
                            ResultType = ResultType.Failure,
                            Exception = null,
                            ValidationErrors = null
                        };
                    }
                    
                }

                return new Result<LoginResponse>
                {
                    Data = new LoginResponse(),
                    Message = "Please provide username/password",
                    ResultType = ResultType.Failure,
                    Exception = null,
                    ValidationErrors = null
                };
            }
            catch (Exception ex)
            {
                return new Result<LoginResponse>
                {
                    Data = new LoginResponse(),
                    Message = "Something goes wrong!",
                    ResultType = ResultType.Exception,
                    Exception = ex,
                    ValidationErrors = null
                };
            }
        }



    }


    public class LoginResponse
    {
        public string Name { get; set; }
        public int SOID { get; set; }
        public int? SORoleID { get; set; }
        public string SORoleName { get; set; }
        public int? RegionalHeadID { get; set; }
        public String Token { get; set; }
        public int RoleID { get; set; }
        public int? SORange { get; set; }
        public int? RegionID { get; set; }
        public int Retailers { get; set; }
        public int RetailersOrders { get; set; }
        public int Distributors { get; set; }
        public int DistributorsOrders { get; set; }

        public List<City> Cities { get; set; }
        public List<Regions> Region { get; set; }
        public List<Customers> Customers { get; set; }
        public List<CustomersForCheckin> RetailersRelatedtoSO { get; set; }

        public List<CustomersForCheckin> DistributorRelatedtoSO { get; set; }
        //public List<int> Retailers { get; set; }
        public List<MainCategories> MainCatg { get; set; }
        public List<RetailerType> RetailerClass { get; set; }
        public List<RetailerType> SORegions { get; set; }
        public List<RetailerType> DistributorList { get; set; }
        public List<RetailerType> SOCities { get; set; }
        public List<RetailerType> Range { get; set; }
        public List<RetailerType> RetailerType { get; set; }
        public List<RetailerType> ReportType { get; set; }
        public List<AllSaleOfficers> SalesOfficer { get; set; }
        public List<AllSaleOfficers> SalesOfficerNames { get; set; }

        public List<City> Followupreasons { get; set; }
        public List<City> NoSaleReason { get; set; }
        public List<City> RsmDcoumentbeforevisit { get; set; }
        public List<City> CompititorBrands { get; set; }
        public List<MMCItems> MMCItems { get; set; }
        public List<City> SOVisitTypes { get; set; }
    }
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class MMCItems
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Quantity { get; set; }
        public int? Carton { get; set; }
        public int? Danda { get; set; }

        public string OrderQuantity { get; set; }
        public string StockQuantity { get; set; }
        public DateTime? OrderDate { get; set; }
        public int Packing { get; set; }
        public int? SortOn { get; set; }
    }

    public class MMCFollowUpReasonDetail
    {
        
        public string ItemName { get; set; }
        public string OrderQuantity { get; set; }
        public string StockQuantity { get; set; }
      
        public DateTime? VisitDate { get; set; }
        public string ReasonForNoSale { get; set; }
        public int? ReasonForNoSaleID { get; set; }
    }

    public class Regions
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

  

    public class Customers
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class CustomersForCheckin
    {
        public int ID { get; set; }
        public string ShopName { get; set; }

        public bool ISActive { get; set; }

    }
    public class MainCategories
    {
        public int MainCategID { get; set; }
        public string MainCategDesc { get; set; }
    }

    public class RetailerType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


    public class AllSaleOfficers
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class AllRetailersRetailerToSO
    {
        public int? ID { get; set; }
       
        public string Name { get; set; }

    }
}