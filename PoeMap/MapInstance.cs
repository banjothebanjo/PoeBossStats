using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoeApi;

namespace PoeMap
{
    public class MapInstance
    {
        List<string> bossRooms = new List<string>() { "Chayula's Domain", "The Shaper's Realm", "The Apex of Sacrifice", "The Alluring Abyss", "Lair of the Hydra", "Lookout", "Palace", "Tower", "Absence of Value and Meaning", "Forge of the Phoenix", "Pit of the Chimera", "Lair of the Hydra", "Maze of the Minotaur" };
        public string IpAddress { get; set; }
        public string LocationName { get; set; }
        public bool IsBossLocation { get; set; }
        public DateTime InstanceCreated { get; set; }

        public CharacterResponse EntryInventory { get; set; }
        public CharacterResponse ExitInventory { get; set; }

        public Item[] FoundItems { get; set; }

        public BossType? bossType = BossType.NONE;

        public MapInstance(string ipAddress,string locationName,bool isBossLocation)
        {
            this.IpAddress = ipAddress;
            this.LocationName = locationName;
            this.IsBossLocation = isBossLocation;
            InstanceCreated = DateTime.Now;
        }

        public BossType GetBossType(Item[] foundItems, string locationName)
        {
            if (locationName == "The Shaper's Realm" && BossLootHandler.IsShaperKill(foundItems))
            {
                return BossType.The_Shaper;
            }
            if (locationName == "Absence of Value and Meaning" && BossLootHandler.IsElderKill(foundItems))
            {
                return BossType.The_Elder;
            }
            if (locationName == "The Shaper's Realm" && BossLootHandler.IsUberElderKill(foundItems))
            {
                return BossType.Uber_Elder;
            }
            if (locationName == "Lair of the Hydra" && BossLootHandler.IsHydraKill(foundItems))
            {
                return BossType.The_Hydra;
            }
            if (locationName == "Pit of the Chimera" && BossLootHandler.IsChimeraKill(foundItems))
            {
                return BossType.The_Chimera;
            }
            if (locationName == "Maze of the Minotaur" && BossLootHandler.IsMinotaurKill(foundItems))
            {
                return BossType.The_Minotaur;
            }
            if (locationName == "Forge of the Phoenix" && BossLootHandler.IsPhoenixKill(foundItems))
            {
                return BossType.The_Phoenix;
            }
            if (locationName == "Tower" && BossLootHandler.IsShaperKill(foundItems))
            {
                return BossType.The_Shaper;
            }
            return BossType.NONE;
        }

    }
}
