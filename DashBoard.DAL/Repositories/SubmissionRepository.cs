using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Infrastructure;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    public class SubmissionRepository : RepositoryBase<SubmissionDoc>,ISubmissionRepository
    {
        public SubmissionRepository(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public interface ISubmissionRepository : IRepository<SubmissionDoc> { }
}
