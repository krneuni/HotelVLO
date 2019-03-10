using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class TerminosCarne
    {
        [Key]
        public string IdTerminoCarne { get; set; }
        public string Termino { get; set; }
        public virtual List<Receta> Receta { get; set; }
    }
}