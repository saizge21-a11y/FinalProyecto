using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProyecto.Models;

namespace FinalProyecto.Controllers
{
    public class PAGOS_PRESTAMOSController : Controller
    {
        private Entities db = new Entities();

        // GET: PAGOS_PRESTAMOS
        public ActionResult Index()
        {
            var pAGOS_PRESTAMOS = db.PAGOS_PRESTAMOS.Include(p => p.PRESTAMOS);
            return View(pAGOS_PRESTAMOS.ToList());
        }

        // GET: PAGOS_PRESTAMOS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGOS_PRESTAMOS pAGOS_PRESTAMOS = db.PAGOS_PRESTAMOS.Find(id);
            if (pAGOS_PRESTAMOS == null)
            {
                return HttpNotFound();
            }
            return View(pAGOS_PRESTAMOS);
        }

        // GET: PAGOS_PRESTAMOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_PRETAMO = new SelectList(db.PRESTAMOS, "ID_PRESTAMO", "MONEDA");
            return View();
        }

        // POST: PAGOS_PRESTAMOS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PAGO_PRESTAMO,ID_PRETAMO,FECHA_PROGRAMADA,INTERES_PROGRAMADO,ESTADO_PAGO,FECHA_RECIBIDO,MONTO_RECIBIDO")] PAGOS_PRESTAMOS pAGOS_PRESTAMOS)
        {
            if (ModelState.IsValid)
            {
                db.PAGOS_PRESTAMOS.Add(pAGOS_PRESTAMOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PRETAMO = new SelectList(db.PRESTAMOS, "ID_PRESTAMO", "MONEDA", pAGOS_PRESTAMOS.ID_PRETAMO);
            return View(pAGOS_PRESTAMOS);
        }

        // GET: PAGOS_PRESTAMOS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGOS_PRESTAMOS pAGOS_PRESTAMOS = db.PAGOS_PRESTAMOS.Find(id);
            if (pAGOS_PRESTAMOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PRETAMO = new SelectList(db.PRESTAMOS, "ID_PRESTAMO", "MONEDA", pAGOS_PRESTAMOS.ID_PRETAMO);
            return View(pAGOS_PRESTAMOS);
        }

        // POST: PAGOS_PRESTAMOS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PAGO_PRESTAMO,ID_PRETAMO,FECHA_PROGRAMADA,INTERES_PROGRAMADO,ESTADO_PAGO,FECHA_RECIBIDO,MONTO_RECIBIDO")] PAGOS_PRESTAMOS pAGOS_PRESTAMOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAGOS_PRESTAMOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PRETAMO = new SelectList(db.PRESTAMOS, "ID_PRESTAMO", "MONEDA", pAGOS_PRESTAMOS.ID_PRETAMO);
            return View(pAGOS_PRESTAMOS);
        }

        // GET: PAGOS_PRESTAMOS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGOS_PRESTAMOS pAGOS_PRESTAMOS = db.PAGOS_PRESTAMOS.Find(id);
            if (pAGOS_PRESTAMOS == null)
            {
                return HttpNotFound();
            }
            return View(pAGOS_PRESTAMOS);
        }

        // POST: PAGOS_PRESTAMOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PAGOS_PRESTAMOS pAGOS_PRESTAMOS = db.PAGOS_PRESTAMOS.Find(id);
            db.PAGOS_PRESTAMOS.Remove(pAGOS_PRESTAMOS);
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
