using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class IncadrareProf
    {
        [Key]
        public int IdxIncadrare
        {
            get;
            set;
        }
        public int IdProfesor { get; set; }
        public virtual Profesor Profesor
        {
            get;
            set;
        }
        public int idClasa { get; set; }
        public virtual Clasa Clasa
        {
            get;
            set;
        }
      
        public virtual AnScolar AnScolar
        {
            get
            {
                return Clasa.AnScolar;
            }
        }

        public int idMaterie { get; set; }
        public virtual Materie Materie
        {
            get;
            set;
        }
     
    }
}