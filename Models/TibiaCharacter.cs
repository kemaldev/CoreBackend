using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    class TibiaCharacter
    {
        public string Name { get; set; }
        public string GuildName { get; set; }
        public string World { get; set; }
        public string Vocation { get; set; }
        public string Residence { get; set; }
        public int Level { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LatestDeath { get; set; }
        public List<string> UsuallyHunts { get; set; }
    }
}
