using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NQTGame;
using System.Drawing;

namespace GameCenter
{
    public abstract class GameHandle : EventComplete
    {
        #region pro d
        public GameData GameData { get; set; }
        //
        public GameForm GameForm { get; private set; } = new GameForm();
        #endregion
        //
        //
        #region pro Game
        public bool IsPlaying { get; set; } = false;
        #endregion
        #region pro timeout
        public double Time { get; set; } = 0;
        public string TimeString { get { return new Time(Time).ToString(); } }
        public int TimeSpeed
        {
            get { return TimeOut.time; }
            set { TimeOut.time = value; }
        }
        public TimeOut TimeOut { get; set; } = new TimeOut();
        #endregion
        //
        //
        #region virtual - abstract function
        public void Run()
        {
            LoadMenuScreen();
        }
        private void TimeOut_Handle(object sender, TimeOutResponse e)
        {
            Time += e.time;
            Update();
        }

        public virtual void Start()
        {
            IsPlaying = true;
            TimeOut = new TimeOut(TimeSpeed);
            TimeOut.Handle += this.TimeOut_Handle;
            TimeOut.Start();
        }
        public virtual void Stop()
        {
            IsPlaying = false;
            TimeOut.Stop();
        }
        public virtual void Pause()
        {
            TimeOut.Stop();
        }
        public virtual void ReStart()
        {
            TimeOut.Start();
        }
        public abstract void Update();
        
        public abstract void InitGame();
        public abstract void LoadGame();
        #endregion
        //
        //
        #region contructor
        public GameHandle(GameData GameData) : this(GameData.GameID, GameData.GameName)
        {

        }
        public GameHandle(int GameID, string GameName) : base()
        {
            GameData = new GameData(GameID, GameName);
            GameForm.Load += GameForm_Load;
        }
        #endregion
        //
        #region init
        public virtual void Init()
        {
            GameForm.FormBorderStyle = FormBorderStyle.None;
            GameForm.WindowState = FormWindowState.Maximized;
            GameForm.LostFocus += GameForm_LostFocus;
            GameForm.GotFocus += GameForm_GotFocus;
            //GameForm.Leave += GameForm_Leave;
            GameForm.Text = GameData.GameName;
            GameForm.Icon = GameData.Icon;
            //
            InitMenuScreen();

            //
            this.SentInitComplete();
        }

        #endregion
        //
        #region form event
        private void GameForm_Load(object sender, EventArgs e)
        {
            Init();
            Run();
            GameForm.Focus();
        }
        //

        private void GameForm_LostFocus(object sender, EventArgs e)
        {
            lost++;
            //GameForm.Text = "got: " + got + " - lost: " + lost + " - leave: " + leave;

        }
        int lost = 0; int got = 0; int leave = 0;
        private void GameForm_GotFocus(object sender, EventArgs e)
        {
            got++;
            GameForm.Focus();
            GameForm.WindowState = FormWindowState.Maximized;
            GameForm.Bounds = Screen.PrimaryScreen.Bounds;
            WinApi.SetWinFullScreen(GameForm.Handle);
            //GameForm.Text = "got: " + got + " - lost: " + lost + " - leave: " + leave;
        }
        private void GameForm_Leave(object sender, EventArgs e)
        {
            leave++;
            GameForm.WindowState = FormWindowState.Minimized;
            WinApi.SetWinMinScreen(GameForm.Handle);
            //GameForm.Text = "got: " + got + " - lost: " + lost + " - leave: " + leave;
        }
        //
        #endregion
        //
        //
        #region screen view
        public Menu MenuView { get; private set; } = new Menu();
        public Pause PauseView { get; private set; } = new Pause();
        public Playing PlayingView { get; private set; } = new Playing();
        public Settings SettingView { get; private set; } = new Settings();
        public About AboutView { get; private set; } = new About();
        public Rank RankView { get; private set; } = new Rank();
        public Exit ExitView { get; private set; } = new Exit();
        //
        //
        public void LoadMenuScreen()
        {
            GameForm.Controls.Clear(); 
            GameForm.Controls.Add(MenuView);
        }
        public void LoadPlayingScreen()
        {
            GameForm.Controls.Clear();
            GameForm.Controls.Add(PlayingView);
        }
        public void LoadPauseScreen()
        {
            GameForm.Controls.Clear();
            GameForm.Controls.Add(PlayingView);
            GameForm.Controls.Add(PauseView);
        }
        public void LoadSettingScreen()
        {
            GameForm.Controls.Clear();
            GameForm.Controls.Add(SettingView);
            if(IsPlaying)
                GameForm.Controls.Add(PlayingView);
            else
                GameForm.Controls.Add(MenuView);
        }
        public void LoadRankScreen()
        {
            GameForm.Controls.Clear();
            GameForm.Controls.Add(RankView);
        }
        public void LoadAboutScreen()
        {
            GameForm.Controls.Clear();
            GameForm.Controls.Add(AboutView);
            if (IsPlaying)
                GameForm.Controls.Add(PlayingView);
            else
                GameForm.Controls.Add(MenuView);
        }
        public void LoadExitScreen()
        {
            GameForm.Controls.Clear();
            GameForm.Controls.Add(ExitView);
            if (IsPlaying)
                GameForm.Controls.Add(PlayingView);
            else
                GameForm.Controls.Add(MenuView);
        }
        #endregion
        //
        #region screen pro
        public Image MenuBackground { get { return MenuView.BackgroundImage; } set { MenuView.BackgroundImage = value; } }
        public Image PlayingBackground { get { return PlayingView.BackgroundImage; } set { PlayingView.BackgroundImage = value; } }
        public Image SettingsBackground { get { return SettingView.BackgroundImage; } set { SettingView.BackgroundImage = value; } }
        public Image PauseBackground { get { return PauseView.BackgroundImage; } set { PauseView.BackgroundImage = value; } }
        public Image AboutBackground { get { return AboutView.BackgroundImage; } set { AboutView.BackgroundImage = value; } }
        public Image RankBackground { get { return RankView.BackgroundImage; } set { RankView.BackgroundImage = value; } }
        public Image ExitBackground { get { return ExitView.BackgroundImage; } set { ExitView.BackgroundImage = value; } }
        #endregion
        //
        #region screen view init
        //
        //
        #region Menu
        Button btn_Play = new Button();
        Button btn_continute = new Button();
        Button btn_setting = new Button();
        Button btn_rank = new Button();
        Button btn_about = new Button();
        Button btn_exit = new Button();
        private void InitMenuScreen()
        {
            MenuView.Controls.Add(btn_Play);
            MenuView.Controls.Add(btn_continute);
            MenuView.Controls.Add(btn_rank);
            MenuView.Controls.Add(btn_setting);
            MenuView.Controls.Add(btn_about);
            MenuView.Controls.Add(btn_exit);
            //
            //
            btn_Play.Text = "Chơi Mới";
            btn_continute.Text = "Chơi Tiếp";
            btn_setting.Text = "Cài đặt";
            btn_rank.Text = "Bảng xếp hạng";
            btn_about.Text = "Thông tin";
            btn_exit.Text = "Thoát";
            MenuView.Dock = DockStyle.Fill;
            int w = (int)(GameForm.Width * 0.2f);
            int h = w / 4;
            btn_Play.Size = btn_continute.Size = btn_setting.Size = btn_rank.Size = btn_about.Size = btn_exit.Size = new Size(w, h);
            int k = h / 3;
            int f = k * 5 + h * 6;
            int first = (GameForm.Height - f) / 2;
            int m_lr = (GameForm.Width - btn_Play.Width) / 2;
            //
            btn_Play.Location = new Point(m_lr, first);
            btn_continute.Location = new Point(m_lr, btn_Play.Location.Y + btn_Play.Height + k);
            btn_rank.Location = new Point(m_lr, btn_continute.Location.Y + btn_continute.Height + k);
            btn_setting.Location = new Point(m_lr, btn_rank.Location.Y + btn_rank.Height + k);
            btn_about.Location = new Point(m_lr, btn_setting.Location.Y + btn_setting.Height + k);
            btn_exit.Location = new Point(m_lr, btn_about.Location.Y + btn_about.Height + k);
            //
            //
            btn_Play.BackColor =
            btn_continute.BackColor =
            btn_rank.BackColor = 
            btn_setting.BackColor = 
            btn_about.BackColor = 
            btn_exit.BackColor = Color.Green;
            //
            btn_Play.ForeColor =
            btn_continute.ForeColor =
            btn_rank.ForeColor =
            btn_setting.ForeColor =
            btn_about.ForeColor =
            btn_exit.ForeColor = Color.White;
            //
            btn_Play.Font =
            btn_continute.Font =
            btn_rank.Font =
            btn_setting.Font =
            btn_about.Font =
            btn_exit.Font = new Font("Arial", 13, FontStyle.Bold);
            //
            btn_Play.Cursor =
            btn_continute.Cursor =
            btn_rank.Cursor =
            btn_setting.Cursor =
            btn_about.Cursor =
            btn_exit.Cursor = Cursors.Hand;
            //
            btn_Play.MouseHover += Btn_Menu_MouseHover;
            btn_continute.MouseHover += Btn_Menu_MouseHover;
            btn_rank.MouseHover += Btn_Menu_MouseHover;
            btn_setting.MouseHover += Btn_Menu_MouseHover;
            btn_about.MouseHover += Btn_Menu_MouseHover;
            btn_exit.MouseHover += Btn_Menu_MouseHover;
            //
            btn_Play.MouseLeave += Btn_Menu_MouseLeave;
            btn_continute.MouseLeave += Btn_Menu_MouseLeave;
            btn_rank.MouseLeave += Btn_Menu_MouseLeave;
            btn_setting.MouseLeave += Btn_Menu_MouseLeave;
            btn_about.MouseLeave += Btn_Menu_MouseLeave;
            btn_exit.MouseLeave += Btn_Menu_MouseLeave;
            //
            //
            btn_Play.Click += Btn_Play_Click;
            btn_continute.Click += Btn_continute_Click;
            btn_rank.Click += Btn_rank_Click;
            btn_setting.Click += Btn_setting_Click;
            btn_about.Click += Btn_about_Click;
            btn_exit.Click += Btn_exit_Click;
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            LoadExitScreen();
        }

        private void Btn_about_Click(object sender, EventArgs e)
        {
            LoadAboutScreen();
        }

        private void Btn_setting_Click(object sender, EventArgs e)
        {
            LoadSettingScreen();
        }

        private void Btn_rank_Click(object sender, EventArgs e)
        {
            LoadRankScreen();
        }

        private void Btn_continute_Click(object sender, EventArgs e)
        {
            LoadGame();
            Start();
        }

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            InitGame();
            Start();
        }

        private void Btn_Menu_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Green;
        }

        private void Btn_Menu_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Blue;
        }
        #endregion
        //
        //

        #endregion
    }
}
