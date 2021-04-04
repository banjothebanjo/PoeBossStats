using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoeApi
{
    public class PoeCharacterInventoryService
    {
        public RestClient _client;

        public PoeCharacterInventoryService(string baseURL)
        {
            _client = new RestClient(baseURL);
        }

        //Get request to POE api
        public CharacterResponse GetData(string accountName,string characterName, string path)
        {
            var request = new RestRequest(path, Method.GET);
            request.AddParameter(new Parameter("accountName", accountName, ParameterType.QueryString));
            request.AddParameter(new Parameter("character", characterName, ParameterType.QueryString));
            request.AddCookie("POESESSID", ConfigurationManager.AppSettings["poe_SESSIONID"]);

            var response = _client.Execute(request);
            var responseobj = JsonConvert.DeserializeObject<CharacterResponse>(response.Content);

            return responseobj;
        }

        //The API response will contain both equipped items and inventory items, therefor i sort through this here, to only recieve inventory data.
        public CharacterResponse GetMainInventoryData(string accountName,string characterName,string path)
        {
            var response = GetData(accountName, characterName, path);
            var sortedList = response.items.Where(e => e.inventoryId == "MainInventory");
            response.items = sortedList.ToArray();

            return response;
        }
    }
}
