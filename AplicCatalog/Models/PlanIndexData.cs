using AplicCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class PlanIndexData
    {
        public IEnumerable<PlanInvatamant> planuriScolare { get; set; }
        public IEnumerable<Materie> materii { get; set; }
        public Dictionary<int,int> IncadrariNrOreMaterii { get; set; }
        public AnScolar anScolar { get; set; }

    }
}