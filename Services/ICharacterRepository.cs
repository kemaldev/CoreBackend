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
        List<TibiaCharacterItemDto> GetTibiaCharacters();
        TibiaCharacterDto GetTibiaCharacter(int id);
        Task<TibiaCharacter> AddHuntingSpotToCharacterAsync(int charId, int huntingSpotId);
        Task<TibiaCharacter> DeleteHuntingSpotFromCharacterAsync(int charId, int huntingSpotId);
    }
}
