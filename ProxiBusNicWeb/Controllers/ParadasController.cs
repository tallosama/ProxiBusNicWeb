using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ProxiBusNicWeb.Models;

namespace ProxiBusNicWeb.Controllers
{
    [Authorize]
    public class ParadasController : Controller
    {
        private ProxiBusNicEntityContainer db = new ProxiBusNicEntityContainer();

        // GET: Paradas
        public ActionResult Index()
        {
            return View(db.Paradas.ToList());
        }

        // GET: Paradas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parada parada = db.Paradas.Find(id);
            if (parada == null)
            {
                return HttpNotFound();
            }
            return View(parada);
        }

        // GET: Paradas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paradas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,Alias,FotoParada,Estado,Longitud,Latitud,FechaCreacion,UsuarioCreacion,FechaModificacion,UsuarioModificacion")] Parada parada)
        {
            HttpPostedFileBase foto = Request.Files[0];


            if (foto.ContentLength != 0)
            {
                WebImage web = new WebImage(foto.InputStream);
                parada.FotoParada = web.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Paradas.Add(parada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parada);
        }

        // GET: Paradas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parada parada = db.Paradas.Find(id);
            if (parada == null)
            {
                return HttpNotFound();
            }
            return View(parada);
        }

        // POST: Paradas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,Alias,FotoParada,Estado,Longitud,Latitud,FechaCreacion,UsuarioCreacion,FechaModificacion,UsuarioModificacion")] Parada parada)
        {

            HttpPostedFileBase foto = Request.Files[0];
            if (foto.ContentLength != 0)
            {
                WebImage web = new WebImage(foto.InputStream);
                parada.FotoParada = web.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Entry(parada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parada);
        }

        // GET: Paradas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parada parada = db.Paradas.Find(id);
            if (parada == null)
            {
                return HttpNotFound();
            }
            return View(parada);
        }

        // POST: Paradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parada parada = db.Paradas.Find(id);
            db.Paradas.Remove(parada);
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
