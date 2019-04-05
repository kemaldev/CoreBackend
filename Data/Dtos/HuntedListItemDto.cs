using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Dtos
{
    public class HuntedListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vocation { get; set; }
        public int Level { get; set; }
        public bool IsOnline { get; set; }
    }
}
