using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DashBoard.BLL.Infrastructure;
using DashBoard.BLL.Interfaces;
using DashBoard.DAL.Infrastructure;
using DashBoard.DAL.Repositories;
using DashBoard.Model.Models;
using Microsoft.AspNet.Identity;

namespace DashBoard.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork DataBase { get; }
        private IUserProfileRepository UserProfileRepository { get; }
        private IPermissionRepository PermissionRepository { get; }
        private UserManager<DutUser> UserManager { get; }
        private RoleManager<DutRole> RoleManager { get; }

        public UserService(IUnitOfWork uow, IUserProfileRepository userProfileRepository,
            IPermissionRepository permissionRepository,UserManager<DutUser> userManager, RoleManager<DutRole> roleManager )
        {
            DataBase = uow;
            UserProfileRepository = userProfileRepository;
            UserManager = userManager;
            RoleManager = roleManager;
            PermissionRepository = permissionRepository;
        }

        public async Task<OperationDetails> Create(DutUser userDto, string password)
        {
            var user = await UserManager.FindByEmailAsync(userDto.Email);
            if (user != null) return new OperationDetails(false, "Користувач з таким email вже існує", "Email");

            var result = await UserManager.CreateAsync(userDto, password);
            if (result.Errors.Any())
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

            // Добавить роль
            await UserManager.AddToRoleAsync(userDto.Id, "user");

            // Создание профиля клиента
            ClientProfile clientProfile = new ClientProfile {Id = userDto.Id, FullName = userDto.ClientProfile.FullName};
            UserProfileRepository.Add(clientProfile);
            await SaveUser();

            return new OperationDetails(true, "Реєстрація успішно пройдена","");

        }

        public async Task<ClaimsIdentity> Authenticate(string login, string password)
        {
            ClaimsIdentity claim = null;
            var user = await UserManager.FindAsync(login, password);
            if (user != null)
                claim =
                    await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task<DutUser> FindByName(string name)
        {
            var user = await UserProfileRepository.GetAsync(p=>p.FullName == name);
            if (user == null) return null;

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

        public async Task<IEnumerable<ClientProfile>> GetAll()
        {
            return await UserProfileRepository.GetAllAsync();
        }

        // Инициализация БД  (начальная)
        public async Task SetInitialData(DutUser adminDto, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await RoleManager.FindByNameAsync(roleName);
                if (role != null) continue;

                role = new DutRole() {Name = roleName};
                await RoleManager.CreateAsync(role);
            }
        }

        /// <summary>
        /// Проверить права доступа
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="requiredPermission">/<controller-action/></param>
        /// <returns></returns>
        public async Task<bool> HasPermission(string userName, string requiredPermission)
        {
            try
            {
                var required = requiredPermission.Split('-');
                var roles = await PermissionRepository.GetPermissionRoles(required[0], required[1]);

                var user = UserManager.FindByName(userName);
                return (from r in user.Roles from dutRole in roles where r.RoleId == dutRole.Id select r).Any();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<OperationDetails> EditProfile(DutUser user)
        {
            var curus = await UserManager.FindByIdAsync(user.Id);
            if(curus == null)
                return new OperationDetails(false, "Користувач не знайдений","");
            curus.ClientProfile.FullName = user.ClientProfile.FullName;
            curus.Email = user.Email;
            curus.PhoneNumber = user.PhoneNumber;

            await SaveUser();
            return new OperationDetails(true,"Данні збережені","");
        }

        public async Task SaveUser()
        {
            await DataBase.CommitAsync();
        }
    }
}
