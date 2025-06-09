using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces
{
    public interface IUserSettingsRepository
    {
        Task<UserSettings?> GetByUserIdAsync(Guid userId);
        Task AddAsync(UserSettings settings);
        Task UpdateAsync(UserSettings settings);
        Task DeleteAsync(UserSettings settings);
    }
}
