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
        static public Color color_texts;
        static public Color color_background;
        static public Color color_textboxes;
        static public Color color_alt1;
        static public Color color_alt2;
        //static public Color color_textboxes = Color.FromArgb(51, 50, 55);
        public Color color_buttons;


        public static void loadColors()
        {
            theme3();
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

        public static void setLightTheme()
        {
            lightTheme();
        }

        public static void setDarkTheme()
        {
            theme3();
        }

        public void current_theme()
        {
            lightTheme();
        }
        // Light theme alternatifleri
        public static void lightTheme() //Default light theme
        {
            color_texts = ColorTranslator.FromHtml("#392E3C");
            color_background = ColorTranslator.FromHtml("#BAB4E1");
            color_textboxes = ColorTranslator.FromHtml("#9891C1");
            color_alt1 = ColorTranslator.FromHtml("#3C3A49");
            color_alt2 = ColorTranslator.FromHtml("#1F1531");
            //color_buttons = Color.White;
        }

        public static void theme2()
        {
            color_texts = ColorTranslator.FromHtml("White");
            color_background = ColorTranslator.FromHtml("#14213D");
            color_textboxes = ColorTranslator.FromHtml("White");
            color_alt1= ColorTranslator.FromHtml("#FCA311");
            color_alt2 = ColorTranslator.FromHtml("#FF8341");
        }

        public static void theme3()
        {
            color_texts = ColorTranslator.FromHtml("#BBE1FA");
            color_background = ColorTranslator.FromHtml("#1B262C");
            color_textboxes = ColorTranslator.FromHtml("#294749");
            color_alt1 = ColorTranslator.FromHtml("#0F4C75");
            color_alt2 = ColorTranslator.FromHtml("#FF8341");
        }

        public static void theme_olaQasem()
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
