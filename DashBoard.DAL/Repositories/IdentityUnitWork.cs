using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Identity;
using DashBoard.DAL.Infrastructure;
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
        private readonly IDataBaseFactory dataBaseFactory;
        private DutContext dataContext;

        public IdentityUnitWork(IDataBaseFactory dataBaseFactory)
        {
            this.dataBaseFactory = dataBaseFactory;
        }

        protected DutContext DataContext => dataContext ?? (dataContext = dataBaseFactory.Get());

        /// <summary>
        /// Збереження всіх даних які відбувалися з БД
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await dataContext.SaveChangesAsync();
        }

        public void Commit()
        {
            dataContext.SaveChanges();
        }
    }
}
