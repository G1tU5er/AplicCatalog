using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class IncadrareMateriePlan
    {
        [Key]
        public int IdincadrareMat
        {
            get;
            set;
        }


    
        public int NrOre
        {
            get;
            set;
        }

        public int IdMaterie { get; set; }
        public virtual Materie Materie
        {
            get;
            set;
        }
        public int IdPlan { get; set; }
        public virtual PlanInvatamant PlanInvatamant
        {
            get;
            set;
        }
    }
}