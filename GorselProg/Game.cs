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
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        PanelHandler panelHandler = new PanelHandler();
        Panel[] panels;

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            formLoginRegister form = new formLoginRegister();
            form.Show();
            this.Hide();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //DESIGN
            panelHandler.hidePanels(panels);
            pnlBuildAGame.Dock = DockStyle.Left;
            pnlBuildAGame.Visible = true;
            
            //CODE
        }

        private void Game_Load(object sender, EventArgs e)
        {
            //DESIGN
            panels = new Panel[] { pnlBuildAGame, pnlCreateAGame, pnlHowToPlay, pnlJoinAGame, pnlPreferences };
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;

            //CODE
        }

        private void btnCreateAGame_Click(object sender, EventArgs e)
        {
            //DESIGN
            pnlCreateAGame.Dock = DockStyle.Left;
            pnlCreateAGame.BringToFront();
            pnlCreateAGame.Visible = true;

            pnlJoinAGame.Visible = false;

            //CODE

        }

        private void btnJoinAGame_Click(object sender, EventArgs e)
        {
            //DESIGN
            pnlJoinAGame.Dock = DockStyle.Left;
            pnlJoinAGame.BringToFront();
            pnlJoinAGame.Visible = true;

            pnlCreateAGame.Visible = false;

            //CODE

        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            //DESIGN
            panelHandler.hidePanels(panels);
            pnlHowToPlay.Visible = true;
            pnlHowToPlay.Dock = DockStyle.Left;

            //CODE
        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            //DESIGN
            panelHandler.hidePanels(panels);
            pnlPreferences.Visible = true;
            pnlPreferences.Dock = DockStyle.Left;

            //CODE
        }

        private void btnLightMode_Click(object sender, EventArgs e)
        {
            btnLightMode.Enabled = false;
            btnLightMode.BackColor = Color.FromArgb(104, 93, 82);
            btnDarkMode.Enabled = true;
            btnDarkMode.BackColor = Color.FromArgb(195, 180, 163);
        }

        private void btnDarkMode_Click(object sender, EventArgs e)
        {
            btnLightMode.Enabled = true;
            btnDarkMode.BackColor = Color.FromArgb(104, 93, 82);
            btnDarkMode.Enabled = false;
            btnLightMode.BackColor = Color.FromArgb(195, 180, 163);
        }
    }
}
