using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Concretes.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        public Task AddAsync(UserProfile profile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserProfile profile)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile?> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}
