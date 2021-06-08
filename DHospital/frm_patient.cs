using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Transactions;
using DHospital.Model;
using System.Data.Entity.Migrations;

namespace DHospital
{
    public partial class frm_patient : Form
    {

        private Random _random = new Random();

        public System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        int patid = 0;
        Account Record;
        Log logrecord;
        public string gStr = "";
        int ac_id;
        bool Exists = false;
        bool alter = false;
        string rdate;
        string rtime;
        bool send = false;

        string oldname = "";

        public frm_patient()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        private void frm_patient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                Button1_Click(sender, e);
            }
            else if (e.Alt && e.Control && e.KeyCode == Keys.A)
            {
                int randomno = RandomNumber(111111, 999999);
                //MessageBox.Show(randomno.ToString());
                WhatsApp.Send("Patient Id: " + patid + "\nDated: " + rdate + "\nOTP for Modification is " + randomno.ToString());
                send = true;
                InputBox box = new InputBox("Type OTP", "", false);
                box.outStr = "OTP";
                box.random = randomno;
                box.ShowInTaskbar = false;
                box.ShowDialog(this);

                if (box.match == true)
                {
                    TextBox1.Enabled = true;
                    // textBox4.Enabled = true;
                    TextBox1.TextBoxEnter();
                    TextBox1.Focus();

                    alter = true;
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (TextBox1.Text != "")
                {
                    DialogResult chk = MessageBox.Show("Are u sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (chk == DialogResult.No)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        this.Dispose();
                    }
                }
                else
                {
                    this.Dispose();
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (validate() == true)
            {

                using (MarwariContext db = new MarwariContext())
                {

                    if (Exists == true && send == false)
                    {

                        string maxdate = DateTime.Parse(db.Accounts.Max(p => p.RDate).ToString()).ToString("dd-MMM-yyyy");
                        rdate = DateTime.Parse(Record.RDate.ToString()).ToString("dd-MMM-yyyy");
                        if (maxdate != rdate)
                        {
                            int randomno = RandomNumber(111111, 999999);
                            //MessageBox.Show(randomno.ToString());
                            WhatsApp.Send("Patient Id: " + patid + "\nDated: " + rdate + "\nOTP for Printing is " + randomno.ToString());
                            send = true;
                            InputBox box = new InputBox("Type OTP", "", false);
                            box.outStr = "OTP";
                            box.random = randomno;
                            box.ShowInTaskbar = false;
                            box.ShowDialog(this);

                            if (box.match == true)
                            {

                                //save();
                                //ac_id = Record.Ac_id;
                                //System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize();
                                //ps.RawKind = (int)System.Drawing.Printing.PaperKind.A4;

                                //printDocument1.DefaultPageSettings.PaperSize = ps;
                                //printDocument1.DefaultPageSettings.Landscape = true;
                                //printDocument1.PrintController = new System.Drawing.Printing.StandardPrintController();


                                //var thread = new Thread(printDocument1.Print);
                                //thread.SetApartmentState(ApartmentState.STA);
                                //thread.Start();


                                //    this.Close();
                                //    this.Dispose();




                            }
                            else if (box.match == false)
                            {
                                this.Close();
                                this.Dispose();
                                return;
                            }
                        }
                    }

                    save();
                    ac_id = Record.Ac_id;
                    System.Drawing.Printing.PaperSize ps = new System.Drawing.Printing.PaperSize();
                    ps.RawKind = (int)System.Drawing.Printing.PaperKind.A4;

                    printDocument1.DefaultPageSettings.PaperSize = ps;
                    printDocument1.DefaultPageSettings.Landscape = true;
                    printDocument1.PrintController = new System.Drawing.Printing.StandardPrintController();


                    var thread = new Thread(printDocument1.Print);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();

                    if (gStr == "0")
                    {

                        LoadData("0", this.Text);
                        TextBox1.Focus();
                    }
                    else
                    {
                        this.Close();
                        this.Dispose();
                    }



                    garbage();
                    GC.Collect();
                }
            }
        }

        private void garbage()
        {
            Version vt;
            for (int i = 0; i < 1000; i++)
            {
                vt = new Version();
            }

        }


        private void save()
        {
            using (MarwariContext db = new MarwariContext())
            {
                using (TransactionScope s = new TransactionScope())
                {
                    if (rdate == "" || rdate == null)
                    {
                        rdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
                        rtime = System.DateTime.Now.ToString("HH:mm:ss");

                    }

                    rtime = label9.Text;

                    int ID = int.Parse(gStr);

                    Exists = db.Accounts.Any(p => p.Ac_id == ID);

                    if (Exists)
                    {
                        Record = db.Accounts.Where(p1 => p1.Ac_id == ID).First();

                        rtime = DateTime.Parse(Record.RTime.ToString()).ToString("HH:mm:ss");

                    }
                    else
                    {

                    }


                    Record.Name = TextBox1.Text;
                    Record.Age = textBox2.Text;
                    Record.Number = textBox3.Text;

                    if (radioButton1.Checked == true)
                    {
                        Record.Sex = "Male";
                    }
                    else if (radioButton2.Checked == true)
                    {
                        Record.Sex = "Female";
                    }
                    else if (radioButton3.Checked == true)
                    {
                        Record.Sex = "Third Gender";
                    }

                    Record.RDate = DateTime.Now;

                    if (radioButton4.Checked == true)
                    {
                        Record.Pay = "Paid";
                    }
                    else if (radioButton5.Checked == true)
                    {
                        Record.Pay = "U.T. Free";
                    }
                    else if (radioButton6.Checked == true)
                    {
                        Record.Pay = "Free (Other)";
                    }

                    Record.Other = "";
                    Record.RDate = DateTime.Parse(rdate.ToString());
                    Record.RTime = rtime;

                    if (!Exists) //add
                    {

                        Record.Pat_id = db.Accounts.Max(p => p.Pat_id) + 1;
                        db.Accounts.Add(Record);
                    }
                    else
                    {
                        // Record.rtime = textBox4.Text;

                        // acid = Record.Ac_id;
                    }

                    //   db.SaveChanges();

                    rdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
                    rtime = System.DateTime.Now.ToString("HH:mm:ss");


                    if (alter == true)
                    {
                        logrecord.RegNo = Record.Pat_id;
                        logrecord.RDate = DateTime.Now.Date;
                        logrecord.RTime = DateTime.Now.ToString("HH:mm:ss");
                        logrecord.OldName = CryptoEngine.Encrypt(oldname, "Marwari-Software");
                        logrecord.NewName = CryptoEngine.Encrypt(Record.Name, "Marwari-Software");
                        //db.Logs.Add(logrecord);
                        db.Logs.AddOrUpdate(logrecord);
                    }
                    db.SaveChanges();
                    s.Complete();
                }
            }
           
            GC.Collect();
            System.Drawing.Icon appIcon = System.Drawing.Icon.ExtractAssociatedIcon(Application.StartupPath + "\\DHospital.exe");
            notifyIcon.Icon = appIcon;
            notifyIcon.Visible = true;
            notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = "Saved";
            notifyIcon.BalloonTipText = "Saved Successfully";
            notifyIcon.ShowBalloonTip(1000);
        }

        private bool validate()
        {
            if (TextBox1.Text.Trim() == "")
            {
                MessageBox.Show("Enter Patient Name");
                TextBox1.BackColor = Color.Aqua;
                TextBox1.Focus();
                return false;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter Age");
                textBox2.BackColor = Color.Aqua;
                textBox2.Focus();
                return false;
            }

            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Enter Room No.");
                textBox3.BackColor = Color.Aqua;
                textBox3.Focus();
                return false;
            }

            DateTime t1 = DateTime.Now;
            DateTime t2 = Convert.ToDateTime("7:50:00 AM");
            DateTime t3 = Convert.ToDateTime("1:45:00 PM");

            if (patid == 0)
            {
                if (!(t1 > t2 && t1 < t3))
                {
                    MessageBox.Show("Try After 7:50 A.M. and Before 1:45 P.M.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        public void LoadData(String str, String frmCaption)
        {
            gStr = str;
            patid = 0;


            int ID = int.Parse(gStr);
            this.Text = frmCaption;
            using (MarwariContext db = new MarwariContext())
            {
                Exists = db.Accounts.Any(p => p.Ac_id == ID);

                if (Exists)
                {
                    logrecord = new Log();
                    Record = db.Accounts.Where(p1 => p1.Ac_id == ID).First();
                    rdate = DateTime.Parse(Record.RDate.ToString()).ToString("dd-MMM-yyyy");
                    rtime = DateTime.Parse(Record.RTime.ToString()).ToString("HH:mm:ss");
                    TextBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    label3.Visible = true;
                    textBox4.Visible = true;
                    textBox4.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;

                    textBox3.Text = Record.Number;
                    textBox4.Text = rtime;
                    oldname = Record.Name;
                }
                else
                {
                    Record = new Account();


                }
            }
            patid = Record.Pat_id;
            TextBox1.Text = Record.Name;
            textBox2.Text = Record.Age;

            if (Record.Sex == "Male")
            {
                radioButton1.Checked = true;
            }
            else if (Record.Sex == "Female")
            {
                radioButton2.Checked = true;
            }
            else if (Record.Sex == "Third Gender")
            {
                radioButton3.Checked = true;
            }

            if (Record.Pay == "Paid")
            {
                radioButton4.Checked = true;
            }
            else if (Record.Pay == "U.T. Free")
            {
                radioButton5.Checked = true;
            }
            else if (Record.Pay == "Free (Other)")
            {
                radioButton6.Checked = true;
            }

        }

        private void frm_patient_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Print(e, ac_id);
        }

        public void Print(System.Drawing.Printing.PrintPageEventArgs e, int ac_id)
        {
            using (MarwariContext db = new MarwariContext())
            {
                Account print_record = db.Accounts.Where(po => po.Ac_id == ac_id).First();

                int w = 590;
                int x = 0;
                int y = 0;

                Font printFontExtraBold = new Font("Arial", 20, FontStyle.Bold);
                Font printFontB = new Font("Arial", 12, FontStyle.Bold);
                Font printFontN = new Font("Arial", 10);


                Graphics g = e.Graphics;

                SolidBrush Brush = new SolidBrush(Color.Black);



                y = y + 20;
                g.DrawString("स्वामी कल्याण देव राजकीय जिला चिकित्सालय", printFontExtraBold, Brush, 10, y);
                y = y + 50;
                g.DrawString("जनपद - मुज़फ्फरनगर", printFontB, Brush, (w - g.MeasureString("जनपद - मुज़फ्फरनगर", printFontB).Width) / 2, y);
                y = y + 30;
                g.DrawString("बाह्य रोगी टिकट", printFontB, Brush, (w - g.MeasureString("बाह्य रोगी टिकट", printFontB).Width) / 2, y);


                string s = "abc";


                y = y + 30;

                g.DrawString("पंजीकरण संख्या-", printFontN, Brush, 10, y);
                g.DrawString(print_record.Pat_id.ToString().PadLeft(7, '0'), printFontB, Brush, 110, y);


                g.DrawString("दिनाक-", printFontN, Brush, 300, y);
                g.DrawString(DateTime.Parse(print_record.RDate.ToString()).ToString(Database.dformat), printFontB, Brush, 370, y);


                y = y + 20;

                g.DrawString("रोगी का नाम-", printFontN, Brush, 10, y);
                g.DrawString(print_record.Name.ToString(), printFontB, Brush, 110, y);




                g.DrawString("समय-", printFontN, Brush, 300, y);




                string atime = print_record.RTime.ToString();



                int ahour = int.Parse(atime.Split(':')[0].ToString());
                int amin = int.Parse(atime.Split(':')[1].ToString());
                int asec = int.Parse(atime.Split(':')[2].ToString());
                DateTime acctime = new DateTime(2016, 1, 1, ahour, amin, asec);



                g.DrawString(acctime.ToString("HH:mm:ss"), printFontB, Brush, 370, y);
                // g.DrawString(rtime, printFontB, Brush, 370, y);


                y = y + 20;

                g.DrawString("आयु-", printFontN, Brush, 10, y);
                g.DrawString(print_record.Age.ToString(), printFontB, Brush, 110, y);



                g.DrawString("कमरा संख्या-", printFontN, Brush, 300, y);
                g.DrawString(print_record.Number.ToString(), printFontB, Brush, 400, y);


                y = y + 20;

                g.DrawString("लिंग-", printFontN, Brush, 10, y);
                g.DrawString(print_record.Sex.ToString(), printFontB, Brush, 110, y);


                g.DrawString("भुगतान-", printFontN, Brush, 300, y);
                g.DrawString(print_record.Pay.ToString(), printFontB, Brush, 400, y);

                y = y + 30;


                Pen p = new Pen(Color.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                Point p1 = new Point(10, y);
                Point p2 = new Point(520, y);
                g.DrawLine(p, p1, p2);

                p1 = new Point(10, y);
                p2 = new Point(10, y + 545);
                g.DrawLine(p, p1, p2);



                p1 = new Point(100, y);
                p2 = new Point(100, y + 545);
                g.DrawLine(p, p1, p2);


                p1 = new Point(520, y);
                p2 = new Point(520, y + 545);
                g.DrawLine(p, p1, p2);

                p1 = new Point(10, y);
                p2 = new Point(520, y);
                g.DrawLine(p, p1, p2);

                p1 = new Point(10, y + 30);
                p2 = new Point(520, y + 30);
                g.DrawLine(p, p1, p2);

                p1 = new Point(10, y + 30);
                p2 = new Point(520, y + 30);
                g.DrawLine(p, p1, p2);

                p1 = new Point(10, y + 545);
                p2 = new Point(520, y + 545);
                g.DrawLine(p, p1, p2);

                g.DrawString("दिनाक", printFontB, Brush, 10, y);
                g.DrawString("उपचार", printFontB, Brush, 260, y);

                y = y + 545;

                g.DrawString("नोट: यह पर्ची केवल 15 दिन तक मान्य है", printFontB, Brush, (w - g.MeasureString("नोट: यह पर्ची केवल 15 दिन तक मान्य है", printFontB).Width) / 2, y);

                System.Drawing.Image img = System.Drawing.Image.FromFile(Application.StartupPath + "\\1.jpg");
                Point loc = new Point(470, 60);
                e.Graphics.DrawImage(img, loc);
            }

        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox1.Text.Trim() == "" && e.KeyCode == Keys.Enter)
            {
                TextBox1.TextBoxEnter();
                MessageBox.Show("Enter Patient Name");
            }
            else
            {
                TextBox1.TextBoxLeave();
                this.Enter2Tab(e);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox2.Text.Trim() == "" && e.KeyCode == Keys.Enter)
            {
                textBox2.TextBoxEnter();
                MessageBox.Show("Enter Age");
            }
            else
            {
                textBox2.TextBoxLeave();
                this.Enter2Tab(e);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox3.Text.Trim() == "" && e.KeyCode == Keys.Enter)
            {
                textBox3.TextBoxEnter();
                MessageBox.Show("Enter Room No");
            }
            else
            {
                textBox3.TextBoxLeave();
                this.Enter2Tab(e);
            }
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            TextBox1.TextBoxEnter();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.TextBoxEnter();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.TextBoxEnter();
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            if (TextBox1.Text.Trim() == "")
            {
                TextBox1.TextBoxEnter();
            }
            else
            {
                TextBox1.Text = TextBox1.Text.ToUpper();
                TextBox1.TextBoxLeave();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
            {
                textBox2.TextBoxEnter();
            }
            else
            {
                textBox2.TextBoxLeave();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.TextBoxLeave();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar.ToString() == ".") && !(e.KeyChar.ToString() == "-");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            groupBox2.GroupBoxEnter();
        }

        private void groupBox2_Leave(object sender, EventArgs e)
        {
            groupBox2.GroupBoxLeave();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            groupBox3.GroupBoxEnter();
        }

        private void groupBox3_Leave(object sender, EventArgs e)
        {
            groupBox3.GroupBoxLeave();
        }

        private void radioButton1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enter2Tab(e);
        }

        private void radioButton2_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enter2Tab(e);
        }

        private void radioButton3_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enter2Tab(e);
        }

        private void radioButton4_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enter2Tab(e);
        }

        private void radioButton5_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enter2Tab(e);
        }

        private void radioButton6_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enter2Tab(e);
        }
    }
}
