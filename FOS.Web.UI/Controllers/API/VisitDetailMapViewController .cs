using FOS.DataLayer;
using FOS.Setup;
using FOS.Shared;
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
    public class VisitDetailMapViewController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult Get(int SOID,string Date)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                List<VisitDetailMapDto> list = new List<VisitDetailMapDto>();

                VisitDetailMapDto comlist;
                List<VisitDetailMapDto> lists = new List<VisitDetailMapDto>();

                VisitDetailMapDto comlists;
                DateTime dtFromTodayUtc = Convert.ToDateTime(Date);

                DateTime dtFromToday = dtFromTodayUtc.Date;
                DateTime dtToToday = dtFromToday.AddDays(1);

                var RegionID = db.SOAttendances.Where(x => x.SOID == SOID && x.CreatedAt >= dtFromTodayUtc && x.CreatedAt <= dtToToday).FirstOrDefault();

                if (SOID > 0 && RegionID.RegionID > 0)
                {
                    object[] param = { SOID };
                  
                    var data = dbContext.Retailers.Where(x => x.RegionID == RegionID.RegionID && x.CityID == RegionID.CityID && x.IsDeleted==false).Select(x => new
                    {
                        ID = x.ID,
                        CustomerName=x.ShopName,
                        Latitude=x.Latitude,
                        Longitude=x.Longitude,
                        VisitPurpose= "NULL",
                        VisitDate=""

                    }).ToList();



                        var result = dbContext.Sp_MyVisitsMapViewForGPC1_4(SOID,dtFromToday,dtToToday,RegionID.RegionID,RegionID.CityID).ToList();

                    foreach (var item in result)

                    {
                        comlist = new VisitDetailMapDto();
                        comlist.CustomerName = item.CustomerName;
                        comlist.Lattitude = item.Lattitude;
                        comlist.Longitude = item.Longitude;
                        comlist.VisitPurpose = item.VisitPurpose;
                        comlist.VisitDate = item.VisitDate;

                        list.Add(comlist);
                    }

                    foreach (var items in data)
                    {
                        DateTime? dat = null;

                        string val = null;
                        comlist = new VisitDetailMapDto();
                        comlist.CustomerName = items.CustomerName;
                        comlist.Lattitude = items.Latitude;
                        comlist.Longitude = items.Longitude;
                        comlist.VisitPurpose = val;
                        comlist.VisitDate = dat;

                        list.Add(comlist);
                    }


                    var FinalList= (from mci in list select mci).Distinct().ToList();

                    var DistinctItems = list.GroupBy(x => x.CustomerName).Select(y => y.First());

                    foreach (var item in DistinctItems)
                    {
                       

                        comlists = new VisitDetailMapDto();
                        comlists.CustomerName = item.CustomerName;
                        comlists.Lattitude = item.Lattitude;
                        comlists.Longitude = item.Longitude;
                        comlists.VisitPurpose = item.VisitPurpose;
                        comlists.VisitDate = item.VisitDate;
                        lists.Add(comlists);
                    }

                    if (lists != null)
                    {
                        return Ok(new
                        {
                            MyVisitsMapView = lists

                        });
                    }
                  
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "MyVisitsMapView  API Failed");
            }
            object[] paramm = {  };
            return Ok(new
            {
                MyVisitsMapView = paramm
            });

        }


    }
}