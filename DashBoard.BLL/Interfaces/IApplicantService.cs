using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        Task<Applicant> Find(int id);
        Task<IEnumerable<Applicant>> Find(Expression<Func<Applicant, bool>> where);

        Task<IEnumerable<Applicant>> GetAll();
    }
}
