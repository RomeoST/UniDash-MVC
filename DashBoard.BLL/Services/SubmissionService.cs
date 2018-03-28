using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Infrastructure;
using DashBoard.DAL.Repositories;
using DashBoard.Model.Models;

namespace DashBoard.BLL.Services
{
    public class SubmissionService : ISubmissionService
    {
        private IUnitOfWork DataBase { get; }
        private ISubmissionRepository submissionRepository { get; }

        public SubmissionService(IUnitOfWork uof, ISubmissionRepository submissionRepository)
        {
            DataBase = uof;
            this.submissionRepository = submissionRepository;
        }

        public async Task<OperationDetails> Create(SubmissionDoc dto)
        {
            var found = await submissionRepository.GetAsync(p=>p.FullName == dto.FullName);
            if (found != null) return new OperationDetails(false, "Такий абітурієнт вже є у базі", "");

            submissionRepository.Add(dto);
            await SaveSubmission();

            return new OperationDetails(true, "Документ абітурієнта створений", "");
        }

        public async Task<OperationDetails> Edit(SubmissionDoc dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationDetails> Delete(SubmissionDoc dto)
        {
            var found = await submissionRepository.GetAsync(p=>p.Id == dto.Id);
            if (found == null) return new OperationDetails(false, "Абітурієнт не знайдений", "");

            submissionRepository.Delete(dto);
            await SaveSubmission();

            return new OperationDetails(true, "Документ абітурієнта видалений", "");
        }

        public async Task SaveSubmission()
        {
            await DataBase.CommitAsync();
        }
    }
}
