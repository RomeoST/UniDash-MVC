using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.Identity;

namespace DashBoard.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DutUserManager UserManager { get;}
        IClientManager ClientManager { get; }
        ISubmissionDoc SubmissionManager { get; }
        IApplicant     ApplicantManager { get; }
        IUStructure    UStructManager { get; }
        DutRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}
