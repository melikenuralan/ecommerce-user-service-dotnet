using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> GetByUserIdAsync(Guid userId);
        Task AddAsync(UserProfile profile);
        Task UpdateAsync(UserProfile profile);
        Task DeleteAsync(UserProfile profile);
    }
}
