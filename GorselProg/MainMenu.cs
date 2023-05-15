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

            bool isRoomCreated = await RoomService.CreateRoom(newRoom);

            if (isRoomCreated)
            {
                MessageBox.Show("Oda başarıyla oluşturuldu.");
                // Oda oluşturulduktan sonra yapılacak işlemler
                // Room oluşturulduğunda yapılacak işlemler
                LobbyGame game = new LobbyGame("Leader");
                game.Show();
                this.Hide();
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

        private void btnSorularSoruEkle_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSoruEkle);
            active_panel = pnlSoruEkle;
        }

        private void btnSorularGoruntule_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSorulariGoruntule);
            active_panel = pnlSorulariGoruntule;
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

        private void btnSoruGuncelleSG_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlSoruGuncelle);
            active_panel = pnlSoruGuncelle;
        }
    }
}
