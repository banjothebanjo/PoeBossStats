using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lurker;
using PoeMap;
using PoeApi;

namespace PoeBossStats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PathOfExileProcessLurker _processLurker;
        public PoeCharacterInventoryService apiService;
        public ClientLurker _clientlurker;
        public MapInstance _mapInstance;
        List<string> bossRooms = new List<string>() { "Chayula's Domain", "The Shaper's Realm", "The Apex of Sacrifice", "The Alluring Abyss", "Lair of the Hydra", "Lookout", "Palace", "Tower", "Absence of Value and Meaning", "Forge of the Phoenix", "Pit of the Chimera", "Lair of the Hydra", "Maze of the Minotaur" };
        private bool _isVisible;

        public MainWindow()
        {
            InitializeComponent();
            ClientCreation();
            _clientlurker.LocationChanged += ClientLurker_LocationChanged;
            _clientlurker.Lurk();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public async void ClientCreation()
        {
            // Process Lurker
            this._processLurker = new PathOfExileProcessLurker();
            var process = await this._processLurker.WaitForProcess();
            ClientLurker client = new ClientLurker(process);
            _clientlurker = client;
        }

        public void ClientLurker_LocationChanged(object sender, Lurker.Patreon.Events.LocationChangedEvent e)
        {
            if (this._mapInstance != null && e.Location.EndsWith("Hideout") && _mapInstance.IsBossLocation)
            {
                this.ExitInv.Items.Clear();
                this.FoundInv.Items.Clear();
                this._mapInstance.ExitInventory = CallApi();
                foreach (var listItem in this._mapInstance.ExitInventory.items)
                {
                    this.ExitInv.Items.Add(listItem);
                }
                this._mapInstance.FoundItems = LogBossLoot(this._mapInstance.EntryInventory, this._mapInstance.ExitInventory);
                var bossType = this._mapInstance.GetBossType(_mapInstance.FoundItems, _mapInstance.LocationName);
                this._mapInstance.bossType = bossType;
                this.bossTypeName.Text = bossType.ToString();
                foreach (var listItem in this._mapInstance.FoundItems.items)
                {
                    this.FoundInv.Items.Add(listItem);
                }
            }
            foreach (var item in bossRooms)
            {
                var yes = e.Location.EndsWith(item);
                if (yes && _clientlurker._instanceIp != _mapInstance?.IpAddress)
                {
                    MapInstance mapInstance = new MapInstance(_clientlurker._instanceIp, e.Location, yes);
                    instance_IP.Text = mapInstance.IpAddress;
                    Mapname.Text = mapInstance.LocationName;
                    IsBossRoom.Text = mapInstance.IsBossLocation ? "Yes" : "No";
                    TimeEntered.Text = mapInstance.InstanceCreated.ToString();
                    mapInstance.EntryInventory = CallApi();
                    this.EntryInv.Items.Clear();
                    foreach (var listItem in mapInstance.EntryInventory.items)
                    {
                        this.EntryInv.Items.Add(listItem);
                    }
                    this._mapInstance = mapInstance;
                }
            }
        }

        public CharacterResponse CallApi()
        {
            var apiService = new PoeCharacterInventoryService("https://www.pathofexile.com");
            var response = apiService.GetMainInventoryData("Keymat20", "fifefeefs", "/character-window/get-items");
            return response;
        }

        public CharacterResponse LogBossLoot(CharacterResponse entryInv, CharacterResponse exitInv)
        {
            var result = exitInv.items.Where(p => !entryInv.items.Any(l => p.id == l.id));
            exitInv.items = result.ToArray();
            return exitInv;
        }

        private void ExitInv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EntryInv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
