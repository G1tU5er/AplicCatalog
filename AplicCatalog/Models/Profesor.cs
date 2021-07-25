using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class Profesor
    {
        public Profesor() {
            this.Materii = new List<Materie>();
        }
        public ApplicationUser user { get; set; }

        [Key]
        public int IdProfesor
        {
            get;
            set;
        }

        //[Required]
        [StringLength(50)]
        public string Nume
        {
            get;
            set;
        }

            
       public ICollection<Materie> Materii { get; set; }

      }
}