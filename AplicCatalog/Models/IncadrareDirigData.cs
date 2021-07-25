using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class IncadrareDirigData
    {
        public IEnumerable<Clasa> Clase { get; set; }
        public Profesor Profesor { get; set; }
        public List<int> ClaseCuDiriginte { get; set; } 
      
        public AnScolar AnScolar { get; set; }
       
    }
}