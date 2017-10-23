using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    public class SubmissionManager : ISubmissionDoc
    {
        public DutContext DataBase { get; set; }

        public SubmissionManager(DutContext db) => DataBase = db;

        public void Create(SubmissionDoc doc)
        {
            DataBase.SubmissionDocs.Add(doc);
            DataBase.SaveChanges();
        }

        public async Task CreateAsync(SubmissionDoc doc)
        {
            DataBase.SubmissionDocs.Add(doc);
            await DataBase.SaveChangesAsync();
        }

        public void Delete(SubmissionDoc doc)
        {
            DataBase.Entry(doc).State = EntityState.Deleted;
            DataBase.SaveChanges();
        }

        public async Task DeleteAsync(SubmissionDoc doc)
        {
            DataBase.Entry(doc).State = EntityState.Deleted;
            await DataBase.SaveChangesAsync();
        }

        public async Task<SubmissionDoc> FindSubmissionById(int id)
        {
            return await DataBase.SubmissionDocs.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<SubmissionDoc> FindSubmissionByName(string name)
        {
            return await DataBase.SubmissionDocs.FirstOrDefaultAsync(p => p.FullName == name);
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
