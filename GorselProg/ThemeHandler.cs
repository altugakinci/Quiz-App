using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GorselProg
{
    class ThemeHandler
    {

        static public Color color_texts = Color.White;
        static public Color color_background = Color.FromArgb(37, 37, 37);
        static public Color color_textboxes = Color.FromArgb(62, 62, 66);
        //static public Color color_textboxes = Color.FromArgb(51, 50, 55);
        public Color color_buttons;

        public ThemeHandler()
        {
            lightTheme();
        }

        public static void changeFormsColor(Form form)
        {
            form.BackColor = color_background;
        }

        public static void changeAllControlsColor(Control c)
        {
            foreach(Control ctrl in c.Controls)
            {
                if (ctrl.HasChildren)
                    changeAllControlsColor(ctrl);
                else
                {
                    if(ctrl is TextBox)
                    {
                        ctrl.BackColor = color_textboxes;
                        ctrl.ForeColor = color_texts;
                    }else if(ctrl is Label)
                    {
                        ctrl.ForeColor = color_texts;
                    }else if(ctrl is MaskedTextBox)
                    {
                        ctrl.BackColor = color_textboxes;
                        ctrl.ForeColor = color_texts;
                    }else if(ctrl is Button)
                    {
                        ctrl.ForeColor = color_texts;
                        ctrl.BackColor = Color.Transparent;
                        Button b = (Button)ctrl;
                        b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    }else if(ctrl is GroupBox)
                    {
                        ctrl.ForeColor = color_texts;
                    }else if(ctrl is RadioButton)
                    {
                        ctrl.ForeColor = color_texts;
                    }else if(ctrl is ListView)
                    {
                        ctrl.BackColor = color_background;
                        ctrl.ForeColor = color_texts;
                    }
                }
            }
        }

        public void setLightTheme()
        {
            theme_olaQasem();
        }

        public void setDarkTheme()
        {
            darkTheme();
        }

        public void current_theme()
        {
            lightTheme();
        }
        // Light theme alternatifleri
        public static void lightTheme() //Default light theme
        {
            color_texts = Color.FromArgb(51, 50, 55);
            color_background = Color.FromArgb(202, 194, 178);
            color_textboxes = Color.FromArgb(135, 118, 109);
            //color_buttons = Color.White;
        }

        public void theme_oldschool()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public static void theme_olaQasem()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void theme_hayleyMarshall()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void theme_1_1()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void gallons_of_blood()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void ziya2()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void galactic_cloud()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void woodland_work()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void blood_in_the_dark()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void amber_eyes()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        //Dark theme alternatifleri

        public static void darkTheme() //Default dark theme
        {
            color_texts = Color.White;
            color_background = Color.FromArgb(37,37,37);
            color_textboxes = Color.FromArgb(62, 62, 66);
            //color_buttons = Color.Black;
        }

        public void theme_darktheme1()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void theme_darktheme2()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void theme_darktheme3()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }

        public void theme_vs_dark_theme()
        {
            color_texts = Color.FromArgb(61, 56, 50);
            color_background = Color.FromArgb(217, 200, 181);
            color_textboxes = Color.FromArgb(164, 144, 124);
        }



        public void applyTheme(Form form, Button[] buttons, Label[] labels, TextBox[] textBoxes,MaskedTextBox[] maskedtextboxes, GroupBox[] groupboxes)
        {
            form.BackColor = color_background;

            foreach (Button b in buttons)
                b.ForeColor = color_texts;

            foreach (Label l in labels)
                l.ForeColor = color_texts;

            foreach (TextBox t in textBoxes)
            {
                t.BackColor = color_textboxes;
                t.ForeColor = color_texts;
            }

            foreach (MaskedTextBox m in maskedtextboxes)
            {
                m.BackColor = color_textboxes;
                m.ForeColor = color_texts;
            }

            foreach (GroupBox g in groupboxes)
                g.ForeColor = color_texts;


        }

        /*
        (27,15,0)
        (52,30,0)
        (78,45,0)
        (103,60,0)
        (129,75,0)

        (255,254,252)
        (136,0,0)
        (93,93,93)
        (158,158,158)
        (222,222,222)

        (0,0,0)
        (17,0,0)
        (34,0,0)
        (51,0,0)
        (68,0,0)

        (202,194,178)
        (135,118,109)
        (84,58,64)
        (51,50,55)
        (96,93,88)
        
        VS Dark Theme
        (0,122,204)
        (62,62,66)
        (45,45,48)
        (37,37,38)
        (30,30,30)
         
        (112,19,19)
        (248,246,230)
        (14,13,13)
        (153,33,33)
        (36,0,0)

        (65,74,76)
        (59,68,75)
        (53,56,57)
        (35,43,43)
        (14,17,17)
         
        (75,56,50)
        (133,68,66)
        (255,244,230)
        (60,47,47)
        (190,155,123)
         */
    }
}
