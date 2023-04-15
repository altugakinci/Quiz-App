﻿using GorselProg.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        PanelHandler ph = new PanelHandler();
        ThemeHandler themeHandler = new ThemeHandler();
        Panel[] panels;
        TextBox[] textboxes;
        MaskedTextBox[] maskedtextboxes;
        Label[] labels;
        Button[] buttons;
        GroupBox[] groupBoxes;

        private void Form1_Load(object sender, EventArgs e)
        {
            ph.showPanel(pnlLogin);
            ph.hidePanel(pnlRegister);
            this.MaximizeBox = false;

            panels = new Panel[] { pnlLogin,pnlRegister };
            textboxes = new TextBox[] { txtRegUsername };
            maskedtextboxes = new MaskedTextBox[] { txtLoginPassword, txtRegPassword, txtRegPassword2, txtLoginEmail };
            labels = new Label[] { lblLoginWarning,lblRegWarning,lblRetLogin,label1,label2,label3,label4,label6,label7,label8,label9 };
            buttons = new Button[] { btnLogin,btnRegister };
            groupBoxes = new GroupBox[] {};

            themeHandler.applyTheme(this, buttons, labels, textboxes,maskedtextboxes, groupBoxes);
        }

        private void lblNoAcc_click(object sender, EventArgs e)
        {
            ph.hidePanel(pnlLogin);
            ph.showPanel(pnlRegister);
        }

        private void lblRetLogin_MouseClick(object sender, MouseEventArgs e)
        {
            ph.hidePanel(pnlRegister);
            ph.showPanel(pnlLogin);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            Game game = new Game();
            this.Hide();
            game.Show();

            /*
            if (txtLoginUsername.Text == "")
            {
                lblLoginWarning.Text = "Kullanıcı adı boş bırakılamaz!";
                return;
            }
            if(txtLoginPassword.Text == "")
            {
                lblLoginWarning.Text = "Şifre alanı boş bırakılamaz!";
                return;
            }
            */
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtRegUsername.Text == "")
            {
                lblRegWarning.Text = "Kullanıcı adı boş bırakılamaz!";
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

            // Hash the password using SHA256 algorithm
            byte[] passwordBytes = Encoding.UTF8.GetBytes(txtRegPassword.Text);
            byte[] hashBytes = new SHA256Managed().ComputeHash(passwordBytes);
            string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            // Save the user to the database
            using (var db = new qAppDBContext()) // Replace with your DbContext class
            {
                var user = new User
                {
                    email = txtRegUsername.Text,
                    password = hashedPassword
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void updateTheme()
        {
            themeHandler.applyTheme(this, buttons, labels, textboxes, maskedtextboxes, groupBoxes);
        }

        private void formLoginRegister_Shown(object sender, EventArgs e)
        {
            themeHandler.darkTheme();
            themeHandler.applyTheme(this, buttons, labels, textboxes, maskedtextboxes, groupBoxes);
        }
    }
}
