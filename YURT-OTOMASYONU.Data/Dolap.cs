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
    
    public partial class Dolap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dolap()
        {
            this.Oda = new HashSet<Oda>();
        }
    
        public int Id { get; set; }
        public Nullable<int> DolapNo { get; set; }
        public Nullable<int> OgrenciId { get; set; }
        public Nullable<int> OdaId { get; set; }
        public Nullable<int> DolapId { get; set; }
    
        public virtual Ogrenci Ogrenci { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oda> Oda { get; set; }
    }
}
