using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public void Remove(User user) => _context.DomainUsers.Remove(user);
        public void Update(User user) => _context.DomainUsers.Update(user);

        public async Task<bool> ExitAsync(Guid id)
        {
            return await _context.DomainUsers
                .AsNoTracking()
                .AnyAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(Email email)
        {
            return await _context.DomainUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.DomainUsers
            .Include(u => u.BlockedUsers)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.DomainUsers.AddAsync(user);
        }

    }
}
