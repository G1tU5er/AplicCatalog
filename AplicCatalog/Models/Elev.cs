using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicCatalog.Models
{
    public class Elev
    {
        [Key]
        public int IdElev
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

        [StringLength(50)]
        public string Prenume
        {
            get;
            set;
        }
    
      public string Mama { get; set; }
        public string Tata { get; set; }
     

    }
    }