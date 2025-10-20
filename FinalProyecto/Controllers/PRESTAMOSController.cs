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
    public class PRESTAMOSController : Controller
    {
        private Entities db = new Entities();

        // GET: PRESTAMOS
        public ActionResult Index()
        {
            var pRESTAMOS = db.PRESTAMOS.Include(p => p.ENTIDADES_FINANCIERAS).Include(p => p.USUARIOS);
            return View(pRESTAMOS.ToList());
        }

        // GET: PRESTAMOS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESTAMOS pRESTAMOS = db.PRESTAMOS.Find(id);
            if (pRESTAMOS == null)
            {
                return HttpNotFound();
            }
            return View(pRESTAMOS);
        }

        // GET: PRESTAMOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_ENTIDAD = new SelectList(db.ENTIDADES_FINANCIERAS, "ID_ENTIDAD", "NOMBRE");
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USUARIO");
            return View();
        }

        // POST: PRESTAMOS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PRESTAMO,ID_ENTIDAD,ID_USUARIO,MONTO,MONEDA,PLAZO_DIAS,FECHA_INICIAL,FECHA_FINAL,TASA_INTERES,MODALIDAD")] PRESTAMOS pRESTAMOS)
        {
            if (ModelState.IsValid)
            {
                db.PRESTAMOS.Add(pRESTAMOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_ENTIDAD = new SelectList(db.ENTIDADES_FINANCIERAS, "ID_ENTIDAD", "NOMBRE", pRESTAMOS.ID_ENTIDAD);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USUARIO", pRESTAMOS.ID_USUARIO);
            return View(pRESTAMOS);
        }

        // GET: PRESTAMOS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESTAMOS pRESTAMOS = db.PRESTAMOS.Find(id);
            if (pRESTAMOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ENTIDAD = new SelectList(db.ENTIDADES_FINANCIERAS, "ID_ENTIDAD", "NOMBRE", pRESTAMOS.ID_ENTIDAD);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USUARIO", pRESTAMOS.ID_USUARIO);
            return View(pRESTAMOS);
        }

        // POST: PRESTAMOS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PRESTAMO,ID_ENTIDAD,ID_USUARIO,MONTO,MONEDA,PLAZO_DIAS,FECHA_INICIAL,FECHA_FINAL,TASA_INTERES,MODALIDAD")] PRESTAMOS pRESTAMOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRESTAMOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ENTIDAD = new SelectList(db.ENTIDADES_FINANCIERAS, "ID_ENTIDAD", "NOMBRE", pRESTAMOS.ID_ENTIDAD);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USUARIO", pRESTAMOS.ID_USUARIO);
            return View(pRESTAMOS);
        }

        // GET: PRESTAMOS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESTAMOS pRESTAMOS = db.PRESTAMOS.Find(id);
            if (pRESTAMOS == null)
            {
                return HttpNotFound();
            }
            return View(pRESTAMOS);
        }

        // POST: PRESTAMOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PRESTAMOS pRESTAMOS = db.PRESTAMOS.Find(id);
            db.PRESTAMOS.Remove(pRESTAMOS);
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
