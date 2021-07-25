using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class CatalogIndexData
    {
        public IEnumerable<Clasa> Clase { get; set; }
        public IEnumerable<Elev> Elevi { get; set; }
        public AnScolar AnScolar { get; set; }
        public IEnumerable<Materie> Materii { get; set; }
        public IEnumerable<Catalog> Note { get; set; }
        public List<int> ListIdMateriiAsignate { get; set; }
        public List<IncadrareDiriginte> EDiriginte { get; set; }
    }
}