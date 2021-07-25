using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    //elevul X in anul scolar Y a fost in clasa Z
    public class Incadrare
    {
        [Key]
        public int IdxIncadrare
        {
            get;
            set;
        }
      public int IdElev { get; set; }
        public virtual Elev Elev
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
        public virtual ICollection<Catalog> Note { get; set; }
    }
}