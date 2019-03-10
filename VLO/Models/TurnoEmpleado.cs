using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLO.Models
{
    public class TurnoEmpleado
    {
        public int IdAsignacion { get; set; }
        public int IdTurno { get; set; }
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public string Fecha { get; set; }
        public string NombreEmpleado { get; set; }
        public string Apellido { get; set; }
        public string Dui { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public bool Estado { get; set; }
        public int IdGenero { get; set; }
    }
}