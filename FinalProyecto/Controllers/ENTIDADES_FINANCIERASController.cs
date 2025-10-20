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
    public class ENTIDADES_FINANCIERASController : Controller
    {
        private Entities db = new Entities();

        // GET: ENTIDADES_FINANCIERAS
        public ActionResult Index()
        {
            return View(db.ENTIDADES_FINANCIERAS.ToList());
        }

        // GET: ENTIDADES_FINANCIERAS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTIDADES_FINANCIERAS eNTIDADES_FINANCIERAS = db.ENTIDADES_FINANCIERAS.Find(id);
            if (eNTIDADES_FINANCIERAS == null)
            {
                return HttpNotFound();
            }
            return View(eNTIDADES_FINANCIERAS);
        }

        // GET: ENTIDADES_FINANCIERAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ENTIDADES_FINANCIERAS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ENTIDAD,NOMBRE,NIT,TELEFONO")] ENTIDADES_FINANCIERAS eNTIDADES_FINANCIERAS)
        {
            if (ModelState.IsValid)
            {
                db.ENTIDADES_FINANCIERAS.Add(eNTIDADES_FINANCIERAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eNTIDADES_FINANCIERAS);
        }

        // GET: ENTIDADES_FINANCIERAS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTIDADES_FINANCIERAS eNTIDADES_FINANCIERAS = db.ENTIDADES_FINANCIERAS.Find(id);
            if (eNTIDADES_FINANCIERAS == null)
            {
                return HttpNotFound();
            }
            return View(eNTIDADES_FINANCIERAS);
        }

        // POST: ENTIDADES_FINANCIERAS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ENTIDAD,NOMBRE,NIT,TELEFONO")] ENTIDADES_FINANCIERAS eNTIDADES_FINANCIERAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eNTIDADES_FINANCIERAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eNTIDADES_FINANCIERAS);
        }

        // GET: ENTIDADES_FINANCIERAS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTIDADES_FINANCIERAS eNTIDADES_FINANCIERAS = db.ENTIDADES_FINANCIERAS.Find(id);
            if (eNTIDADES_FINANCIERAS == null)
            {
                return HttpNotFound();
            }
            return View(eNTIDADES_FINANCIERAS);
        }

        // POST: ENTIDADES_FINANCIERAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ENTIDADES_FINANCIERAS eNTIDADES_FINANCIERAS = db.ENTIDADES_FINANCIERAS.Find(id);
            db.ENTIDADES_FINANCIERAS.Remove(eNTIDADES_FINANCIERAS);
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
