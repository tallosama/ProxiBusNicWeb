using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProxiBusNicWeb.Models;

namespace ProxiBusNicWeb.Controllers
{
    [Authorize]
    public class BusParadasController : Controller
    {
        private ProxiBusNicEntityContainer db = new ProxiBusNicEntityContainer();

        // GET: BusParadas
        public ActionResult Index()
        {
            var busParadas = db.BusParadas.Include(b => b.Parada).Include(b => b.Bus);
            return View(busParadas.ToList());
        }

        // GET: BusParadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusParada busParada = db.BusParadas.Find(id);
            if (busParada == null)
            {
                return HttpNotFound();
            }
            return View(busParada);
        }

        // GET: BusParadas/Create
        public ActionResult Create()
        {
            ViewBag.ParadaId = new SelectList(db.Paradas, "Id", "Descripcion");
            ViewBag.BusId = new SelectList(db.Buses, "Id", "NumeroRuta");
            return View();
        }

        // POST: BusParadas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ParadaId,BusId")] BusParada busParada)
        {
            if (ModelState.IsValid)
            {
                db.BusParadas.Add(busParada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParadaId = new SelectList(db.Paradas, "Id", "Descripcion", busParada.ParadaId);
            ViewBag.BusId = new SelectList(db.Buses, "Id", "NumeroRuta", busParada.BusId);
            return View(busParada);
        }

        // GET: BusParadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusParada busParada = db.BusParadas.Find(id);
            if (busParada == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParadaId = new SelectList(db.Paradas, "Id", "Descripcion", busParada.ParadaId);
            ViewBag.BusId = new SelectList(db.Buses, "Id", "NumeroRuta", busParada.BusId);
            return View(busParada);
        }

        // POST: BusParadas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParadaId,BusId")] BusParada busParada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busParada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParadaId = new SelectList(db.Paradas, "Id", "Descripcion", busParada.ParadaId);
            ViewBag.BusId = new SelectList(db.Buses, "Id", "NumeroRuta", busParada.BusId);
            return View(busParada);
        }

        // GET: BusParadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusParada busParada = db.BusParadas.Find(id);
            if (busParada == null)
            {
                return HttpNotFound();
            }
            return View(busParada);
        }

        // POST: BusParadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusParada busParada = db.BusParadas.Find(id);
            db.BusParadas.Remove(busParada);
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
