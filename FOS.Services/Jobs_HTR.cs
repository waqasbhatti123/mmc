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
    using System.Collections.Generic;
    
    public partial class Jobs_HTR
    {
        public int HTRTID { get; set; }
        public int TID { get; set; }
        public int ID { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> RegionalHeadType { get; set; }
        public Nullable<int> RegionalHeadID { get; set; }
        public string VisitType { get; set; }
        public string RetailerType { get; set; }
        public Nullable<int> SaleOfficerID { get; set; }
        public Nullable<int> CityID { get; set; }
        public string Areas { get; set; }
        public bool Status { get; set; }
        public Nullable<int> VisitPlanType { get; set; }
        public string VisitPlanDays { get; set; }
        public string JobType { get; set; }
        public Nullable<System.DateTime> DateOfAssign { get; set; }
        public Nullable<System.DateTime> DateOfCompletion { get; set; }
        public Nullable<System.DateTime> StartingDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> LastProcessed { get; set; }
    }
}
