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
    
    public partial class RetailerPainter
    {
        public Nullable<int> SaleOfficerID { get; set; }
        public int RetailerID { get; set; }
        public int PainterID { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public string City { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Market { get; set; }
        public string POS { get; set; }
        public string PainterName { get; set; }
    
        public virtual Retailer Retailer { get; set; }
    }
}
