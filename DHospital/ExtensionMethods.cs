using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DHospital
{
    public static class ExtensionMethods
    {
        
        public static void TextBoxEnter(this TextBox tb)
        {
            tb.BackColor = System.Drawing.Color.AntiqueWhite;
            tb.ForeColor = System.Drawing.Color.Black;
        }

        public static void TextBoxLeave(this TextBox tb)
        {
            tb.BackColor = System.Drawing.Color.White;
            tb.ForeColor = System.Drawing.Color.Black;
        }

        public static void Enter2Tab(this Form thisfrm, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                thisfrm.SelectNextControl(thisfrm.ActiveControl, true, true, true, true);
            }
            thisfrm.Activate();
        }

        public static void GroupBoxEnter(this GroupBox gb)
        {
            gb.BackColor = System.Drawing.Color.AntiqueWhite;
            gb.ForeColor = System.Drawing.Color.Black;
        }

        public static void GroupBoxLeave(this GroupBox gb)
        {
            gb.BackColor = System.Drawing.Color.White;
            gb.ForeColor = System.Drawing.Color.Black;
        }
    }
}
