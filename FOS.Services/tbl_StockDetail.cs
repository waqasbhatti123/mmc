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
    
    public partial class tbl_StockDetail
    {
        public int StockDetailID { get; set; }
        public int StockMasterID { get; set; }
        public int itemID { get; set; }
        public decimal Quantity { get; set; }
        public Nullable<System.DateTime> StockTakingTime { get; set; }
        public Nullable<int> Createdby { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> DealerID { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Tbl_MasterStock Tbl_MasterStock { get; set; }
    }
}
