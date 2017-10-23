using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;

namespace DashBoard.BLL.Services
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork DataBase { get; set; }

        public RoleService(IUnitOfWork uof) => DataBase = uof;

        public async Task<OperationDetails> Create(DutRole role)
        {
            var result = await DataBase.RoleManager.CreateAsync(role);
            return result.Succeeded 
                ? new OperationDetails(true, "Додан новий тип користувача", "") 
                : new OperationDetails(false, result.Errors.Aggregate("", (current, resultError) => current + (resultError + ",")), "Role");
        }

        public async Task<OperationDetails> Edit(DutRole model)
        {
            var role = await DataBase.RoleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                IdentityResult result = await DataBase.RoleManager.UpdateAsync(model);
                return result.Succeeded 
                    ? new OperationDetails(true, "Оновлення пройшло успішно", "") 
                    : new OperationDetails(false, result.Errors.Aggregate("", (s, s1) => s + (s1 + ",")), "Role");
            }
            return new OperationDetails(false, "Тип користувача не знайден", "");
        }

        public async Task<OperationDetails> Delete(string id)
        {
            var role = await DataBase.RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await DataBase.RoleManager.DeleteAsync(role);
                return result.Succeeded
                    ? new OperationDetails(true, "Оновлення пройшло успішно", "")
                    : new OperationDetails(false, result.Errors.Aggregate("", (s, s1) => s + (s1 + ",")), "Role");
            }
            return new OperationDetails(false, "Тип користувача не знайден", "");
        }

        public List<DutRole> GetAll()
        {
            var roles = DataBase.RoleManager.Roles.ToList();
            return roles;
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
