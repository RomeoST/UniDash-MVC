using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BLL.Infrastructure;
using DashBoard.Model.Models;

namespace DashBoard.BLL.Interfaces
{
    public interface IRoleService : IDisposable
    {
        Task<OperationDetails> Create(DutRole role);
        Task<OperationDetails> Edit(DutRole role);
        Task<OperationDetails> Delete(string id);
        List<DutRole> GetAll();
    }
}
