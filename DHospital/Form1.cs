using DHospital.Model;
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
    public partial class Form1 : Form
    {  
        UserInfo user_record = new UserInfo();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Login Form";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter username");
                textBox1.Focus();
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Enter password");
                textBox2.Focus();
                return;
            }
            

            using (MarwariContext db= new MarwariContext())
            {
                int count = db.UserInfos.Count();
                //MessageBox.Show(count.ToString());
                bool Exists = db.UserInfos.Any(contact => contact.UName.Equals(textBox1.Text) && contact.UPass.Equals(textBox2.Text));

                if (Exists)
                {  
                    user_record = db.UserInfos.Where(p1 => p1.UName.Equals(textBox1.Text) && p1.UPass.Equals(textBox2.Text)).First();

                    Company company = db.Companies.FirstOrDefault();

                    Database.setVariable(textBox3.Text, textBox4.Text, textBox1.Text, user_record.UType, DateTime.Parse(company.StartFrom.ToString("dd-MMM-yyyy")), DateTime.Parse(company.EndAt.ToString("dd-MMM-yyyy")));
                    
                    Database.ldate = DateTime.Parse(dateTimePicker1.Text);

                    frm_main frm = new frm_main();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                    textBox1.Focus();
                }
            }
            //db.Connection.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox2.TextBoxLeave();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.TextBoxEnter();
        }

        
    }
}
