//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TakipProje.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Bakim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bakim()
        {
            this.BakimAciklama = new HashSet<BakimAciklama>();
        }
    
        public int ID { get; set; }
        public string BakimAdi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public System.DateTime BakimTarihi { get; set; }
        
        public string BakimYapanPersonel { get; set; }
        public Nullable<int> Periyot { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BakimAciklama> BakimAciklama { get; set; }
    }
}
