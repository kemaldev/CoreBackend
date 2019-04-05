using Data.Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICharacterRepository
    {
        Task<TibiaCharacterDto> GetTibiaCharacterAsync(int id);
        Task<TibiaCharacter> AddHuntingSpotToCharacter(int charId, int huntingSpotId);
    }
}
