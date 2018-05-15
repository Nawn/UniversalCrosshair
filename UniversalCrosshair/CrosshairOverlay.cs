using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversalCrosshair
{
    public partial class CrosshairOverlay : Form
    {

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);        


        public CrosshairOverlay()
        {
            InitializeComponent();
        }

        private void CrosshairOverlay_Load(object sender, EventArgs e)
        {
            // Making it transparent
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;


            // Making it unclickable
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
        }

        private void CrosshairOverlay_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
