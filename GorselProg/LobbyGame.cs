﻿using GorselProg.Model;
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

            //Lobi lideriyse:
            if (  rank.Equals("Leader") )
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

        private void btnPlayerLeave_Click(object sender, EventArgs e)
        {
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
                button.ForeColor = Color.Green;
            else
                button.ForeColor = ThemeHandler.color_texts;

            categoryButtons[buttonIndex] = !categoryButtons[buttonIndex];
            
        }

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

        private async void btnLeaderBaslat_ClickAsync(object sender, EventArgs e)
        {
            /*
            // Get the selected categories
            // TODO:Kategoriler
            List<Category> categories = GetSelectedCategories();

            // Get the currently logged in user
            int sessionUserId = UserSession.Instance.Id;

            // Create an instance of RoomService
            var roomService = new RoomService(new qAppDBContext());

            // Call the StartGame method
            var result = await roomService.StartGame(roomId, categories, sessionUserId,DateTime.Now.AddMinutes(10));

            if (result)
            {
                MessageBox.Show("Game started successfully!");
            }
            else
            {
                MessageBox.Show("Failed to start game.");
            }
             */

            PanelHandler.setPanelFill(active_panel, pnlGame);
            active_panel = pnlGame;
        }
    }
}
