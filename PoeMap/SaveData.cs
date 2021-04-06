using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using PoeApi;
using Newtonsoft.Json;

namespace PoeMap
{
    public class SaveData
    {
        public static SQLiteConnection sqlite_conn;
        public SaveData()
        {
            sqlite_conn = CreateConnection();
        }

        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection($@"Data Source=C:\Users\Benjamin\Documents\GitHub\PoeBossStatsTool\PoeBossStatsData.db");
         // Open the connection:
         try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }


        public void InsertData(MapInstance instance)
        {
            var BKModel = MapDataToDbModel(instance);

            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO BossKills(LocationName,BossType,InstanceEntered,Character,Ascendancy,Level,League,JSON) VALUES (@LocationName,@BossType,@InstanceEntered,@Character,@Ascendancy,@Level,@League,@JSON)", sqlite_conn);
            insertSQL.Parameters.Add("@LocationName", System.Data.DbType.String);
            insertSQL.Parameters.Add("@BossType", System.Data.DbType.String);
            insertSQL.Parameters.Add("@InstanceEntered", System.Data.DbType.String);
            insertSQL.Parameters.Add("@Character", System.Data.DbType.String);
            insertSQL.Parameters.Add("@Ascendancy", System.Data.DbType.String);
            insertSQL.Parameters.Add("@Level", System.Data.DbType.Int32);
            insertSQL.Parameters.Add("@League", System.Data.DbType.String);
            insertSQL.Parameters.Add("@JSON", System.Data.DbType.String);
            insertSQL.Parameters["@LocationName"].Value = BKModel.LocationName;
            insertSQL.Parameters["@BossType"].Value = BKModel.BossType;
            insertSQL.Parameters["@InstanceEntered"].Value = BKModel.InstanceCreated;
            insertSQL.Parameters["@Character"].Value = BKModel.Character;
            insertSQL.Parameters["@Ascendancy"].Value = BKModel.Ascendancy;
            insertSQL.Parameters["@Level"].Value = BKModel.Level;
            insertSQL.Parameters["@League"].Value = BKModel.League;
            insertSQL.Parameters["@JSON"].Value = BKModel.JSON;
            try
            {
                insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public List<BossKillModel> SelectData(string BossType,string League)
        {
            List<BossKillModel> bosskillList = new List<BossKillModel>();
            SQLiteCommand insertSQL = new SQLiteCommand("SELECT * FROM BossKills WHERE League == @League", sqlite_conn);
            insertSQL.Parameters.Add("@BossType", System.Data.DbType.String);
            insertSQL.Parameters.Add("@League", System.Data.DbType.String);
            insertSQL.Parameters["@BossType"].Value = BossType;
            insertSQL.Parameters["@League"].Value = League;
            using (SQLiteDataReader oReader = insertSQL.ExecuteReader())
            {
                while (oReader.Read())
                {
                    bosskillList.Add(new BossKillModel
                    {
                        LocationName = oReader["LocationName"].ToString(),
                        BossType = oReader["BossType"].ToString(),
                        InstanceCreated = oReader["InstanceEntered"].ToString(),
                        Created = oReader["Created"].ToString(),
                        Character = oReader["Character"].ToString(),
                        Ascendancy = oReader["Ascendancy"].ToString(),
                        Level = Int32.Parse(oReader["Level"].ToString()),
                        League = oReader["League"].ToString(),
                        JSON = oReader["JSON"].ToString()
                    });
                }
            }
            return bosskillList;
        }

        public static BossKillModel MapDataToDbModel(MapInstance instance)
        {
            var data = new BossKillModel();
            data.Character = instance.ExitInventory.character.name;
            data.LocationName = instance.LocationName;
            data.Ascendancy = instance.ExitInventory.character._class;
            data.Level = instance.ExitInventory.character.level;
            data.League = instance.ExitInventory.character.league;
            data.BossType = instance.bossType.Value.ToString();
            data.InstanceCreated = instance.InstanceCreated.ToString();

            var JSON = JsonConvert.SerializeObject(instance.FoundItems);
            data.JSON = JSON;

            return data;
        }
    }

    public class BossKillModel
    {
        public string Character { get; set; }
        public string LocationName { get; set; }
        public string BossType { get; set; }
        public string InstanceCreated { get; set; }
        public string Ascendancy { get; set; }
        public int Level { get; set; }
        public string League { get; set; }
        public string JSON { get; set; }
        public string Created { get; set; }
        public List<string> Items { get; set; }

    }

}
