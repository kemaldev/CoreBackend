using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class HuntedList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HuntedListCharacter> HuntedListCharacters { get; set; }
    }
}
