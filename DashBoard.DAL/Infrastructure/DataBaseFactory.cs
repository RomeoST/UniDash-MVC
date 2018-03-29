using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;

namespace DashBoard.DAL.Infrastructure
{
    public class DataBaseFactory : Disposable ,IDataBaseFactory
    {
        private DutContext dataContext;
        private static string _connectionString = "";

        public DataBaseFactory() { }
        public DataBaseFactory(string connectionString) => _connectionString = connectionString;

        public DutContext Get() => dataContext ?? (dataContext = new DutContext(_connectionString));

        protected override void DisposeCore() => dataContext?.Dispose();
    }
}
