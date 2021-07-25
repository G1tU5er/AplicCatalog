using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class Clasa
    {
        [Key]
        public int IdClasa
        {
            get;
            set;
        }

      
        [StringLength(50)]
        //exemplu: 9A, 10B etc
        public string NumeClasa
        {
            get;
            set;
        }
        //exemplu: Mate-Info intensiv engleza; Filologie; etc.....
        public String Descriere { get; set; }

        public int IdPlan { get; set; }
        public virtual PlanInvatamant PlanDeInvatamant { get; set; }

        //nota: aceasta proprietate nu genereaza camp specific in tabela Clasa, pentru ca anul scolar este luat din planul de invatamant
        public AnScolar AnScolar
        {
            get
            {
                return (PlanDeInvatamant== null? null: PlanDeInvatamant.AnScolar);
            }
        }
        public int NrClasa { get; set; }
    }
}