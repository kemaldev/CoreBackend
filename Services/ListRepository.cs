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
    public class ListRepository : IListRepository
    {
        private readonly HuntedListContext _context;
        private readonly TibiaParser _parser;

        public ListRepository(HuntedListContext context, TibiaParser parser)
        {
            _parser = parser;
            _context = context;
        }

        public async Task CreateHuntedListAsync(string name)
        {
            _context.HuntedList.Add(new HuntedList
            {
                Name = name
            });
            await _context.SaveChangesAsync();
        }

        public async Task<HuntedList> DeleteHuntedListAsync(int id)
        {
            var huntedList = _context.HuntedList.Find(id);

            if (huntedList == null)
            {
                return null;
            }

            _context.HuntedList.Remove(huntedList);
            await _context.SaveChangesAsync();

            return huntedList;
        }

        public List<HuntedListsDto> GetAllHuntedLists()
        {

            var huntedLists = _context.HuntedList
                .Select(h => new HuntedListsDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    AmountOnline = h.HuntedListCharacters.Where(c => c.TibiaCharacter.IsOnline).Count()
                }).ToList();

            return huntedLists;
        }

        public async Task<HuntedList> UpdateHuntedListAsync(int id, string name)
        {
            HuntedList huntedList = _context.HuntedList.FirstOrDefault(h => h.Id == id);
            if (huntedList == null)
            {
                return null;
            }

            huntedList.Name = name;
            _context.HuntedList.Update(huntedList);
            await _context.SaveChangesAsync();

            return huntedList;
        }

        public HuntedListDto GetHuntedList(int id)
        {
            var huntedList = _context.HuntedList.Select(
                hl => new HuntedListDto
                {
                    Id = hl.Id,
                    Name = hl.Name,
                    TibiaCharacters = hl.HuntedListCharacters.Select(x => x.TibiaCharacter).Select(x => new HuntedListItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Vocation = x.Vocation,
                        Level = x.Level,
                        IsOnline = x.IsOnline
                    }).OrderBy(x => x.Vocation)
                    .ThenByDescending(x => x.Level)
                    .ToList()
                }).FirstOrDefault(x => x.Id == id);

            if(huntedList == null)
            {
                return null;
            }

            return huntedList;
        }

        //This method should probably be moved to another repository
        private async Task<TibiaCharacter> GetTibiaCharacterAsync(string characterName)
        {
            var tibiaCharacter = GetCharacterFromDB(characterName); //probably move this out elsewhere
            if (tibiaCharacter == null)
            {
                tibiaCharacter = await _parser.GetTibiaCharacterAsync(characterName);

                if (tibiaCharacter == null)
                {
                    return null;
                }
            }

            return tibiaCharacter;
        }

        public async Task<HuntedList> AddCharacterToListAsync(string characterName, int listId)
        {
            var huntedList = _context.HuntedList
                .Include(h => h.HuntedListCharacters)
                .ThenInclude(h => h.TibiaCharacter)
                .FirstOrDefault(h => h.Id == listId);
            if (huntedList == null)
            {
                return null;
            }

            var tibiaCharacter = await GetTibiaCharacterAsync(characterName);
            if (tibiaCharacter == null)
            {
                return null;
            }

            var huntedListCharacter = huntedList.HuntedListCharacters
                .FirstOrDefault(c => c.TibiaCharacter.Name == tibiaCharacter.Name);
            if (huntedListCharacter == null)
            {
                huntedList.HuntedListCharacters.Add(new HuntedListCharacter
                {
                    HuntedListId = huntedList.Id,
                    HuntedList = huntedList,
                    TibiaCharacterId = tibiaCharacter.Id,
                    TibiaCharacter = tibiaCharacter
                });
            }
            else
            {
                return null;
            }

            await _context.SaveChangesAsync();

            return huntedList;
        }

        //This method should probably be moved to another repository
        //Not used atm.
        private async Task<bool> CharacterHasRelationsAsync(int characterId, int listId)
        {
            bool characterExists = false;
            List<HuntedList> huntedLists = await _context.HuntedList.Include(x => x.HuntedListCharacters).ThenInclude(x => x.TibiaCharacter).Where(hl => hl.Id != listId).ToListAsync();
            foreach (var hList in huntedLists)
            {
                var hListCharacters = hList.HuntedListCharacters;
                foreach (var hLCharacter in hListCharacters)
                {
                    if (hLCharacter.TibiaCharacterId == characterId)
                    {
                        characterExists = true;
                        break;
                    }
                }
            }

            return characterExists;
        }

        public async Task<HuntedList> DeleteCharacterFromListAsync(int characterId, int listId)
        {
            HuntedList huntedList = _context.HuntedList
                .Include(h => h.HuntedListCharacters)
                .FirstOrDefault(h => h.Id == listId);
            if (huntedList == null)
            {
                return null;
            }

            HuntedListCharacter huntedListCharacter = huntedList.HuntedListCharacters
                .FirstOrDefault(c => c.TibiaCharacterId == characterId);
            if (huntedListCharacter == null)
            {
                return null;
            }

            huntedList.HuntedListCharacters.Remove(huntedListCharacter);

            await _context.SaveChangesAsync();

            return huntedList;
        }

        public async Task<HuntedList> DeleteGuildFromListAsync(string name, int listId)
        {
            HuntedList huntedList = _context.HuntedList
                .Include(h => h.HuntedListCharacters)
                .ThenInclude(h => h.TibiaCharacter)
                .FirstOrDefault(h => h.Id == listId);
            if (huntedList == null)
            {
                return null;
            }

            var huntedListCharacters = huntedList.HuntedListCharacters
                .Where(x => x.TibiaCharacter.GuildName == name)
                .ToList();
            if (huntedListCharacters == null)
            {
                return null;
            }

            foreach (var huntedCharacter in huntedListCharacters)
            {
                huntedList.HuntedListCharacters.Remove(huntedCharacter);
            }

            if(_context.ChangeTracker.HasChanges())
                await _context.SaveChangesAsync();

            return huntedList;
        }

        private TibiaCharacter GetCharacterFromDB(string name)
        {
            var tibiaCharacter = _context.TibiaCharacter.FirstOrDefault(x => x.Name.Equals(name));
            return tibiaCharacter;
        }

        public async Task<HuntedList> AddGuildToListAsync(string name, int listId)
        {
            HuntedList huntedList = _context.HuntedList
                .Include(x => x.HuntedListCharacters)
                .ThenInclude(x => x.TibiaCharacter)
                .FirstOrDefault(h => h.Id == listId);
            if (huntedList == null)
            {
                return null;
            }

            var guildCharacterNames = await _parser.GetCharacterNamesFromGuildAsync(name);
            if (guildCharacterNames == null)
            {
                return null;
            }

            foreach (var charName in guildCharacterNames)
            {
                var huntedListCharacter = huntedList.HuntedListCharacters
                    .FirstOrDefault(c => c.TibiaCharacter.Name == charName);
                if (huntedListCharacter == null)
                {
                    TibiaCharacter tibiaCharacter = null;
                    TibiaCharacter charFromDB = GetCharacterFromDB(charName);
                    if (charFromDB == null)
                    {
                        tibiaCharacter = await _parser.GetTibiaCharacterAsync(charName);
                    }
                    else
                    {
                        tibiaCharacter = charFromDB;
                    }
                    huntedList.HuntedListCharacters.Add(new HuntedListCharacter
                    {
                        HuntedListId = huntedList.Id,
                        HuntedList = huntedList,
                        TibiaCharacterId = tibiaCharacter.Id,
                        TibiaCharacter = tibiaCharacter
                    });
                }
                else
                {
                    continue;
                }
            }

            if(_context.ChangeTracker.HasChanges())
                await _context.SaveChangesAsync();
            

            return huntedList;
        }
    }
}