using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicCatalog.Models
{
    public class PlanInvatamant

    {
            
        public PlanInvatamant()
        {
            this.AnScolar = new AnScolar();
            this.Materii = new List<Materie>();
         
        }
        [Key]
        public int IdPlan
        {
            get;
            set;
        }

       
       // [Required]
        //de exemplu: Plan de invatamant pentru clasele a 9-a de profil mate-info
        public String Nume { get; set; }
      

        public  int IdAn { get; set; }
        public virtual AnScolar AnScolar { get; set; }

        public virtual ICollection<Materie> Materii { get; set; }
        public int NrClasa { get; set; }

    }
}