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
    public partial class Master : Form
    {
        string gstr = "";
        BindingSource bs = new BindingSource();

        public Master()
        {
            InitializeComponent();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_patient frm = new frm_patient();
            frm.LoadData("0", "Patient");

            frm.ShowDialog(this);
        }

        private void Master_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }

        public void filter()
        {
            String strTemp = textBox1.Text;
            strTemp = strTemp.Replace("%", "?");
            strTemp = strTemp.Replace("[", string.Empty);
            strTemp = strTemp.Replace("]", string.Empty);


            //string strfilter = "";
            //for (int i = 0; i < dataGridView1.ColumnCount; i++)
            //{
            //    if (strfilter != "")
            //    {
            //        strfilter += " or ";
            //    }
            //    strfilter += "(" + dataGridView1.Columns[i].HeaderCell.Value.ToString() + " like *" + strTemp + "* " + ")";
            //}
            //  bs.Filter = null;
            //bs.Filter = strfilter;
            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = bs;

            //bs.DataSource = db.ACCOUNTs
            //       .Select(rpt => new { pat_id = rpt.pat_id, name = rpt.name, number = rpt.number, sex = rpt.sex, age = rpt.age, rdate = rpt.rdate, pay = rpt.pay, Ac_id = rpt.Ac_id })
            //       .OrderByDescending(d => d.pat_id).ToList()
            //       .Select(rpt => new { Registration = rpt.pat_id, Patient = rpt.name, RoomNumber = rpt.number, Gender = rpt.sex, Age = rpt.age, Date = DateTime.Parse(rpt.rdate.ToString()).ToString("dd-MMM-yyyy"), Payment = rpt.pay, Ac_id = rpt.Ac_id })
            //       .Where(w => w.Gender.Contains(textBox1.Text)).ToList();
            using (MarwariContext db = new MarwariContext())
            {
                if (textBox1.Text.Trim() == "")
                {
                    bs.DataSource = db.Accounts
                       .Select(rpt => new { Registration = rpt.Pat_id, Name1 = rpt.Name, RoomNumber1 = rpt.Number, Gender1 = rpt.Sex, Age1 = rpt.Age, Date1 = rpt.RDate, Pay1 = rpt.Pay, Ac_id = rpt.Ac_id }).Where(p1 => p1.Date1 >= dateTimePicker1.Value.Date && p1.Date1 <= dateTimePicker2.Value.Date)
                       .OrderByDescending(d => d.Registration).ToList()
                       .Select(rpt => new { Registration_No = rpt.Registration, Name = rpt.Name1, Room_Number = rpt.RoomNumber1, Gender = rpt.Gender1, Age = rpt.Age1, Date = DateTime.Parse(rpt.Date1.ToString()).ToString("dd-MMM-yyyy"), Pay = rpt.Pay1, Ac_id = rpt.Ac_id })
                       .ToList();
                }
                else
                {
                    bs.DataSource = db.Accounts
                       .Select(rpt => new { Registration = rpt.Pat_id, Name1 = rpt.Name, RoomNumber1 = rpt.Number, Gender1 = rpt.Sex, Age1 = rpt.Age, Date1 = rpt.RDate, Pay1 = rpt.Pay, Ac_id = rpt.Ac_id }).Where(p1 => p1.Date1 >= dateTimePicker1.Value.Date && p1.Date1 <= dateTimePicker2.Value.Date)
                       .OrderByDescending(d => d.Registration).ToList()
                       .Select(rpt => new { Registration_No = rpt.Registration, Name = rpt.Name1, Room_Number = rpt.RoomNumber1, Gender = rpt.Gender1, Age = rpt.Age1, Date = DateTime.Parse(rpt.Date1.ToString()).ToString("dd-MMM-yyyy"), Pay = rpt.Pay1, Ac_id = rpt.Ac_id })
                       .Where(w => w.Name.Contains(textBox1.Text.ToUpper())).ToList();

                }
                dataGridView1.DataSource = bs.DataSource;

                dataGridView1.Columns["Registration_No"].Width = 100;
                dataGridView1.Columns["Name"].Width = 150;
                dataGridView1.Columns["Room_Number"].Width = 110;
                dataGridView1.Columns["Gender"].Width = 100;
                dataGridView1.Columns["Age"].Width = 80;
                dataGridView1.Columns["Date"].Width = 100;

                dataGridView1.Columns["Pay"].Width = 100;

                label3.Text = dataGridView1.Rows.Count.ToString();

                dataGridView1.Columns["Ac_id"].Visible = false;
                dataGridView1.Columns["print1"].DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["print1"].DisplayIndex = dataGridView1.Columns.Count - 2 + 1;
            }
        }

        public void LoadData(string str, string frmCaption)
        {
            using (MarwariContext db = new MarwariContext())
            {
                gstr = str;
                this.Text = frmCaption;

                if (str == "Patient")
                {
                    //bs.DataSource = db.ACCOUNTs
                    //     .Select(rpt => new { pat_id = rpt.pat_id, name = rpt.name, number = rpt.number, sex = rpt.sex, age = rpt.age, rdate = rpt.rdate, pay = rpt.pay, Ac_id = rpt.Ac_id })
                    //     .OrderByDescending(d => d.pat_id).ToList()
                    //     .Select(rpt => new { Registration = rpt.pat_id, Patient = rpt.name, RoomNumber = rpt.number, Gender = rpt.sex, Age = rpt.age, Date = DateTime.Parse(rpt.rdate.ToString()).ToString("dd-MMM-yyyy"), Payment = rpt.pay, Ac_id = rpt.Ac_id }).ToList();

                    //dataGridView1.DataSource = bs;


                    //  bs.DataSource = db.ACCOUNTs.Select(rpt => new { Registration_No = rpt.pat_id, Name = rpt.name, Room_Number = rpt.number, Gender = rpt.sex, Age = rpt.age, Date = rpt.rdate, Pay = rpt.pay, Ac_id = rpt.Ac_id }).Where(p1 => p1.Date >= dateTimePicker1.Value.Date  &&  p1.Date <= dateTimePicker2.Value.Date).OrderByDescending(d => d.Registration_No);
                    bs.DataSource = db.Accounts
                     .Select(rpt => new { Registration = rpt.Pat_id, Name1 = rpt.Name, RoomNumber1 = rpt.Number, Gender1 = rpt.Sex, Age1 = rpt.Age, Date1 = rpt.RDate, Pay1 = rpt.Pay, Ac_id = rpt.Ac_id }).Where(p1 => p1.Date1 >= dateTimePicker1.Value.Date && p1.Date1 <= dateTimePicker2.Value.Date)
                     .OrderByDescending(d => d.Registration).ToList()
                     .Select(rpt => new { Registration_No = rpt.Registration, Name = rpt.Name1, Room_Number = rpt.RoomNumber1, Gender = rpt.Gender1, Age = rpt.Age1, Date = DateTime.Parse(rpt.Date1.ToString()).ToString("dd-MMM-yyyy"), Pay = rpt.Pay1, Ac_id = rpt.Ac_id })
                     .ToList();
                    dataGridView1.DataSource = bs.DataSource;

                    dataGridView1.Columns["Registration_No"].Width = 100;
                    dataGridView1.Columns["Name"].Width = 150;
                    dataGridView1.Columns["Room_Number"].Width = 110;
                    dataGridView1.Columns["Gender"].Width = 100;
                    dataGridView1.Columns["Age"].Width = 80;
                    dataGridView1.Columns["Date"].Width = 100;

                    dataGridView1.Columns["Pay"].Width = 100;

                    label3.Text = dataGridView1.Rows.Count.ToString();
                    button1.Text = "New Patients";
                    label2.Text = "List of Patients";
                    dataGridView1.Columns["Ac_id"].Visible = false;

                }
                dataGridView1.Columns["print1"].DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["print1"].DisplayIndex = dataGridView1.Columns.Count - 2 + 1;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gstr == "Patient")
            {
                if (dataGridView1.CurrentCell.OwningColumn.Name == "print1")
                {
                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Registration_No"].Value.ToString() == "0")
                    {
                        return;
                    }
                    else
                    {
                        frm_patient frm = new frm_patient();
                        frm.LoadData(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Ac_id"].Value.ToString(), "Print Patient");
                        filter();
                        frm.ShowDialog();
                        LoadData(gstr, "Patient");
                    }

                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // filter();
        }

        private void Master_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
            dateTimePicker1.Value = Database.ldate;

            dateTimePicker2.CustomFormat = "dd-MMM-yyyy";
            dateTimePicker2.Value = Database.ldate;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadData("Patient", "Patient");
            filter();
        }
    }
}
