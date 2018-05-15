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
            // If they've chosen a process
            if (processSelector.SelectedIndex > -1)
            {
                // Something has been chosen
                IntPtr handle = FindWindow(null, processSelector.SelectedItem.ToString());
                GetWindowRect(handle, out _rect);


                overlayForm.Show();

                // Reposition and Resize
                overlayForm.Size = new Size(_rect.right - _rect.left, _rect.bottom - _rect.top);
                overlayForm.Top = _rect.top;
                overlayForm.Left = _rect.left;

                Pen newBrush;

                // If they've chosen a brush color
                if (crosshairColor.SelectedIndex > -1)
                {
                    var chosenColor = crosshairColor.SelectedItem.ToString();
                    switch (chosenColor)
                    {
                        case "Red":
                            newBrush = new Pen(Color.Red);
                            break;
                        case "Blue":
                            newBrush = new Pen(Color.Blue);
                            break;
                        case "Green":
                            newBrush = new Pen(Color.Green);
                            break;
                        case "Yellow":
                            newBrush = new Pen(Color.Yellow);
                            break;
                        default:
                            newBrush = new Pen(Color.Red);
                            break;
                    }
                }
                else
                {
                    newBrush = new Pen(Color.Red);
                }

                // Draw on the overlay
                Graphics formGraphics;

                formGraphics = overlayForm.CreateGraphics();

                var centerHeight = overlayForm.Size.Height / 2.0f;
                var centerWidth = overlayForm.Size.Width / 2.0f;

                formGraphics.DrawLine(newBrush, centerWidth, centerHeight - 10, centerWidth, centerHeight + 10);
                formGraphics.DrawLine(newBrush, centerWidth - 10, centerHeight, centerWidth + 10, centerHeight);
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
