using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class TurnoEmpleadoController : Controller
    {
        private Context db = new Context();
        // GET: TurnoEmpleado
        public ActionResult Index()
        {
            var query2 = (from c in db.AsignacionTurno
                          join b in db.Empleado on c.IdEmpleado equals b.IdEmpleado
                          join d in db.Turnos on c.IdTurno equals d.IdTurno
                          select new TurnoEmpleado()
                          {
                              NombreEmpleado = b.Nombre,
                              Apellido = b.Apellido,
                              IdAsignacion = c.IdAsignacion,
                              IdTurno = c.IdTurno,
                              IdEmpleado = c.IdEmpleado,
                              Nombre = d.Nombre,
                              HoraInicial = d.HoraInicial,
                              HoraFinal = d.HoraFinal,
                              Fecha = d.Fecha


                          }).ToList();


            return View(query2);
        }
    }
}