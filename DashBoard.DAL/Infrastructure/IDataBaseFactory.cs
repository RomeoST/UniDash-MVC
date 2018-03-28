using System;
using DashBoard.DAL.EF;

namespace DashBoard.DAL.Infrastructure
{
    public interface IDataBaseFactory : IDisposable
    {
        DutContext Get();
    }
}
