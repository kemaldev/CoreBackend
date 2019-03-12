using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class HuntingSpotCharacter
    {
        public int HuntingSpotId { get; set; }
        public HuntingSpot HuntingSpot { get; set; }
        public int TibiaCharacterId { get; set; }
        public TibiaCharacter TibiaCharacter { get; set; }
    }
}
