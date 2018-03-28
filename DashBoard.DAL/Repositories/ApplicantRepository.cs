using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Infrastructure;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    /// <summary>
    /// Клас логіки системи абітурієнтів
    /// </summary>
    public class ApplicantRepository : RepositoryBase<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public interface IApplicantRepository : IRepository<Applicant> { }
}
