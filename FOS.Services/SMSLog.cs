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
    
    public partial class SMSLog
    {
        public int SmsID { get; set; }
        public int SaleOfficerID { get; set; }
        public int RetailerID { get; set; }
        public string PhoneNo { get; set; }
        public string SmsPin { get; set; }
        public int Status { get; set; }
        public string ErrorDetail { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        public virtual SaleOfficer SaleOfficer { get; set; }
        public virtual Retailer Retailer { get; set; }
    }
}