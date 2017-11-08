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
    public class UStructService : IUStructService
    {
        private IUnitOfWork DataBase { get; }
        public UStructService(IUnitOfWork uof) => DataBase = uof;

        public OperationDetails CreateInstitite(Institute institute)
        {
            var inst = DataBase.UStructManager.FindInstitutes(p => p.Name == institute.Name);
            if(inst.Any())
                return new OperationDetails(false, "Такий інститут вже існує", "");

            DataBase.UStructManager.CreateInstitute(institute);
            return new OperationDetails(true, "Інститут доданий", "");
        }

        public OperationDetails DeleteInstitute(Institute institute)
        {
            var inst = DataBase.UStructManager.FindInstitutes(p => p.Name == institute.Name);
            if (!inst.Any())
                return new OperationDetails(false, "Такого інституту не існує", "");

            DataBase.UStructManager.DeleteInstitute(institute);
            return new OperationDetails(true, $"{institute.Name} - видалений", "");
        }

        public IEnumerable<Institute> FindInstitute(Func<Institute, bool> @where) => DataBase.UStructManager.FindInstitutes(where);

        public OperationDetails CreateFaculty(Faculty faculty)
        {
            var inst = DataBase.UStructManager.FindFaculties(p => p.Name == faculty.Name);
            if (inst.Any())
                return new OperationDetails(false, "Такий факультет вже існує", "");

            DataBase.UStructManager.CreateFaculty(faculty);
            return new OperationDetails(true, "Факультет доданий", "");
        }

        public OperationDetails DeleteFaculty(Faculty faculty)
        {
            var inst = DataBase.UStructManager.FindFaculties(p => p.Name == faculty.Name);
            if (!inst.Any())
                return new OperationDetails(false, "Такого факультету не існує", "");

            DataBase.UStructManager.DeleteFaculty(faculty);
            return new OperationDetails(true, $"{faculty.Name} - видалений", "");
        }

        public IEnumerable<Faculty> FindFaculty(Func<Faculty, bool> @where) => DataBase.UStructManager.FindFaculties(where);

        public OperationDetails CreateDepartment(Department department)
        {
            var inst = DataBase.UStructManager.FindDepartments(p => p.Name == department.Name);
            if (inst.Any())
                return new OperationDetails(false, "Така спеціальність вже існує", "");

            DataBase.UStructManager.CreateDepartment(department);
            return new OperationDetails(true, "Спеціальність додана", "");
        }

        public OperationDetails DeleteDepartment(Department department)
        {
            var inst = DataBase.UStructManager.FindDepartments(p => p.Name == department.Name);
            if (!inst.Any())
                return new OperationDetails(false, "Такої спецальності не існує", "");

            DataBase.UStructManager.DeleteDepartment(department);
            return new OperationDetails(true, $"{department.Name} - видалений", "");
        }

        public IEnumerable<Department> FindDepartments(Func<Department, bool> where) =>DataBase.UStructManager.FindDepartments(where);

        public void Dispose() => DataBase.Dispose();
    }
}
