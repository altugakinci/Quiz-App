using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GorselProg
{
    class PanelHandler
    {

        public void showPanel(Panel panel)
        {
            panel.Visible = true;
            panel.Dock = DockStyle.Fill;
        }

        public void hidePanel(Panel panel)
        {
            panel.Visible = false;
        }

    }
}
