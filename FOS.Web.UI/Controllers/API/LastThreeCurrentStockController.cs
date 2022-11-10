﻿using FOS.DataLayer;
using FOS.Setup;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace FOS.Web.UI.Controllers.API
{
    public class LastThreeCurrentStockController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult Get(int RetailerID)
        {
            List<Sp_LastthreeCurrentStock_Result> result = new List<Sp_LastthreeCurrentStock_Result>();
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                List<MMCItems> MAinCat = new List<MMCItems>();
                MMCItems cty;
                if (RetailerID > 0)
                {
                    object[] param = { RetailerID };

                    // var Reason = db.JobsDetails.Where(x => x.JobID == JobID).FirstOrDefault();

                    var IDS   = (from jobitem in db.JobsDetails
                                 where jobitem.RetailerID==RetailerID
                                          orderby jobitem.JobDate descending
                                          select jobitem).Take(3);



                    foreach (var item in IDS)
                    {
                        result = dbContext.Sp_LastthreeCurrentStock(item.JobID).ToList();

                        foreach (var items in result)
                        {
                            cty = new MMCItems();

                            cty.Name = items.ItemName;
                            cty.Carton = items.Carton;
                            cty.Danda = items.Danda;
                            cty.OrderDate = items.VisitDate;

                            MAinCat.Add(cty);

                        }

                      





                    }

                    if (MAinCat != null && MAinCat.Count > 0)
                    {
                        return Ok(new
                        {
                            LastThreeStock = MAinCat

                        });
                    }



                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "VisitDetailController GET API Failed");
            }
            object[] paramm = {};
            return Ok(new
            {
                LastThreeStock = paramm
            });

        }


    }
}