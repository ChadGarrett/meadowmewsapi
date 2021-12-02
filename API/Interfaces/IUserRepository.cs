using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
         void Update(AppUser appUser);
         Task<IEnumerable<AppUser>> GetUsers();
         Task<AppUser> GetUser(int id);
         Task<AppUser> GetUserByEmail(string emailAddress);
         
    }
}