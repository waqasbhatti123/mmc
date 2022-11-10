﻿using FOS.DataLayer;
using FOS.Shared;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FOS.Setup
{
    public class ManageQrActivity
    {

        // Get SalesOfficer Area Names With Line Break ...
        public static string GetSaleOfficerAreaName(int intSaleOfficerID)
        {
            String strAreaName = String.Empty;
            FOSDataModel dbContext = new FOSDataModel();
            var objSaleOfficer = dbContext.SaleOfficers.Where(s => s.ID == intSaleOfficerID).FirstOrDefault();
            strAreaName = string.Join("<br/>", objSaleOfficer.Areas.Select(p => p.Name));

            return strAreaName;
        }

        // Get SalesOfficer Area ID With Comma Seperator ...
        public static string GetSaleOfficerAreaID(int intSaleOfficerID)
        {
            String strAreaName = String.Empty;
            FOSDataModel dbContext = new FOSDataModel();
            var objSaleOfficer = dbContext.SaleOfficers.Where(s => s.ID == intSaleOfficerID).FirstOrDefault();
            strAreaName = string.Join(",", objSaleOfficer.Areas.Select(p => p.ID));

            return strAreaName;
        }

        // Get All Regions Related To SalesOfficer ...
        public static List<RegionData> GetRegionRelatedToSaleOfficer(int SaleOfficerID, int RegionalHeadID)
        {
            List<RegionData> strRegionID = new List<RegionData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    var objSaleOfficer = dbContext.SaleOfficers.Where(s => s.ID == SaleOfficerID && s.RegionalHeadID == RegionalHeadID).FirstOrDefault();
                    strRegionID = objSaleOfficer.RegionalHead.RegionalHeadRegions
                        .Where(r => r.RegionHeadID == RegionalHeadID)
                        .Select(u => new RegionData
                                {
                                    RegionID = u.RegionID,
                                    Name = u.Region.Name
                                }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            

            return strRegionID;
        }


        // Get All SalesOfficer Related To CityID ...
        public static List<SaleOfficerData> GetSaleOfficerByRegionalHeadID(int RegionalHeadID)
        {
            List<SaleOfficerData> qrData = new List<SaleOfficerData>();

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    qrData = dbContext.SaleOfficers.Where(u => u.RegionalHeadID == RegionalHeadID && u.IsDeleted == false).ToList()
                            .Select(
                                u => new SaleOfficerData
                                {
                                    ID = u.ID,
                                    RegionalHeadID = u.RegionalHeadID,
                                    Name = u.Name,
                                    RegionalHeadName = u.RegionalHead.Name,
                                    Phone1 = u.Phone1 == null ? "" : u.Phone1,
                                    Phone2 = u.Phone2 == null ? "" : u.Phone2,
                                    CityName = u.City != null ? u.City.Name : "",
                                    CityID = u.CityID != null ? u.CityID : 0,
                                    AreaName = GetSaleOfficerAreaName(u.ID),
                                    AreaID = GetSaleOfficerAreaID(u.ID),
                                    LastUpdate = u.LastUpdate
                                }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return qrData;
        }

        // Get All SalesOfficers List For Grid ...
        public static List<SaleOfficerData> GetSaleOfficerList()
        {
            List<SaleOfficerData> qrData = new List<SaleOfficerData>();

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    qrData = dbContext.SaleOfficers.Where(u => u.IsDeleted == false).ToList()
                            .Select(
                                u => new SaleOfficerData
                                {
                                    ID = u.ID,
                                    RegionalHeadID = u.RegionalHeadID,
                                    Name = u.Name,
                                    UserName = u.UserName,
                                    Password = u.Password,
                                    RegionalHeadName = u.RegionalHead.Name,
                                    Phone1 = u.Phone1 == null ? "" : u.Phone1,
                                    Phone2 = u.Phone2 == null ? "" : u.Phone2,
                                    CityName = u.City != null ? u.City.Name : "",
                                    CityID = u.CityID != null ? u.CityID : 0,
                                    AreaName =  GetSaleOfficerAreaName(u.ID),
                                    AreaID = GetSaleOfficerAreaID(u.ID),
                                    LastUpdate = u.LastUpdate
                                }).ToList();
                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Get TODO List Failed");
                throw;
            }

            return qrData;
        }

        // Get All SalesOfficers List For Grid ...
        public static List<QrActivityData> GetQrActivityListForGrid(int RegionalHeadType , int RegionalHeadID)
        {
            List<QrActivityData> qrData = new List<QrActivityData>();

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    qrData = dbContext.QrActivities.ToList()
                            .Select(
                                u => new QrActivityData
                                {
                                    QrID = u.QrID,
                                    Title = u.Title,
                                    Detail = u.Detail,
                                    CityName = u.City != null ? u.City.Name : "",
                                    CityID = u.CityID,
                                    QrCode = u.QrCode,
                                    DueDateString = u.DueDate.HasValue ? u.DueDate.Value.ToString("dd/MM/yyyy") : "",
                                    Priority = u.Priority.ToString(),
                                    PriorityName = u.Priority.ToString(),
                                    Status = u.Status.ToString(),
                                    StatusName = u.Status.ToString(),
                                }).ToList();

                    foreach (var item in qrData)
                    {
                        if(int.Parse(item.PriorityName) == (int)PriorityEnum.Low)
                        {
                            item.PriorityName = PriorityEnum.Low.ToString();
                        }
                        else if (int.Parse(item.PriorityName) == (int)PriorityEnum.Medium)
                        {
                            item.PriorityName = PriorityEnum.Medium.ToString();
                        }
                        else if (int.Parse(item.PriorityName) == (int)PriorityEnum.High)
                        {
                            item.PriorityName = PriorityEnum.High.ToString();
                        }

                        if (int.Parse(item.StatusName) == (int)StatusEnum.Pending)
                        {
                            item.StatusName = StatusEnum.Pending.ToString();
                        }
                        else if (int.Parse(item.StatusName) == (int)StatusEnum.Completed)
                        {
                            item.StatusName = StatusEnum.Completed.ToString();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Get qr activity List Failed");
                throw;
            }

            return qrData;
        }

        public static List<QrSOData> GetQrSOListForGrid(string qrCode)
        {
            List<QrSOData> qrData = new List<QrSOData>();

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    qrData = dbContext.QrSODetails.Where(p=> p.QrCode.Equals(qrCode)).ToList()
                            .Select(
                                u => new QrSOData
                                {
                                    QrSoID = u.QrSoId,
                                    SaleOfficerId = u.SaleOfficerId,
                                    RetailerID = u.RetailerID,
                                    SaleOfficerName = u.SaleOfficer.Name,
                                    RetailerName = u.Retailer.ShopName,
                                    QrCode = u.QrCode,
                                    CreatedOnString = u.CreatedOn.ToString("dd-MMM-yyyy"),
                                    Remarks = u.Remarks
                                }).ToList();

                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Get QR SO List Failed");
                throw;
            }

            return qrData;
        }
        //Get All SalesOfficer List Method...
        public static List<SaleOfficer> GetAllSaleOfficerList()
        {
            List<SaleOfficer> qrData = new List<SaleOfficer>();
            using (FOSDataModel dbContext = new FOSDataModel())
            {
                qrData = dbContext.SaleOfficers.ToList();
            }
            return qrData;
        }

        //Get All SalesOfficer List Method...
        public static List<SaleOfficer> GetAllSaleOfficerListRelatedtoregionalHeadID(int RHID)
        {
            List<SaleOfficer> qrData = new List<SaleOfficer>();
            using (FOSDataModel dbContext = new FOSDataModel())
            {
                if (RHID == 0)
                {
                    qrData = dbContext.SaleOfficers.ToList();
                }
                else
                {
                    qrData = dbContext.SaleOfficers.Where(s => s.RegionalHeadID == RHID).ToList();
                }
                
            }
            return qrData;
        }

        //Insert OR Update SalesOfficers ...
        public static Boolean AddUpdateQrActivities(QrActivityData obj)
        {
            Boolean boolFlag = false;

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    QrActivity saleofficerObj = new QrActivity();

                    if (obj.QrID == 0)
                    {
                        //saleofficerObj.QrID = dbContext.QrActivities.OrderByDescending(u => u.QrID).Select(u => u.QrID).FirstOrDefault() + 1;
                        saleofficerObj.Title = obj.Title;
                        saleofficerObj.QrCode = obj.QrCode;
                        saleofficerObj.Detail = obj.Detail;
                        if (obj.CityID.HasValue)
                        {
                            saleofficerObj.CityID = obj.CityID.Value;
                        }
                        if (!string.IsNullOrEmpty(obj.DueDateString))
                        {
                            saleofficerObj.DueDate = DateTime.Parse(obj.DueDateString);
                        }
                        saleofficerObj.Priority = int.Parse(obj.Priority);
                        saleofficerObj.Status = int.Parse(obj.Status);

                        saleofficerObj.CreatedOn = DateTime.Now;
                        saleofficerObj.CreatedBy = 1;
                        saleofficerObj.ActivityType = 1;

                        dbContext.QrActivities.Add(saleofficerObj);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        saleofficerObj = dbContext.QrActivities.Where(u => u.QrID == obj.QrID).FirstOrDefault();
                        if (saleofficerObj != null)
                        {
                            saleofficerObj.Title = obj.Title;
                            saleofficerObj.QrCode = obj.QrCode;
                            saleofficerObj.Detail = obj.Detail;
                            if (obj.CityID.HasValue)
                            {
                                saleofficerObj.CityID = obj.CityID.Value;
                            }
                            if (!string.IsNullOrEmpty(obj.DueDateString))
                            {
                                saleofficerObj.DueDate = DateTime.Parse(obj.DueDateString);
                            }
                            saleofficerObj.Priority = int.Parse(obj.Priority);
                            saleofficerObj.Status = int.Parse(obj.Status);
                            saleofficerObj.UpdatedOn = DateTime.Now;
                            saleofficerObj.UpdatedBy = 1;
                        }
                        
                        dbContext.SaveChanges();
                    }

                    boolFlag = true;
                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Add QR Activity Failed");
                boolFlag = false;
            }
            return boolFlag;
        }

        // Delete DeleteQrActivity ...
        public static int DeleteQrActivity(int qrID)
        {
            int Resp = 0;

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    QrActivity qr = dbContext.QrActivities.Where(u => u.QrID == qrID).FirstOrDefault();
                    dbContext.QrActivities.Remove(qr);
                    //obj.IsDeleted = true;
                    dbContext.SaveChanges();
                }
            }
            catch(Exception exp)
            {
                Log.Instance.Error(exp, "Delete QR Activity Failed");
                Resp = 1;
            }
            return Resp;
        }

        public static List<QrSOData> GetResultSO(string search, string sortOrder, int start, int length, List<QrSOData> dtResult, List<string> columnFilters)
        {
            return FilterResultSO(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static List<QrActivityData> GetResult(string search, string sortOrder, int start, int length, List<QrActivityData> dtResult, List<string> columnFilters)
        {
            return FilterResult(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static int Count(string search, List<QrActivityData> dtResult, List<string> columnFilters)
        {
            return FilterResult(search, dtResult, columnFilters).Count();
        }

        public static int CountSO(string search, List<QrSOData> dtResult, List<string> columnFilters)
        {
            return FilterResultSO(search, dtResult, columnFilters).Count();
        }
        private static IQueryable<QrActivityData> FilterResult(string search, List<QrActivityData> dtResult, List<string> columnFilters)
        {
            IQueryable<QrActivityData> results = dtResult.AsQueryable();

            //results = results.Where(p => (search == null || (p.Name != null && p.Name.ToLower().Contains(search.ToLower()) || p.RegionalHeadName != null && p.RegionalHeadName.ToLower().Contains(search.ToLower()) || p.CityName != null && p.CityName.ToLower().Contains(search.ToLower()) || p.AreaName != null && p.AreaName.ToLower().Contains(search.ToLower()) || p.Phone1 != null && p.Phone1.ToLower().Contains(search.ToLower())
            //    || p.Phone2 != null && p.Phone2.ToLower().Contains(search.ToLower())))
            //    && (columnFilters[2] == null || (p.Name != null && p.Name.ToLower().Contains(columnFilters[3].ToLower())))
            //    && (columnFilters[3] == null || (p.RegionalHeadName != null && p.RegionalHeadName.ToLower().Contains(columnFilters[3].ToLower())))
            //     && (columnFilters[4] == null || (p.CityName != null && p.CityName.ToLower().Contains(columnFilters[3].ToLower())))
            //      && (columnFilters[5] == null || (p.AreaName != null && p.AreaName.ToLower().Contains(columnFilters[3].ToLower())))
            //    && (columnFilters[6] == null || (p.Phone1 != null && p.Phone1.ToLower().Contains(columnFilters[4].ToLower())))
            //    && (columnFilters[7] == null || (p.Phone2 != null && p.Phone2.ToLower().Contains(columnFilters[5].ToLower())))
            //    );

            return results;
        }

        private static IQueryable<QrSOData> FilterResultSO(string search, List<QrSOData> dtResult, List<string> columnFilters)
        {
            IQueryable<QrSOData> results = dtResult.AsQueryable();

            return results;
        }
        public static List<RegionalHeadData> GetRegionalHeadAccordingToType(int RegionalHeadType)
        {
            List<RegionalHeadData> regionalHeadData = new List<RegionalHeadData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    regionalHeadData = dbContext.RegionalHeads.Where(u => u.Type == RegionalHeadType && u.IsDeleted == false).ToList()
                            .Select(
                                u => new RegionalHeadData
                                {
                                    ID = u.ID,
                                    Name = u.Name,
                                }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return regionalHeadData;
        }



        public static List<SaleOfficerData> GetSaleOfficerListByRegionalHeadID(int RegionalHeadID)
        {
            List<SaleOfficerData> qrData = new List<SaleOfficerData>();

            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    qrData = dbContext.SaleOfficers.Where(u => u.RegionalHeadID == RegionalHeadID && u.IsDeleted == false).ToList()
                            .Select(
                                u => new SaleOfficerData
                                {
                                    ID = u.ID,
                                    Name = u.Name,
                                }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return qrData;
        }
    }
}