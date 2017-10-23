using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;

namespace DashBoard.BLL.Services
{
    public class SubmissionService : ISubmissionService
    {
        private IUnitOfWork DataBase { get; set; }

        public SubmissionService(IUnitOfWork uof) => DataBase = uof;

        public async Task<OperationDetails> Create(SubmissionDoc dto)
        {
            var found = await DataBase.SubmissionManager.FindSubmissionByName(dto.FullName);
            if (found == null)
            {
                await DataBase.SubmissionManager.CreateAsync(dto);
                return new OperationDetails(true, "Документ абітурієнта створений", "");
            }
            return new OperationDetails(false, "Такий абітурієнт вже є у базі", "");
        }

        public async Task<OperationDetails> Edit(SubmissionDoc dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationDetails> Delete(SubmissionDoc dto)
        {
            var found = await DataBase.SubmissionManager.FindSubmissionById(dto.Id);
            if (found != null)
            {
                await DataBase.SubmissionManager.DeleteAsync(dto);
                return new OperationDetails(true, "Документ абітурієнта видалений", "");
            }
            return new OperationDetails(false, "Абітурієнт не знайдений", "");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
