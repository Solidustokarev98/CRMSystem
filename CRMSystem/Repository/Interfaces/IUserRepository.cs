using CRMSystem.Models;

namespace CRMSystem.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }


        void Save(User user);
        void Delete(int id);
    }
}
