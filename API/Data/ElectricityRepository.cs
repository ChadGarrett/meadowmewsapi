using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ElectricityRepository : IElectricityRepository
    {
        private readonly DataContext _context;
        public ElectricityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddPurchase(ElectricityPurchase purchase)
        {
            await _context.electricityPurchases.AddAsync(purchase);
        }

        public async Task<IEnumerable<ElectricityPurchase>> GetPurchases()
        {
            return await _context.electricityPurchases.ToListAsync();
        }

        public void UpdatePurchase(ElectricityPurchase purchase)
        {
            _context.Entry(purchase).State = EntityState.Modified;
        }

        public void DeletePurchase(ElectricityPurchase purchase)
        {
            _context.electricityPurchases.Remove(purchase);
        }

        public async Task<ElectricityPurchase> GetPurchase(int id)
        {
            return await _context.electricityPurchases.FindAsync(id);
        }
    }
}