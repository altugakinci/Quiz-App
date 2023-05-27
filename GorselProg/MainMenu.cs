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
using System.Drawing.Text;

namespace GorselProg
{
    public partial class formMainMenu : Form
    {
        public formMainMenu()
        {
            InitializeComponent();
        }

        //Aktif panelin tutulduğu değişken
        static Panel active_panel;

        //Ana Menü Formu yüklendiğinde çalışacak komutlar.
        private void Game_Load(object sender, EventArgs e)
        {
            active_panel = pnlMainMenu;
            ThemeHandler.changeAllControlsColor(this);
            ThemeHandler.changeFormsColor(this);
            this.WindowState = FormWindowState.Maximized;
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMainMenu_FormClosing);
        }

        #region Routing

        //Oturumu kapatma butonu
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            isLeaving = true;
            UserService.LogoutUser();
            formLoginRegister form = new formLoginRegister();
            form.Show();
            this.Hide();
        }

        //Gamebuilderi açar, Create ve Join işlemleri gelir.
        private void btnPlay_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlBuildAGame);
            active_panel = pnlBuildAGame;
        }

        //Oda kurma panelini açar.
        private void btnCreateAGame_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlCreateAGame);
            active_panel = pnlCreateAGame;
        }

        //Odaya katılma panelini açar.
        private void btnJoinAGame_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlJoinAGame);
            active_panel = pnlJoinAGame;
        }

        //Nasıl oynanır panelini açar.
        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlHowToPlay);
            active_panel = pnlHowToPlay;
        }

        //Seçenekler panelini açar.
        private void btnPreferences_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlPreferences);
            active_panel = pnlPreferences;
        }

        //Create-Join seçilen panelden Ana Menüye dönmeyi sağlar.
        private void btnBAGBack_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }

        //Odaya katılma panelinde buildera geri döner.
        private void btnJAGGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlBuildAGame);
            active_panel = pnlBuildAGame;
        }

        //Odayı oluşturma panelinden buildera geri döner.
        private void btnCAGGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlBuildAGame);
            active_panel = pnlBuildAGame;
        }

        //Nasıl oynanır panelinden Ana Menüye dönmeyi sağlar.
        private void btnHTPGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }

        //Seçeneklerden Ana Menüye dönmeyi sağlar.
        private void btnPreferencesGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }

        private void btnProfileGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }

        private void btnSorular_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSorular);
            active_panel = pnlSorular;
        }

        private void btnSorularGeri_Click(object sender, EventArgs e)
        {

            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }

        private void btnSoruEkleGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSorular);
            active_panel = pnlSorular;
        }

        private void btnSorulariGoruntule_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSorular);
            active_panel = pnlSorular;
        }

        private void btnSorularSoruEkle_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSoruEkle);
            active_panel = pnlSoruEkle;
        }

        private void btnSorulariGoruntuleGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSorular);
            active_panel = pnlSorular;
        }

        private void btnSoruGuncelleGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSorular);
            active_panel = pnlSorular;
        }

        private void btnSorularGoruntule_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSorulariGoruntule);
            active_panel = pnlSorulariGoruntule;

            viewQuestions("all");
        }

        private void btnProfiliDuzenle_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlProfileDuzenle);
            active_panel = pnlProfileDuzenle;
            txtPDUsername.Text = lblProfileUsername.Text;
            txtPDMail.Text = lblProfileMail.Text;
        }

        private void btnProfiliDuzenleGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlProfile);
            active_panel = pnlProfile;
        }

        private async void btnProfile_Click(object sender, EventArgs e)
        {
            User current = UserSession.Instance.GetCurrentUser();
            Objects.UserGamesSummary sum = await UserService.GetUserGamesSummary(current.Id);
            lblProfileUsername.Text = current.UserName;
            lblProfileMail.Text = current.Email;
            lblProfileLevel.Text = $"{current.Level}. Level";
            lblProfileXP.Text = $"{current.Xp} / 500";
            prgProfileXP.Value = current.Xp;

            lblProfilePlayedGames.Text = sum.TotalGamesPlayed.ToString();
            lblProfileWins.Text = sum.WonGames.ToString();

            //spor tarih sanat bilim eğl
            lblProfileSpor.Text = sum.Category1Correct.ToString();
            lblProfileTarih.Text = sum.Category2Correct.ToString();
            lblProfileSanat.Text = sum.Category3Correct.ToString();
            lblProfileBilim.Text = sum.Category4Correct.ToString();
            lblProfileEglence.Text = sum.Category5Correct.ToString();

            PanelHandler.setPanelMiddle(this, active_panel, pnlProfile);
            active_panel = pnlProfile;
        }

        #endregion

        //Açık ve koyu temaların ayarlanması
        #region Themes
        private void btnLightMode_Click(object sender, EventArgs e)
        {
            ThemeHandler.setLightTheme();
            ThemeHandler.changeAllControlsColor(this);
            ThemeHandler.changeFormsColor(this);
        }

        private void btnDarkMode_Click(object sender, EventArgs e)
        {
            ThemeHandler.setDarkTheme();
            ThemeHandler.changeAllControlsColor(this);
            ThemeHandler.changeFormsColor(this);
        }
        #endregion

        #region Oda oluşturma - Katılma Modülleri

        private async void btnCAGCreateRoom_ClickAsync(object sender, EventArgs e)
        {
            //Oda ismi ve şifrenin boş olup olmadığının kontrolü
            if (txtCAGRoomName.Text == "" || txtCAGRoomPassword.Text == "")
            {
                MessageBox.Show("Oda ismi veya şifreyi eksiksiz girdiğinize emin olun!", "Oda Oluşturma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            User admin = UserSession.Instance.GetCurrentUser();

            //List<Category> categories = RoomSession.Instance.GetAllCategories();

            // Oda oluşturma işlemini yap
            var newRoom = new Room
            {
                Id = Guid.NewGuid(),
                Name = txtCAGRoomName.Text,
                Password = txtCAGRoomPassword.Text,
                Code = Helper.GenerateRoomCode(),
                AdminId = admin.Id,

            };
            //Servis başarılı sonuç döndüğünde kullanıcıyı bilgilendiriyoruz.
            bool isRoomCreated = await RoomService.CreateRoom(newRoom);

            if (isRoomCreated)
            {
                // Room oluşturulduğunda yapılacak işlemler
                isLeaving = true;
                LobbyGame game = new LobbyGame();
                game.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Oda oluşturulurken bir hata oluştu.");
            }


            

        }

        private async void btnJAGJoinRoom_ClickAsync(object sender, EventArgs e)
        {
            string roomCode = txtJoinCode.Text;
            string roomPassword = txtJoinPassword.Text;
            User currentUser = UserSession.Instance.GetCurrentUser();


            bool joined = await RoomService.JoinRoom(roomCode, roomPassword, currentUser);

            if (joined)
            {
                isLeaving = true;
                //MessageBox.Show("Odaya katılma işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LobbyGame game = new LobbyGame();
                game.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Odaya katılma işlemi başarısız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Odaya katılma işlemi başarısız olduğunda yapılacak işlemler
            }

        }

        #endregion

        #region Profil İşlemleri

        private async void btnProfiliKaydet_Click(object sender, EventArgs e)
        {
            if (txtPDMevcutSifre.Text == "" && txtPDYeniSifre.Text == "" && txtPDYeniSifreTekrar.Text == "" && txtPDMail.Text == "" && txtPDUsername.Text == "")
            {
                // TODO: uygun bir hata mesajı
            }
            else if (txtPDYeniSifre.Text != txtPDYeniSifreTekrar.Text)
            {
                // Todo: uygun bir hata mesajı
            }

            string currPassword = txtPDMevcutSifre.Text;
            Guid currUser = UserSession.Instance.GetCurrentUser().Id;

            string newPassword = txtPDYeniSifre.Text;
            string newMail = txtPDMail.Text;
            string newUserName = txtPDUsername.Text;

            var updateUser = new User
            {
                UserName = newUserName,
                Email = newMail,
                Password = newPassword,
            };

            bool isSaved = await UserService.UpdateUser(updateUser, currPassword, currUser);

            if (isSaved)
            {
                //TODO: uygun bir hata mesajı
                // TODO: profil sayfasına geri yönlendirme
            }
            else
            {
                // Todo: uygun bir hata mesajı
            }


        }

        #endregion

        #region Soruları Görüntüleme
        private async void viewQuestions(string category)
        {
            // ListView temizle
            lvSorular.Items.Clear();

            // Soruları veritabanından çek
            List<Question> questions = await QuestionService.GetAllQuestions();
            int count = 0;
            // Soruları ListView'e ekle
            if (category.Equals("all"))
            {
                foreach (Question question in questions)
                {
                    ListViewItem item = new ListViewItem(question.Id.ToString());
                    string[] options = Helper.SplitString(question.OptionsText);
                    //item.SubItems.Add(question.Id.ToString());
                    item.SubItems.Add(question.Category.Name);
                    item.SubItems.Add(question.QuestionText);
                    item.SubItems.Add(options[0]);
                    item.SubItems.Add(options[1]);
                    item.SubItems.Add(options[2]);
                    item.SubItems.Add(options[3]);

                    item.SubItems.Add(question.CorrectAnswerIndex.ToString());
                    count++;
                    lvSorular.Items.Add(item);
                }
            }
            else
            {
                foreach (Question question in questions)
                {
                    if (question.Category.Name.Equals(category))
                    {
                        ListViewItem item = new ListViewItem(question.Id.ToString());
                        string[] options = Helper.SplitString(question.OptionsText);
                        //item.SubItems.Add(question.Id.ToString());
                        item.SubItems.Add(question.Category.Name);
                        item.SubItems.Add(question.QuestionText);
                        item.SubItems.Add(options[0]);
                        item.SubItems.Add(options[1]);
                        item.SubItems.Add(options[2]);
                        item.SubItems.Add(options[3]);

                        item.SubItems.Add(question.CorrectAnswerIndex.ToString());
                        count++;
                        lvSorular.Items.Add(item);
                    }
                }
            }
            lblSoruSayisi.Text = $"Soru sayısı: {count}";
        }

        Button guncelle_aktif_buton;
        Guid guncellenecek_soru_id;

        #endregion

        #region Soruları Görüntülemede Kategoriye Göre Sıralama
        private void btnSporSG_Click(object sender, EventArgs e)
        {
            viewQuestions("Spor");
        }

        private void btnTarihSG_Click(object sender, EventArgs e)
        {
            viewQuestions("Tarih");
        }

        private void btnSanatSG_Click(object sender, EventArgs e)
        {
            viewQuestions("Sanat");
        }

        private void btnBilimSG_Click(object sender, EventArgs e)
        {
            viewQuestions("Bilim");
        }

        private void btnEglenceSG_Click(object sender, EventArgs e)
        {
            viewQuestions("Eglence");
        }

        private void btnTumuSG_Click(object sender, EventArgs e)
        {
            viewQuestions("all");
        }

        #endregion

        #region Soruları Güncelleme Metotları

        private void btnSoruGuncelleSG_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSoruGuncelle);
            active_panel = pnlSoruGuncelle;

            guncellenecek_soru_id = Guid.Parse(lvSorular.SelectedItems[0].SubItems[0].Text);
            txtSoruSG.Text = lvSorular.SelectedItems[0].SubItems[2].Text;
            txtOption1SG.Text = lvSorular.SelectedItems[0].SubItems[3].Text;
            txtOption2SG.Text = lvSorular.SelectedItems[0].SubItems[4].Text;
            txtOption3SG.Text = lvSorular.SelectedItems[0].SubItems[5].Text;
            txtOption4SG.Text = lvSorular.SelectedItems[0].SubItems[6].Text;
            Button[] buttons_update = new Button[] { btnSanatGuncelle, btnBilimGuncelle, btnEglenceGuncelle, btnSoruGuncelle, btnSporGuncelle };
            foreach (Button b in buttons_update)
            {
                if (b.Text.Equals(lvSorular.SelectedItems[0].SubItems[1].Text))
                {
                    b.ForeColor = Color.Green;
                    guncelle_aktif_buton = b;
                }
            }
            RadioButton[] radios_update = new RadioButton[] { rbOption1SG, rbOption2SG, rbOption3SG, rbOption4SG };
            int index = int.Parse(lvSorular.SelectedItems[0].SubItems[7].Text);
            radios_update[index].Checked = true;
        }

        private void selectButtons_Update(object sender)
        {
            Button aktif = (Button)sender;
            guncelle_aktif_buton.ForeColor = ThemeHandler.color_texts;
            aktif.ForeColor = Color.Green;
            guncelle_aktif_buton = aktif;
        }
        #endregion

        #region Soruları Silme Metotları

        private async void btnSoruSil_Click(object sender, EventArgs e)
        {

            Guid id = Guid.Parse(lvSorular.SelectedItems[0].SubItems[0].Text);
            await QuestionService.DeleteQuestion(id);

            viewQuestions("all");
        }

        #endregion

        #region Soru Ekleme Modülleri

        int se_cat_index;
        Button ekleme_aktif_buton;
        private void selectButtons_Add(object sender, int index)
        {
            Button basilan_button = (Button)sender;
            se_cat_index = index;

            if(ekleme_aktif_buton != null)
            {
                ekleme_aktif_buton.ForeColor = ThemeHandler.color_texts;
            }
            basilan_button.ForeColor = Color.Green;
            ekleme_aktif_buton = basilan_button;

        }

        private async void btnSoruEkle_Click(object sender, EventArgs e)
        {
            
            string questionText = txtSoruEkleSoru.Text;
            string optionsText = Helper.ConcatenateStrings(txtSoruEkleOpt1.Text, txtSoruEkleOpt2.Text, txtSoruEkleOpt3.Text, txtSoruEkleOpt4.Text);
            bool[] options = new bool[] {
                rbSoruEkleDogru1.Checked,
                rbSoruEkleDogru2.Checked,
                rbSoruEkleDogru3.Checked,
                rbSoruEkleDogru4.Checked
            };

            int correctAnswerIndex = Helper.FindFirstTrueIndex(options);

            int cat_index = se_cat_index;
            Guid categoryId = Guid.Empty;

            using (var db = new qAppDBContext())
            {
                var Category = await db.Categories.FirstOrDefaultAsync(c => c.Index == cat_index);
                if(Category != null)
                    categoryId = Category.Id;
            }

            if (questionText != "" && optionsText != "" && correctAnswerIndex != -1 && categoryId != null)
            {
                // Yeni bir Question nesnesi oluşturun
                var newQuestion = new Question
                {
                    Id = Guid.NewGuid(),
                    QuestionText = questionText,
                    OptionsText = optionsText,
                    CorrectAnswerIndex = correctAnswerIndex,
                    CategoryId = categoryId
                };

                // QuestionService'e yeni soruyu ekleyin
                bool result = await QuestionService.AddQuestion(newQuestion);

                if (result)
                {
                    MessageBox.Show("Soru başarıyla eklendi.");
                    ClearQuestionFields();

                    PanelHandler.setPanelMiddle(this, active_panel, pnlSoruEkle);
                    active_panel = pnlSoruEkle;
                }
                else
                {
                    MessageBox.Show("Soru eklenirken bir hata oluştu.");
                }
            }
            else
            {
                MessageBox.Show("Alanları kontrol edin.", "Soru Ekleme Uyarısı", MessageBoxButtons.OK);
            }

            // Name eşleşen kategoriyi getir

            
            
        }

        private void ClearQuestionFields()
        {
            txtSoruEkleSoru.Text = "";

            txtSoruEkleOpt1.Text = "";
            txtSoruEkleOpt2.Text = "";
            txtSoruEkleOpt3.Text = "";
            txtSoruEkleOpt4.Text = "";

            rbSoruEkleDogru1.Checked = false;
            rbSoruEkleDogru2.Checked = false;
            rbSoruEkleDogru3.Checked = false;
            rbSoruEkleDogru4.Checked = false;

            ekleme_aktif_buton.ForeColor = ThemeHandler.color_texts;

            btnSporSE_Click(btnSporSE, EventArgs.Empty);

            // Categories
        }

        #endregion

        #region Soru Güncelle

        private async void btnSoruGuncelle_Click(object sender, EventArgs e)
        {
            Question guncellenecek_soru = await QuestionService.GetQuestionById(guncellenecek_soru_id);
            string questionText = txtSoruSG.Text;
            string optionsText = Helper.ConcatenateStrings(txtOption1SG.Text, txtOption2SG.Text, txtOption3SG.Text, txtOption4SG.Text);
            bool[] options = new bool[] {
                rbOption1SG.Checked,
                rbOption2SG.Checked,
                rbOption3SG.Checked,
                rbOption4SG.Checked
            };

            int correctAnswerIndex = Helper.FindFirstTrueIndex(options);

            // Kategori ismi
            string categoryName = guncelle_aktif_buton.Text;

            Guid categoryId = Guid.Empty;
            using (var db = new qAppDBContext())
            {
                var Category = await db.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                categoryId = Category.Id;
            }


            // Mevcut question güncelleyeceğiz.

            guncellenecek_soru.Id = guncellenecek_soru_id;
            guncellenecek_soru.QuestionText = questionText;
            guncellenecek_soru.OptionsText = optionsText;
            guncellenecek_soru.CorrectAnswerIndex = correctAnswerIndex;
            guncellenecek_soru.CategoryId = categoryId;

            // QuestionService'den update edeceğiz.
            bool result = await QuestionService.UpdateQuestion(guncellenecek_soru);

            if (result)
            {
                MessageBox.Show("Soru başarıyla güncellendi.");
                //ClearQuestionFields();
                PanelHandler.setPanelMiddle(this, active_panel, pnlSorulariGoruntule);
                active_panel = pnlSorulariGoruntule;
            }
            else
            {
                MessageBox.Show("Soru güncellenirken bir hata oluştu.");
            }
        }

        #endregion

        #region Soru Güncellemede Kategori Butonları

        private void btnSporGuncelle_Click(object sender, EventArgs e)
        {
            selectButtons_Update(sender);
        }

        private void btnTarihGuncelle_Click(object sender, EventArgs e)
        {
            selectButtons_Update(sender);
        }

        private void btnSanatGuncelle_Click(object sender, EventArgs e)
        {
            selectButtons_Update(sender);
        }

        private void btnBilimGuncelle_Click(object sender, EventArgs e)
        {
            selectButtons_Update(sender);
        }

        private void btnEglenceGuncelle_Click(object sender, EventArgs e)
        {
            selectButtons_Update(sender);
        }

        #endregion

        #region Soru Eklemede Kategori Butonları

        private void btnSporSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender, 0);
        }

        private void btnTarihSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender, 1);
        }

        private void btnSanatSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender, 2);
        }

        private void btnBilimSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender, 3);
        }

        private void btnEglenceSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender, 4);
        }

        #endregion

        #region Quit Handle

        public bool isLeaving = false;
        private void formMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isLeaving)
            {
                Environment.Exit(0);
            }
        }

        #endregion
    }
    
}
