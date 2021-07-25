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
using Microsoft.AspNet.Identity;

namespace AplicCatalog.Controllers
{
    [Authorize(Roles ="Profesor")]
   
    public class CatalogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Catalogs
        public ActionResult Index(int? id, int? idElev, int? idMaterie)
        { 
            var viewModel = new CatalogIndexData();
            ViewBag.listaAni = db.AnScolars.OrderByDescending(x => x.InceputAnScolar).ToList();

            viewModel.Clase = db.Clasas.SqlQuery("select distinct a.IdClasa,a.NumeClasa, a.Descriere,a.IdPlan, a.NrClasa from Clasas a, Profesors b, IncadrareProfs c where a.IdClasa= c.idClasa and b.IdProfesor= c.IdProfesor and b.user_Id=@userId", new SqlParameter("@userId", User.Identity.GetUserId())).ToList();
            if (viewModel.Clase== null || viewModel.Clase.Count()==0)
            {
                ModelState.AddModelError("", "Nu sunteti incadrat la nici o clasa!");
                return View(viewModel);
            }
            if (id != null)
            {
                ViewBag.IdClasa = id.Value;
                viewModel.AnScolar = db.AnScolars.Find(viewModel.Clase.Where(
                         i => i.IdClasa == id.Value).Single().PlanDeInvatamant.IdAn);

               
                viewModel.Elevi = db.Elevi.SqlQuery("select * from Elevs a, Incadrares b where a.IdElev=b.IdElev and b.idClasa=@id order by a.Nume, a.Prenume", new SqlParameter("@id", id.Value)).ToList();
               
            }
            if (idElev != null)
            {
                ViewBag.IdElev = idElev;
                Elev elev = db.Elevi.Find(idElev);
                if (elev != null)
                    ViewBag.NumeElev = elev.Nume + " " + elev.Prenume;
                viewModel.Materii =
                      db.Materii.SqlQuery("select * from Materies a, MateriePlanInvatamants b, PlanInvatamants c where a.IdMaterie= b.Materie_IdMaterie and c.IdPlan= b.PlanInvatamant_IdPlan and exists (select * from Clasas d where d.IdPlan = c.IdPlan and d.IdClasa = @id) order by a.NumeMaterie", new SqlParameter("@id", id.Value)).ToList();
                viewModel.Note = db.Cataloage.SqlQuery("select * from Catalogs where exists (select * from Incadrares where idClasa=@id and IdElev=@idElev and IdxIncadrare= Catalogs.IdxIncadrare)", new SqlParameter("@id", id.Value), new SqlParameter("@idElev", idElev.Value)).ToList();
                var userId = User.Identity.GetUserId();
                var listaMateriiProf = db.Materii.SqlQuery("select * from Materies a, ProfesorMateries b, Profesors c, IncadrareProfs d where a.IdMaterie= b.Materie_IdMaterie and b.Profesor_IdProfesor= c.IdProfesor and c.user_Id=@userId and a.IdMaterie= d.idMaterie and d.IdProfesor=c.IdProfesor and d.idClasa=@id", new SqlParameter("@userId", userId), new SqlParameter("@id", id.Value)).ToList();
                if (listaMateriiProf!=null)
                {
                    foreach(var item in listaMateriiProf)
                    {
                        viewModel.ListIdMateriiAsignate = new List<int>();
                        viewModel.ListIdMateriiAsignate.Add(item.IdMaterie);
                    }
                }
                var eDiriginte = db.IncadrariDiriginte.SqlQuery("select * from IncadrareDirigintes a, Profesors b where a.IdProfesor=b.IdProfesor and b.user_Id=@userId and a.idClasa=@idClasa", new SqlParameter("@userId", userId), new SqlParameter("@idClasa", id.Value)).ToList();
                viewModel.EDiriginte = eDiriginte;
            }
           

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            var viewModel = new CatalogIndexData();
            ViewBag.listaAni = db.AnScolars.ToList().OrderByDescending(x => x.InceputAnScolar);
            int idAn = int.Parse(col[1]);
            string numeClasa = col[0];
            int? id = null;

            if (idAn != 0)
            {
                //db.Clasas.SqlQuery("select * from Clasas where exists (select idPlan from PlanInvatamants where IdAn=@idAn and IdPlan= Clasas.IdPlan) ", paramAn, paramNumeClasa).ToList();
                ViewBag.IdAn = idAn;
                ViewBag.NumeClasa = numeClasa;
                SqlParameter paramAn = new SqlParameter("@idAn", idAn);
                SqlParameter paramNumeClasa = new SqlParameter("@numeClasa", numeClasa);
                viewModel.Clase = db.Clasas.SqlQuery("select * from Clasas a, Profesors b, IncadrareProfs c  where exists (select idPlan from PlanInvatamants where IdAn=@idAn and IdPlan= a.IdPlan) and a.IdClasa = c.idClasa and b.IdProfesor = c.IdProfesor and b.user_Id = @userId and a.NumeClasa=@numeClasa order by a.NumeClasa", new SqlParameter("@userId", User.Identity.GetUserId()), paramAn, paramNumeClasa).ToList();
                if (viewModel.Clase != null && viewModel.Clase.Count() > 0)
                {
                    id = viewModel.Clase.ToList()[0].IdClasa;
                    viewModel.AnScolar = db.AnScolars.Find(idAn);

                    if (id != null)
                    {
                        ViewBag.IdClasa = id;

                        viewModel.Elevi = db.Elevi.SqlQuery("select * from Elevs a, Incadrares b where a.IdElev=b.IdElev and b.idClasa=@id order by a.Nume, a.Prenume", new SqlParameter("@id", id.Value)).ToList();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nu aveti incadrare la clasa cautata!");
                    return View(viewModel);
                }


            }

            return View(viewModel);
        }

      

        // GET: Catalogs/Create
        public ActionResult Create( int idClasa, int idMaterie, int idElev)
        {
            Materie mat = db.Materii.Find(idMaterie);
            Catalog cat = new Catalog();
                      
            Elev elev = db.Elevi.Find(idElev);
            if (elev != null)
                ViewBag.NumeElev = elev.Nume +" "+ elev.Prenume;
            ViewBag.idClasa = idClasa;
            ViewBag.idElev = idElev;
           
            if (mat != null)
                ViewBag.NumeMaterie = mat.NumeMaterie;
            cat.DataNota = DateTime.Now;
            return View(cat);
        }

        // POST: Catalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Catalog catalog, FormCollection col)
        {
            if (ModelState.IsValid)

            {
                int paramclass = int.Parse(col[1]);
                int paramElev = int.Parse(col[2]);
                Incadrare elevIncadrat = db.Incadrari.Where(x => x.idClasa == paramclass).Where(x => x.IdElev == paramElev).Single();
                catalog.IdxIncadrare = elevIncadrat.IdxIncadrare;
                catalog.Incadrare = elevIncadrat;
                db.Entry(catalog.Incadrare.AnScolar).State = EntityState.Unchanged;
                db.Entry(catalog.Incadrare.Clasa.PlanDeInvatamant).State = EntityState.Unchanged;
                db.Entry(catalog.Incadrare.Clasa).State = EntityState.Unchanged;
                db.Entry(catalog.Incadrare.Clasa.PlanDeInvatamant.AnScolar).State = EntityState.Unchanged;
                db.Entry(catalog.Incadrare.Elev).State = EntityState.Unchanged;
                db.Cataloage.Add(catalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          
            return View(catalog);
        }
        public ActionResult MotiveazaAbsenta(int? idNota)
        {
            if (idNota == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var absenta = db.Cataloage.Find(idNota);
            if (absenta == null)
            {
                return HttpNotFound();
            }
            return View(absenta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MotiveazaAbsenta(Catalog absenta)
        {
            if (ModelState.IsValid)
            {
                 db.Entry(absenta).State = EntityState.Modified;
               
                var incadrare = db.Incadrari.Find(absenta.IdxIncadrare);
                absenta.Incadrare = incadrare;
                var materie = db.Materii.Find(absenta.IdMaterie);
                absenta.Materie = materie;
                db.Entry(absenta.Incadrare).State = EntityState.Unchanged;
                db.Entry(absenta.Materie).State = EntityState.Unchanged;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(absenta);
        }
        public ActionResult AdaugaAbsenta(int idClasa, int idMaterie, int idElev)
        {
            Catalog cat = new Catalog();
            Elev elev = db.Elevi.Find(idElev);
            if (elev != null)
                ViewBag.NumeElev = elev.Nume + " " + elev.Prenume;
            ViewBag.idClasa = idClasa;
            ViewBag.idElev = idElev;
            Materie mat = db.Materii.Find(idMaterie);
            if (mat != null)
                ViewBag.NumeMaterie = mat.NumeMaterie;
            cat.DataAbsenta = DateTime.Now;
            return View(cat);
        }

        // POST: Catalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdaugaAbsenta(Catalog catalog, FormCollection col)
        {
            if (ModelState.IsValid)

            {
                int paramclass = int.Parse(col[1]);
                int paramElev = int.Parse(col[2]);
                Incadrare elevIncadrat = db.Incadrari.Where(x => x.idClasa == paramclass).Where(x => x.IdElev == paramElev).Single();
                catalog.IdxIncadrare = elevIncadrat.IdxIncadrare;
                catalog.Incadrare = elevIncadrat;
                db.Entry(catalog.Incadrare.AnScolar).State = EntityState.Unchanged;
                db.Entry(catalog.Incadrare.Clasa.PlanDeInvatamant).State = EntityState.Unchanged;
                db.Entry(catalog.Incadrare.Clasa).State = EntityState.Unchanged;
                db.Entry(catalog.Incadrare.Clasa.PlanDeInvatamant.AnScolar).State = EntityState.Unchanged;
                db.Entry(catalog.Incadrare.Elev).State = EntityState.Unchanged;
                db.Cataloage.Add(catalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(catalog);
        }

      

        // GET: Catalogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Cataloage.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // POST: Catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catalog catalog = db.Cataloage.Find(id);
            db.Cataloage.Remove(catalog);
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
