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
    public class TerminosCarnesController : Controller
    {
        private Context db = new Context();

        // GET: TerminosCarnes
        public ActionResult Index()
        {
            return View(db.TerminosCarne.ToList());
        }

        // GET: TerminosCarnes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerminosCarne terminosCarne = db.TerminosCarne.Find(id);
            if (terminosCarne == null)
            {
                return HttpNotFound();
            }
            return View(terminosCarne);
        }

        // GET: TerminosCarnes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TerminosCarnes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTerminoCarne,Termino")] TerminosCarne terminosCarne)
        {
            if (ModelState.IsValid)
            {
                db.TerminosCarne.Add(terminosCarne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(terminosCarne);
        }

        // GET: TerminosCarnes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerminosCarne terminosCarne = db.TerminosCarne.Find(id);
            if (terminosCarne == null)
            {
                return HttpNotFound();
            }
            return View(terminosCarne);
        }

        // POST: TerminosCarnes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTerminoCarne,Termino")] TerminosCarne terminosCarne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terminosCarne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(terminosCarne);
        }

        // GET: TerminosCarnes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerminosCarne terminosCarne = db.TerminosCarne.Find(id);
            if (terminosCarne == null)
            {
                return HttpNotFound();
            }
            return View(terminosCarne);
        }

        // POST: TerminosCarnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TerminosCarne terminosCarne = db.TerminosCarne.Find(id);
            db.TerminosCarne.Remove(terminosCarne);
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
