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
    
    public partial class Retailer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Retailer()
        {
            this.CompititorInfoes = new HashSet<CompititorInfo>();
            this.Complaints = new HashSet<Complaint>();
            this.JobsDetails = new HashSet<JobsDetail>();
            this.JobsHistories = new HashSet<JobsHistory>();
            this.QrSODetails = new HashSet<QrSODetail>();
            this.RetailerPainters = new HashSet<RetailerPainter>();
            this.SMSLogs = new HashSet<SMSLog>();
            this.Tbl_MasterStock = new HashSet<Tbl_MasterStock>();
            this.TblReminders = new HashSet<TblReminder>();
            this.DispatchInVans = new HashSet<DispatchInVan>();
        }
    
        public int TID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string RetailerCode { get; set; }
        public string CNIC { get; set; }
        public string AccountNo { get; set; }
        public string CardNumber { get; set; }
        public string BankName { get; set; }
        public Nullable<int> DealerID { get; set; }
        public int SaleOfficerID { get; set; }
        public string ShopName { get; set; }
        public string Location { get; set; }
        public string LocationName { get; set; }
        public string LocationMargin { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string LandLineNo { get; set; }
        public string Market { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> AreaID { get; set; }
        public string Address { get; set; }
        public string RetailerType { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string Type { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<int> ZoneID { get; set; }
        public Nullable<int> Source { get; set; }
        public Nullable<int> UpdateSource { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> Action { get; set; }
        public Nullable<bool> IsVerified { get; set; }
        public string BankName2 { get; set; }
        public string AccountNo2 { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string ContactPerson { get; set; }
        public string ContactCellNo { get; set; }
        public string EducationSystem { get; set; }
        public Nullable<int> NoOfBranches { get; set; }
        public Nullable<int> StudentStrength { get; set; }
        public Nullable<int> NoOfTeachers { get; set; }
        public Nullable<int> FeeStructID { get; set; }
        public string NewSessionStartDate { get; set; }
        public Nullable<int> CurrentSylabusID { get; set; }
        public Nullable<bool> BookShop { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string CustomeCode { get; set; }
        public string Remarks { get; set; }
        public string Email { get; set; }
        public string TypeOfShop { get; set; }
        public string ShopCategory { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> RetailerClass { get; set; }
        public Nullable<int> RetailerChannel { get; set; }
        public string NewArea { get; set; }
        public Nullable<int> RangeADealer { get; set; }
        public Nullable<int> RangeBDealer { get; set; }
        public Nullable<int> RangeCDealer { get; set; }
        public Nullable<int> RangeID { get; set; }
        public string Shoptype { get; set; }
        public string NewOrOld { get; set; }
        public Nullable<int> Quota { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual City City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompititorInfo> CompititorInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual Dealer Dealer { get; set; }
        public virtual FeeStructure FeeStructure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobsDetail> JobsDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobsHistory> JobsHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QrSODetail> QrSODetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RetailerPainter> RetailerPainters { get; set; }
        public virtual Retailer Retailers1 { get; set; }
        public virtual Retailer Retailer1 { get; set; }
        public virtual SaleOfficer SaleOfficer { get; set; }
        public virtual Sylabu Sylabu { get; set; }
        public virtual Zone Zone { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSLog> SMSLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_MasterStock> Tbl_MasterStock { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblReminder> TblReminders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DispatchInVan> DispatchInVans { get; set; }
    }
}