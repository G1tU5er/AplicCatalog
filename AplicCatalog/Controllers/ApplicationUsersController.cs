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
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AplicCatalog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            var context = new ApplicationDbContext();
            ViewBag.RoleId = new SelectList(context.Roles.Where(x => x.Name != "Admin"), "Name", "Name");
            return View();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model, params string[] selectedRoles)
        {
           
            if (ModelState.IsValid)
            {
              
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email,  Valid = true, DataActivareCont = DateTime.Now, DataExpirareCont = DateTime.Now };
                var result =  UserManager.Create(user, model.Password);
               if (selectedRoles != null && (selectedRoles.Contains("Profesor") || selectedRoles.Contains("Diriginte")))
                {
                   Profesor ut = new Profesor();
                        try
                        {
                            ut.user = user;
                            db.Entry(ut.user).State = EntityState.Unchanged;
                            db.Profesori.Add(ut);
                            db.SaveChanges();
                        }
                        catch (Exception ex) { throw ex; }
                
                  
                }

                if (result.Succeeded)
                {
                   
                    if (selectedRoles != null)
                    {
                        var result2 = UserManager.AddToRoles(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(db.Roles.ToList(), "Name", "Name");
                            return View();
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", result.Errors.First());
                    ViewBag.RoleId = new SelectList(db.Roles.Where(x => x.Name != "Admin"), "Name", "Name");
                    return View();
                }
                return RedirectToAction("Index");

            }

            // If we got this far, something failed, redisplay form
            ViewBag.RoleId = new SelectList(db.Roles.Where(x => x.Name != "Admin"), "Name", "Name");
            return View(model);

        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var applicationUser = UserManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            var userRoles =  UserManager.GetRoles(applicationUser.Id);
            var context = new ApplicationDbContext();

            return View(new AppUserViewModel()
            {
                DataActivareCont = applicationUser.DataActivareCont,
            DataExpirareCont = applicationUser.DataExpirareCont,
            UserName = applicationUser.UserName,
            Id = applicationUser.Id,
            Email = applicationUser.Email,
            Valid = applicationUser.Valid,
                RolesList = context.Roles.Where(x => x.Name != "Admin").ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }
          
        

    // POST: ApplicationUsers/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(AppUserViewModel editUser, params string[] selectedRole)
        {
          
            if (ModelState.IsValid)
            {
            var user =  UserManager.FindById(editUser.Id);
            if (user == null)
            {
                return HttpNotFound();
            }

                user.Id = editUser.Id;
                user.UserName = editUser.UserName;
                user.DataActivareCont = editUser.DataActivareCont;
                user.DataExpirareCont = editUser.DataExpirareCont;
               user.Valid = editUser.Valid;
                user.LockoutEnabled = editUser.LockoutEnabled;
                user.Email = editUser.Email;
               
                var userRoles = UserManager.GetRoles(user.Id);
                selectedRole = selectedRole ?? new string[] { };
                var result = UserManager.AddToRoles(user.Id,
                    selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = UserManager.RemoveFromRoles(user.Id,
                    userRoles.Except(selectedRole).ToArray<string>());
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Ceva a esuat.");
            return View();
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            try
            {
                db.Users.Remove(applicationUser);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Nu se poate sterge un profesor incadrat la clasa!");
                return View(applicationUser);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)

                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
