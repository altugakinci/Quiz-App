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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GorselProg
{
    public partial class formLoginRegister : Form
    {
        public formLoginRegister()
        {
            InitializeComponent();

        }

        Panel active_panel;

        private void Form1_Load(object sender, EventArgs e)
        {
            active_panel = pnlLogin;
            PanelHandler.setPanelFill(active_panel, pnlLogin);

            ThemeHandler.loadColors();
            ThemeHandler.changeAllControlsColor(this);
            ThemeHandler.changeFormsColor(this);
            this.WindowState = FormWindowState.Maximized;

            lblVersion.Text = $"Version: {Application.ProductVersion}";
        }

        #region Routings
        private void lblNoAcc_click(object sender, EventArgs e)
        {
            PanelHandler.setPanelFill(active_panel, pnlRegister);
            active_panel = pnlRegister;
        }

        private void lblRetLogin_MouseClick(object sender, MouseEventArgs e)
        {
            PanelHandler.setPanelFill(active_panel, pnlLogin);
            active_panel = pnlLogin;
        }
        #endregion

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLoginEmail.Text == "")
            {
                lblLoginWarning.Text = "Email alanı boş bırakılamaz!";
                return;
            }
            if(txtLoginPassword.Text == "")
            {
                lblLoginWarning.Text = "Şifre alanı boş bırakılamaz!";
                return;
            }


            #region Login

           
            #endregion

            bool isValid = await UserService.LoginUser(txtLoginEmail.Text, txtLoginPassword.Text);

            // TODO: Loading işlemleri buraya eklenebilir

            if (!isValid) {
                MessageBox.Show("Kullanıcı adı veya şifre yanlıştır.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                formMainMenu game = new formMainMenu();
                this.Hide();
                game.Show();
                game.WindowState = FormWindowState.Maximized;
            }
            
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtRegUsername.Text == "")
            {
                lblRegWarning.Text = "Kullanıcı adı boş bırakılamaz!";
                return;
            }
            if (txtRegMail.Text == "")
            {
                lblRegWarning.Text = "Email alanı boş bırakılamaz!";
                return;
            }
            if (txtRegPassword.Text == "")
            {
                lblRegWarning.Text = "Şifre alanı boş bırakılamaz!";
                return;
            }
            if (txtRegPassword2.Text == "")
            {
                lblRegWarning.Text = "Şifreyi tekrar girmelisiniz!";
                return;
            }
            if (!txtRegPassword.Text.Equals(txtRegPassword2.Text))
            {
                lblRegWarning.Text = "Şifreler Uyuşmuyor!";
                return;
            }

            User user = new User { UserName = txtRegUsername.Text, Email = txtRegMail.Text,Password = txtRegPassword.Text };
            
            bool isSucces = await UserService.AddUser(user);

            if(isSucces)
            {
                MessageBox.Show("Başarılı bir şekilde kayıt oluşturulmuştur.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Böyle bir kullanıcı vardır", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // TODO: buraya belki bi loading gibi birşey gelebilir

            txtRegUsername.Text = "";
            txtRegMail.Text = "";
            txtRegPassword.Text = "";
            txtRegPassword2.Text = "";

            PanelHandler.setPanelFill(active_panel, pnlLogin);
            active_panel = pnlLogin;
        }

        #region Enter tuşu entegrasyonu
        private void txtLoginPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null,null);
            }
        }

        private void txtLoginEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void txtRegPassword2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister_Click(null, null);
            }
        }

        private void txtRegUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister_Click(null, null);
            }
        }

        private void txtRegMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister_Click(null, null);
            }
        }

        private void txtRegPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister_Click(null, null);
            }
        }
        #endregion

    }
}
