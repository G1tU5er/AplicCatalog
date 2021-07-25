using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class ProfesorIndexData
    {

        public IEnumerable<Profesor> Profesori { get; set; }
        public IEnumerable<Materie> Materii { get; set; }
    }
}