using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class ClasaIndexData
    {
        public IEnumerable<Clasa> clase { get; set; }
        public PlanInvatamant planInvatamant { get; set; }
        public AnScolar anScolar { get; set; }
    }
}