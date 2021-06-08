using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace DHospital
{
    public partial class frm_main : Form
    {
        string MyPath = "";
        string MyFileName = "";
        string str = "";
        int j = 0;
        public frm_main()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Master frm = new Master();
            frm.MdiParent = this;
            frm.LoadData("Patient", "Patient");

            frm.Show();
        }

        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ch = MessageBox.Show(null, "Are you sure to exit?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ch == DialogResult.OK)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult ch = MessageBox.Show(null, "Are you sure to exit?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ch == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }



        private void frm_main_Load(object sender, EventArgs e)
        {
            setMenu();

            this.Text = Database.fname + " [" + Database.fyear + "]";
            statusStrip1.Items[0].Text = "Krishna Software && Systems  ";
            statusStrip1.Items[2].Text = Database.ExeDate.ToString("yy.M.d");
            statusStrip1.Items[4].Text = Database.uname;
            statusStrip1.Items[6].Text = Database.ldate.ToString(Database.dformat);
            statusStrip1.Items[9].Text = "0131 2437969";
            statusStrip1.Items[11].Text = Database.fyear;

            FileInfo fInfo = new FileInfo(Application.StartupPath + "\\System\\" + Database.fname + ".jpg");
            if (fInfo.Exists)
            {
                this.BackgroundImage = new Bitmap(Application.StartupPath + "\\System\\" + Database.fname + ".jpg");
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            fInfo = null;
        }

        public void setMenu()
        {
            
                panel1.Visible = true;

                if (Database.utype == "User")
                {
                    button4.Visible = false;
                    button5.Visible = false;
                    button8.Location = new Point(13, 270);
                }
                else
                {
                    button4.Visible = true;
                    button5.Visible = true;
                }
          

          
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            gg.DateWiseReport(Database.ldate);
            gg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmChangePass frm = new frmChangePass();
            frm.MdiParent = this;
            frm.LoadData(Database.uname, "Change Password");
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPEG Files(*.jpg) | *.jpg";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                this.BackgroundImage = new Bitmap(ofd.FileName);
                this.BackgroundImageLayout = ImageLayout.Stretch;
                GC.Collect();
                var filePath = Application.StartupPath + "\\System\\" + Database.fname + ".jpg";
                File.Copy(ofd.FileName, Application.StartupPath + "\\System\\" + Database.fname + DateTime.Now.ToString("yyMMddhmmssfff") + ".jpg", true);

                MessageBox.Show("Done");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature Not Available");
        }
       
       
        public void DirectorySearch1(string dir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    str = str + dir + "\\" + (Path.GetFileName(f)) + "\r\n";
                }

                foreach (string d in Directory.GetDirectories(dir, "*"))
                {
                    DirectorySearch(d);
                }
                MessageBox.Show(MyFileName + " " + " " + str);






            }
            catch (System.Exception ex)
            {

            }
        }
        public void DirectorySearch(string dir)
        {
            try
            {

                foreach (string d in Directory.GetDirectories(dir, "*"))
                {
                    DirectorySearch(d);





                }


                foreach (string f in Directory.GetFiles(dir))
                {
                    str = str + dir + "\\" + (Path.GetFileName(f)) + ";";

                }



            }
            catch (System.Exception ex)
            {

            }
        }
        public int LastRowTotal(Excel.Worksheet wks)
        {
            Excel.Range lastCell = wks.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            return lastCell.Row;
        }
        private void button3_Click(object sender, EventArgs e)
        {


        }

        
    }

}
