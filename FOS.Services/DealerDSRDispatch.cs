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
    
    public partial class DealerDSRDispatch
    {
        public int ID { get; set; }
        public Nullable<int> JobID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> OrderQuantity { get; set; }
        public Nullable<int> DispatchQuantity { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public Nullable<int> SOID { get; set; }
        public string Slab { get; set; }
        public Nullable<decimal> Schemevalue { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> DelieveryboyID { get; set; }
    }
}
