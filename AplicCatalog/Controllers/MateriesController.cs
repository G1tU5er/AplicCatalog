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
    [Authorize(Roles = "Secretar")]
    public class MateriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Materies
        public ActionResult Index()
        {
            return View(db.Materii.OrderBy(x =>x.NumeMaterie).ToList());
        }

       // GET: Materies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Materies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Materie materie)
        {
            if (ModelState.IsValid)
            {
                db.Materii.Add(materie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materie);
        }

        // GET: Materies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materie materie = db.Materii.Find(id);
            if (materie == null)
            {
                return HttpNotFound();
            }
            return View(materie);
        }

        // POST: Materies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Materie materie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materie);
        }

        // GET: Materies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materie materie = db.Materii.Find(id);
            if (materie == null)
            {
                return HttpNotFound();
            }
            return View(materie);
        }

        // POST: Materies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Materie materie = db.Materii.Find(id);
            try
            {
                db.Materii.Remove(materie);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Nu se poate sterge o materie asignata!");
                return View(materie);
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
