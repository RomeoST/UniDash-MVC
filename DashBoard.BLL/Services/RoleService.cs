using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Infrastructure;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;
using Ninject;

namespace DashBoard.BLL.Services
{
    public class RoleService : IRoleService
    {
        [Inject] public IUnitOfWork DataBase { get; set; }
        [Inject] public RoleManager<DutRole> RoleManager { get; set; }

        public async Task<OperationDetails> Create(DutRole role)
        {
            var result = await RoleManager.CreateAsync(role);
            return result.Succeeded 
                ? new OperationDetails(true, "Додан новий тип користувача", "") 
                : new OperationDetails(false, result.Errors.Aggregate("", (current, resultError) => current + (resultError + ",")), "Role");
        }

        public async Task<OperationDetails> Edit(DutRole model)
        {
            var role = await RoleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.UpdateAsync(model);
                return result.Succeeded 
                    ? new OperationDetails(true, "Оновлення пройшло успішно", "") 
                    : new OperationDetails(false, result.Errors.Aggregate("", (s, s1) => s + (s1 + ",")), "Role");
            }
            return new OperationDetails(false, "Тип користувача не знайден", "");
        }

        public async Task<OperationDetails> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await RoleManager.DeleteAsync(role);
                return result.Succeeded
                    ? new OperationDetails(true, "Оновлення пройшло успішно", "")
                    : new OperationDetails(false, result.Errors.Aggregate("", (s, s1) => s + (s1 + ",")), "Role");
            }
            return new OperationDetails(false, "Тип користувача не знайден", "");
        }

        public List<DutRole> GetAll()
        {
            var roles = RoleManager.Roles.ToList();
            return roles;
        }
    }
}
