using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Interfaces;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DashBoard.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork DataBase { get; set; }

        public UserService(IUnitOfWork uow) => DataBase = uow;

        public async Task<OperationDetails> Create(DutUser userDto, string password)
        {
            var user = await DataBase.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                var result = await DataBase.UserManager.CreateAsync(userDto, password);
                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                // Добавить роль
                await DataBase.UserManager.AddToRoleAsync(userDto.Id, "user");

                // Создание профиля клиента
                ClientProfile clientProfile = new ClientProfile {Id = userDto.Id, FullName = userDto.ClientProfile.FullName};
                await DataBase.ClientManager.CreateAsync(clientProfile);
                await DataBase.SaveAsync();

                return new OperationDetails(true, "Реєстрація успішно пройдена","");
            }

            return new OperationDetails(false, "Користувач з таким email вже існує", "Email");
        }

        public async Task<ClaimsIdentity> Authenticate(string login, string password)
        {
            ClaimsIdentity claim = null;
            var user = await DataBase.UserManager.FindAsync(login, password);
            if (user != null)
                claim =
                    await DataBase.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task<DutUser> FindByName(string name)
        {
            var user = await DataBase.ClientManager.FindByName(name);
            if (user != null)
            {
                var result = new DutUser
                {
                    ClientProfile = user, 
                    Id = user.DutUser.Id,
                    UserName = user.DutUser.UserName,
                    Email = user.DutUser.Email,
                    PhoneNumber = user.DutUser.PhoneNumber,
                };
                return result;
            }
            return null;
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            return DataBase.ClientManager.GetAll();
        }

        // Инициализация БД  (начальная)
        public async Task SetInitialData(DutUser adminDto, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await DataBase.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new DutRole() {Name = roleName};
                    await DataBase.RoleManager.CreateAsync(role);
                }
            }

            //await Create(adminDto);
        }

        public async Task<OperationDetails> EditProfile(DutUser user)
        {
            var curus = await DataBase.UserManager.FindByIdAsync(user.Id);
            if(curus == null)
                return new OperationDetails(false, "Користувач не знайдений","");
            curus.ClientProfile.FullName = user.ClientProfile.FullName;
            curus.Email = user.Email;
            curus.PhoneNumber = user.PhoneNumber;

            await DataBase.SaveAsync();
            return new OperationDetails(true,"Данні збережені","");
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
