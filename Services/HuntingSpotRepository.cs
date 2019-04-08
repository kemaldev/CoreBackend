using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Data.Dtos;
using Models;

namespace Services
{
    public class HuntingSpotRepository : IHuntingSpotRepository
    {
        private HuntedListContext _context;

        public HuntingSpotRepository(HuntedListContext context)
        {
            _context = context;
        }

        public async Task AddHuntingSpotAsync(HuntingSpot huntingSpot)
        {
            _context.HuntingSpot.Add(huntingSpot);
            await _context.SaveChangesAsync();
        }

        public async Task<HuntingSpot> DeleteHuntingSpotAsync(int id)
        {
            var huntingSpot = _context.HuntingSpot.FirstOrDefault(x => x.Id == id);
            if(huntingSpot == null)
            {
                return null;
            }

            _context.HuntingSpot.Remove(huntingSpot);
            await _context.SaveChangesAsync();

            return huntingSpot;
        }

        public async Task<List<HuntingSpotDto>> GetAllHuntingSpotsAsync()
        {
            var huntingSpots = await _context.HuntingSpot.Select(x => new HuntingSpotDto
            {
                Id = x.Id,
                Name = x.Name,
                Location = x.Location
            }).ToListAsync();

            return huntingSpots;
        }

        public HuntingSpotDto GetHuntingSpot(int id)
        {
            var huntingSpot = _context.HuntingSpot
                .Select(x => new HuntingSpotDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location
                })
                .FirstOrDefault(x => x.Id == id);

            return huntingSpot;
        }

        public async Task<HuntingSpot> UpdateHuntingSpotAsync(int id, HuntingSpot huntingSpot)
        {
            var hSpot = _context.HuntingSpot.FirstOrDefault(x => x.Id == id);
            if(hSpot == null)
            {
                return null;
            }

            hSpot.Name = huntingSpot.Name;
            hSpot.Location = huntingSpot.Location;
            _context.HuntingSpot.Update(hSpot);
            await _context.SaveChangesAsync();

            return hSpot;
        }
    }
}
