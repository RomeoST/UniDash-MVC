using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BLL.Infrastructure;
using DashBoard.Model.Models;

namespace DashBoard.BLL.Interfaces
{
    public interface IUStructService : IDisposable
    {
        OperationDetails CreateInstitite(Institute institute);
        OperationDetails DeleteInstitute(Institute institute);
        IEnumerable<Institute> FindInstitute(Func<Institute, bool> where);

        OperationDetails CreateFaculty(Faculty faculty);
        OperationDetails DeleteFaculty(Faculty faculty);
        IEnumerable<Faculty> FindFaculty(Func<Faculty, bool> where);

        OperationDetails CreateDepartment(Department department);
        OperationDetails DeleteDepartment(Department department);
        IEnumerable<Department> FindDepartments(Func<Department, bool> where);
    }
}
