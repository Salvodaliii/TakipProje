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
    
    public partial class Lisans
    {
        public int ID { get; set; }
        public string ProgramAdi { get; set; }
        public Nullable<int> Adet { get; set; }
        public string FirmaAdi { get; set; }
        public string SabitTelefon { get; set; }
        public string Gsm { get; set; }
        public string Mail { get; set; }
        public Nullable<System.DateTime> AlimTarihi { get; set; }
        public Nullable<System.DateTime> BitisTarihi { get; set; }
        public Nullable<System.DateTime> YenilemeTarihi { get; set; }
        public Nullable<decimal> AlisFiyati { get; set; }
    }
}
