using DHospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DHospital
{
    public partial class frmChangePass : Form
    {
       
        public System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        UserInfo user_record = new UserInfo();
        public string gStr = "";

        public frmChangePass()
        {
            InitializeComponent();
        }

        private void frmChangePass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }

        public void LoadData(string uname, string FrmCaption)
        {
            using (MarwariContext db = new MarwariContext())
            {
                gStr = uname;

                bool Exists = db.UserInfos.Any(contact => contact.UName.Equals(gStr));

                this.Text = FrmCaption;

                if (Exists)
                {
                    user_record = db.UserInfos.Where(p1 => p1.UName.Equals(gStr)).First();
                    textBox1.Text = user_record.UName;
                    textBox2.Text = user_record.UPass;
                }
                else
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (validate() == true)
            {
                save();
            }
        }
        public void save()
        {
            using (MarwariContext db = new MarwariContext())
            {
                user_record.UPass = textBox2.Text;
                db.UserInfos.AddOrUpdate(user_record);
                db.SaveChanges();
                this.Close();
                this.Dispose();
                System.Drawing.Icon appIcon = System.Drawing.Icon.ExtractAssociatedIcon(Application.StartupPath + "\\DHospital.exe");
                notifyIcon.Icon = appIcon;
                notifyIcon.Visible = true;
                notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
                notifyIcon.BalloonTipTitle = "Saved";
                notifyIcon.BalloonTipText = "Saved Successfully";
                notifyIcon.ShowBalloonTip(1000);
            }
        }

        private bool validate()
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Enter User Name");
                textBox1.BackColor = Color.Aqua;
                textBox1.Focus();
                return false;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter Password");
                textBox2.BackColor = Color.Aqua;
                textBox2.Focus();
                return false;
            }
            return true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enter2Tab(e);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            this.Enter2Tab(e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.TextBoxEnter();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.TextBoxLeave();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.TextBoxEnter();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.TextBoxLeave();
        }
    }
}
