using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicCatalog.Models;
using AplicCatalog.viewModel;

namespace AplicCatalog.Controllers
{
    [Authorize(Roles = "Secretar")]
    public class ProfesorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profesors
        public ActionResult Index(int? id)
        {
            var viewModel = new ProfesorIndexData();
            viewModel.Profesori = db.Profesori
                .Include(i => i.Materii).Include(i =>i.user).OrderBy(x =>x.user.Email).ToList();


            if (id != null)
            {
                ViewBag.IdProfesor = id.Value;
                viewModel.Materii = viewModel.Profesori.Where(
                    i => i.IdProfesor == id.Value).Single().Materii;
               
            }

            return View(viewModel);
        }

        

        // GET: Profesors/Create
        public ActionResult Create(int? id)
        {
            Profesor prof = db.Profesori.Include(i => i.Materii).Where(i => i.IdProfesor == id.Value).Single() ;
            ViewBag.IdProfesor = id;
            PopulateAssignedMateriiData(prof);
           
            return View(prof);
        }
        private void PopulateAssignedMateriiData(Profesor profesor)
        {
            var allMaterii = db.Materii.OrderBy(x =>x.NumeMaterie);
            var viewModel = new List<MaterieAsignata>();
            if (profesor != null)
            {
                var materiilePlanului = new HashSet<int>(profesor.Materii.Select(c => c.IdMaterie));

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
          
            ViewBag.Materii = viewModel;
        }

        // POST: Profesors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( string[] selectedMaterii, int IdProfesor)
        {
            Profesor profesor = db.Profesori.Find(IdProfesor);
            if (ModelState.IsValid)
            {
                
               for (int i = 0; i < selectedMaterii.Length; i++)
                {
                    profesor.Materii.Add(db.Materii.Find(Int32.Parse(selectedMaterii[i])));
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.Materii = db.Materii.OrderBy(x =>x.NumeMaterie).ToList();
            return View(profesor);
        }
        // GET: Profesors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = db.Profesori
            .Include(i => i.Materii).Include( i =>i.user)
            .Single(x => x.IdProfesor == id);
            PopulateAssignedMateriiData(profesor);
            if (profesor == null)
            {
                return HttpNotFound();
            }

           return View(profesor);

        }

        // POST: Profesors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] selectedMaterii)
        {
            var profesorToUpdate = db.Profesori
             .Include(i => i.Materii)
             .Single(x => x.IdProfesor == id);

            if (TryUpdateModel(profesorToUpdate, "", new string[] {"Nume"}))
            {
                try
                {
                    UpdatePlanInvatamantMaterii(selectedMaterii, profesorToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex) { throw ex; }
            }
            PopulateAssignedMateriiData(profesorToUpdate);
            return View(profesorToUpdate);

        }

        private void UpdatePlanInvatamantMaterii(string[] selectedMaterii, Profesor profesorToUpdate)
        {
            if (selectedMaterii == null)
            {
                profesorToUpdate.Materii = new List<Materie>();
                return;
            }

            var selectedMaterieHS = new HashSet<string>(selectedMaterii);
            var planMaterii = new HashSet<int>
                (profesorToUpdate.Materii.Select(c => c.IdMaterie));
            foreach (var materie in db.Materii.OrderBy(x =>x.NumeMaterie))
            {
                if (selectedMaterieHS.Contains(materie.IdMaterie.ToString()))
                {
                    if (!planMaterii.Contains(materie.IdMaterie))
                    {
                        profesorToUpdate.Materii.Add(materie);
                    }
                }
                else
                {
                    if (planMaterii.Contains(materie.IdMaterie))
                    {
                        profesorToUpdate.Materii.Remove(materie);
                    }
                }
            }
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
