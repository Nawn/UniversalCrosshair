using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalCrosshair
{
    public partial class CrosshairOverlay : Form
    {
        public CrosshairOverlay()
        {
            InitializeComponent();
        }

        private void CrosshairOverlay_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
        }
    }
}
