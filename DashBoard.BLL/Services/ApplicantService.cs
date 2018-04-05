using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Infrastructure;
using DashBoard.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DashBoard.DAL.Repositories;
using Ninject;

namespace DashBoard.BLL.Services
{
    public class ApplicantService : IApplicantService
    {
        public IUnitOfWork DataBase { get; set; }
        public IApplicantRepository ApplicantRepository { get; set; }

        public ApplicantService(IUnitOfWork uof, IApplicantRepository rep)
        {
            DataBase = uof;
            ApplicantRepository = rep;
        }

        /// <summary>
        /// Створення абітурієнта
        /// </summary>
        /// <param name="app">Дані про абітурієнта</param>
        /// <returns></returns>
        public async Task<OperationDetails> CreateApplicant(Applicant applicant)
        {
            try
            {
                var app = await ApplicantRepository.GetAsync(p => p.PhoneApplicant == applicant.PhoneApplicant ||
                                                                    (p.MailApplicant == applicant.MailApplicant && !string.IsNullOrEmpty(applicant.MailApplicant)) ||
                                                                    p.NameApplicant == applicant.NameApplicant);
                if (app != null)
                    return new OperationDetails(false, "Такий абітурієнт вже існує\nId - " + app.ApplicantId, "");

                applicant.DateAdd = DateTime.Now;
                applicant.DateEdit = DateTime.Now;

                ApplicantRepository.Add(applicant);
                await SaveApplicant();
                var newUser = await ApplicantRepository.GetAsync(p => p.PhoneApplicant == applicant.PhoneApplicant);

                return new OperationDetails(true, "Абітурієнт доданий у базу!", $"{newUser.ApplicantId}");
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }

        }

        /// <summary>
        /// Редагування даних абітурієнта
        /// </summary>
        /// <param name="app">Дані про абітурієнта, перевірка по id</param>
        /// <returns></returns>
        public async Task<OperationDetails> EditApplicant(Applicant app)
        {
            //TODO: Дописать проверки на корректность вводимых данных
            var result = ApplicantRepository.GetById(app.ApplicantId);
            if(result == null)
                return new OperationDetails(false, "Помилка при додавані, повторіть спробу пізніше", "");

            result.NameApplicant = app.NameApplicant;
            result.Address = app.Address;
            result.MailApplicant = app.MailApplicant;
            result.MarkResult = app.MarkResult;
            result.PhoneApplicant = app.PhoneApplicant;
            result.SchoolCollege = app.SchoolCollege;
            result.Speciality = app.Speciality;
            result.DateEdit = DateTime.Now;
            await SaveApplicant();

            return new OperationDetails(true, "Абітурієнт збережений!", "");
        }

        /// <summary>
        /// Видалення абітурієнта
        /// </summary>
        /// <param name="app">Дані про абітурієнта, перевірка по id</param>
        /// <returns></returns>
        public async Task<OperationDetails> DeleteApplicant(Applicant app)
        {
            var result = ApplicantRepository.GetById(app.ApplicantId);
            if(result == null)
                return new OperationDetails(false, "Помилка при видалені, повторіть спробу пізніше", "");

            ApplicantRepository.Delete(app);
            await SaveApplicant();
            return new OperationDetails(true, "Абітурієнт видалений", "");
        }

        public Applicant GetApplicantById(int id) => ApplicantRepository.GetById(id);

        /// <summary>
        /// Отримати всіх абітурієнтів
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Applicant>> GetApplicants() => await ApplicantRepository.GetAllAsync();

        public async Task SaveApplicant()
        {
            await DataBase.CommitAsync();
        }
    }
}
