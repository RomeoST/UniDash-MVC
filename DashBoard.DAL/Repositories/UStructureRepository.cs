using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Infrastructure;

namespace DashBoard.DAL.Repositories
{
    public class UStructureRepository<T> : RepositoryBase<T>, IUStructureRepository<T> where T : class 
    {
        protected UStructureRepository(IDataBaseFactory factory) : base(factory) { }
    }

    public interface IUStructureRepository<T> : IRepository<T> where T : class 
    { }
}
