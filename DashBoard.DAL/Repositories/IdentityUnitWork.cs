using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Identity;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DashBoard.DAL.Repositories
{
    /// <summary>
    /// Шаблон UnitOfWork - призван отслеживать все изменения данных, которые мы производим с доменной моделью в рамках бизнес-транзакции.
    /// После того, как бизнес-транзакция закрывается, все изменения попадают в БД в виде единой транзакции.
    /// </summary>
    public class IdentityUnitWork : IUnitOfWork
    {
        private readonly DutContext db;

        private readonly DutUserManager userManager;
        private readonly DutRoleManager roleManager;
        private readonly IClientManager clientManager;
        private readonly ISubmissionDoc submissionManager;
        private readonly IApplicant     applicantManager;

        public IdentityUnitWork(string connectionString)
        {
            db = new DutContext(connectionString);
            userManager = new DutUserManager(new UserStore<DutUser>(db));
            roleManager = new DutRoleManager(new RoleStore<DutRole>(db));
            clientManager = new ClientManager(db);
            applicantManager = new ApplicantManager(db);
            //submissionManager = new SubmissionManager(db);
        }

        public DutUserManager UserManager => userManager;
        public DutRoleManager RoleManager => roleManager;
        public IClientManager ClientManager => clientManager;
        public ISubmissionDoc SubmissionManager => submissionManager;
        public IApplicant ApplicantManager => applicantManager;

        /// <summary>
        /// Збереження всіх даних які відбувалися з БД
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                    applicantManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
