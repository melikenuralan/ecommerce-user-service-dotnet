using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.ValueObjects;
using UserService.Persistence.Data;

namespace UserService.Persistence.Concretes.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext _context;
        public UserRepository(UserServiceDbContext context) => _context = context;
        public void AddUser(User user) => _context.DomainUsers.Add(user);

        public Task<bool> ExitAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(Email email)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(User user)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
