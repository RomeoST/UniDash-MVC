using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.EF;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    /// <summary>
    /// Клас логіки системи абітурієнтів
    /// </summary>
    public class ApplicantManager : IApplicant
    { 
        public DutContext DataBase { get; set; }
        public ApplicantManager(DutContext context) => DataBase = context;

        /// <summary>
        /// Створити абітуієнта
        /// </summary>
        /// <param name="app">Екземпляр класу абітурієнта</param>
        public void Create(Applicant app)
        {
            DataBase.Applicants.Add(app);
            DataBase.SaveChanges();
        }

        /// <summary>
        /// Створити абітурєнта .Асінхроний режим
        /// </summary>
        /// <param name="app">Екземпляр класу абітурієнта</param>
        public async Task CreateAsync(Applicant app)
        {
            DataBase.Applicants.Add(app);
            await DataBase.SaveChangesAsync();
        }

        /// <summary>
        /// Видалити абітурієнта
        /// </summary>
        /// <param name="app">Екземпляп класу для видалення</param>
        public void Delete(Applicant app)
        {
            DataBase.Entry(app).State = EntityState.Deleted;
            DataBase.SaveChanges();
        }

        /// <summary>
        /// Асінхроне видалення абітурієнта
        /// </summary>
        /// <param name="app">Укземпляр класу для видання</param>
        /// <returns></returns>
        public async Task DeleteAsync(Applicant app)
        {
            DataBase.Entry(app).State = EntityState.Deleted;
            await DataBase.SaveChangesAsync();
        }

        /// <summary>
        /// Знайти абітурієнта по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Applicant> Find(int id) => await DataBase.Applicants.FirstOrDefaultAsync(p => p.ApplicantId == id);

        /// <summary>
        /// Пошук абітурієнтів за домогою Linq
        /// </summary>
        /// <param name="where">Параметр пошуку</param>
        /// <returns></returns>
        public async Task<IEnumerable<Applicant>> Find(Expression<Func<Applicant, bool>> @where)
        {
            return await DataBase.Applicants.Where(where).ToListAsync();
        } 

        /// <summary>
        /// Отримати всіх абітурієнтів
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Applicant>> GetAll() => await DataBase.Applicants.ToListAsync();

        public void Dispose() =>  DataBase.Dispose();
    }
}
