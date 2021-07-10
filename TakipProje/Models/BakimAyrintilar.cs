using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakipProje.Models
{
    public class BakimAyrintilar
    {
        public IEnumerable<Bakim> bakim { get; set; }
        public IEnumerable<BakimAciklama> bakimAciklama { get; set; }
    }
}