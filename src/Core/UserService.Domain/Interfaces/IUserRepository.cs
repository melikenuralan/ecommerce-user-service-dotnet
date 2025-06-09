using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmailAsync(Email email);
        Task<bool> ExitAsync(Guid id);
        void AddUser(User user);
        Task AddAsync(User user);
        void Remove(User user);
        void Update(User user);
    }
}
