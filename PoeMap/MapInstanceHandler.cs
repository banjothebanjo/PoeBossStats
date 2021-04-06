using PoeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PoeMap
{
    public class MapInstanceHandler
    {
        List<string> bossRooms = new List<string>() { "Chayula's Domain", "The Shaper's Realm", "The Apex of Sacrifice", "The Alluring Abyss", "Lair of the Hydra", "Lookout", "Palace", "Tower", "Absence of Value and Meaning", "Forge of the Phoenix", "Pit of the Chimera", "Lair of the Hydra", "Maze of the Minotaur" };

        public MapInstance EnteredMap(string location, string instanceIp, MapInstance mapInstance)
        {
            foreach (var item in bossRooms)
            {
                var yes = location.EndsWith(item);
                if (yes && instanceIp != mapInstance?.IpAddress)
                {
                    MapInstance _mapInstance = new MapInstance(instanceIp, location, yes);
                    _mapInstance.EntryInventory = CallApi();
                    return _mapInstance;
                }
                else if (yes && mapInstance?.bossType == BossType.NONE)
                {
                    mapInstance.EntryInventory = CallApi();
                    return mapInstance;
                }
            }
            return null;
        }

        public MapInstance LeftMap(string location, string instanceIp, MapInstance mapInstance)
        {
            if (mapInstance != null && location.EndsWith("Hideout") && mapInstance.IsBossLocation)
            {
                var tempInstance = mapInstance;
                tempInstance.ExitInventory = CallApi();
                tempInstance.FoundItems = LogBossLoot(mapInstance.EntryInventory, mapInstance.ExitInventory);
                var bossType = tempInstance.GetBossType(mapInstance.FoundItems, mapInstance.LocationName);
                tempInstance.bossType = bossType;
                if (bossType != BossType.NONE)
                {
                    var temp = new SaveData();
                    temp.InsertData(tempInstance);
                }
                return tempInstance;
            }
            return null;
        }

        public void ReinteredMap()
        {

        }

        public CharacterResponse CallApi()
        {
            var apiService = new PoeCharacterInventoryService("https://www.pathofexile.com");
            var response = apiService.GetMainInventoryData("Keymat20", "fifefeefs", "/character-window/get-items");
            return response;
        }

        public Item[] LogBossLoot(CharacterResponse entryInv, CharacterResponse exitInv)
        {
            var result = exitInv.items.Where(p => !entryInv.items.Any(l => p.id == l.id));
            return result.ToArray();
        }
    }
}
