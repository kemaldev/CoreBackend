using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class HuntedListCharacter
    {
        public int HuntedListId { get; set; }
        public HuntedList HuntedList { get; set; }
        public int TibiaCharacterId { get; set; }
        public TibiaCharacter TibiaCharacter { get; set; }

    }
}
