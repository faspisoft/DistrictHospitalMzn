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
    public partial class Frm_student : Form
    {
        
        public Frm_student()
        {
            InitializeComponent();
        }

        private void Frm_student_Load(object sender, EventArgs e)
        {

            List<int> numbers = new List<int>{1,2,3,4,5,6,7,8,9,10 };
            IEnumerable<int> evennum = numbers;


            BindingSource bs = new BindingSource();


            bs.DataSource = evennum;

            dataGridView1.DataSource = evennum;
        }
    }
}
