using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Interfaces
{
    public interface IUStructure : IDisposable
    {
        void CreateFaculty(Faculty faculty);
        void DeleteFaculty(Faculty faculty);
        IEnumerable<Faculty> FindFaculties(Func<Faculty, bool> where);

        void CreateInstitute(Institute institute);
        void DeleteInstitute(Institute institute);
        IEnumerable<Institute> FindInstitutes(Func<Institute, bool> where);

        void CreateDepartment(Department department);
        void DeleteDepartment(Department department);
        IEnumerable<Department> FindDepartments(Func<Department, bool> where);
    }
}
