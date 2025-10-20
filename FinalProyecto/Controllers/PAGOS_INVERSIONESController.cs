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
    public class PAGOS_INVERSIONESController : Controller
    {
        private Entities db = new Entities();

        // GET: PAGOS_INVERSIONES
        public ActionResult Index()
        {
            var pAGOS_INVERSIONES = db.PAGOS_INVERSIONES.Include(p => p.INVERSIONES);
            return View(pAGOS_INVERSIONES.ToList());
        }

        // GET: PAGOS_INVERSIONES/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGOS_INVERSIONES pAGOS_INVERSIONES = db.PAGOS_INVERSIONES.Find(id);
            if (pAGOS_INVERSIONES == null)
            {
                return HttpNotFound();
            }
            return View(pAGOS_INVERSIONES);
        }

        // GET: PAGOS_INVERSIONES/Create
        public ActionResult Create()
        {
            ViewBag.ID_INVERSIONES = new SelectList(db.INVERSIONES, "ID_INVERSIONES", "MONEDA");
            return View();
        }

        // POST: PAGOS_INVERSIONES/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PAGO_INVERSIONES,ID_INVERSIONES,FECHA_PROGRAMADA,INTERES_PROGRAMADO,ESTADO_PAGO,FECHA_RECIBIDO,MONTO_RECIBIDO")] PAGOS_INVERSIONES pAGOS_INVERSIONES)
        {
            if (ModelState.IsValid)
            {
                db.PAGOS_INVERSIONES.Add(pAGOS_INVERSIONES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_INVERSIONES = new SelectList(db.INVERSIONES, "ID_INVERSIONES", "MONEDA", pAGOS_INVERSIONES.ID_INVERSIONES);
            return View(pAGOS_INVERSIONES);
        }

        // GET: PAGOS_INVERSIONES/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGOS_INVERSIONES pAGOS_INVERSIONES = db.PAGOS_INVERSIONES.Find(id);
            if (pAGOS_INVERSIONES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_INVERSIONES = new SelectList(db.INVERSIONES, "ID_INVERSIONES", "MONEDA", pAGOS_INVERSIONES.ID_INVERSIONES);
            return View(pAGOS_INVERSIONES);
        }

        // POST: PAGOS_INVERSIONES/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PAGO_INVERSIONES,ID_INVERSIONES,FECHA_PROGRAMADA,INTERES_PROGRAMADO,ESTADO_PAGO,FECHA_RECIBIDO,MONTO_RECIBIDO")] PAGOS_INVERSIONES pAGOS_INVERSIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAGOS_INVERSIONES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_INVERSIONES = new SelectList(db.INVERSIONES, "ID_INVERSIONES", "MONEDA", pAGOS_INVERSIONES.ID_INVERSIONES);
            return View(pAGOS_INVERSIONES);
        }

        // GET: PAGOS_INVERSIONES/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGOS_INVERSIONES pAGOS_INVERSIONES = db.PAGOS_INVERSIONES.Find(id);
            if (pAGOS_INVERSIONES == null)
            {
                return HttpNotFound();
            }
            return View(pAGOS_INVERSIONES);
        }

        // POST: PAGOS_INVERSIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PAGOS_INVERSIONES pAGOS_INVERSIONES = db.PAGOS_INVERSIONES.Find(id);
            db.PAGOS_INVERSIONES.Remove(pAGOS_INVERSIONES);
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
