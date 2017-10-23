using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Interfaces
{
    public interface ISubmissionDoc : IDisposable
    {
        void Create(SubmissionDoc doc);
        Task CreateAsync(SubmissionDoc doc);
        void Delete(SubmissionDoc doc);
        Task DeleteAsync(SubmissionDoc doc);

        Task<SubmissionDoc> FindSubmissionById(int id);
        Task<SubmissionDoc> FindSubmissionByName(string name);
    }
}
