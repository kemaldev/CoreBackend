using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class ListRepository : IListRepository
    {
        private readonly HuntedListContext _context;

        public ListRepository(HuntedListContext context)
        {
            _context = context;
        }

        public async Task AsyncCreateHuntedList(HuntedList huntedList)
        {
            _context.HuntedList.Add(huntedList);
            await _context.SaveChangesAsync();
        }

        public async Task<HuntedList> AsyncDeleteHuntedList(int id)
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

        public async Task<List<HuntedList>> AsyncGetAllHuntedLists()
        {
            return await _context.HuntedList.AsNoTracking().ToListAsync();
        }

        public async Task<HuntedList> AsyncUpdateHuntedList(int id, HuntedList huntedList)
        {
            if (!_context.HuntedList.Any(t => t.Id == id))
            {
                return null;
            }

            _context.HuntedList.Update(huntedList);
            await _context.SaveChangesAsync();

            return huntedList;
        }

        public HuntedList GetHuntedList(int id)
        {
            return _context.HuntedList.Find(id);
        }
    }
}
