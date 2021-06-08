using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHospital
{
    class Database
    {
        public static String fname;
        public static String fyear;
        public static String uname;
        public static String utype;

        public static DateTime ldate = new DateTime();
        public static DateTime stDate = new DateTime();
        public static DateTime enDate = new DateTime();
        public static DateTime ExeDate = DateTime.Parse("19-Nov-2016");
        public static String dformat = "dd-MMM-yyyy";


        public static void setVariable(String fnm, String fyr, String unm, String utyp,  DateTime dt1, DateTime dt2)
        {
            fname = fnm;
            fyear = fyr;
            uname = unm;
            utype = utyp;
            
            stDate = DateTime.Parse(dt1.ToString("dd-MMM-yyyy"));
            enDate = DateTime.Parse(dt2.ToString("dd-MMM-yyyy"));
        }
    }
}
