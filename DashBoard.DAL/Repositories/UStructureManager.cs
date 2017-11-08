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
    public class UStructureManager : IUStructure
    {
        private DutContext DataBase { get; }
        public UStructureManager(DutContext context) => DataBase = context;

        public void CreateFaculty(Faculty faculty)
        {
            DataBase.Faculties.Add(faculty);
            DataBase.SaveChanges();
        }

        public void DeleteFaculty(Faculty faculty)
        {
            DataBase.Entry(faculty).State = EntityState.Deleted;
            DataBase.SaveChanges();
        }

        public IEnumerable<Faculty> FindFaculties(Func<Faculty, bool> @where) => DataBase.Faculties.Where(where);

        public void CreateInstitute(Institute institute)
        {
            DataBase.Institutes.Add(institute);
            DataBase.SaveChanges();
        }

        public void DeleteInstitute(Institute institute)
        {
            DataBase.Entry(institute).State = EntityState.Deleted;
            DataBase.SaveChanges();
        }

        public IEnumerable<Institute> FindInstitutes(Func<Institute, bool> @where) => DataBase.Institutes.Where(where);

        public void CreateDepartment(Department department)
        {
            DataBase.Departments.Add(department);
            DataBase.SaveChanges();
        }

        public void DeleteDepartment(Department department)
        {
            DataBase.Entry(department).State = EntityState.Deleted;
            DataBase.SaveChanges();
        }

        public IEnumerable<Department> FindDepartments(Func<Department, bool> @where) => DataBase.Departments.Where(where);

        public void Dispose() => DataBase.Dispose();
    }
}
