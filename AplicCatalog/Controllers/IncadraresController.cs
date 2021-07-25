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
using System.Text;

namespace AplicCatalog.Controllers
{
    [Authorize(Roles = "Secretar")]
    public class IncadraresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Incadrares
      
        public ActionResult Index(int? id)
        {
            var viewModel = new IncadrareIndexData();
            ViewBag.listaAni = db.AnScolars.ToList().OrderByDescending(x => x.InceputAnScolar);
          
            viewModel.Clase = db.Clasas
                    .Include(i => i.PlanDeInvatamant.AnScolar).OrderBy(x =>x.NumeClasa).ToList();

            if (id != null)
            {
                ViewBag.IdClasa = id.Value;
                viewModel.anScolar = db.AnScolars.Find(viewModel.Clase.Where(
                         i => i.IdClasa == id.Value).Single().PlanDeInvatamant.IdAn);

                viewModel.Elevi = db.Elevi.SqlQuery("Select * from Elevs  where exists(select IdElev from Incadrares where IdClasa = @id and IdElev = Elevs.IdElev) order by Nume, Prenume", new SqlParameter("@id", id.Value)).ToList();
            }

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            var viewModel = new IncadrareIndexData();
            ViewBag.listaAni = db.AnScolars.ToList().OrderByDescending(x => x.InceputAnScolar);
            int idAn = int.Parse(col[1]);
            string numeClasa = col[0];
            int? id=null;

            if (idAn !=0)
            {
                ViewBag.IdAn = idAn;
                ViewBag.NumeClasa = numeClasa;
                SqlParameter paramAn = new SqlParameter("@idAn", idAn);
                SqlParameter paramNumeClasa = new SqlParameter("@numeClasa", numeClasa);
               viewModel.Clase = db.Clasas.SqlQuery("select * from Clasas where exists (select idPlan from PlanInvatamants where IdAn=@idAn and IdPlan= Clasas.IdPlan) and NumeClasa=@numeClasa order by NumeClasa", paramAn, paramNumeClasa).ToList();

                if (viewModel.Clase != null && viewModel.Clase.Count() > 0)
                {
                    id = viewModel.Clase.ToList()[0].IdClasa;
                    viewModel.anScolar = db.AnScolars.Find(idAn);

                    if (id != null)
                    {
                        ViewBag.IdClasa = id;

                        viewModel.Elevi = db.Elevi.SqlQuery("Select * from Elevs  where exists(select IdElev from Incadrares where IdClasa = @id and IdElev = Elevs.IdElev) order by Nume, Prenume", new SqlParameter("@id", id.Value)).ToList();
                    }
                }


            }
        
           return View(viewModel);
        }

        // GET: Incadrares/Create
        public ActionResult Create(int? id)
        {
            Incadrare incadrareNoua = new Incadrare();
            incadrareNoua.Clasa = db.Clasas.Find(id);
            ViewBag.listaElevi = db.Elevi.ToList().OrderBy(x =>x.Nume).OrderBy(x =>x.Prenume);
            return View(incadrareNoua);
        }

        // POST: Incadrares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Incadrare incadrare, string[] eleviSelectati)
        {
                  
            if (ModelState.IsValid)
            {
               
                foreach (var id in eleviSelectati)
                {
                    var elev = db.Elevi.Find(int.Parse(id));
                    incadrare.Elev = elev;
                    db.Entry(incadrare.Elev).State = EntityState.Unchanged;
                    var clasa = db.Clasas.Find(incadrare.Clasa.IdClasa);
                    if (clasa != null)
                        incadrare.Clasa = clasa;
                    db.Entry(incadrare.Clasa).State = EntityState.Unchanged;
                    db.Incadrari.Add(incadrare);
                    db.SaveChanges();
                    //
                }
              
                return RedirectToAction("Index");
            }

            return View(incadrare);
        }
     
        // GET: Incadrares/Export/5
        public ActionResult ExportElevi(int? id, int nrClasa)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            ViewBag.IdClasa = id;
            Clasa cls = new Clasa();
            List<AnScolar> anScolarAsignat = db.AnScolars.SqlQuery(" select * from Clasas a, PlanInvatamants b, AnScolars c where a.IdPlan= b.IdPlan and a.IdClasa=@idClasa and b.IdAn=c.IdAn", new SqlParameter("@idClasa", id.Value)).ToList();
            if (anScolarAsignat!=null && anScolarAsignat.Count() > 0)
            {
                var planInvatamantViitor = db.PlanuriScolare.SqlQuery("select * from PlanInvatamants a, AnScolars b where a.IdAn=b.IdAn and a.NrClasa=@nrClasa and b.InceputAnScolar=@inceputAnScolar", new SqlParameter("@nrClasa", nrClasa+1), new SqlParameter("@inceputAnScolar", anScolarAsignat[0].InceputAnScolar + 1)).ToList();
                ViewBag.AnulNouAsignat = (anScolarAsignat[0].InceputAnScolar + 1) + "-" + (anScolarAsignat[0].SfarsitAnScolar + 1);
                if (planInvatamantViitor != null && planInvatamantViitor.Count() > 0)
                {
                    ViewBag.planInvatamantNou = planInvatamantViitor[0].Nume;
                    ViewBag.NrClasa = nrClasa + 1;
                    cls.PlanDeInvatamant = planInvatamantViitor[0];
                    cls.IdPlan = planInvatamantViitor[0].IdPlan;
                }
                else
                {
                    ModelState.AddModelError("", "Nu exista plan de invatamant pentru anul scolar urmator!");
                    return View(cls);
                }
            }
            else
            {
                ModelState.AddModelError("","Anul scolar urmator nu exista in baza de date!");
                return View(cls);
            }
            
          
            return View(cls);
        
        }
      
        // POST: Incadrares/Export/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult ExportElevi(FormCollection col, Clasa clasa)
        {
           
            int paramclassVechi = int.Parse(col[1]);

            IEnumerable<Incadrare> listaIncadrari = from r in db.Incadrari
                                                    where r.Clasa.IdClasa == paramclassVechi
                                                    select r;
           
            if (listaIncadrari != null && listaIncadrari.ToList().Count()>0)
            {
               if (ModelState.IsValid)
                {
                    var planInvatamant = db.PlanuriScolare.Find(clasa.IdPlan);
                    clasa.PlanDeInvatamant = planInvatamant;
                    db.Entry(clasa.PlanDeInvatamant.AnScolar).State = EntityState.Unchanged;
                    db.Entry(clasa.AnScolar).State = EntityState.Unchanged;
                    db.Entry(clasa.PlanDeInvatamant).State = EntityState.Unchanged;
                    db.Clasas.Add(clasa);
                    db.SaveChanges();
                   
                }

                foreach (var item in listaIncadrari.ToList())
                {
                    Incadrare obj = new Incadrare();

                    obj.Clasa = clasa;

                    obj.Elev = db.Incadrari.Find(item.IdxIncadrare).Elev;

                    db.Entry(obj.Elev).State = EntityState.Unchanged;

                    db.Entry(obj.Clasa).State = EntityState.Unchanged;
                    db.Incadrari.Add(obj);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Acesta clasa nu are nici un elev incadrat!");
                return View(clasa);
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportFile(FormCollection col)
        {
            string[] columnNames = new string[] { "Nume", "Prenume", "Mama","Tata" };

            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            foreach (string columnName in columnNames)
            {
                //Add the Header row for CSV file.
                csv += columnName + ',';
            }

            //Add new line.
            csv += "\r\n";

            int paramclassVechi = int.Parse(col[1]);

            IEnumerable<Incadrare> listaIncadrari = from r in db.Incadrari
                                                    where r.Clasa.IdClasa == paramclassVechi
                                                    select r;
            if (listaIncadrari != null )
            {
                foreach (var item in listaIncadrari.ToList())
                {
                    //Add the Data rows.
                    Elev elev = db.Incadrari.Find(item.IdxIncadrare).Elev;
                    csv += elev.Nume.Replace(",", ";") + ',';
                    csv += elev.Prenume.Replace(",", ";") + ',';
                    csv += elev.Mama.Replace(",", ";") + ',';
                    csv += elev.Tata.Replace(",", ";") + ',';

                    //Add new line.
                    csv += "\r\n";
                }
              
            }

            //Download the CSV file.
            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "application/text", "ExportElevi.csv");

        }

        // GET: Incadrares/Delete/5
        public ActionResult Delete(int? idElev, int idClasa)
        {
            if (idElev == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elev incadrare = db.Elevi.Find(idElev);
            if (incadrare == null)
            {
                return HttpNotFound();
            }
            return View(incadrare);
        }

        // POST: Incadrares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idElev,int idClasa)
        {
            SqlParameter paramIdElev = new SqlParameter("@idElev", idElev);
            SqlParameter paramIdClasa = new SqlParameter("@idClasa", idClasa);
            Incadrare incadrare = db.Incadrari.SqlQuery("Select * from Incadrares where  IdClasa = @idClasa and IdElev = @idElev",paramIdClasa,paramIdElev).ToList().Single();
            db.Incadrari.Remove(incadrare);
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
