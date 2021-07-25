using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class Materie
    {
        [Key]
        public int IdMaterie
        {
            get;
            set;
        }

       // [Required]
        [StringLength(50)]
        public string NumeMaterie
        {
            get;
            set;
        }

        //nu e obligatoriu ca aceasta proprietate sa fie prezenta; daca e, poate mentine eventuale comentarii/observatii/particularitati date in forma de text
        /// <summary>
        /// Obtine sau seteaza comentariu.
        /// </summary>
        /// <value>
        /// Ccomentariul pentru aceasta materie.
        /// </value>
        public String Comentariu { get; set; }
      
        public virtual ICollection<Catalog> Note { get; set; }
        public virtual ICollection<PlanInvatamant> planuriScolare { get; set; }
        public virtual ICollection<Profesor> Profesori { get; set; }

    }
}