﻿using FOS.DataLayer;
using FOS.Web.UI.Common;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace FOS.Web.UI.Controllers.API
{
    public class RetailerEditController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public Result<SuccessResponse> Post(EditRetailermodel rm)
        {
            Retailer retailerObj = new Retailer();
            try
            {
               
                    //ADD New Retailer 
                    retailerObj = db.Retailers.Where(u => u.ID == rm.ID).FirstOrDefault();


                   // var data = db.Retailers.Where(x => x.RegionID == rm.RegionID && x.CityID == rm.CityID && x.Phone1 == rm.CellNo1).FirstOrDefault();

                retailerObj.Name = rm.OwnerName;
                retailerObj.SaleOfficerID = rm.SalesOfficerID;



                retailerObj.DealerID = rm.DistributorIDRangeA;


                retailerObj.ShopName = rm.ShopName;
                retailerObj.AreaID = rm.AreaID;
                retailerObj.CityID = rm.CityID;
                // Zone ID  is saving in Regions Table bcx in menu region changes to zone
                retailerObj.ZoneID = 1;
                retailerObj.RegionID = rm.RegionID;
            
                retailerObj.NewArea = rm.AreaName;
              
               
                retailerObj.Phone1 = rm.CellNo1;
                retailerObj.Phone2 = rm.CellNo2;
                retailerObj.Email = rm.Email;
                retailerObj.RetailerClass = rm.RetailerClass;
                retailerObj.RetailerChannel = rm.RetailerChannel;
                retailerObj.RangeID = 6;
              


                retailerObj.Remarks = rm.Remarks;
                retailerObj.IsActive = true;
                retailerObj.Status = true;
                retailerObj.RetailerType = "Retailer";
                retailerObj.IsDeleted = false;
                retailerObj.Address = rm.Address;
                retailerObj.Shoptype = rm.ShopType;
                retailerObj.Quota = rm.Quota;
                retailerObj.NewOrOld = rm.OldorNew;
             
                retailerObj.CreatedBy = rm.SalesOfficerID;

                //db.Retailers.Add(retailerObj);
                //END

                // Add Token Detail ...
             

                    return new Result<SuccessResponse>
                    {
                        Data = null,
                        Message = "Retailer Edit Successful",
                        ResultType = ResultType.Success,
                        Exception = null,
                        ValidationErrors = null
                    };
               
               

            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Retailer Edit API Failed");
                return new Result<SuccessResponse>
                {
                    Data = null,
                    Message = "Retailer Edit API Failed",
                    ResultType = ResultType.Exception,
                    Exception = ex,
                    ValidationErrors = null
                };
            }



        }


       

        public class SuccessResponse
        {

        }
        public class EditRetailermodel
        {
            public int ID { get; set; }
            public string ShopName { get; set; }
            public string OwnerName { get; set; }
            public string CellNo1 { get; set; }
            public string CellNo2 { get; set; }
            public int SalesOfficerID { get; set; }
            public string Email { get; set; }
            public string AreaName { get; set; }
            public int RegionID { get; set; }
            public int CityID { get; set; }
            public int ZoneID { get; set; }
            public int AreaID { get; set; }
            public string Address { get; set; }
            public string ShopType { get; set; }
            public int Quota { get; set; }
            public string OldorNew { get; set; }
            public int DistributorIDRangeA { get; set; }
            public int DistributorIDRangeB { get; set; }
            public int DistributorIDRangeC { get; set; }
            public string Token { get; set; }
            public int RetailerClass { get; set; }
            public int RetailerChannel { get; set; }
            public string Picture1 { get; set; }
         
            public string Remarks { get; set; }
            public bool IsVerified { get; set; }
        }

        }
    }
