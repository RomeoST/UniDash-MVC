using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    /// <summary>
    /// Клас логіки системи абітурієнтів
    /// </summary>
    public class ApplicantManager : RepositoryBase<Applicant>, IApplicant
    {
        public ApplicantManager(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public interface IApplicant : IRepository<Applicant> { }
}
