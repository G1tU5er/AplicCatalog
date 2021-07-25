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
    [Authorize(Roles ="Secretar")]
    public class AnScolarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnScolars
        public ActionResult Index()
        {
            return View(db.AnScolars.OrderBy(x =>x.InceputAnScolar).ToList());
        }

       // GET: AnScolars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnScolars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAn,InceputAnScolar,SfarsitAnScolar")] AnScolar anScolar)
        {
            if (ModelState.IsValid)
            {
                db.AnScolars.Add(anScolar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anScolar);
        }

        // GET: AnScolars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnScolar anScolar = db.AnScolars.Find(id);
            if (anScolar == null)
            {
                return HttpNotFound();
            }
            return View(anScolar);
        }

        // POST: AnScolars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAn,InceputAnScolar,SfarsitAnScolar")] AnScolar anScolar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anScolar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anScolar);
        }

        // GET: AnScolars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnScolar anScolar = db.AnScolars.Find(id);
            if (anScolar == null)
            {
                return HttpNotFound();
            }
            return View(anScolar);
        }

        // POST: AnScolars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnScolar anScolar = db.AnScolars.Find(id);
            try
            {
                db.AnScolars.Remove(anScolar);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Nu se poate sterge un an scolar asignat unui plan de invatamant!");
                return View(anScolar);
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
