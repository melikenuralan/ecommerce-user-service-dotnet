using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.ValueObjects;
using UserService.Persistence.Data;

namespace UserService.Persistence.Concretes.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext _context;
        public UserRepository(UserServiceDbContext context) => _context = context;
        public void AddUser(User user) => _context.Users.Add(user);
        public void Remove(User user) => _context.Users.Remove(user);
        public void Update(User user) => _context.Users.Update(user);

        public async Task<bool> ExitAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(Email email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users
            .Include(u => u.BlockedUsers)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(); // bu şart
        }

    }
}
