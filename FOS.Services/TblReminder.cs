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
    
    public partial class TblReminder
    {
        public int ReminderID { get; set; }
        public int JobID { get; set; }
        public int SaleOfficerID { get; set; }
        public int RetailerID { get; set; }
        public string ReminderDate { get; set; }
        public string Remarks { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    
        public virtual Job Job { get; set; }
        public virtual SaleOfficer SaleOfficer { get; set; }
        public virtual Retailer Retailer { get; set; }
    }
}
