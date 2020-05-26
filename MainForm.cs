using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeBattle
{
    public partial class MainForm : Form
    {
        bool FirstTimeActivation { get; set; } = true;
        bool Connected { get; set; } = false;

        string ServerUrl { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        void ResizeDefault()
        {
            Width = 1200;
            Height = 800;
        }

        void Connect()
        {

        }

        void ToggleLogWindow()
        {

        }
        #region События

        #region Кнопички

        private void bConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void bShowLog_Click(object sender, EventArgs e)
        {
            ToggleLogWindow();
        }

        private void bResize_Click(object sender, EventArgs e)
        {
            ResizeDefault();
        }

        #endregion
        #region Колдунства

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (FirstTimeActivation)
            {
                ResizeDefault();
                FirstTimeActivation = false;
            }
        }

        #endregion
    }
}
