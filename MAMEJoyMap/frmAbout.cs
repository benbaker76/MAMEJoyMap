using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace MAMEJoyMap
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            var version = Assembly.GetExecutingAssembly().GetName().Version;

            lblAbout.Text = $"{Application.ProductName} {version.ToString(3)}\nBy Ben Baker";
        }

        private void butOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}