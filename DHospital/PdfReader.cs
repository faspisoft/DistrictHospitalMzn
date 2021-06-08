using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DHospital
{
    public partial class PdfReader : Form
    {
        public PdfReader()
        {
            InitializeComponent();
        }

        private void PdfReader_Load(object sender, EventArgs e)
        {
            
        }
        public void LoadFile(string str)
        {
            axAcroPDF1.LoadFile(str);

        }

        private void PdfReader_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }
    }
}
