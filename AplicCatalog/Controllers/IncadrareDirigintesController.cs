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
    public class IncadrareDirigintesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IncadrareDirigintes
        public ActionResult Index(int? id)
        {
            var viewModel = new IncadrareDirigData();
            ViewBag.listaAni = db.AnScolars.OrderByDescending(x => x.InceputAnScolar).ToList();

            viewModel.Clase = db.Clasas.Include(i => i.PlanDeInvatamant)
                    .Include(i => i.PlanDeInvatamant.AnScolar).OrderBy(x => x.NumeClasa).ToList();
            var incadrariDirig = db.IncadrariDiriginte.ToList();
            List<int> listIdClase = new List<int>();
            if (incadrariDirig!=null)
            {
               
                foreach(var item in incadrariDirig)
                {
                    listIdClase.Add(item.idClasa);
                }
                viewModel.ClaseCuDiriginte = listIdClase;
            }

            if (id != null)
            {
                ViewBag.IdClasa = id.Value;
                viewModel.AnScolar = db.AnScolars.Find(viewModel.Clase.Where(
                         i => i.IdClasa == id.Value).Single().PlanDeInvatamant.IdAn);

                var IncadrariDirig = db.IncadrariDiriginte.Where(x => x.idClasa == id).ToList();
                ViewBag.IncadrareDirig = IncadrariDirig;

                if (IncadrariDirig != null && IncadrariDirig.Count() > 0)
                {
                    Profesor prof = db.Profesori.Find(IncadrariDirig[0].IdProfesor);
                    viewModel.Profesor = prof;
                }

            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            var viewModel = new IncadrareDirigData();
            ViewBag.listaAni = db.AnScolars.OrderByDescending(x => x.InceputAnScolar).ToList();
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

                        //viewModel.Materii =
                        //     db.Materii.SqlQuery("select * from Materies a, MateriePlanInvatamants b, PlanInvatamants c where a.IdMaterie= b.Materie_IdMaterie and c.IdPlan= b.PlanInvatamant_IdPlan and exists (select * from Clasas d where d.IdPlan = c.IdPlan and d.IdClasa = @id) order by a.NumeMaterie", new SqlParameter("@id", id.Value)).ToList();
                    }
                }


            }

            return View(viewModel);
        }

        public ActionResult AdaugaDirig(int idClasa)
        {
            IncadrareDiriginte incadrareNoua = new IncadrareDiriginte();
            incadrareNoua.Clasa = db.Clasas.Find(idClasa);
            ViewBag.listaProfesori = db.Profesori.SqlQuery("select * from Profesors a, IncadrareProfs b where a.IdProfesor=b.IdProfesor and b.idClasa=@idClasa order by a.Nume", new SqlParameter("@idClasa", idClasa)).ToList();
            ViewBag.IdClasa = idClasa;
            return View(incadrareNoua);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdaugaDirig(IncadrareDiriginte incadrare, string[] profesoriSelectati)
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
                    db.IncadrariDiriginte.Add(incadrare);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            return View(incadrare);
        }

        public ActionResult Delete(int? idClasa)
        {
            if (idClasa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncadrareDiriginte incadrareProf = db.IncadrariDiriginte.Where(x => x.idClasa == idClasa).Single();
            if (incadrareProf == null)
            {
                return HttpNotFound();
            }
            return View(incadrareProf);
        }

        // POST: IncadrareProfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idClasa)
        {
            IncadrareDiriginte incadrareProf = db.IncadrariDiriginte.Where(x => x.idClasa == idClasa).Single();
            db.IncadrariDiriginte.Remove(incadrareProf);
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
