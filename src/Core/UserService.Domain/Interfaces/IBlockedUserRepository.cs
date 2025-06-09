using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces
{
    public interface IBlockedUserRepository
    {
        Task<List<BlockedUser>> GetBlockedUsersByUserIdAsync(Guid userId);
        Task AddAsync(BlockedUser blockedUser);
        Task RemoveAsync(BlockedUser blockedUser);
        Task<bool> IsUserBlockedAsync(Guid userId, Guid blockedUserId);
    }
}