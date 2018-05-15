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
using System.Runtime.InteropServices;

namespace UniversalCrosshair
{
    public partial class Form1 : Form
    {

        RECT _rect;

        public struct RECT
        {
            public int left, top, right, bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

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

            if (processSelector.SelectedIndex > -1)
            {
                // Something has been chosen
                IntPtr handle = FindWindow(null, processSelector.SelectedItem.ToString());
                GetWindowRect(handle, out _rect);


                overlayForm.Show();
                overlayForm.Size = new Size(_rect.right - _rect.left, _rect.bottom - _rect.top);
                overlayForm.Top = _rect.top;
                overlayForm.Left = _rect.left;
            }
            else
            {
                MessageBox.Show("Choose something!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            overlayForm.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
