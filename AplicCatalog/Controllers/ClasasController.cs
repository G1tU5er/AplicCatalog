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
    [Authorize(Roles = "Profesor, Secretar")]
    public class ClasasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clasas
        public ActionResult Index(int? id)
        {
            var viewModel = new ClasaIndexData();
            viewModel.clase = db.Clasas
                .Include(i => i.PlanDeInvatamant).OrderBy(x =>x.NumeClasa).ToList();


            if (id != null)
            {
                ViewBag.IdClasa = id.Value;

                viewModel.anScolar = db.AnScolars.Find(viewModel.clase.Where(i => i.IdClasa == id.Value).Single().PlanDeInvatamant.IdAn);
               
            }

            return View(viewModel);
        }

        // GET: Clasas/Create
        public ActionResult Create()
        {
            Clasa cls = new Clasa();
            ViewBag.PlanuriScolare = db.PlanuriScolare.Include(i=>i.AnScolar).OrderBy( x=>x.Nume).ToList();
          
            return View(cls);
        }

        // POST: Clasas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clasa clasa)
        {
            if (ModelState.IsValid)
            {
                clasa.IdPlan= clasa.PlanDeInvatamant.IdPlan;
                db.Entry(clasa.PlanDeInvatamant.AnScolar).State = EntityState.Unchanged;
                db.Entry(clasa.AnScolar).State = EntityState.Unchanged;
                db.Entry(clasa.PlanDeInvatamant).State = EntityState.Unchanged;
                db.Clasas.Add(clasa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clasa);
        }

        // GET: Clasas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clasa clasa = db.Clasas
              .Include(i => i.PlanDeInvatamant)
              .Include(i => i.PlanDeInvatamant.AnScolar)
              .SingleOrDefault(x => x.IdClasa == id);
        
            
            if (clasa == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlanuriScolare = db.PlanuriScolare.OrderBy(x =>x.Nume).ToList();
           
            return View(clasa);
        }

        // POST: Clasas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clasa clasa)
        {
            if (ModelState.IsValid)
            {
                clasa.IdPlan = clasa.PlanDeInvatamant.IdPlan;
                db.Entry(clasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clasa);
        }

        // GET: Clasas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clasa clasa = db.Clasas.Find(id);
            if (clasa == null)
            {
                return HttpNotFound();
            }
            return View(clasa);
        }

        // POST: Clasas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clasa clasa = db.Clasas.Find(id);
            try
            {
                db.Clasas.Remove(clasa);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Nu se poate sterge o clasa cu elevi incadrati!");
                return View(clasa);
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
