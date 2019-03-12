using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IListRepository
    {

        Task<List<HuntedList>> AsyncGetAllHuntedLists();
        HuntedList GetHuntedList(int id);
        Task AsyncCreateHuntedList(HuntedList huntedList);
        Task<HuntedList> AsyncUpdateHuntedList(int id, HuntedList huntedList);
        Task<HuntedList> AsyncDeleteHuntedList(int id);

    }
}
