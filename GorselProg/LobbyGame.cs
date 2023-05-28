using GorselProg.Model;
using GorselProg.Services;
using GorselProg.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
        
        //Aktif paneli bu değişkende tutuyoruz.
        public static Panel active_panel;

        //Lobi yüklendiğinde çalışacak olan komutlar
        private void LobbyGame_Load(object sender, EventArgs e)
        {
            //Bu olaylar form beklenmedik şekilde kapanırsa uygulanacak olan komutların bağlantısı.
            //Application.ApplicationExit += new EventHandler(ApplicationExitHandler);
            //Application.ThreadExit += new EventHandler(ThreadExitHandler);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            

            //Lobinin yukarısında oda ismi ve kodun görüntülenmesini sağlıyor.
            string room_code = RoomSession.Instance.GetCurrentRoom().Code;
            string room_name = RoomSession.Instance.GetCurrentRoom().Name;
            lblPlayerRoomName.Text = $"{room_name} #{room_code}";
            lblLeaderRoomName.Text = $"{room_name} #{room_code}";

            //Adminin veya kullanıcının lobiyi oluşturmasına göre hangi refresh timerlarının başlatılacağı.
            User currentuser = UserSession.Instance.GetCurrentUser();
            Room room = RoomSession.Instance.GetCurrentRoom();
            if (currentuser.Id.Equals(room.AdminId))
            {
                active_panel = pnlLobbyLeader;
                PanelHandler.setPanelFill(active_panel, pnlLobbyLeader);
                active_panel = pnlLobbyLeader;

                timerForChatLeader.Start();
                MessageBox.Show("Liderin chat timerı başlatıldı.");
                timerForPlayersLeader.Start();
                MessageBox.Show("Liderin oyuncular timerı başlatıldı.");
            }
            else
            {
                active_panel = pnlLobbyPlayer;
                PanelHandler.setPanelFill(active_panel, pnlLobbyPlayer);
                active_panel = pnlLobbyPlayer;

                timerForChat.Start();
                MessageBox.Show("Oyuncunun chat timerı başlatıldı.");
                timerForPlayers.Start();
                MessageBox.Show("Oyuncunun oyuncular timerı başlatıldı.");
                timerForCheckCurrGame.Start();
                MessageBox.Show("Oyuncunun oyunu bekleme timerı başlatıldı.");
            }

            //Tüm forma geçerli temanın uygulanmasını sağlıyor.
            ThemeHandler.changeFormsColor(this);
            ThemeHandler.changeAllControlsColor(this);
            this.WindowState = FormWindowState.Maximized;
        }

        

        #region Lobiden çıkış işlemleri
        //Oyuncu lobiden çıkış yapma butonuna bastığında gerçekleşen işlemler.
        private async void btnPlayerLeave_Click(object sender, EventArgs e)
        {
            isLeaving = true;
            closeCounter++;
            this.Close();
        }

        //Lider lobiden çıkış yapmak istediğinde gerçekleşen işlemler.
        private async void btnLeaderLeave_Click(object sender, EventArgs e)
        {
            isLeaving = true;
            closeCounter++;
            this.Close();
        }

        #endregion

        #region Liderin Kategori Secimi
        //Lider kategori seçtiği zaman hem butonları yeşil yapan hem de seçilen kategori listesine ekleme yapması için Helper'ı çağıran metot.
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

        //Spor kategorisi seçildiğinde 0 indexini togglebuttons'a gönderiyor.
        private void btnLeaderSpor_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 0);
        }

        //Bilim kategorisi seçildiğinde 1 indexini togglebuttons'a gönderiyor.
        private void btnLeaderBilim_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 1);
        }

        //Tarih kategorisi seçildiğinde 2 indexini togglebuttons'a gönderiyor.
        private void btnLeaderTarih_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 2);
        }

        //Sanat kategorisi seçildiğinde 3 indexini togglebuttons'a gönderiyor.
        private void btnLeaderSanat_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 3);
        }

        //Eğlence kategorisi seçildiğinde 4 indexini togglebuttons'a gönderiyor.
        private void btnLeaderEglence_Click(object sender, EventArgs e)
        {
            toggleButtons(sender, 4);
        }
        #endregion

        #region Tüm Mesajlaşma İşlemleri

        #region Mesaj Gonderme Butonlari

        //Liderin mesaj göndermesini sağlayan buton.
        private void btnLeaderMsgSend_Click(object sender, EventArgs e)
        {
            sendMessageLeader();
        }

        //Oyuncunun mesaj göndermesini sağlayan buton.
        private void btnPlayerSend_Click(object sender, EventArgs e)
        {
            sendMessagePlayer();
        }
        #endregion

        #region Enter Ile Mesaj Gonderme
        //Textbox seçiliyken Enter'a tıklandığında çalışan metotlar.
        private void txtLeaderMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                sendMessageLeader();
            }
        }
        //Textbox seçiliyken Enter'a tıklandığında çalışan metotlar.
        private void txtPlayerMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendMessagePlayer();
            }
        }
        #endregion

        #region Mesaj Gonderme Metotlari
        //Oyuncu mesaj gönderirken mesajı oluşturma ve database'de güncelleme işlemi.
        private async void sendMessagePlayer()
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
        //Lider mesaj gönderirken mesajı oluşturma ve database'de güncelleme işlemi.
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

        #endregion

        #region Timers
        int closeCounter = 0;

        //Liderin oyuncu listesini güncelleyen timer'ın ticki.
        private async void timerForPlayersLeader_Tick(object sender, EventArgs e)
        {
            lvLeaderPlayers.Items.Clear();
            Room room = RoomSession.Instance.GetCurrentRoom();
            if (room == null)
            {
                stopAllTimers();
                closeCounter++;
                this.Close();
                return;
            }
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

        //Liderin sohbet listesini güncelleyen timer'ın ticki.
        private async void timerForChatLeader_Tick(object sender, EventArgs e)
        {
            lvLeaderChat.Items.Clear();
            Room room = RoomSession.Instance.GetCurrentRoom();
            if (room == null)
            {
                stopAllTimers();
                closeCounter++;
                this.Close();
                return;
            }
            List<Model.Message> messages = await MessageService.GetMessagesByRoomId(room.Id);
            foreach (Model.Message m in messages)
            {
                string user = m.User.UserName;
                string message = m.MessageText;
                ListViewItem item = new ListViewItem(user);
                item.SubItems.Add(message);
                lvLeaderChat.Items.Add(item);
            }
            lvLeaderChat.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            lvLeaderChat.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            if (lvLeaderChat.Items.Count > 0)
                lvLeaderChat.EnsureVisible(lvLeaderChat.Items.Count - 1);
            
        }

        //Oyuncuların oyuncu listesini güncelleyen timer'ın ticki.
        private async void timerForPlayers_Tick(object sender, EventArgs e)
        {
            lvPlayerPlayers.Items.Clear();
            Room roomSession = RoomSession.Instance.GetCurrentRoom();

            if(roomSession == null)
            {
                stopAllTimers();
                closeCounter++;
                this.Close();
                return;
            }

            //using (var context = new qAppDBContext())
            //{
            //    var room = await context.Rooms.FirstOrDefaultAsync(r=>r.Id == roomSession.Id);
            //    roomSession = room;
            //}

            var user = UserSession.Instance.GetCurrentUser();
            if(roomSession.AdminId == user.Id)
            {
                PanelHandler.setPanelFill(active_panel, pnlLobbyLeader);
                active_panel = pnlLobbyLeader;
                stopPlayerTimers();
                startLeaderTimers();
            }
            List<User> players = await RoomService.GetPlayers(roomSession.Id);
            foreach (User u in players)
            {
                string guid = u.Id.ToString();
                string username = u.UserName;
                ListViewItem item = new ListViewItem(guid);
                item.SubItems.Add(username);
                lvPlayerPlayers.Items.Add(item);
            }
            lvPlayerPlayers.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            
            bool isPlayer = await RoomService.CheckRoomStatus(user.Id, roomSession.Id);

            if(!isPlayer)
            {
                stopAllTimers();
                isLeaving = true;
                closeCounter++;
                this.Close();
            }
        }
        
        //Oyuncuların sohbet listesini güncelleyen timer'ın ticki.
        private async void timerForChat_Tick(object sender, EventArgs e)
        {
            lvPlayerChat.Items.Clear();
            Room room = RoomSession.Instance.GetCurrentRoom();
            if (room == null)
            {
                stopAllTimers();
                closeCounter++;
                this.Close();
                return;
            }
            List<Model.Message> messages = await MessageService.GetMessagesByRoomId(room.Id);
            foreach (Model.Message m in messages)
            {
                string user = m.User.UserName;
                string message = m.MessageText;
                ListViewItem item = new ListViewItem(user);
                item.SubItems.Add(message);
                lvPlayerChat.Items.Add(item);
            }
            lvPlayerChat.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            lvPlayerChat.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        //Oyuncular için, liderin oyunu başlatıp başlatmadığını 500ms tick ile kontrol eden timer.
        private async void timerForCheckCurrGame_Tick(object sender, EventArgs e)
        {
            Room curr_room = RoomSession.Instance.GetCurrentRoom();
            if (curr_room == null)
            {
                //stopPlayerTimers();
                stopAllTimers();
                closeCounter++;
                this.Close();
                return;
            }
            var isReadyToPlay = await RoomService.CheckCurrentGame(curr_room.Id);
            if (isReadyToPlay)
            {
                var curr_game = GameSession.Instance.GetCurrentGame();

                question_list = GameSession.Instance.GetAllQuestions();
                question_index = 0;
                printQuestion();

                PanelHandler.setPanelFill(active_panel, pnlGameStarting);
                active_panel = pnlGameStarting;
                timerForStart.Start();
                timerForCheckCurrGame.Stop();

            }
        }

        //Oyun bittikten sonra lobiye dönüşte kullanılan panel.
        int returnLobby = 20;
        private void timerForReturnLobby_Tick(object sender, EventArgs e)
        {

            Room current_room = RoomSession.Instance.GetCurrentRoom();
            if (current_room == null)
            {
                stopAllTimers();
                closeCounter++;
                this.Close();
                return;
            }

            lblReturnLobby.Text = $"{returnLobby.ToString()} saniye içinde lobiye dönülecek...";
            returnLobby--;
            if (returnLobby == 0)
            {
                if (UserSession.Instance.GetCurrentUser().Id == current_room.AdminId)
                {
                    PanelHandler.setPanelFill(active_panel, pnlLobbyLeader);
                    active_panel = pnlLobbyLeader;
                }
                else
                {
                    PanelHandler.setPanelFill(active_panel, pnlLobbyPlayer);
                    active_panel = pnlLobbyPlayer;
                    timerForCheckCurrGame.Start();
                }
                returnLobby = 20;
                timerForReturnLobby.Stop();
            }
        }

        #endregion

        #region Cevaplar
        //Sorulara cevap vermek için kullanılması gereken metotlar. Butonlar, kendi indexlerini metota gönderiyorlar.
        private void btnOption1_Click(object sender, EventArgs e)
        {
            answerQuestion(0);
            disableOtherOptionButtons((Button)sender);
        }

        private void btnOption2_Click(object sender, EventArgs e)
        {
            answerQuestion(1);
            disableOtherOptionButtons((Button)sender);
        }

        private void btnOption3_Click(object sender, EventArgs e)
        {
            answerQuestion(2);
            disableOtherOptionButtons((Button)sender);
        }

        private void btnOption4_Click(object sender, EventArgs e)
        {
            answerQuestion(3);
            disableOtherOptionButtons((Button)sender);
        }

        //Cevap verilen butonu yeşil, ve hepsini disable et.
        Button[] option_buttons_list;
        private void disableOtherOptionButtons(Button sender)
        {
            option_buttons_list = new Button[] { btnOption1, btnOption2, btnOption3, btnOption4 };
            sender.ForeColor = Color.Green;

            foreach(Button btn in option_buttons_list)
            {
                btn.Enabled = false;
            }
        }

        private void resetAllOptionButtons()
        {
            option_buttons_list = new Button[] { btnOption1, btnOption2, btnOption3, btnOption4 };

            foreach (Button btn in option_buttons_list)
            {
                btn.Enabled = true;
                btn.ForeColor = ThemeHandler.color_texts;
            }
        }
        #endregion

        #region Oyun Baslangici

        //Oyunun global değişkenleri
        int game_timer = 0;
        int time_for_question = 10;
        int[] remaining_seconds = new int[10];
        int question_index;
        List<Question> question_list;
        Question current_question;
        string[] options;

        //Liderin oyunu başlatmasını sağlayan buton.
        private void btnLeaderBaslat_ClickAsync(object sender, EventArgs e)
        {
            List<Category> categories = RoomSession.Instance.GetSelectedCategories();
            if (categories.Count != 0)
            {
                startGame();
                PanelHandler.setPanelFill(active_panel, pnlGameStarting);
                active_panel = pnlGameStarting;
                timerForStart.Start();
            }
            else
            {
                MessageBox.Show("Kategori Seçmediniz.");
            }
               
        }

        //Yükleme süresi bittiği anda çalışan metot.
        private async void startGame()
        {
            //Roomsessionda tutulan önbellekteki seçilen kategorileri getiriyoruz.
            List<Category> categories = RoomSession.Instance.GetSelectedCategories();
            Room room = RoomSession.Instance.GetCurrentRoom();

            //İlgili servisin oyunu başlatması ve database'e kaydetmesi.
            var result = await GameService.StartGame(room.Id, categories, DateTime.Now, DateTime.Now.AddMinutes(10));

            if (result != null) //Oyunu başlatma başarılı
            {
                //MessageBox.Show("Oyun başladı!");
                question_list = GameSession.Instance.GetAllQuestions();
                question_index = 0;
            }
            else //Oyun başlatılamadı.
            {
                MessageBox.Show("Oyun başlatılamadı.");
            }
        }

        //Soru cevaplama için verilen sürenin güncellenmesi.
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

        //Oyun başlamadan önceki Yarışma Başlıyor paneli.
        int loading = 3;
        private void timerForStart_Tick(object sender, EventArgs e)
        {
            lblGameStartingCD.Text = loading.ToString();
            loading--;

            if (loading == -1)
            {
                timerForStart.Stop();
                printQuestion();
                PanelHandler.setPanelFill(active_panel, pnlGame);
                active_panel = pnlGame;
                loading = 3;
            }
        }

        //Sonraki soruya geçişte kullanılan Sonraki Soru paneli.
        int next;
        private void timerForNextLoading_Tick(object sender, EventArgs e)
        {
            lblNextLoading.Text = next.ToString();
            --next;
            if (next == -1)
            {
                timerForNextLoading.Stop();
                printQuestion();
                next = 1;
                
                lblNextLoading.Text = "3";
            }
        }

        //Panel geldiğinde sıradaki soruyu ilgili alanlara basan metot.
        private async void printQuestion()
        {
            resetAllOptionButtons();
            if (question_index == GameSession.Instance.GetAllQuestions().Count)
            {
                getSummary();
                return;
            }
            PanelHandler.setPanelFill(active_panel, pnlGame);
            active_panel = pnlGame;

            //Soruyu çekiyoruz.
            current_question = question_list[question_index];

            string question_text = current_question.QuestionText;
            options = Helper.SplitString(current_question.OptionsText);
            int correct_ans_index = current_question.CorrectAnswerIndex;

            lblSoru.Text = question_text;
            btnOption1.Text = options[0];
            btnOption2.Text = options[1];
            btnOption3.Text = options[2];
            btnOption4.Text = options[3];

            Category curr_question_category = null;

            using (var db = new qAppDBContext())
            {
                var Category = await db.Categories.FirstOrDefaultAsync(c => c.Id == current_question.CategoryId );
                if (Category != null)
                    curr_question_category = Category;
            }

            lblCategory.Text = curr_question_category.Name.ToString();

            question_index++;
            timerForGame.Start();
        }

        //Cevabı servis yardımı ile db'e ileten metot.
        private async void answerQuestion(int index)
        {
            User current_user = UserSession.Instance.GetCurrentUser();
            Game current_game = GameSession.Instance.GetCurrentGame();

            await GameService.AnswerQuestion(current_user.Id, current_question.Id, current_game.Id, options[index]);
        }

        //Oyun bitiminde sonuç ekranını getiren metot.
        private async void getSummary()
        {
            User curr_user = UserSession.Instance.GetCurrentUser();
            Game curr_game = GameSession.Instance.GetCurrentGame();
            Room curr_room = RoomSession.Instance.GetCurrentRoom();

            //db ile bağlantı kurup özeti önbelleğe alıyoruz.
            var summary = await GameService.GetSummaryGame(curr_game.Id, curr_user.Id,curr_room.Id);

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
            //Oynanan oyunu kapatıyor, diğer oyuncular otomatik yeni bir oyuna başlamasın.
            Helper.ClearGameSession();
            //Özetten belli bir süre sonra lobiye dönülmesi gerekiyor.
            timerForReturnLobby.Start();
        }

        //Sonraki soru yükleme ekranı
        private void nextLoading()
        {
            lblTimeLeft.Text = "10";
            PanelHandler.setPanelFill(active_panel, pnlNextLoading);
            active_panel = pnlNextLoading;

            timerForNextLoading.Start();
        }

        #endregion

        #region Game Quits

        public bool isLeaving = false;
        //Form kapatılmaya çalışıldığında handle edilir ve db'de ilgili yerler güncellenir.
        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(closeCounter == 1)
            {
                 if (isLeaving)
                    {
                    //stopAllTimers();

                    Model.User curr_user = UserSession.Instance.GetCurrentUser();
                    Room curr_room = RoomSession.Instance.GetCurrentRoom();
                    if (curr_room != null)
                    {
                        await RoomService.ExitRoom(curr_room.Id, curr_user);
                    }
                    closeCounter = 0;
                    formMainMenu f = new formMainMenu();
                    f.Show();
                   
                    return;
                 }
                 else
                 {
                        //stopAllTimers();

                    Model.User curr_user = UserSession.Instance.GetCurrentUser();
                    Room curr_room = RoomSession.Instance.GetCurrentRoom();
                    if (curr_room != null)
                    {
                        await RoomService.ExitRoom(curr_room.Id, curr_user);
                    }
                    Environment.Exit(0);

                }
            }
            

            /*
            if (isLeaving)
            {
                DialogResult result = MessageBox.Show("Ana menüye dönmek istediğinize emin misiniz?", "Ana Menüye Dön", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    // Kapatma işlemini iptal etmek için e.Cancel değerini true olarak ayarlayın
                    e.Cancel = true;

                    stopPlayerTimers();
                    stopLeaderTimers();
                }
                else if(result == DialogResult.Yes)
                {
                    stopPlayerTimers();
                    stopLeaderTimers();
                    

                    
                    await RoomService.ExitRoom(curr_room.Id, curr_user);
                    isLeaving = false;

                    
                }
            }
            else if (!isLeaving)
            {
                Room curr_room = RoomSession.Instance.GetCurrentRoom();
                Model.User curr_user = UserSession.Instance.GetCurrentUser();
                await RoomService.ExitRoom(curr_room.Id, curr_user);
                stopLeaderTimers();
                stopPlayerTimers();
                Environment.Exit(0);
            }*/
            
        }

        //Uygulama kapatılırsa bunun handle edilmesi ve db'de ilgili yerlerin güncellenmesi.
        //private async void ApplicationExitHandler(object sender, EventArgs e)
        //{
        //    Room curr_room = RoomSession.Instance.GetCurrentRoom();
        //    if (curr_room == null)
        //    {
        //        stopPlayerTimers();
        //        return;
        //    }
        //    Model.User curr_user = UserSession.Instance.GetCurrentUser();
        //    await RoomService.ExitRoom(curr_room.Id, curr_user);
        //    stopAllTimers();
        //    //Veritabanından current room dan ilgili kullanıcıyı sileceğiz.
        //}
        
        ////Uygulama olağanüstü bir durumla kapatılırsa bunun kontrol edilip db'nin güncellenmesi.
        //private async void ThreadExitHandler(object sender, EventArgs e)
        //{
        //    Room curr_room = RoomSession.Instance.GetCurrentRoom();
        //    if (curr_room == null)
        //    {
        //        stopPlayerTimers();
        //        return;
        //    }
        //    Model.User curr_user = UserSession.Instance.GetCurrentUser();
        //    await RoomService.ExitRoom(curr_room.Id, curr_user);
        //    stopAllTimers();
        //    //Veritabanından current room dan ilgili kullanıcıyı sileceğiz.
        //}

        #endregion

        #region Timer Kontrolü
        private void stopLeaderTimers()
        {
            MessageBox.Show("Liderin tüm timerları durduruldu.");
            timerForChatLeader.Stop();
            timerForPlayersLeader.Stop();
        }
        private void stopPlayerTimers()
        {
            MessageBox.Show("Oyuncunun tüm timerları durduruldu.");
            stopAllTimers();
            timerForPlayers.Stop();
            timerForChat.Stop();
        }
        private void stopAllTimers()
        {
            timerForChatLeader.Stop();
            timerForPlayersLeader.Stop();
            timerForPlayers.Stop();
            timerForChat.Stop();
            timerForCheckCurrGame.Stop();
            MessageBox.Show("Lider & oyuncu tüm timerları durduruldu.");
        }
        private void startLeaderTimers()
        {
            MessageBox.Show("Liderin tüm timerları başlatıldı.");
            timerForChatLeader.Start();
            timerForPlayersLeader.Start();
        }
        private void startPlayerTimers()
        {
            MessageBox.Show("Oyuncunun tüm timerları başlatıldı.");
            timerForChat.Start();
            timerForPlayers.Start();
        }
        #endregion

        #region Kick ve Ban
        //Liderin, oyuncuyu lobiden atma işlemi
        private async void btnLeaderKick_Click(object sender, EventArgs e)
        {
            // kick the player
            Room curr_room = RoomSession.Instance.GetCurrentRoom();
            Guid userId = Guid.Parse(lvLeaderPlayers.SelectedItems[0].SubItems[0].Text);
            await RoomService.KickUser(curr_room.Id,userId);
        }

        //Liderin, oyuncuyu lobiden banlama işlemi
        private async void btnLeaderBan_Click(object sender, EventArgs e)
        {
            // Ban the player
            Room curr_room = RoomSession.Instance.GetCurrentRoom();
            Guid userId = Guid.Parse(lvLeaderPlayers.SelectedItems[0].SubItems[0].Text);
            User admin = UserSession.Instance.GetCurrentUser();
            await RoomService.BanUser(userId,curr_room.Id,admin.Id);
        }
        #endregion
    }
}
