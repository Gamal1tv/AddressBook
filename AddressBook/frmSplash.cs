using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();

            for (int i = 0; i < 100; i++)
            {
                progressBar2.Value = i;
                lblProgress.Text = i.ToString();
                if (i >= 46)
                {
                    lblProgress.BackColor = Color.FromArgb(0, 175, 0);
                }
                else
                {
                    lblProgress.BackColor = Color.FromArgb(230, 230, 230);
                }
            }
        }
    }
}
