using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class ReversiForm : Form
    {
        public ReversiForm()
        {
            InitializeComponent();
        }

        private void ReversiForm_Load(object sender, EventArgs e)
        {

        }

        private void ReversiForm_Click(object sender, EventArgs e)
        {
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            this.Size = new Size(this.Width + 200, this.Height + 200);
            pnReversi.Size = new Size(200, 200);
        }
    }
}
