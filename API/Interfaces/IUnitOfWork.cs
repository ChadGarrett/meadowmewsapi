using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
         ///////////
         // Repos //
         ///////////

         IElectricityRepository electricityRepository { get; }
         
         /////////////
         // Helpers //
         /////////////
         
         Task<bool> Complete();
         bool HasChanges();
    }
}