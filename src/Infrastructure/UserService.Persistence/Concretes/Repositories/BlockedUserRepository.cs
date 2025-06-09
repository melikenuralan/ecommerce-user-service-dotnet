using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Concretes.Repositories
{
    public class BlockedUserRepository : IBlockedUserRepository
    {
        public Task AddAsync(BlockedUser blockedUser)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlockedUser>> GetBlockedUsersByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserBlockedAsync(Guid userId, Guid blockedUserId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(BlockedUser blockedUser)
        {
            throw new NotImplementedException();
        }
    }
}
