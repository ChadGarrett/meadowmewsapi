using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ElectricityRepository : IElectricityRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ElectricityRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddPurchase(ElectricityPurchase purchase)
        {
            await _context.electricityPurchases.AddAsync(purchase);
        }

        public async Task<IEnumerable<ElectricityPurchaseDto>> GetPurchases()
        {
            return await _context.electricityPurchases
                .Include(p => p.Property)
                .Include(p => p.Property.AppUser)
                .ProjectTo<ElectricityPurchaseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
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