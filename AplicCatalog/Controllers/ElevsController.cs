using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicCatalog.Models;

namespace AplicCatalog.Controllers
{
    [Authorize(Roles = "Diriginte, Secretar")]
    public class ElevsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Elevs
        public ActionResult Index()
        {
            return View(db.Elevi.OrderBy(x =>x.Nume).OrderBy(x =>x.Prenume).ToList());
        }

      
        // GET: Elevs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Elevs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdElev,Nume,Prenume,Mama,Tata")] Elev elev)
        {
            if (ModelState.IsValid)
            {
                db.Elevi.Add(elev);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(elev);
        }

        // GET: Elevs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elev elev = db.Elevi.Find(id);
            if (elev == null)
            {
                return HttpNotFound();
            }
            return View(elev);
        }

        // POST: Elevs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdElev,Nume,Prenume,Mama,Tata")] Elev elev)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elev).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(elev);
        }

        // GET: Elevs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elev elev = db.Elevi.Find(id);
            if (elev == null)
            {
                return HttpNotFound();
            }
            return View(elev);
        }

        // POST: Elevs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Elev elev = db.Elevi.Find(id);
            try
            {
                db.Elevi.Remove(elev);
                db.SaveChanges();
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "Nu se poate sterge un elev incadrat!");
                return View(elev);
            }
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
