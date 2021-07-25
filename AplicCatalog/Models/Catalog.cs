using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class Catalog
    {
        [Key]
        public int IdNota
        {
            get;
            set;
        }
        public int IdMaterie { get; set; }
        public virtual Materie Materie { get; set; }
        public int IdxIncadrare { get; set; }
        public virtual Incadrare Incadrare { get; set; }

        public int? Nota
        {
            get;
            set;
        }
        [Display(Name = "Data Nota")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNota
        {
            get;
            set;
        }

        [Display(Name = "Data Absenta")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataAbsenta
        {
            get;
            set;
        }

    
        public bool AbsentaMotivata
        {
            get;
            set;
        }

      
    }
}