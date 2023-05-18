using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GorselProg
{
    // Panellerin kolayca hareketi ve boyutlandırması için kullanılan bir handler sınıfı.
    class PanelHandler
    {

        public static void setPanelMiddle(Form form, Panel inactive_panel, Panel active_panel)
        {
            inactive_panel.Visible = false;
            active_panel.Size = new Size(600, 600);
            active_panel.Left = (form.Width - active_panel.Width) / 2;
            active_panel.Top = (form.Height - active_panel.Height) / 2;
            active_panel.Visible = true;
        }

        public static void setPanelFill(Panel inactive_panel,Panel active_panel)
        {
            inactive_panel.Visible = false;
            active_panel.Visible = true;
            active_panel.Dock = DockStyle.Fill;
        }

        // Paneli göstermek için metot
        public void showPanel(Panel panel)
        {
            panel.Visible = true;
            panel.Dock = DockStyle.Fill;
        }

        // Paneli gizlemek için metot
        public void hidePanel(Panel panel)
        {
            panel.Visible = false;
        }

        public void hidePanels(Panel[] panels)
        {
            foreach(Panel p in panels)
            {
                p.Visible = false;
            }
        }

        // Paneli sağa genişletmek için metot
        // Ana menüde kullanılıyor.
        public void dockToRight(Panel panel)
        {
            panel.Dock = DockStyle.Right;
        }
    }
}
