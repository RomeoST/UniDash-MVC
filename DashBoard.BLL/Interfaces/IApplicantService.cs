using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BLL.Infrastructure;
using DashBoard.Model.Models;

namespace DashBoard.BLL.Interfaces
{
    public interface IApplicantService : IDisposable
    {
        Task<OperationDetails> Create(Applicant app);
        Task<OperationDetails> Edit(Applicant app);
        Task<OperationDetails> Delete(Applicant app);

        IEnumerable<Applicant> Find(Func<Applicant, bool> where);

        Task<IEnumerable<Applicant>> GetAll();
    }
}
