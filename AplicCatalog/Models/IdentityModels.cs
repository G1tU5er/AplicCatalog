using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using AplicCatalog.viewModel;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AplicCatalog.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
     
             
        public bool Valid
        {
            get;
            set;
        }

        [Display(Name = "Data Activare Cont")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataActivareCont
        {
            get;
            set;
        }

        [Display(Name = "Data Expirare Cont")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataExpirareCont
        {
            get;
            set;
        }
   
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("myConStr", throwIfV1Schema: false)
        {
        }
    

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Profesor> Profesori { get; set; }
        public virtual DbSet<Materie> Materii { get; set; }
        public virtual DbSet<Elev> Elevi { get; set; }
      
        public virtual DbSet<Catalog> Cataloage { get; set; }
       
        public  DbSet<PlanInvatamant> PlanuriScolare { get; set; }

        public virtual System.Data.Entity.DbSet<Clasa> Clasas { get; set; }

        public  System.Data.Entity.DbSet<AnScolar> AnScolars { get; set; }
        public virtual System.Data.Entity.DbSet<Incadrare> Incadrari { get; set; }
        public virtual System.Data.Entity.DbSet<IncadrareProf> IncadrariProf { get; set; }
        public virtual System.Data.Entity.DbSet<IncadrareDiriginte> IncadrariDiriginte { get; set; }
        public virtual System.Data.Entity.DbSet<IncadrareMateriePlan> IncadrariMaterii { get; set; }
  
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
           
        }


    }
}