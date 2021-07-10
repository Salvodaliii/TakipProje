using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakipProje.Models
{
    public class BakimAyrinti
    {
        public IEnumerable<Bakim> bakim { get; set; }
        public IEnumerable<BakimAciklama> bakimNot { get; set; }
    }
}