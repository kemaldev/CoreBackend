using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Dtos;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly HuntedListContext _context;

        public CharacterRepository(HuntedListContext context)
        {
            _context = context;
        }

        public List<TibiaCharacterItemDto> GetTibiaCharacters()
        {
            var tibiaCharacters = _context.TibiaCharacter.Select(c => new TibiaCharacterItemDto
            {
                Id = c.Id,
                Name = c.Name,
                Vocation = c.Vocation,
                Level = c.Level,
                IsOnline = c.IsOnline,
            }).ToList();

            return tibiaCharacters;
        }

        public async Task<TibiaCharacter> AddHuntingSpotToCharacterAsync(int charId, int huntingSpotId)
        {
            var tibiaCharacter = _context.TibiaCharacter
                .Include(x => x.HuntingSpotCharacters)
                .ThenInclude(x => x.HuntingSpot)
                .FirstOrDefault(x => x.Id == charId);

            if(tibiaCharacter == null)
            {
                return null;
            }

            var huntingSpot = _context.HuntingSpot.FirstOrDefault(x => x.Id == huntingSpotId);

            if(huntingSpot == null)
            {
                return null;
            }

            var huntingSpotCharacter = tibiaCharacter.HuntingSpotCharacters
                .FirstOrDefault(x => x.HuntingSpotId == huntingSpotId);

            if(huntingSpotCharacter == null)
            {
                tibiaCharacter.HuntingSpotCharacters.Add(new HuntingSpotCharacter
                {
                    HuntingSpotId = huntingSpotId,
                    HuntingSpot = huntingSpot,
                    TibiaCharacterId = charId,
                    TibiaCharacter = tibiaCharacter
                });
            }
            else
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return tibiaCharacter;
        }

        public async Task<TibiaCharacter> DeleteHuntingSpotFromCharacterAsync(int charId, int huntingSpotId)
        {
            var tibiaCharacter = _context.TibiaCharacter
                .Include(x => x.HuntingSpotCharacters)
                .ThenInclude(x => x.HuntingSpot)
                .FirstOrDefault(x => x.Id == charId);

            if(tibiaCharacter == null)
            {
                return null;
            }

            var huntingSpotCharacter = tibiaCharacter.HuntingSpotCharacters
                .FirstOrDefault(x => x.HuntingSpotId == huntingSpotId);

            if(huntingSpotCharacter == null)
            {
                return null;
            }


            tibiaCharacter.HuntingSpotCharacters.Remove(huntingSpotCharacter);

            await _context.SaveChangesAsync();
            return tibiaCharacter;
        }

        public TibiaCharacterDto GetTibiaCharacter(int id)
        {
            var tibiaCharacter = _context.TibiaCharacter.Select(
                    c => new TibiaCharacterDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        GuildName = c.GuildName,
                        World = c.World,
                        Vocation = c.Vocation,
                        Residence = c.Residence,
                        LatestDeathBy = c.LatestDeathBy,
                        Level = c.Level,
                        IsOnline = c.IsOnline,
                        LastLogin = c.LastLogin,
                        LatestDeath = c.LatestDeath,
                        HuntingSpots = c.HuntingSpotCharacters
                        .Where(x => x.TibiaCharacterId == id)
                        .Select(x => new HuntingSpotDto
                        {
                            Id = x.HuntingSpotId,
                            Name = x.HuntingSpot.Name,
                            Location = x.HuntingSpot.Location
                        }).ToList()
                    }
                ).FirstOrDefault(c => c.Id == id);

            return tibiaCharacter;
        }
    }
}
