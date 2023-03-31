using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GorselProg
{
    // Panellerin kolayca hareketi ve boyutlandırması için kullanılan bir handler sınıfı.
    class PanelHandler
    {

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
