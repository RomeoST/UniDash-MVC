using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BLL.Infrastructure;
using DashBoard.Model.Models;
using DashBoard.Model.Models.StructUniversity;

namespace DashBoard.BLL.Interfaces
{
    public interface IUStructService
    {
        Task<OperationDetails> Create(UStructBase structure, TypeStructUniversity type);
        Task<OperationDetails> Delete(UStructBase structure, TypeStructUniversity type);
        Task<UStructBase> GetById(int id, TypeStructUniversity type);
        Task<IEnumerable<UStructBase>> GetAll(TypeStructUniversity type);

        Task<IEnumerable<Department>> GetAdmissionDepartments();

        Task SaveStructure();
    }
}
