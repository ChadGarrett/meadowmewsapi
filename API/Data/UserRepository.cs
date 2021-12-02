using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUser(int id)
        {
            return await _context.appUsers.FindAsync(id);
        }

        public async Task<AppUser> GetUserByEmail(string emailAddress)
        {
            return await _context.appUsers.FirstOrDefaultAsync(u => u.Email == emailAddress);
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _context.appUsers.Include(p => p.Photo).ToListAsync();
        }

        public void Update(AppUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
        }
    }
}