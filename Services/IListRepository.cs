using Data.Dbos;
using Data.Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IListRepository
    {
        List<HuntedListsDto> GetAllHuntedLists();
        HuntedListDto GetHuntedList(int id);

        Task<HuntedList> AddCharacterToListAsync(string characterName, int listId);

        Task<HuntedList> DeleteCharacterFromListAsync(int characterId, int listId);

        Task<HuntedList> DeleteGuildFromListAsync(string guildName, int listId);

        Task<HuntedList> AddGuildToListAsync(string guildName, int listId);
        Task CreateHuntedListAsync(string name);
        Task<HuntedList> UpdateHuntedListAsync(int id, String name);
        Task<HuntedList> DeleteHuntedListAsync(int id);

    }
}
