﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TibiaCharacter
    {

        public TibiaCharacter()
        {
            HuntedListCharacters = new List<HuntedListCharacter>();
            HuntingSpotCharacters = new List<HuntingSpotCharacter>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string GuildName { get; set; }
        public string World { get; set; }
        public string Vocation { get; set; }
        public string Residence { get; set; }
        public string LatestDeathBy { get; set; }
        public int Level { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LatestDeath { get; set; }
        public ICollection<HuntedListCharacter> HuntedListCharacters { get; set; }
        public ICollection<HuntingSpotCharacter> HuntingSpotCharacters { get; set; }
    }
}
