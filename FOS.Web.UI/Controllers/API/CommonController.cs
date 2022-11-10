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
    public class CommonController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult GetCitiesList(int regionalHeadId) // now getting DEALERID 
        {
            try
            {
                //var dealerId = regionalHeadId; // now r getting cities on basis of DealerID
                //if (dealerId > 0)
                //{
                //    var retailerCityIds = ManageRetailer.GetDealerRetailerCityIdsList(regionalHeadId);
                //    var cities = ManageCity.GetCityListByDealerRetailerCityIds(retailerCityIds);
                //    if (cities != null && cities.Count > 0)
                //    {
                //        return Ok(new
                //        {
                //            Cities = cities.Where(s => s.IsActive).Select(d => new
                //            {
                //                d.ID,
                //                d.Name
                //            }).OrderBy(d => d.Name)
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Get Cities List API Failed");
            }

            return Ok(new
            {
                Cities = new { }
            });
        }

        public IHttpActionResult GetAreasList(int cityId)
        {
            try
            {
                if (cityId > 0)
                {
                    var areas = ManageArea.GetAreaListByCityID(cityId);
                    if (areas != null && areas.Count > 0)
                    {
                        return Ok(new
                        {
                            Areas = areas.Where(s => s.IsActive).Select(d => new
                            {
                                d.ID,
                                d.Name
                            }).OrderBy(d => d.Name)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Get Areas List API Failed");
            }

            return Ok(new
            {
                Areas = new { }
            });
        }


        public IHttpActionResult GetZonesList(int cityId)
        {
            try
            {
                if (cityId > 0)
                {
                    var zones = ManageZone.GetZonesByCityID(cityId);
                    if (zones != null && zones.Count > 0)
                    {
                        return Ok(new
                        {
                            Zones = zones.Select(d => new
                            {
                                d.ID,
                                d.Name
                            }).OrderBy(d => d.Name)
                        });
                    }
                }
                else
                {
                    var zones = ManageZone.GetZoneList();
                    if (zones != null && zones.Count > 0)
                    {
                        return Ok(new
                        {
                            Zones = zones.Select(d => new
                            {
                                d.ID,
                                d.Name
                            }).OrderBy(d => d.Name)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Get Zones by CITYID List API Failed");
            }

            return Ok(new
            {
                Zones = new { }
            });
        }


        [Route("api/FeesStruct")]
        [HttpGet]
        public List<FeeStructure> FeesStruct()
        {
            List<FeeStructure> cities = new List<FeeStructure>();
            FeeStructure fee;

            var dbFees = db.FeeStructures.Where(c => c.IsActive).ToList();

            foreach (var dbCty in dbFees)
            {
                fee = new FeeStructure();
                fee.FeeStructID = dbCty.FeeStructID;
                fee.FeeStructName = dbCty.FeeStructName;

                cities.Add(fee);
            }

            return cities;
        }


        [Route("api/GetCities")]
        [HttpGet]
        public List<Regions> GetCities(int? ID)
        {
            List<Regions> cities = new List<Regions>();
            //Regions cty;

            //var dbCities = db.RegionalHeadRegions.Where(c => c.RegionHeadID == ID).ToList();


            var dbregions = (from i in db.RegionalHeadRegions
                             join re in db.Regions on i.RegionID equals re.ID
                             where (i.RegionHeadID == ID)
                             select new Regions
                             {
                                 ID = re.ID,
                                 Name = re.Name
                             }).ToList();

            
           // var dbRegions=db.Regions.Where(x=>dbCities.Contains())

            //foreach (var dbCty in dbCities)
            //{
            //    cty = new Regions();
            //    cty.ID = dbCty.ID;
            //    cty.Name = dbCty.Name;

            //    cities.Add(cty);
            //}

            return dbregions;
        }


        [Route("api/SyllabusOffered")]
        [HttpGet]
        public List<Customers> SyllabusOffered()
        {
            List<Customers> Syllabus = new List<Customers>();
            Customers cty;

            var dbCities = db.Retailers.Where(c => c.IsActive).ToList();

            foreach (var dbCty in dbCities)
            {
                cty = new Customers();
                cty.ID = dbCty.ID;
                cty.Name = dbCty.Name;

                Syllabus.Add(cty);
            }

            return Syllabus;
        }


        [Route("api/CustomersRrelatedToSoForCheckin/{Id}")]
        [HttpGet]
        public List<CustomersForCheckin> CustomersRrelatedToSoForCheckin(int Id)
        {
            List<CustomersForCheckin> CustomerValidate = new List<CustomersForCheckin>();
            CustomersForCheckin cty;

            var dbCities = db.Retailers.Where(c => c.SaleOfficerID == Id && c.IsActive == true).ToList();

            foreach (var dbCty in dbCities)
            {
                cty = new CustomersForCheckin();
                cty.ID = dbCty.ID;
                cty.ShopName = dbCty.ShopName;
                cty.ISActive = dbCty.IsActive;

                CustomerValidate.Add(cty);
            }

            return CustomerValidate;
        }


        [Route("api/CustomersRrelatedToSoForCheckin/{Id}")]
        [HttpGet]
        public List<CustomersForCheckin> CustomersRrelatedRegionIDForCheckin(int Id, int RangeID,int CityID)
        {
            List<CustomersForCheckin> CustomerValidate = new List<CustomersForCheckin>();
            CustomersForCheckin cty;


            var rangeid = db.SaleOfficers.Where(x => x.ID == RangeID).Select(x => x.RangeID).FirstOrDefault();

            var dbCities = db.Retailers.Where(c => c.RegionID == Id && c.CityID==CityID && c.IsActive == true && c.RangeID== rangeid).ToList();

            foreach (var dbCty in dbCities)
            {
                cty = new CustomersForCheckin();
                cty.ID = dbCty.ID;
                cty.ShopName = dbCty.ID + "/" + " " + dbCty.ShopName;
                cty.ISActive = dbCty.IsActive;

                CustomerValidate.Add(cty);
            }

            return CustomerValidate;
        }


        public List<CustomersForCheckin> DistributorRrelatedToSoForCheckin(int Id)
        {
            List<CustomersForCheckin> CustomerValidate = new List<CustomersForCheckin>();
            CustomersForCheckin cty;

            var dbCities = db.Dealers.Where(c => c.SaleOfficerID == Id && c.IsActive == true).ToList();

            foreach (var dbCty in dbCities)
            {
                cty = new CustomersForCheckin();
                cty.ID = dbCty.ID;
                cty.ShopName = dbCty.ShopName;
                cty.ISActive = dbCty.IsActive;

                CustomerValidate.Add(cty);
            }

            return CustomerValidate;
        }





        [Route("api/GetMainCategory")]
        [HttpGet]
        public List<MainCategories> MainCat(int RangeID)
        {
            List<MainCategories> MAinCat = new List<MainCategories>();
            MainCategories cty;
            if (RangeID != 0)
            {
                var dbMainCat = db.MainCategories.Where(c => c.IsActive == true).ToList();

                foreach (var dbCty in dbMainCat)
                {
                    cty = new MainCategories();
                    cty.MainCategID = dbCty.MainCategID;
                    cty.MainCategDesc = dbCty.MainCategDesc;

                    MAinCat.Add(cty);
                }
            }
            else
            {
                var dbMainCat = db.MainCategories.Where(c => c.IsActive == true).ToList();

                foreach (var dbCty in dbMainCat)
                {
                    cty = new MainCategories();
                    cty.MainCategID = dbCty.MainCategID;
                    cty.MainCategDesc = dbCty.MainCategDesc;

                    MAinCat.Add(cty);
                }
            }
            return MAinCat;
        }



        public List<RetailerType> RetailerType()
        {
            List<RetailerType> MAinCat = new List<RetailerType>();
            RetailerType cty;

            var dbMainCat = db.Tbl_RetailerClass.Where(c => c.Status == true).ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new RetailerType();
                cty.ID = dbCty.ID;
                cty.Name = dbCty.RetailerClass;

                MAinCat.Add(cty);
            }

            return MAinCat;
        }




        public List<RetailerType> RetailerType1()
        {
            List<RetailerType> MAinCat = new List<RetailerType>();
            RetailerType cty;

            var dbMainCat = db.Tbl_RetailerType.Where(c => c.IsActive == true).ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new RetailerType();
                cty.ID = dbCty.ID;
                cty.Name = dbCty.RetailerType;

                MAinCat.Add(cty);
            }

            return MAinCat;
        }


        public List<RetailerType> Soregions(int? ID)
        {
            List<RetailerType> MAinCat = new List<RetailerType>();
            RetailerType cty;

            var dbMainCat = db.RegionalHeadRegions.Where(c => c.RegionHeadID == ID).ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new RetailerType();

                var name = db.Regions.Where(x => x.ID == dbCty.RegionID).FirstOrDefault();
                cty.ID = name.ID;
                cty.Name = name.Name;

                MAinCat.Add(cty);
            }

            return MAinCat;
        }

        public List<RetailerType> SoCities(int? ID)
        {
            List<RetailerType> MAinCat = new List<RetailerType>();
            RetailerType cty;

            var dbMainCat = db.RegionalHeadRegions.Where(c => c.RegionHeadID == ID).Select(c=>c.RegionID).FirstOrDefault();

            var cities = db.Cities.Where(x => x.RegionID == dbMainCat).Select(x => new RetailerType
            {
                ID=x.ID,
                Name=x.Name

            }).ToList();

            return cities;
        }

        public List<RetailerType> SoDistributor(int? ID)
        {
            List<RetailerType> MAinCat = new List<RetailerType>();
            RetailerType cty;

            var dbMainCat = db.RegionalHeadRegions.Where(c => c.RegionHeadID == ID).Select(c => c.RegionID).FirstOrDefault();

            var cities = db.Dealers.Where(x => x.RegionID == dbMainCat).Select(x => new RetailerType
            {
                ID = x.ID,
                Name = x.ShopName

            }).ToList();

            return cities;
        }
        public List<RetailerType> Reporttype()
        {
            List<RetailerType> MAinCat = new List<RetailerType>();
            RetailerType cty;

            var dbMainCat = db.ReportTypes.ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new RetailerType();
                cty.ID = dbCty.ID;
                cty.Name = dbCty.Name;

                MAinCat.Add(cty);
            }

            return MAinCat;
        }

        public List<AllSaleOfficers> SalesOfficersNames(int saleofficerID)
        {  
            List<AllSaleOfficers> MAinCat = new List<AllSaleOfficers>();
            AllSaleOfficers cty;
            List<AllSaleOfficers> list;


            if (saleofficerID > 0)
            {
                //string SOName = "";
                var dbMainCat = db.Tbl_Access.Where(c => c.RepotedUP == saleofficerID && c.Status == true).Select(c => new
                {
                    id = c.ReportedDown
                });

                foreach (var dbCty in dbMainCat)
                {
                    cty = new AllSaleOfficers();
                    cty.ID = dbCty.id;
                    cty.Name = db.SaleOfficers.Where(x => x.ID == dbCty.id && x.IsActive==true).Select(x => x.Name).FirstOrDefault();

                    MAinCat.Add(cty);
                }
             
               
            }
            else
            {

            }


            return MAinCat.OrderBy(x=>x.Name).ToList();
        }


        public List<City> FollowUp()
        {
            List<City> MAinCat = new List<City>();
            City cty;
            List<City> list;



            //string SOName = "";
            var dbMainCat = db.Tbl_FollowupReasons.Where(c => c.Status == true).ToList();

                foreach (var dbCty in dbMainCat)
                {
                    cty = new City();
                    cty.ID = dbCty.ID;
                   cty.Name = dbCty.Name;

                    MAinCat.Add(cty);
                }


           


            return MAinCat.OrderBy(x => x.Name).ToList();
        }

        public List<City> NoSale()
        {
            List<City> MAinCat = new List<City>();
            City cty;
            List<City> list;



            //string SOName = "";
            var dbMainCat = db.Tbl_NoSaleReason.Where(c => c.IsActive == true).ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new City();
                cty.ID = dbCty.ID;
                cty.Name = dbCty.Name;

                MAinCat.Add(cty);
            }





            return MAinCat.OrderBy(x => x.Name).ToList();
        }

        public List<City> RSMDoc()
        {
            List<City> MAinCat = new List<City>();
            City cty;
            List<City> list;



            //string SOName = "";
            var dbMainCat = db.Tbl_ChecklistBeforeVisit.Where(c => c.IsActive == true).ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new City();
                cty.ID = dbCty.ID;
                cty.Name = dbCty.Name;

                MAinCat.Add(cty);
            }





            return MAinCat.OrderBy(x => x.Name).ToList();
        }

        public List<City> ComBrands()
        {
            List<City> MAinCat = new List<City>();
            City cty;
            List<City> list;



            //string SOName = "";
            var dbMainCat = db.TBL_CompititorBrandsForDropDown.Where(c => c.IsActive == true).ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new City();
                cty.ID = dbCty.ID;
                cty.Name = dbCty.Name;

                MAinCat.Add(cty);
            }





            return MAinCat.OrderBy(x => x.ID).ToList();
        }


        public List<MMCItems> MMCItemsList()
        {
            List<MMCItems> MAinCat = new List<MMCItems>();
            MMCItems cty;
            List<MMCItems> list;



            //string SOName = "";
            var dbMainCat = db.Items.Where(c => c.IsActive == true).ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new MMCItems();
                cty.ID = dbCty.ItemID;
                cty.Name = dbCty.ItemName;
                cty.Packing = dbCty.Packing;
                cty.Price = dbCty.Price;
                cty.SortOn = dbCty.SortOrder;
                MAinCat.Add(cty);
            }





            return MAinCat.OrderBy(x => x.SortOn).ToList();
        }

        public List<City> SOVisittypes()
        {
            List<City> MAinCat = new List<City>();
            City cty;
            List<City> list;



            //string SOName = "";
            var dbMainCat = db.Tbl_VisittypesOfSO.Where(c => c.IsActive == true).ToList();

            foreach (var dbCty in dbMainCat)
            {
                cty = new City();
                cty.ID = dbCty.ID;
                cty.Name = dbCty.Name;

                MAinCat.Add(cty);
            }





            return MAinCat.OrderBy(x => x.Name).ToList();
        }
        public List<AllSaleOfficers> SalesOfficers(int? RegionalHeadID, int saleofficerID)
        {
            List<AllSaleOfficers> MAinCat = new List<AllSaleOfficers>();
            AllSaleOfficers cty;


            //if (RegionalHeadID == 5)
            //{
            //    if (saleofficerID == 23)
            //    {
            //        var dbMainCat = db.SaleOfficers.Where(c => c.IsActive == true).ToList();

            //        foreach (var dbCty in dbMainCat)
            //        {
            //            cty = new AllSaleOfficers();
            //            cty.ID = dbCty.ID;
            //            cty.Name = dbCty.Name;

            //            MAinCat.Add(cty);
            //        }
            //    }
            //    else
            //    {

            //    }

            //}

            if (RegionalHeadID == 4)
            {

                if (saleofficerID == 1)
                {
                    var dbMainCat2 = db.SaleOfficers.Where(c => c.RegionalHeadID == RegionalHeadID).ToList();

                    foreach (var dbCty in dbMainCat2)
                    {
                        cty = new AllSaleOfficers();
                        cty.ID = dbCty.ID;
                        cty.Name = dbCty.Name;

                        MAinCat.Add(cty);
                    }
                }
                else
                {
                    var dbMainCat2 = db.SaleOfficers.Where(c => c.ID == saleofficerID).ToList();

                    foreach (var dbCty in dbMainCat2)
                    {
                        cty = new AllSaleOfficers();
                        cty.ID = dbCty.ID;
                        cty.Name = dbCty.Name;

                        MAinCat.Add(cty);
                    }

                }
            }
            //else if (RegionalHeadID == 10)
            //{

            //    if (saleofficerID == 18)
            //    {
            //        var dbMainCat2 = db.SaleOfficers.Where(c => c.RegionalHeadID == RegionalHeadID).ToList();

            //        foreach (var dbCty in dbMainCat2)
            //        {
            //            cty = new AllSaleOfficers();
            //            cty.ID = dbCty.ID;
            //            cty.Name = dbCty.Name;

            //            MAinCat.Add(cty);
            //        }
            //    }
            //    else
            //    {
            //        var dbMainCat2 = db.SaleOfficers.Where(c => c.ID == saleofficerID).ToList();

            //        foreach (var dbCty in dbMainCat2)
            //        {
            //            cty = new AllSaleOfficers();
            //            cty.ID = dbCty.ID;
            //            cty.Name = dbCty.Name;

            //            MAinCat.Add(cty);
            //        }

            //    }
            //}
            //else
            //{
            //    var dbMainCat2 = db.SaleOfficers.Where(c => c.RegionalHeadID == RegionalHeadID).ToList();

            //    foreach (var dbCty in dbMainCat2)
            //    {
            //        cty = new AllSaleOfficers();
            //        cty.ID = dbCty.ID;
            //        cty.Name = dbCty.Name;

            //        MAinCat.Add(cty);
            //    }
            //}



            return MAinCat;
        }



    }
}