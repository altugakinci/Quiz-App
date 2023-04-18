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

        private void btnCAGCreateRoom_Click(object sender, EventArgs e)
        {
            LobbyGame game = new LobbyGame("Leader");
            game.Show();
            this.Hide();
        }

        private void btnJAGJoinRoom_Click(object sender, EventArgs e)
        {
            LobbyGame game = new LobbyGame("Player");
            game.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlProfile);
            active_panel = pnlProfile;
        }

        private void btnProfileGeri_Click(object sender, EventArgs e)
        {
            PanelHandler.setPanelMiddle(this, active_panel, pnlMainMenu);
            active_panel = pnlMainMenu;
        }
    }
}
