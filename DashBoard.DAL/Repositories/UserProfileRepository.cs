using DashBoard.DAL.Infrastructure;
using DashBoard.Model.Models;

namespace DashBoard.DAL.Repositories
{
    /// <summary>
    /// Клас логіки роботи з детальними даними користувача
    /// </summary>
    public class UserProfileRepository : RepositoryBase<ClientProfile>, IUserProfileRepository
    {
        public UserProfileRepository(IDataBaseFactory factory) : base(factory) { }
    }

    public interface IUserProfileRepository : IRepository<ClientProfile> { }
}
