using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class HuntingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<HuntingSpotCharacter> HuntingSpotCharacters { get; set; }
    }
}
