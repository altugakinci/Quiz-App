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
    public partial class formMainMenu : Form
    {
        public formMainMenu()
        {
            InitializeComponent();
        }
        Panel active_panel;
        //ThemeHandler themeHandler = new ThemeHandler();
        PanelHandler panelHandler = new PanelHandler();



        private void btnSignOut_Click(object sender, EventArgs e)
        {
            UserService.LogoutUser();
            formLoginRegister form = new formLoginRegister();
            form.Show();
            form.updateTheme();
            this.Hide();
            
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //DESIGN

            PanelHandler.setPanelMiddle(this, active_panel, pnlBuildAGame);
            active_panel = pnlBuildAGame;
            //setPanel(pnlBuildAGame);
            
            //CODE
        }

        private void Game_Load(object sender, EventArgs e)
        {
            //DESIGN

            active_panel = pnlMainMenu;
            ThemeHandler.changeAllControlsColor(this);
            ThemeHandler.changeFormsColor(this);
            this.WindowState = FormWindowState.Maximized;
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);

            //CODE
        }

        private void btnCreateAGame_Click(object sender, EventArgs e)
        {
            //DESIGN

            PanelHandler.setPanelMiddle(this, active_panel, pnlCreateAGame);
            active_panel = pnlCreateAGame;

            //CODE

        }

        private void btnJoinAGame_Click(object sender, EventArgs e)
        {
            //DESIGN

            PanelHandler.setPanelMiddle(this, active_panel, pnlJoinAGame);
            active_panel = pnlJoinAGame;

            //CODE

        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {

            //DESIGN

            PanelHandler.setPanelMiddle(this, active_panel, pnlHowToPlay);
            active_panel = pnlHowToPlay;

            //CODE
        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            //DESIGN

            PanelHandler.setPanelMiddle(this, active_panel, pnlPreferences);
            active_panel = pnlPreferences;

            //CODE
        }

        private void btnLightMode_Click(object sender, EventArgs e)
        {
            ThemeHandler.theme_olaQasem();
            ThemeHandler.changeAllControlsColor(this);
            ThemeHandler.changeFormsColor(this);
        }

        private void btnDarkMode_Click(object sender, EventArgs e)
        {
            ThemeHandler.darkTheme();
            ThemeHandler.changeAllControlsColor(this);
            ThemeHandler.changeFormsColor(this);
        }

        private void btnBAGBack_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }

        private void btnJAGGeri_Click(object sender, EventArgs e)
        {
            //DESIGN

            PanelHandler.setPanelMiddle(this, active_panel, pnlBuildAGame);
            active_panel = pnlBuildAGame;

            //CODE
        }

        private void btnCAGGeri_Click(object sender, EventArgs e)
        {
            //DESIGN

            PanelHandler.setPanelMiddle(this, active_panel, pnlBuildAGame);
            active_panel = pnlBuildAGame;

            //CODE
        }

        private void btnHTPGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }

        private void btnPreferencesGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }

        private async void btnCAGCreateRoom_ClickAsync(object sender, EventArgs e)
        {
            
            if (txtCAGRoomName.Text == "" && txtCAGRoomPassword.Text == "")
            {
                // Hata mesajı dönelim

            }

            User admin = UserSession.Instance.GetCurrentUser();

            
            //List<Category> categories = RoomSession.Instance.GetAllCategories();

            // Oda oluşturma işlemini yap
            var newRoom = new Room
            {
                Name = txtCAGRoomName.Text,
                Password = txtCAGRoomPassword.Text,
                Code = "C101",
                AdminId = admin.Id,

            };

            //bool isRoomCreated = await RoomService.CreateRoom(newRoom);

            if (true)
            {
                MessageBox.Show("Oda başarıyla oluşturuldu.");
                // Oda oluşturulduktan sonra yapılacak işlemler
            }
            else
            {
                MessageBox.Show("Oda oluşturulurken bir hata oluştu.");
            }


            // Room oluşturulduğunda yapılacak işlemler
             LobbyGame game = new LobbyGame("Leader");
             game.Show();
             this.Hide();

        }

        private async void btnJAGJoinRoom_ClickAsync(object sender, EventArgs e)
        {
            string roomCode = txtJoinCode.Text;
            string roomPassword = txtJoinPassword.Text;
            User currentUser = UserSession.Instance.GetCurrentUser();

            //bool joined = await RoomService.JoinRoom(roomCode, roomPassword, currentUser);

            if (true)
            {
                MessageBox.Show("Odaya katılma işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Odaya katılma işlemi başarılı olduğunda yapılacak işlemler
                LobbyGame game = new LobbyGame("Player");
                game.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Odaya katılma işlemi başarısız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Odaya katılma işlemi başarısız olduğunda yapılacak işlemler
            }

        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlProfile);
            active_panel = pnlProfile;
            prgProfileXP.Value = 50;
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

        private  void btnSorularSoruEkle_Click(object sender, EventArgs e)
        {
            
            PanelHandler.setPanelMiddle(this, active_panel, pnlSoruEkle);
            active_panel = pnlSoruEkle;

            btnSporSE.ForeColor = Color.Green;
            ekleme_aktif_buton = btnSporSE;
            
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
            Guid categoryId = Guid.Parse("DDB28117-98FF-4015-9CBD-4B53BFD5272A");

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
            btnSporSE.ForeColor = Color.Green;
            ekleme_aktif_buton = btnSporSE;

           // Categories
        }

        private void btnSorularGoruntule_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSorulariGoruntule);
            active_panel = pnlSorulariGoruntule;

            viewQuestions("all");
            
        }

        private async void viewQuestions(string category)
        {
            // ListView temizle
            lvSorular.Items.Clear();

            // Soruları veritabanından çek
            List<Question> questions = await QuestionService.GetAllQuestions();

            // Soruları ListView'e ekle
            if (category.Equals("all")) {
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

                        lvSorular.Items.Add(item);
                    }
                }
            }
            
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

        Button guncelle_aktif_buton;
        Guid guncellenecek_soru_id;
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
            Button[] buttons_update = new Button[] { btnSanatGuncelle, btnBilimGuncelle, btnEglenceGuncelle,btnSoruGuncelle,btnSporGuncelle };
            foreach(Button b in buttons_update)
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

        private async void btnSoruSil_Click(object sender, EventArgs e)
        {

            Guid id = Guid.Parse(lvSorular.SelectedItems[0].Text);
            await QuestionService.DeleteQuestion(id);

            viewQuestions("all");
        }

        private void selectButtons_Update(object sender)
        {
            Button aktif = (Button)sender;
            guncelle_aktif_buton.ForeColor = ThemeHandler.color_texts;
            aktif.ForeColor = Color.Green;
            guncelle_aktif_buton = aktif;
        }

        string soruekleme_kategori;
        Button ekleme_aktif_buton;
        private void selectButtons_Add(object sender)
        {
            Button basilan_button = (Button)sender;
            basilan_button.ForeColor = Color.Green;
            soruekleme_kategori = basilan_button.Text;

            ekleme_aktif_buton.ForeColor = ThemeHandler.color_texts;
            ekleme_aktif_buton = basilan_button;
            
        }

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
            Guid categoryId = Guid.Parse("DDB28117-98FF-4015-9CBD-4B53BFD5272A");

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
                ClearQuestionFields();
                PanelHandler.setPanelMiddle(this, active_panel, pnlSoruEkle);
                active_panel = pnlSoruEkle;
            }
            else
            {
                MessageBox.Show("Soru güncellenirken bir hata oluştu.");
            }
        }

        private void btnSporSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender);
        }

        private void btnTarihSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender);
        }

        private void btnSanatSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender);
        }

        private void btnBilimSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender);
        }

        private void btnEglenceSE_Click(object sender, EventArgs e)
        {
            selectButtons_Add(sender);
        }
    }
}
