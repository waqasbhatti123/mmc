﻿using FOS.DataLayer;
using FOS.Setup;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace FOS.Web.UI.Controllers.API
{
    public class GetRetailersRelatedToRegionController : ApiController
    {

        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult Get(int RegionID,int RangeID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                //List<RetailerData> MAinCat = new List<RetailerData>();
                if (RegionID > 0)
                {
                    object[] param = { RegionID };

                    // RetailerData cty;

                    var result = dbContext.Sp_GetRetailersRelatedtoRegion(RegionID).ToList();


                    if (result != null && result.Count > 0)
                    {
                        return Ok(new
                        {
                            Retailers = result

                        });
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "VisitDetailController GET API Failed");
            }
            object[] paramm = { };
            return Ok(new
            {
                Retailers = paramm
            });

        }


    }
}