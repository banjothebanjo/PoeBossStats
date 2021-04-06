using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoeApi;

namespace PoeMap
{
    public static class BossLootHandler
    {
        private static string path = Path.Combine(Environment.CurrentDirectory, @"Resources\ShaperId");
        private static string debugpath = @"C:\Users\Benjamin\Documents\GitHub\PoeLootStatus\PoeLootStats\Resources\";

        //Compares Exit inventory to ShaperId.json (this is possible shaper drops in JSON), if any items matching these json values, returns true = it was a shaper kill
        //This helps with fights like Uber elder, which is also in "Shaper's realm", so by checking on looted items, and letting that determine the boss type, is safer.
        public static bool IsShaperKill(Item[] exitInv)
        {
            using (StreamReader r = new StreamReader(debugpath + "ShaperId.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.name == l.name));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            using (StreamReader r = new StreamReader(debugpath + "ShaperUnid.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.baseType == l.baseType && p.identified == l.identified && p.frameType == l.frameType));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsElderKill(Item[] exitInv)
        {
            using (StreamReader r = new StreamReader(debugpath + "ElderId.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.name == l.name));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            using (StreamReader r = new StreamReader(debugpath + "ElderUnid.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.baseType == l.baseType && p.identified == l.identified && p.frameType == l.frameType));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsUberElderKill(Item[] exitInv)
        {
            using (StreamReader r = new StreamReader(debugpath + "UElderId.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.name == l.name));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            using (StreamReader r = new StreamReader(debugpath + "UElderUnid.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.baseType == l.baseType && p.identified == l.identified && p.frameType == l.frameType));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsHydraKill(Item[] exitInv)
        {
            using (StreamReader r = new StreamReader(debugpath + "Hydra.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.baseType == l.baseType));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPhoenixKill(Item[] exitInv)
        {
            using (StreamReader r = new StreamReader(debugpath + "Phoenix.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.baseType == l.baseType));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsMinotaurKill(Item[] exitInv)
        {
            using (StreamReader r = new StreamReader(debugpath + "Minotaur.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.baseType == l.baseType));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsChimeraKill(Item[] exitInv)
        {
            using (StreamReader r = new StreamReader(debugpath + "Chimera.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Models.BossItem>(json);
                var result = items.items.Where(p => exitInv.Any(l => p.baseType == l.baseType));
                if (result.Count() > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
