
using LoginPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginPage.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace LoginPage.ViewModels
{
    public class UserAdminPageViewModel : ViewModel
    {
        //המסך של גל
        //טלסי מזכיר הפיקר לא עובד בגלל הבאג במוצר
        //ויש עוד בעיות לא מזוהות אם טוענים את השחקנים, מנקים ומנסים לטעון שוב
        // בכללי כל פעם שמנסים לטעון שוב
        // הסוויפ ווי לא עובד
        //יש מצב זה בגלל שזה באימולטור של ווינדוס אבל לא הצלחתי ליצור אחד של הטלפון
        //ובגלל זה לא הצלחתי לבדוק בכלל את הסוויפ ווי ואת הפעולות שלו
        //אבל בתקווה זה יעבוד

        //הערה נוספת בזמן הבדיקה בכיתה והמצב מחריד
        //דברים שעבדו לגמרי יום לפני לא עובדים בכיתה
        //בווינדוס, למרות שבו השתמשתי בבית, אי אפשר להגיע בכלל למסך שלי, כי משום זה לא מראה את המניו
        //בטלפון שום כפתור לא עובד וחוץ מלהכניס טקסט שום דבר לא עובד לא הכפתורים לא עושים כלום
        //הפיקר עדיין לא עובד אבל זה בגלל הבאג שאתה אישרת לי להשאיר
        #region Variables
        private List<Player> fullList;
        private List<Player> filteredList;
        private RankService rankService;
        private UserService service;
        private bool isRefreshing;
        private string username;
        private string password;
        private string mail;
        private Player selectedPlayer;
        private Darga selectedDarga;
        private int selectedIndex;
        #endregion
        #region Properties
        public ObservableCollection<Player> Players { get; set; }
        public ObservableCollection<Darga> Dargas { get; set; }
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
        public string Username { get => username; set { username = value; ((Command)AddPlayerCommand).ChangeCanExecute(); } }
        public string Password { get => password; set { password = value; ((Command)AddPlayerCommand).ChangeCanExecute(); } }
        public string Mail { get => mail; set { mail = value; ((Command)AddPlayerCommand).ChangeCanExecute(); } }
        public Player SelectedPlayer { get => selectedPlayer; set { selectedPlayer = value; OnPropertyChanged(); } }
        public Darga SelectedDarga { get => selectedDarga; set { if (selectedDarga != value) { selectedDarga = value; OnPropertyChanged(); FilterPlayers(); } } }
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged(); } }
        #endregion
        #region Commands
        public ICommand RefreshCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand LoadPlayersCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }
        public ICommand ResetPointsCommand { get; private set; }
        public ICommand AddPlayerCommand { get; private set; }
        #endregion
        #region Building Screen
        public UserAdminPageViewModel(UserService service, RankService Rservice)
        {
            CreateDynamicDataBases();
            LoadServices(service, Rservice);
            LoadCommands(service);
        }
        private void CreateDynamicDataBases()
        {
            fullList = new List<Player>();
            Players = new ObservableCollection<Player>();
            Dargas = new ObservableCollection<Darga>();
        }
        private void LoadServices(UserService service, RankService Rservice)
        {
            this.rankService = Rservice;
            this.service = service;
            var Darga = rankService.Dargas;
            Dargas.Clear();
            foreach (var darga in Darga)
            {
                Dargas.Add(darga);
            }
        }
        private void LoadCommands (UserService service)
        {
            RefreshCommand = new Command(async () => await Refresh());
            LoadPlayersCommand = new Command(async () => await LoadPlayers());
            ClearCommand = new Command(async () => await Clear());
            FilterCommand = new Command(async () => await FilterPlayers());
            ClearFilterCommand = new Command(async () => await LoadPlayers());
            DeleteCommand = new Command<Player>(async (p) => await Delete(p));
            ResetPointsCommand = new Command<Player>(async (p) => await ResetPoints(p));
            AddPlayerCommand = new Command(() =>
            {
                Player p = new Player()
                { PlayerName = Username, PlayerPass = Password, PlayerMail = Mail, PLayerPoints = 0, Darga = Dargas[0], PlayerId = service.GetIndexForNewPlayer() };
                Players.Add(p);
                fullList.Add(p);
            });
        }
        #endregion
        #region Tasks
        private async Task Refresh()
        {
            IsRefreshing = true;
            Players.Clear();
            IsRefreshing = false;
        }
        private async Task LoadPlayers()
        {
            fullList = service.GetPlayers();
            Players.Clear();
            foreach (var player in fullList)
            {
                Players.Add(player);
            }
        }
        public async Task Clear()
        {
            Players.Clear();
        }
        private async Task FilterPlayers()
        {
            if (SelectedDarga == null)
            {
                fullList = service.GetPlayers();
                Players.Clear();
                foreach (var player in fullList)
                {
                    Players.Add(player);
                }
            }
            else
            {
                filteredList = fullList.Where(Player => Player.Darga.DargaId == SelectedDarga.DargaId).ToList();
                Players.Clear();
                foreach (var player in filteredList)
                {
                    Players.Add(player);
                }
            }
        }
        private async Task Delete(Player p)
        {
            if (p != null)
            {
                Players.Remove(p);
                SelectedPlayer = null;
            }
        }
        private async Task ResetPoints(Player p)
        {
            if (p != null)
            {
                p.PLayerPoints = 0;
                SelectedPlayer = null;
            }
        }
        #endregion
    }
}
