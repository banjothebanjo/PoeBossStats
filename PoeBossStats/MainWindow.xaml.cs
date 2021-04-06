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
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using Lurker;
using PoeMap;
using PoeApi;

namespace PoeBossStats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public PathOfExileProcessLurker _processLurker;
        public MapInstanceHandler instanceHandler = new MapInstanceHandler();
        public PoeCharacterInventoryService apiService;
        public ClientLurker _clientlurker;
        public MapInstance _mapInstance;

        public MainWindow()
        {
            InitializeComponent();
            ClientCreation();
            FillBossListUi();
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
            var enteredmapTemp = instanceHandler.EnteredMap(e.Location, _clientlurker._instanceIp, _mapInstance);
            var leftmapTemp = instanceHandler.LeftMap(e.Location, _clientlurker._instanceIp, _mapInstance);
            if (enteredmapTemp == null && leftmapTemp != null)
            {
                _mapInstance = leftmapTemp;
                FillUi();
            }
            else if (leftmapTemp == null && enteredmapTemp != null)
            {
                _mapInstance = enteredmapTemp;
                FillUi();
            }
        }

        public void FillBossListUi()
        {
            var saveData = new SaveData();
            var tempList = saveData.SelectData("The_Shaper,The_Elder", "Ritual");
            var shaperList = tempList.Where(e => e.BossType == "The_Shaper");
            var elderList = tempList.Where(e => e.BossType == "The_Elder");
            this.ShaperListView.Items.Clear();
            this.ElderListView.Items.Clear();
            foreach (var item in shaperList)
            {
                this.ShaperListView.Items.Add(item);
            }
            foreach (var item in elderList)
            {
                this.ElderListView.Items.Add(item);
            }
        }

        public void FillUi()
        {
            this.EntryInv.Items.Clear();
            this.ExitInv.Items.Clear();
            this.FoundInv.Items.Clear();

            this.Character.Text = _mapInstance.EntryInventory.character.name;
            this.Ascendancy.Text = _mapInstance.EntryInventory.character._class;
            this.Level.Text = _mapInstance.EntryInventory.character.level.ToString();
            this.League.Text = _mapInstance.EntryInventory.character.league;
            instance_IP.Text = _mapInstance.IpAddress;
            Mapname.Text = _mapInstance.LocationName;
            IsBossRoom.Text = _mapInstance.IsBossLocation ? "Yes" : "No";
            TimeEntered.Text = _mapInstance.InstanceCreated.ToString();
            this.bossTypeName.Text = _mapInstance.bossType.ToString();
            foreach (var listItem in _mapInstance.EntryInventory.items)
            {
                this.EntryInv.Items.Add(listItem);
            }
            if (_mapInstance.ExitInventory != null)
            {
                foreach (var listItem in this._mapInstance.ExitInventory?.items)
                {
                    this.ExitInv.Items.Add(listItem);
                }
            }
            if (_mapInstance.FoundItems != null)
            {
                foreach (var listItem in this._mapInstance.FoundItems)
                {
                    this.FoundInv.Items.Add(listItem);
                }
                FillBossListUi();
            }
        }

        private void ExitInv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EntryInv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
