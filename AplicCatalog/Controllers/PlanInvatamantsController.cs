using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicCatalog.Models;
using System.Diagnostics;
using AplicCatalog.viewModel;

namespace AplicCatalog.Controllers
{
    [Authorize(Roles = "Secretar")]
    public class PlanInvatamantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlanInvatamants
       
           public ActionResult Index(int? id)
        {
            var viewModel = new PlanIndexData();
            viewModel.planuriScolare = db.PlanuriScolare
                .Include(i => i.Materii).OrderBy(x =>x.Nume).ToList();

          
            if (id != null)
            {
                ViewBag.IdPlan = id.Value;
                viewModel.materii = viewModel.planuriScolare.Where(
                    i => i.IdPlan == id.Value).Single().Materii.OrderBy(x =>x.NumeMaterie);
                viewModel.anScolar = db.AnScolars.Find(viewModel.planuriScolare.Where(
                    i => i.IdPlan == id.Value).Single().IdAn);
        var IncadrariNrOreMaterii = db.IncadrariMaterii.Where(i => i.IdPlan == id.Value).ToList();
                Dictionary<int, int> listIdMaterii = new Dictionary<int, int>();
                if (IncadrariNrOreMaterii != null && IncadrariNrOreMaterii.Count()>0)
                {
                    foreach (var item in IncadrariNrOreMaterii)
                        listIdMaterii.Add(item.IdMaterie,item.NrOre);
                }
                viewModel.IncadrariNrOreMaterii = listIdMaterii;
                   
            }

        return View(viewModel);
        }
    
      
        // GET: PlanInvatamants/Create
        public ActionResult Create()
        {

            PopulateAssignedMateriiData(null);
            ViewBag.listaAni = db.AnScolars.OrderByDescending(x =>x.InceputAnScolar).ToList();
            return View();
        }

        // POST: PlanInvatamants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanInvatamant planInvatamant, string[] selectedMaterii)
        {
           if (ModelState.IsValid)
            {
                planInvatamant.IdAn = planInvatamant.AnScolar.IdAn;
                db.Entry(planInvatamant.AnScolar).State = EntityState.Unchanged;
                for (int i = 0; i < selectedMaterii.Length; i++)
                {
                    planInvatamant.Materii.Add(db.Materii.Find(Int32.Parse(selectedMaterii[i])));
                }

                db.PlanuriScolare.Add(planInvatamant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
             ViewBag.listaAni = db.AnScolars.OrderByDescending(x =>x.InceputAnScolar).ToList();
             ViewBag.Materii = db.Materii.OrderBy(x =>x.NumeMaterie).ToList();
            return View(planInvatamant);
        }

        public ActionResult AdaugaNrOre(int? id, int? idPlan)
        {
            IncadrareMateriePlan incadrareNoua = new IncadrareMateriePlan();
            incadrareNoua.Materie = db.Materii.Find(id);
            incadrareNoua.IdMaterie = incadrareNoua.Materie.IdMaterie;
            ViewBag.NumeMaterie = incadrareNoua.Materie.NumeMaterie;
            incadrareNoua.PlanInvatamant = db.PlanuriScolare.Find(idPlan);
          
            return View(incadrareNoua);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdaugaNrOre(IncadrareMateriePlan incadrareNrOre)
        {
            if (ModelState.IsValid)
            {
                var materie = db.Materii.Find(incadrareNrOre.IdMaterie);
                incadrareNrOre.Materie = materie;
                var planinvatamant = db.PlanuriScolare.Find(incadrareNrOre.IdPlan);
                incadrareNrOre.PlanInvatamant = planinvatamant;
                db.Entry(incadrareNrOre.Materie).State = EntityState.Unchanged;
                db.Entry(incadrareNrOre.PlanInvatamant).State = EntityState.Unchanged;
                db.IncadrariMaterii.Add(incadrareNrOre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incadrareNrOre);
        }
        public ActionResult ModificaNrOre(int? id, int? idPlan)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncadrareMateriePlan incadrareNrOre = db.IncadrariMaterii.Where(x =>x.IdMaterie== id).Where(x=>x.IdPlan== idPlan).Single();
            if (incadrareNrOre == null)
            {
                return HttpNotFound();
            }
            var materie = db.Materii.Find(id);
            ViewBag.NumeMaterie = materie.NumeMaterie;
            return View(incadrareNrOre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModificaNrOre(IncadrareMateriePlan incadrareNrOre)
        {
            if (ModelState.IsValid)
            {
                 var materie = db.Materii.Find(incadrareNrOre.IdMaterie);
                incadrareNrOre.Materie = materie;
                var planinvatamant = db.PlanuriScolare.Find(incadrareNrOre.IdPlan);
                incadrareNrOre.PlanInvatamant = planinvatamant;
                db.Entry(incadrareNrOre.Materie).State = EntityState.Unchanged;
                db.Entry(incadrareNrOre.PlanInvatamant).State = EntityState.Unchanged;
                db.Entry(incadrareNrOre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incadrareNrOre);
        }

        // GET: PlanInvatamants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanInvatamant planInvatamant = db.PlanuriScolare
            .Include(i => i.Materii)
            .Include(i => i.AnScolar)
            .Single(x => x.IdPlan == id);
            PopulateAssignedMateriiData(planInvatamant);
            if (planInvatamant == null)
            {
                return HttpNotFound();
            }
          
            ViewBag.listaAni = db.AnScolars.OrderByDescending(x =>x.InceputAnScolar).ToList();
            return View(planInvatamant);
          
        }
        private void PopulateAssignedMateriiData(PlanInvatamant plan)
        {
            var allMaterii = db.Materii.OrderBy(x =>x.NumeMaterie);
            var viewModel = new List<MaterieAsignata>();
            if (plan != null) {
                var materiilePlanului = new HashSet<int>(plan.Materii.Select(c => c.IdMaterie));
              
                foreach (var materie in allMaterii)
                {
                    viewModel.Add(new MaterieAsignata
                    {
                        IdMaterie = materie.IdMaterie,
                        NumeMaterie = materie.NumeMaterie,
                        asignat = materiilePlanului.Contains(materie.IdMaterie)
                    });
                }
            }
            else
            {//new plan created
                foreach (var materie in allMaterii)
                {
                    viewModel.Add(new MaterieAsignata
                    {
                        IdMaterie = materie.IdMaterie,
                        NumeMaterie = materie.NumeMaterie,
                        asignat = false
                    });
                }
            }
            ViewBag.Materii = viewModel;
        }

        // POST: PlanInvatamants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id,PlanInvatamant plan, string[] selectedMaterii)
        {
            var planInvatamantToUpdate = db.PlanuriScolare
             .Include(i => i.Materii)
             .Single(x => x.IdPlan == id);

           if (TryUpdateModel(planInvatamantToUpdate, "", new string[] { "Nume","NrClasa" }))
            {
                try
                {
                    UpdatePlanInvatamantMaterii(selectedMaterii, planInvatamantToUpdate);
                    planInvatamantToUpdate.AnScolar.IdAn = plan.AnScolar.IdAn;
                    db.Entry(planInvatamantToUpdate.AnScolar).State = EntityState.Unchanged;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex) { throw ex; }
            }
            PopulateAssignedMateriiData(planInvatamantToUpdate);
            return View(plan);

        }

        private void UpdatePlanInvatamantMaterii(string[] selectedMaterii, PlanInvatamant planToUpdate)
        {
            if (selectedMaterii == null)
            {
                planToUpdate.Materii = new List<Materie>();
                return;
            }

            var selectedMaterieHS = new HashSet<string>(selectedMaterii);
            var planMaterii = new HashSet<int>
                (planToUpdate.Materii.Select(c => c.IdMaterie));
            foreach (var materie in db.Materii)
            {
                if (selectedMaterieHS.Contains(materie.IdMaterie.ToString()))
                {
                    if (!planMaterii.Contains(materie.IdMaterie))
                    {
                        planToUpdate.Materii.Add(materie);
                    }
                }
                else
                {
                    if (planMaterii.Contains(materie.IdMaterie))
                    {
                        planToUpdate.Materii.Remove(materie);
                    }
                }
            }
        }
        // GET: PlanInvatamants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanInvatamant planInvatamant = db.PlanuriScolare.Find(id);
            if (planInvatamant == null)
            {
                return HttpNotFound();
            }
            return View(planInvatamant);
        }

        // POST: PlanInvatamants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanInvatamant planInvatamant = db.PlanuriScolare.Find(id);
            try
            {
                db.PlanuriScolare.Remove(planInvatamant);
                db.SaveChanges();
            }
            catch (Exception ex) {
                ModelState.AddModelError("","Nu se poate sterge un plan de invatamant asignat!");
                return View(planInvatamant);
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
