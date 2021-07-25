using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class IncadrareIndexData
    {
        public IEnumerable<Clasa> Clase { get; set; }
        public IEnumerable<Elev> Elevi { get; set; }
        public AnScolar anScolar { get; set; }
    }
}