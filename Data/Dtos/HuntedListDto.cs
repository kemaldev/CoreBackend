using Data.Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Dtos
{
    public class HuntedListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<HuntedListItemDto> TibiaCharacters { get; set; }
    }
}
