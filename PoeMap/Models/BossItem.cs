using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeMap.Models
{

    public class BossItem
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string baseType { get; set; }
        public bool identified { get; set; }
        public int frameType { get; set; }
    }

}
