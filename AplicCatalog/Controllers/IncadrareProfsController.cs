using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicCatalog.Models;
using System.Data.SqlClient;

namespace AplicCatalog.Controllers
{
    [Authorize(Roles = "Secretar")]
   
    public class IncadrareProfsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IncadrareProfs
        public ActionResult Index(int? id)
        {
            var viewModel = new IncadrareProfData();
            ViewBag.listaAni = db.AnScolars.OrderByDescending(x =>x.InceputAnScolar).ToList();

            viewModel.Clase = db.Clasas.Include(i =>i.PlanDeInvatamant)
                    .Include(i => i.PlanDeInvatamant.AnScolar).OrderBy(x =>x.NumeClasa).ToList();

            if (id != null)
            {
                ViewBag.IdClasa = id.Value;
                viewModel.AnScolar = db.AnScolars.Find(viewModel.Clase.Where(
                         i => i.IdClasa == id.Value).Single().PlanDeInvatamant.IdAn);

                viewModel.Materii = 
                    db.Materii.SqlQuery("select * from Materies a, MateriePlanInvatamants b, PlanInvatamants c where a.IdMaterie= b.Materie_IdMaterie and c.IdPlan= b.PlanInvatamant_IdPlan and exists (select * from Clasas d where d.IdPlan = c.IdPlan and d.IdClasa = @id) order by a.NumeMaterie", new SqlParameter("@id", id.Value)).ToList();

             
                var Incadrari = db.IncadrariProf.Where(x => x.idClasa == id).ToList();
                List<Profesor> profesoriAsignati = new List<Profesor>();
                if (Incadrari != null)
               {
                    List<int> listIdMaterie = new List<int>();
                    foreach (var item in Incadrari)
                    {
                        Profesor prof = db.Profesori.Find(item.IdProfesor);
                        Materie materieAsignata = db.Materii.Find(item.idMaterie);
                        listIdMaterie.Add(item.idMaterie);

                        prof.Materii.Add(materieAsignata);
                        profesoriAsignati.Add(prof);
                    }
                    viewModel.Profesori = profesoriAsignati;
                    viewModel.listIdMaterie = listIdMaterie;

                }
            }

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            var viewModel = new IncadrareProfData();
            ViewBag.listaAni = db.AnScolars.OrderByDescending(x =>x.InceputAnScolar).ToList();
            int idAn = int.Parse(col[1]);
            string numeClasa = col[0];
            int? id = null;

            if (idAn != 0)
            {
                ViewBag.IdAn = idAn;
                ViewBag.NumeClasa = numeClasa;
                SqlParameter paramAn = new SqlParameter("@idAn", idAn);
                SqlParameter paramNumeClasa = new SqlParameter("@numeClasa", numeClasa);
                viewModel.Clase = db.Clasas.SqlQuery("select * from Clasas where exists (select idPlan from PlanInvatamants where IdAn=@idAn and IdPlan= Clasas.IdPlan) and NumeClasa=@numeClasa", paramAn, paramNumeClasa).ToList();

                if (viewModel.Clase != null && viewModel.Clase.Count() > 0)
                {
                    id = viewModel.Clase.ToList()[0].IdClasa;
                    viewModel.AnScolar = db.AnScolars.Find(idAn);

                    if (id != null)
                    {
                        ViewBag.IdClasa = id;

                        viewModel.Materii =
                             db.Materii.SqlQuery("select * from Materies a, MateriePlanInvatamants b, PlanInvatamants c where a.IdMaterie= b.Materie_IdMaterie and c.IdPlan= b.PlanInvatamant_IdPlan and exists (select * from Clasas d where d.IdPlan = c.IdPlan and d.IdClasa = @id) order by a.NumeMaterie", new SqlParameter("@id", id.Value)).ToList();
                    }
                }


            }

            return View(viewModel);
        }
    
        // GET: IncadrareProfs/Create
        public ActionResult Create(int idClasa,int idMaterie)
        {
            IncadrareProf incadrareNoua = new IncadrareProf();
            incadrareNoua.Clasa = db.Clasas.Find(idClasa);
            ViewBag.listaProfesori = db.Profesori.SqlQuery("select * from Profesors a, profesormateries b, Materies c where a.IdProfesor= b.profesor_idprofesor and b.materie_idmaterie=c.IdMaterie and c.IdMaterie=@idMaterie order by a.Nume", new SqlParameter("@idMaterie", idMaterie)).ToList();
            return View(incadrareNoua);
        }

        // POST: IncadrareProfs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IncadrareProf incadrare, string[] profesoriSelectati)
        {

            if (ModelState.IsValid)
            {

                foreach (var id in profesoriSelectati)
                {
                    var profesor = db.Profesori.Find(int.Parse(id));
                    incadrare.Profesor = profesor;
                    db.Entry(incadrare.Profesor).State = EntityState.Unchanged;
                    var clasa = db.Clasas.Find(incadrare.Clasa.IdClasa);
                    if (clasa != null)
                        incadrare.Clasa = clasa;
                    db.Entry(incadrare.Clasa).State = EntityState.Unchanged;
                    db.IncadrariProf.Add(incadrare);
                    db.SaveChanges();
                  
                }

                return RedirectToAction("Index");
            }

            return View(incadrare);
        }

      
        // GET: IncadrareProfs/Delete/5
        public ActionResult Delete(int? idMaterie, int? idClasa)
        {
            if (idMaterie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncadrareProf incadrareProf = db.IncadrariProf.Where(x =>x.idMaterie== idMaterie).Where( x=>x.idClasa== idClasa).Single();
            if (incadrareProf == null)
            {
                return HttpNotFound();
            }
            return View(incadrareProf);
        }

        // POST: IncadrareProfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idMaterie, int? idClasa)
        {
            IncadrareProf incadrareProf = db.IncadrariProf.Where(x => x.idMaterie == idMaterie).Where(x => x.idClasa == idClasa).Single();
            db.IncadrariProf.Remove(incadrareProf);
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
