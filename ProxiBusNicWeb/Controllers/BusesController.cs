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
    public class BusesController : Controller
    {
        private ProxiBusNicEntityContainer db = new ProxiBusNicEntityContainer();

        // GET: Buses
        public ActionResult Index()
        {
            List<Bus> bus = null;
            if (User.IsInRole("UsuarioAnonimo"))
            {
                bus = db.Buses.Where(b=>b.Estado).ToList();
            }
            else
            {
                bus = db.Buses.ToList();
            }
            return View(bus);
        }

        // GET: Buses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Buses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buses/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumeroRuta,Estado,FotoBus,FechaCreacion,UsuarioCreacion,FechaModificacion,UsuarioModificacion")] Bus bus)
        {
            HttpPostedFileBase foto = Request.Files[0];


            if (foto.ContentLength != 0)
            {
                WebImage web = new WebImage(foto.InputStream);
                bus.FotoBus = web.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Buses.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bus);
        }

        // GET: Buses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumeroRuta,Estado,FotoBus,FechaCreacion,UsuarioCreacion,FechaModificacion,UsuarioModificacion")] Bus bus)
        {
            HttpPostedFileBase foto = Request.Files[0];


            if (foto.ContentLength != 0)
            {
                WebImage web = new WebImage(foto.InputStream);
                bus.FotoBus = web.GetBytes();
            }


            if (ModelState.IsValid)
            {
                db.Entry(bus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bus);
        }

        // GET: Buses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bus bus = db.Buses.Find(id);
            db.Buses.Remove(bus);
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
