using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Concretes.Repositories
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        public Task AddAsync(UserSettings settings)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserSettings settings)
        {
            throw new NotImplementedException();
        }

        public Task<UserSettings?> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
