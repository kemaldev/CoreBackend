using Data.Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IHuntingSpotRepository
    {
        Task<List<HuntingSpotDto>> GetAllHuntingSpotsAsync();
        HuntingSpotDto GetHuntingSpot(int id);
        Task AddHuntingSpotAsync(HuntingSpot huntingSpot);
        Task<HuntingSpot> DeleteHuntingSpotAsync(int id);
        Task<HuntingSpot> UpdateHuntingSpotAsync(int id, HuntingSpot huntingSpot);
    }
}
