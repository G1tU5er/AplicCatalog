using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicCatalog.Models
{
    public class AnScolar
    {
        public AnScolar() {
           
        }
        [Key]
        public int IdAn
        {
            get;
            set;
        }

        [Required]
        public int InceputAnScolar { get; set; }

        [Required]
        public int SfarsitAnScolar { get; set; }

        public virtual ICollection<PlanInvatamant> planuriScolare { get; set; }

    }
}