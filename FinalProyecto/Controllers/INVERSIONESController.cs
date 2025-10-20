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
    public class INVERSIONESController : Controller
    {
        private Entities db = new Entities();

        // GET: INVERSIONES
        public ActionResult Index()
        {
            var iNVERSIONES = db.INVERSIONES.Include(i => i.USUARIOS);
            return View(iNVERSIONES.ToList());
        }

        // GET: INVERSIONES/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVERSIONES iNVERSIONES = db.INVERSIONES.Find(id);
            if (iNVERSIONES == null)
            {
                return HttpNotFound();
            }
            return View(iNVERSIONES);
        }

        // GET: INVERSIONES/Create
        public ActionResult Create()
        {
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USUARIO");
            return View();
        }

        // POST: INVERSIONES/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_INVERSIONES,ID_USUARIO,MONTO,MONEDA,PLAZO_DIAS,FECHA_INICIAL,FECHA_FINAL,TASA_INTERES,MODALIDAD")] INVERSIONES iNVERSIONES)
        {
            if (ModelState.IsValid)
            {
                db.INVERSIONES.Add(iNVERSIONES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USUARIO", iNVERSIONES.ID_USUARIO);
            return View(iNVERSIONES);
        }

        // GET: INVERSIONES/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVERSIONES iNVERSIONES = db.INVERSIONES.Find(id);
            if (iNVERSIONES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USUARIO", iNVERSIONES.ID_USUARIO);
            return View(iNVERSIONES);
        }

        // POST: INVERSIONES/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INVERSIONES,ID_USUARIO,MONTO,MONEDA,PLAZO_DIAS,FECHA_INICIAL,FECHA_FINAL,TASA_INTERES,MODALIDAD")] INVERSIONES iNVERSIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNVERSIONES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USUARIO", iNVERSIONES.ID_USUARIO);
            return View(iNVERSIONES);
        }

        // GET: INVERSIONES/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVERSIONES iNVERSIONES = db.INVERSIONES.Find(id);
            if (iNVERSIONES == null)
            {
                return HttpNotFound();
            }
            return View(iNVERSIONES);
        }

        // POST: INVERSIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            INVERSIONES iNVERSIONES = db.INVERSIONES.Find(id);
            db.INVERSIONES.Remove(iNVERSIONES);
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
