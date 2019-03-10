using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class EmpleadosController : Controller
    {
        private Context db = new Context();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleado = db.Empleado.Include(e => e.Genero);
            return View(empleado.ToList());

        }
        
        public ActionResult Prueba()
        {
            List<Genero> generos = db.Genero.ToList();
            ViewBag.ListadeGenero = new SelectList(generos, "IdGenero", "Nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Index(string txtbuscar)
        {
            var listar = db.Empleado;
            var query = (from p in listar where p.Nombre.Contains(txtbuscar) select p);
            return View(query.ToList());
        }
        public JsonResult GetEmpleadoList()
        {
            List<EmpleadoViewModel> EmpList = db.Empleado.Where(x => x.IsDeleted == false).Select(x => new EmpleadoViewModel
            {
                IdEmpleado = x.IdEmpleado,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Dui = x.Dui,
                Edad = x.Edad,
                Telefono = x.Telefono,
                Direccion = x.Direccion,
                Mail = x.Mail,
                Cargo = x.Cargo,
                Salario = x.Salario,
                Estado = x.Estado,
                Genero = x.Genero.Nombre
            }).ToList();

            return Json(EmpList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmpleadoById(int IdEmpleado)
        {
            Empleado model = db.Empleado.Where(x => x.IdEmpleado == IdEmpleado).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(EmpleadoViewModel model)
        {
            var result = false;
            try
            {
                if (model.IdEmpleado > 0)
                {
                    Empleado Emp = db.Empleado.SingleOrDefault(x => x.IsDeleted == false && x.IdEmpleado == model.IdEmpleado);
                    Emp.Nombre = model.Nombre;
                    Emp.Apellido = model.Apellido;
                    Emp.Dui = model.Dui;
                    Emp.Edad = model.Edad;
                    Emp.Telefono = model.Telefono;
                    Emp.Direccion = model.Direccion;
                    Emp.Mail = model.Mail;
                    Emp.Cargo = model.Cargo;
                    Emp.Salario = model.Salario;
                    Emp.Estado = model.Estado;
                    Emp.IdGenero = model.IdGenero;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Empleado Emp = new Empleado();
                    Emp.Nombre = model.Nombre;
                    Emp.Apellido = model.Apellido;
                    Emp.Dui = model.Dui;
                    Emp.Edad = model.Edad;
                    Emp.Telefono = model.Telefono;
                    Emp.Direccion = model.Direccion;
                    Emp.Mail = model.Mail;
                    Emp.Cargo = model.Cargo;
                    Emp.Salario = model.Salario;
                    Emp.IdGenero = model.IdGenero;
                    Emp.Estado = model.Estado;
                    Emp.IsDeleted = false;
                    db.Empleado.Add(Emp);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEmpleadoRecord(int IdEmpleado)
        {
            bool result = false;
            Empleado Emp = db.Empleado.SingleOrDefault(x => x.IsDeleted == false && x.IdEmpleado == IdEmpleado);
            if (Emp != null)
            {
                Emp.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.IdGenero = new SelectList(db.Genero, "IdGenero", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmpleado,Nombre,Apellido,Dui,Edad,Telefono,Direccion,Mail,Cargo,Salario,Estado,IdGenero")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGenero = new SelectList(db.Genero, "IdGenero", "Nombre", empleado.IdGenero);
            return View(empleado);
        }

        //// GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGenero = new SelectList(db.Genero, "IdGenero", "Nombre", empleado.IdGenero);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmpleado,Nombre,Apellido,Dui,Edad,Telefono,Direccion,Mail,Cargo,Salario,Estado,IdGenero")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGenero = new SelectList(db.Genero, "IdGenero", "Nombre", empleado.IdGenero);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleado.Find(id);
            db.Empleado.Remove(empleado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

//// GET: Empleados/Details/5
//public ActionResult Details(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    Empleado empleado = db.Empleado.Find(id);
//    if (empleado == null)
//    {
//        return HttpNotFound();
//    }
//    return View(empleado);
//}

//// POST: Empleados/Create
//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Create([Bind(Include = "IdEmpleado,Nombre,Apellido,Dui,Edad,Telefono,Direccion,Mail,Cargo,Salario,Estado,IdGenero")] Empleado empleado)
//{
//    if (ModelState.IsValid)
//    {
//        db.Empleado.Add(empleado);
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    ViewBag.IdGenero = new SelectList(db.Genero, "IdGenero", "Nombre", empleado.IdGenero);
//    return View(empleado);
//}

//// GET: Empleados/Edit/5
//public ActionResult Edit(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    Empleado empleado = db.Empleado.Find(id);
//    if (empleado == null)
//    {
//        return HttpNotFound();
//    }
//    ViewBag.IdGenero = new SelectList(db.Genero, "IdGenero", "Nombre", empleado.IdGenero);
//    return View(empleado);
//}

//// POST: Empleados/Edit/5
//// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Edit([Bind(Include = "IdEmpleado,Nombre,Apellido,Dui,Edad,Telefono,Direccion,Mail,Cargo,Salario,Estado,IdGenero")] Empleado empleado)
//{
//    if (ModelState.IsValid)
//    {
//        db.Entry(empleado).State = EntityState.Modified;
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }
//    ViewBag.IdGenero = new SelectList(db.Genero, "IdGenero", "Nombre", empleado.IdGenero);
//    return View(empleado);
//}

//// GET: Empleados/Delete/5
//public ActionResult Delete(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    Empleado empleado = db.Empleado.Find(id);
//    if (empleado == null)
//    {
//        return HttpNotFound();
//    }
//    return View(empleado);
//}

//// POST: Empleados/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public ActionResult DeleteConfirmed(int id)
//{
//    Empleado empleado = db.Empleado.Find(id);
//    db.Empleado.Remove(empleado);
//    db.SaveChanges();
//    return RedirectToAction("Index");
//}

//protected override void Dispose(bool disposing)
//{
//    if (disposing)
//    {
//        db.Dispose();
//    }
//    base.Dispose(disposing);
//}
