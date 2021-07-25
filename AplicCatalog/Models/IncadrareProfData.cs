using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class IncadrareProfData
    {
        public IEnumerable<Clasa> Clase { get; set; }
        public IEnumerable<Profesor> Profesori { get; set; }
        public List<int> listIdMaterie { get; set; }

        public AnScolar AnScolar { get; set; }
        public IEnumerable<Materie> Materii { get; set; }
    }
}