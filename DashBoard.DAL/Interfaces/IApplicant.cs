using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Interfaces
{
    public interface IApplicant : IDisposable
    {
        void Create(Applicant app);
        Task CreateAsync(Applicant app);

        void Delete(Applicant app);
        Task DeleteAsync(Applicant app);

        Task<Applicant> Find(int id);
        IEnumerable<Applicant> Find(Func<Applicant, bool> where);

        Task<IEnumerable<Applicant>> GetAll();
    }
}
