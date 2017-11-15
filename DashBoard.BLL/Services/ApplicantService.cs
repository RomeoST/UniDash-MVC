using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;

namespace DashBoard.BLL.Services
{
    public class ApplicantService : IApplicantService
    {
        private IUnitOfWork DataBase { get; set; }

        public ApplicantService(IUnitOfWork uof) => DataBase = uof;

        /// <summary>
        /// Створення абітурієнта
        /// </summary>
        /// <param name="app">Дані про абітурієнта</param>
        /// <returns></returns>
        public async Task<OperationDetails> Create(Applicant applicant)
        {
            try
            {
                var app = await DataBase.ApplicantManager.Find(p => p.PhoneApplicant == applicant.PhoneApplicant ||
                                                                    p.MailApplicant == applicant.MailApplicant || p.NameApplicant == applicant.NameApplicant);
                if (app != null)
                    return new OperationDetails(false, "Такий абітурієнт вже існує\nId - " + app.FirstOrDefault().ApplicantId, "");

                await DataBase.ApplicantManager.CreateAsync(applicant);
                return new OperationDetails(true, "Абітурієнт доданий у базу!", "");
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
        public async Task<OperationDetails> Edit(Applicant app)
        {
            //TODO: Дописать проверки на корректность вводимых данных
            var result = await DataBase.ApplicantManager.Find(app.ApplicantId);
            if(result == null)
                return new OperationDetails(false, "Помилка при додавані, повторіть спробу пізніше", "");

            result.NameApplicant = app.NameApplicant;
            result.Address = app.Address;
            result.MailApplicant = app.MailApplicant;
            result.MarkResult = app.MarkResult;
            result.PhoneApplicant = app.PhoneApplicant;
            result.SchoolCollege = app.SchoolCollege;
            result.Speciality = app.Speciality;
            await DataBase.SaveAsync();

            return new OperationDetails(true, "Абітурієнт збережений!", "");
        }

        /// <summary>
        /// Видалення абітурієнта
        /// </summary>
        /// <param name="app">Дані про абітурієнта, перевірка по id</param>
        /// <returns></returns>
        public async Task<OperationDetails> Delete(Applicant app)
        {
            var result = await DataBase.ApplicantManager.Find(app.ApplicantId);
            if(result == null)
                return new OperationDetails(false, "Помилка при видалені, повторіть спробу пізніше", "");

            await DataBase.ApplicantManager.DeleteAsync(app);
            return new OperationDetails(true, "Абітурієнт видалений", "");
        }

        public async Task<Applicant> Find(int id) => await DataBase.ApplicantManager.Find(id);

        /// <summary>
        /// Пошук абітурієнта через where
        /// </summary>
        /// <param name="where">Параметр пошуку</param>
        /// <returns></returns>
        public async Task<IEnumerable<Applicant>> Find(Func<Applicant, bool> @where) => await DataBase.ApplicantManager.Find(where);

        /// <summary>
        /// Отримати всіх абітурієнтів
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Applicant>> GetAll() => await DataBase.ApplicantManager.GetAll();

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
