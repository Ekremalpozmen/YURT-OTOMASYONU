//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YURT_OTOMASYONU.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Odemeler
    {
        public int Id { get; set; }
        public Nullable<int> OgrenciId { get; set; }
        public Nullable<System.DateTime> OdemeTarihi { get; set; }
        public Nullable<int> Tutar { get; set; }
    
        public virtual Ogrenci Ogrenci { get; set; }
    }
}