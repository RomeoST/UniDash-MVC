using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using DashBoard.DAL.Configuration;
using DashBoard.Model.Models;
using DashBoard.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DashBoard.DAL.EF
{
    public class DutContext : IdentityDbContext<DutUser>
    {
        private static bool _firstInit = true; // Что бы не перестоздавать БД при каждом запросе
        public DutContext() { }
        public DutContext(string connectionString) : base(connectionString, throwIfV1Schema: false)
        {
            if (!_firstInit) return;

            Database.Initialize(true);
            _firstInit = false;
        }

        static DutContext()
        {
            //Database.SetInitializer(new CreateDbAlways());
            Database.SetInitializer(new CreateDbIfNot());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new ClientProfileConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<ClientProfile> ClientProfiles { get; set; }
        public virtual DbSet<SubmissionDoc> SubmissionDocs { get; set; }
        public virtual DbSet<Applicant>     Applicants     { get; set; }

        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Institute> Institutes { get; set; }
    }

    public static class HelpContext
    {
        public static void SeedAdminAccount(this DutContext context, string userName, string passaword)
        {
            var userManager = new UserManager<DutUser>(new UserStore<DutUser>(context));

            var user =  userManager.Find(userName, passaword);
            if(user != null) return;

            SeedUserRoles(context,new List<string> {"admin", "user"});

            user = new DutUser {UserName = userName};
            var result = userManager.Create(user, passaword);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "admin");
                var m = new ClientManager(context);
                m.Create(new ClientProfile { Id = user.Id, Enable = true, CreateDate = DateTime.Now });
                m.DataBase.SaveChanges();
            }
            else
            {
                var e = new Exception("Could not add default account");

                var enumerator = result.Errors.GetEnumerator();
                foreach (var resultError in result.Errors)
                {
                    e.Data.Add(enumerator.Current, resultError);
                }
                throw e;
            }
        }

        public static void SeedUserRoles(this DutContext context, List<string> Roles)
        {
            var roleManager = new RoleManager<DutRole>(new RoleStore<DutRole>(context));

            foreach (var role in Roles)
            {
                var roleexists = roleManager.RoleExists(role);
                if (roleexists) continue;

                roleManager.Create(new DutRole{Name = role});
            }
        }
    }
}
