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
    
    public partial class DealerRanx
    {
        public int ID { get; set; }
        public Nullable<int> DealerID { get; set; }
        public Nullable<int> RangeID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> AreaID { get; set; }
    
        public virtual Dealer Dealer { get; set; }
        public virtual MainCategory MainCategory { get; set; }
    }
}
