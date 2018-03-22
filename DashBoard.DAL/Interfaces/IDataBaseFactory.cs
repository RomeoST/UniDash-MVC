using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;

namespace DashBoard.DAL.Interfaces
{
    public interface IDataBaseFactory : IDisposable
    {
        DutContext Get();
    }
}
