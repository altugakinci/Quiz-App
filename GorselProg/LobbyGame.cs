using GorselProg.Model;
using GorselProg.Services;
using GorselProg.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GorselProg
{
    public partial class LobbyGame : Form
    {
        public LobbyGame()
        {
            InitializeComponent();
        }

        private String rank; //Geçici bir rank değişkeni.
        public LobbyGame(String rank) //Geçici bir constructor.
        {
            InitializeComponent();
            this.rank = rank;
        }
        
        Panel active_panel;

        private void LobbyGame_Load(object sender, EventArgs e)
        {
            Application.ApplicationExit += new EventHandler(ApplicationExitHandler);
            Application.ThreadExit += new EventHandler(ThreadExitHandler);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

            string room_code = RoomSession.Instance.GetCurrentRoom().Code;
            string room_name = RoomSession.Instance.GetCurrentRoom().Name;

            lblPlayerRoomName.Text = $"{room_name} #{room_code}";
            lblLeaderRoomName.Text = $"{room_name} #{room_code}";

            User currentuser = UserSession.Instance.GetCurrentUser();
            Room room = RoomSession.Instance.GetCurrentRoom();

            if (currentuser.Id.Equals(room.AdminId))
            {
                timerForChatLeader.Start();
                timerForPlayersLeader.Start();
            }
            else
            {
                timerForChat.Start();
                timerForPlayers.Start();
            }

            //Lobi lideriyse:
            if (rank.Equals("Leader"))
            {
                active_panel = pnlLobbyLeader;
                PanelHandler.setPanelFill(active_panel, pnlLobbyLeader);
            }
            //Oyuncuysa:
            else if ( rank.Equals("Player")) 
            {
                active_panel = pnlLobbyPlayer;
                PanelHandler.setPanelFill(active_panel, pnlLobbyPlayer);
            }

            ThemeHandler.changeFormsColor(this);
            ThemeHandler.changeAllControlsColor(this);
            this.WindowState = FormWindowState.Maximized;
        }

        private async void btnPlayerLeave_Click(object sender, EventArgs e)
        {
            timerForChat.Stop();
            timerForPlayers.Stop();
            Guid room_id = RoomSession.Instance.GetCurrentRoom().Id;
            User current = UserSession.Instance.GetCurrentUser();
            await RoomService.ExitRoom(room_id, current);

            formMainMenu mainmenu = new formMainMenu();
            mainmenu.Show();
            this.Hide();
        }

        private void btnLeaderLeave_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Oda dağıtılacaktır. Yine de ayrılmak istiyor musunuz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(cevap == DialogResult.Yes)
            {
                formMainMenu mainmenu = new formMainMenu();
                mainmenu.Show();
                this.Hide();
            }            
        }

        bool[] categoryButtons = new bool[] { false,false,false,false,false };

        private void toggleButtons(object sender, int buttonIndex)
        {

            Button button = (Button)sender;

            if (button.ForeColor != Color.Green)
            {
                button.ForeColor = Color.Green;
                Helper.AddSelectedCategory(buttonIndex);
            }
            else
            {
                button.ForeColor = ThemeHandler.color_texts;
                Helper.RemoveSelectedCategory(buttonIndex);
            }

        }

        #region Liderin Kategori Secimi
        private void btnLeaderSpor_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 0);
        }

        private void btnLeaderBilim_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 1);
        }

        private void btnLeaderTarih_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 2);
        }

        private void btnLeaderSanat_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 3);
        }

        private void btnLeaderEglence_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 4);
        }
        #endregion

        private void btnLeaderBaslat_ClickAsync(object sender, EventArgs e)
        {
            PanelHandler.setPanelFill(active_panel, pnlGameStarting);
            active_panel = pnlGameStarting;

            timerForStart.Start();
        }
        bool isPlayerPlay;

        private async void startGame()
        {
            
            var curr_game = GameSession.Instance.GetCurrentGame();
            if(curr_game != null)
            {
                question_list = GameSession.Instance.GetAllQuestions();
                question_index = 0;
                printQuestion();
            }
            else
            {
                // Get the selected categories
                List<Category> categories = RoomSession.Instance.GetSelectedCategories();
                Room room = RoomSession.Instance.GetCurrentRoom();

                var result = await GameService.StartGame(room.Id, categories, DateTime.Now, DateTime.Now.AddMinutes(10));

                if (result != null)
                {
                    //MessageBox.Show("Game started successfully!");
                    question_list = GameSession.Instance.GetAllQuestions();
                    question_index = 0;
                    printQuestion();
                }
                else
                {
                    MessageBox.Show("Failed to start game.");
                }
            }
           

            PanelHandler.setPanelFill(active_panel, pnlGame);
            active_panel = pnlGame;
        }

        private void btnGameGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelFill(active_panel, pnlLobbyLeader);
            active_panel = pnlLobbyLeader;
        }

        #region Mesaj Gonderme Butonlari
        private void btnLeaderMsgSend_Click(object sender, EventArgs e)
        {
            sendMessageLeader();
        }

        private async void btnPlayerSend_Click(object sender, EventArgs e)
        {
            if(txtPlayerMsg.Text != "")
            {
                User current = UserSession.Instance.GetCurrentUser();
                Room room = RoomSession.Instance.GetCurrentRoom();
                string message = txtPlayerMsg.Text;
                txtPlayerMsg.Clear();
                await MessageService.SendMessageAsync(current.Id, message, room.Id);
            }
        }
        #endregion

        #region Enter Ile Mesaj Gonderme
        private void txtLeaderMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                sendMessageLeader();
            }
        }
        private void txtPlayerMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendMessagePlayer();
            }
        }
        #endregion

        #region Mesaj Gonderme Metotlari
        private async void sendMessagePlayer()
        {
            User current = UserSession.Instance.GetCurrentUser();
            Room room = RoomSession.Instance.GetCurrentRoom();
            string message = txtPlayerMsg.Text;
            txtPlayerMsg.Clear();
            await MessageService.SendMessageAsync(current.Id, message, room.Id);
        }

        private async void sendMessageLeader()
        {
            if (txtLeaderMsg.Text != "")
            {
                User current = UserSession.Instance.GetCurrentUser();
                Room room = RoomSession.Instance.GetCurrentRoom();
                string message = txtLeaderMsg.Text;
                txtLeaderMsg.Clear();
                await MessageService.SendMessageAsync(current.Id, message, room.Id);
            }
        }
        #endregion

        #region Listeler ile Timer Handling
        private void lvPlayerPlayers_MouseClick(object sender, MouseEventArgs e)
        {
            timerForPlayers.Stop();
        }

        private void lvLeaderPlayers_MouseClick(object sender, MouseEventArgs e)
        {
            timerForPlayersLeader.Stop();
        }

        private void lvPlayerPlayers_MouseLeave(object sender, EventArgs e)
        {
            timerForPlayers.Start();
        }

        private void lvLeaderPlayers_MouseLeave(object sender, EventArgs e)
        {
            timerForChatLeader.Start();
        }
        #endregion

        #region Timers
        private async void timerForPlayersLeader_Tick(object sender, EventArgs e)
        {
            lvLeaderPlayers.Items.Clear();
            Room room = RoomSession.Instance.GetCurrentRoom();
            List<User> players = await RoomService.GetPlayers(room.Id);
            foreach (User u in players)
            {
                string guid = u.Id.ToString();
                string username = u.UserName;
                ListViewItem item = new ListViewItem(guid);
                item.SubItems.Add(username);
                lvLeaderPlayers.Items.Add(item);
            }
            lvLeaderPlayers.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private async void timerForChatLeader_Tick(object sender, EventArgs e)
        {
            lvLeaderChat.Items.Clear();
            Room room = RoomSession.Instance.GetCurrentRoom();
            List<Model.Message> messages = await MessageService.GetMessagesByRoomId(room.Id);
            foreach (Model.Message m in messages)
            {
                string user = m.User.UserName;
                string message = m.MessageText;
                ListViewItem item = new ListViewItem(user);
                item.SubItems.Add(message);
                lvLeaderChat.Items.Add(item);
            }
            lvLeaderChat.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            if (lvLeaderChat.Items.Count > 0)
                lvLeaderChat.EnsureVisible(lvLeaderChat.Items.Count - 1);
            
        }

        private async void timerForPlayers_Tick(object sender, EventArgs e)
        {
            lvPlayerPlayers.Items.Clear();
            Room room = RoomSession.Instance.GetCurrentRoom();
            List<User> players = await RoomService.GetPlayers(room.Id);
            foreach (User u in players)
            {
                string guid = u.Id.ToString();
                string username = u.UserName;
                ListViewItem item = new ListViewItem(guid);
                item.SubItems.Add(username);
                lvPlayerPlayers.Items.Add(item);
            }
            lvPlayerPlayers.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private async void timerForChat_Tick(object sender, EventArgs e)
        {
            lvPlayerChat.Items.Clear();
            Room room = RoomSession.Instance.GetCurrentRoom();
            List<Model.Message> messages = await MessageService.GetMessagesByRoomId(room.Id);
            foreach (Model.Message m in messages)
            {
                string user = m.User.UserName;
                string message = m.MessageText;
                ListViewItem item = new ListViewItem(user);
                item.SubItems.Add(message);
                lvPlayerChat.Items.Add(item);
            }
            lvPlayerChat.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        #endregion

        #region Cevaplar
        private void btnOption1_Click(object sender, EventArgs e)
        {
            answerQuestion(0);
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            answerQuestion(1);
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            answerQuestion(2);
        }

        private void btnOption4_Click(object sender, EventArgs e)
        {
            answerQuestion(3);
        }
        #endregion

        #region Oyun Baslangici

        int game_timer = 0;
        int time_for_question = 10;
        int[] remaining_seconds = new int[10];
        int question_index;
        List<Question> question_list;
        Question current_question;
        string[] options;

        private void timerForGame_Tick(object sender, EventArgs e)
        {
            game_timer++;
            lblTimeLeft.Text = (time_for_question - game_timer).ToString();
            if(game_timer == time_for_question)
            {
                timerForGame.Stop();
                game_timer = 0;
                next = 3;
                nextLoading();
            }
        }

        int loading = 3;
        private void timerForStart_Tick(object sender, EventArgs e)
        {
            lblGameStartingCD.Text = loading.ToString();
            loading--;

            if (loading == -1)
            {
                timerForStart.Stop();
                startGame();
                return;
            }
        }

        int next;
        private void timerForNextLoading_Tick(object sender, EventArgs e)
        {
            lblNextLoading.Text = next.ToString();
            --next;
            if (next == -1)
            {
                timerForNextLoading.Stop();
                printQuestion();
                PanelHandler.setPanelFill(active_panel, pnlGame);
                active_panel = pnlGame;
                lblNextLoading.Text = "3";
            }
        }

        int returnLobby = 20;
        private void timerForReturnLobby_Tick(object sender, EventArgs e)
        {
            lblReturnLobby.Text = returnLobby.ToString();
            returnLobby--;
            if(returnLobby == 0)
            {
                if(UserSession.Instance.GetCurrentUser().Id == RoomSession.Instance.GetCurrentRoom().AdminId)
                {
                    PanelHandler.setPanelFill(active_panel, pnlLobbyLeader);
                    active_panel = pnlLobbyLeader;
                }
                else
                {
                    PanelHandler.setPanelFill(active_panel, pnlLobbyPlayer);
                    active_panel = pnlLobbyPlayer;
                }
                timerForReturnLobby.Stop();
            }
        }

        private void printQuestion()
        {
            
            if (question_index == 2)
            {
                getSummary();
                return;
            }

            current_question = question_list[question_index];

            string question_text = current_question.QuestionText;
            options = Helper.SplitString(current_question.OptionsText);
            int correct_ans_index = current_question.CorrectAnswerIndex;

            txtGameQuestionText.Text = question_text;
            btnOption1.Text = options[0];
            btnOption2.Text = options[1];
            btnOption3.Text = options[2];
            btnOption4.Text = options[3];

            question_index++;
            timerForGame.Start();
        }

        private async void answerQuestion(int index)
        {
            User current_user = UserSession.Instance.GetCurrentUser();
            Game current_game = GameSession.Instance.GetCurrentGame();

            await GameService.AnswerQuestion(current_user.Id, current_question.Id, current_game.Id, options[index]);
        }

        private async void getSummary()
        {

            User curr_user = UserSession.Instance.GetCurrentUser();
            Game curr_game = GameSession.Instance.GetCurrentGame();

            var summary = await GameService.GetSummaryGame(curr_game.Id, curr_user.Id);

            if(summary != null)
            {
                if(summary.FirstUser != null)
                    lblSumWinnerName.Text = summary.FirstUser.UserName;
                
                if(summary.SecondUser != null)
                    lblSumSecondName.Text = summary.SecondUser.UserName;

                if(summary.ThirdUser != null)
                    lblSumThirdName.Text = summary.ThirdUser.UserName;

                lblSumSpor.Text = summary.Category1Correct.ToString();
                lblSumTarih.Text = summary.Category2Correct.ToString();
                lblSumSanat.Text = summary.Category3Correct.ToString();
                lblSumBilim.Text = summary.Category4Correct.ToString();
                lblSumEglence.Text = summary.Category5Correct.ToString();

                lblSumLevel.Text = $"{summary.Level}. Seviye";
                lblSumXP.Text = $"{summary.SumXP} / 500";
                prgSumXP.Value = summary.SumXP;

                if (summary.isLevelUp)
                    lblSumLevelUp.Visible = true;
                else
                    lblSumLevelUp.Visible = false;
            }
            else
            {
                MessageBox.Show("Bir hata oluştu.");
            }

            PanelHandler.setPanelFill(active_panel, pnlSum);
            active_panel = pnlSum;
            timerForReturnLobby.Start();
        }

        private void nextLoading()
        {
            lblTimeLeft.Text = "10";
            PanelHandler.setPanelFill(active_panel, pnlNextLoading);
            active_panel = pnlNextLoading;

            timerForNextLoading.Start();
        }
        #endregion

        private async void timerForCheckCurrGame_Tick(object sender, EventArgs e)
        {
            Room curr_room = RoomSession.Instance.GetCurrentRoom();
            var isReadyToPlay = await RoomService.CheckCurrentGame(curr_room.Id);
            if (isReadyToPlay) ;
        }

        #region Game Quits

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkmak İstediğinize Emin Misiniz?", "Uygulamadan Çıkış", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                // Kapatma işlemini iptal etmek için e.Cancel değerini true olarak ayarlayın
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void ApplicationExitHandler(object sender, EventArgs e)
        {
            //Veritabanından current room dan ilgili kullanıcıyı sileceğiz.
        }

        private void ThreadExitHandler(object sender, EventArgs e)
        {
            //Veritabanından current room dan ilgili kullanıcıyı sileceğiz.
        }
        #endregion

        
    }
}
