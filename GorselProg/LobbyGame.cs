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

        private async void btnLeaderBaslat_ClickAsync(object sender, EventArgs e)
        {
            

             // Get the selected categories
             
             List<Category> categories = RoomSession.Instance.GetSelectedCategories();

             Room room = RoomSession.Instance.GetCurrentRoom();
             // Call the StartGame method
             var result = await GameService.StartGame(room.Id, categories, DateTime.Now,DateTime.Now.AddMinutes(10));

             if (result != null)
             {
                //MessageBox.Show("Game started successfully!");
                question_list = GameSession.Instance.GetAllQuestions();
                question_index = 0;
                printQuestion();
                timerForGame.Start();
             }
             else
             {
                 MessageBox.Show("Failed to start game.");
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
            User current = UserSession.Instance.GetCurrentUser();
            Room room = RoomSession.Instance.GetCurrentRoom();
            string message = txtPlayerMsg.Text;
            txtPlayerMsg.Clear();
            await MessageService.SendMessageAsync(current.Id, message, room.Id);
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
            User current = UserSession.Instance.GetCurrentUser();
            Room room = RoomSession.Instance.GetCurrentRoom();
            string message = txtLeaderMsg.Text;
            txtLeaderMsg.Clear();
            await MessageService.SendMessageAsync(current.Id, message, room.Id);
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


        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            timerForPlayers.Start();
        }


        private void btnShowSum_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelFill(active_panel, pnlSum);
            active_panel = pnlSum;
            prgSumXP.Value = 50;
        }

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
        int time_for_question = 5;
        int[] remaining_seconds = new int[10];
        int question_index;
        List<Question> question_list;
        Question current_question;
        string[] options;

        private void timerForGame_Tick(object sender, EventArgs e)
        {
            game_timer++;
            if(game_timer == time_for_question)
            {
                printQuestion();
                game_timer = 0;
            }
        }

        private void printQuestion()
        {

            if(question_index == 2)
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

            }

            PanelHandler.setPanelFill(active_panel, pnlSum);
            active_panel = pnlSum;
        }

        private void nextLoading()
        {
            
        }
        #endregion

        private void timerForCheckCurrGame_Tick(object sender, EventArgs e)
        {

        }
    }
}
