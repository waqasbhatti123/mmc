//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FOS.DataLayer
{
    using System;
    
    public partial class sp_GetKPISummarySOWise_Result
    {
        public Nullable<int> ID { get; set; }
        public string RegionName { get; set; }
        public string SaleOfficerName { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<System.DateTime> DateofOrders { get; set; }
        public Nullable<int> TotalVisits { get; set; }
        public Nullable<int> ProductiveOrders { get; set; }
        public Nullable<int> NonProductive { get; set; }
        public Nullable<int> Productiveperage { get; set; }
        public Nullable<System.DateTime> MarketStart { get; set; }
        public Nullable<System.DateTime> MarketClose { get; set; }
    }
}
