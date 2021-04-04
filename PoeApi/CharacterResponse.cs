using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeApi
{

    public class CharacterResponse
    {
        public Item[] items { get; set; }
        public Character character { get; set; }
    }

    public class Character
    {
        public string name { get; set; }
        public string league { get; set; }
        public int classId { get; set; }
        public int ascendancyClass { get; set; }
        public string _class { get; set; }
        public int level { get; set; }
        public long experience { get; set; }
        public bool lastActive { get; set; }
    }

    public class Item
    {
        public bool verified { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string icon { get; set; }
        public string league { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string typeLine { get; set; }
        public string baseType { get; set; }
        public bool identified { get; set; }
        public int ilvl { get; set; }
        public Property[] properties { get; set; }
        public Requirement[] requirements { get; set; }
        public string[] utilityMods { get; set; }
        public string[] explicitMods { get; set; }
        public string descrText { get; set; }
        public string[] flavourText { get; set; }
        public int frameType { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public string inventoryId { get; set; }
        public string[] implicitMods { get; set; }
        public string[] enchantMods { get; set; }
        public Socket[] sockets { get; set; }
        public Socketeditem[] socketedItems { get; set; }
        public Influences influences { get; set; }
        public string[] craftedMods { get; set; }
        public bool shaper { get; set; }
        public int stackSize { get; set; }
        public int maxStackSize { get; set; }
    }

    public class Influences
    {
        public bool hunter { get; set; }
        public bool shaper { get; set; }
        public bool crusader { get; set; }
    }

    public class Property
    {
        public string name { get; set; }
        public object[][] values { get; set; }
        public int displayMode { get; set; }
        public int type { get; set; }
    }

    public class Requirement
    {
        public string name { get; set; }
        public object[][] values { get; set; }
        public int displayMode { get; set; }
        public string suffix { get; set; }
    }

    public class Socket
    {
        public int group { get; set; }
        public string attr { get; set; }
        public string sColour { get; set; }
    }

    public class Socketeditem
    {
        public bool verified { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public string icon { get; set; }
        public bool support { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string typeLine { get; set; }
        public string baseType { get; set; }
        public bool identified { get; set; }
        public int ilvl { get; set; }
        public Property[] properties { get; set; }
        public Requirement[] requirements { get; set; }
        public string secDescrText { get; set; }
        public string[] explicitMods { get; set; }
        public string descrText { get; set; }
        public int frameType { get; set; }
        public int socket { get; set; }
        public string colour { get; set; }
        public bool corrupted { get; set; }
        public Additionalproperty[] additionalProperties { get; set; }
        public Hybrid hybrid { get; set; }
    }

    public class Hybrid
    {
        public bool isVaalGem { get; set; }
        public string baseTypeName { get; set; }
        public Property[] properties { get; set; }
        public string[] explicitMods { get; set; }
        public string secDescrText { get; set; }
    }

    public class Additionalproperty
    {
        public string name { get; set; }
        public object[][] values { get; set; }
        public int displayMode { get; set; }
        public float progress { get; set; }
        public int type { get; set; }
    }

}
