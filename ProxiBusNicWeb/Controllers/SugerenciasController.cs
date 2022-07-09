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
    public class SugerenciasController : Controller
    {
        private ProxiBusNicEntityContainer db = new ProxiBusNicEntityContainer();

        // GET: Sugerencias
        public ActionResult Index()
        {
            var sugerencias = db.Sugerencias.Include(s => s.Parada);
            return View(sugerencias.ToList());
        }

        // GET: Sugerencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sugerencia sugerencia = db.Sugerencias.Find(id);
            if (sugerencia == null)
            {
                return HttpNotFound();
            }
            return View(sugerencia);
        }

        // GET: Sugerencias/Create
        public ActionResult Create()
        {
            ViewBag.ParadaId = new SelectList(db.Paradas, "Id", "Descripcion");
            return View();
        }

        // POST: Sugerencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DescripcionSugerencia,UsuarioCreacion,FechaCreacion,ParadaId")] Sugerencia sugerencia)
        {
            if (ModelState.IsValid)
            {
                db.Sugerencias.Add(sugerencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParadaId = new SelectList(db.Paradas, "Id", "Descripcion", sugerencia.ParadaId);
            return View(sugerencia);
        }

        // GET: Sugerencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sugerencia sugerencia = db.Sugerencias.Find(id);
            if (sugerencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParadaId = new SelectList(db.Paradas, "Id", "Descripcion", sugerencia.ParadaId);
            return View(sugerencia);
        }

        // POST: Sugerencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DescripcionSugerencia,UsuarioCreacion,FechaCreacion,ParadaId")] Sugerencia sugerencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sugerencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParadaId = new SelectList(db.Paradas, "Id", "Descripcion", sugerencia.ParadaId);
            return View(sugerencia);
        }

        // GET: Sugerencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sugerencia sugerencia = db.Sugerencias.Find(id);
            if (sugerencia == null)
            {
                return HttpNotFound();
            }
            return View(sugerencia);
        }

        // POST: Sugerencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sugerencia sugerencia = db.Sugerencias.Find(id);
            db.Sugerencias.Remove(sugerencia);
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
