using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IElectricityRepository
    {
         Task<IEnumerable<ElectricityPurchase>> GetPurchases();
         Task AddPurchase(ElectricityPurchase purchase);
         void UpdatePurchase(ElectricityPurchase purchase);
         Task<ElectricityPurchase> GetPurchase(int id);
         void DeletePurchase(ElectricityPurchase purchase);
    }
}