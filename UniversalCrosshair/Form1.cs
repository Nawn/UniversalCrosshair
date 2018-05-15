using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace UniversalCrosshair
{
    public partial class Form1 : Form
    {

        CrosshairOverlay overlayForm = new CrosshairOverlay();

        public Form1()
        {
            InitializeComponent();
        }

        private void refreshProcessBtn_Click(object sender, EventArgs e)
        {
            string[] undesired = { "svchost", "conhost", "spoolsv", "lsass" };
            processSelector.Items.Clear();
            Process[] localAll = Process.GetProcesses();

            foreach (var item in localAll)
            {
                if (!String.IsNullOrEmpty(item.MainWindowTitle))
                {
                    processSelector.Items.Add(item.MainWindowTitle);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            overlayForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            overlayForm.Hide();
        }
    }
}
