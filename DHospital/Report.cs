using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using DHospital.Model;

namespace DHospital
{
    public partial class Report : Form
    {
        BindingSource bs = new BindingSource();
      

        static Company comp_record ;

        DateTime stdt = new DateTime();
        DateTime endt = new DateTime();
        public String frmptyp;
        public static String frmptyp2;
        public String DecsOfReport;
        public static String DecsOfReport2;
        public string str = "";
        public static string Pagesize = "";
        public static string str2 = "";

        public Report()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "dd-MMM-yyyy";
            using (MarwariContext db = new MarwariContext())
            {
                comp_record = db.Companies.First();
            }
        }

        private void Report_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }

        public void DateWiseReport(DateTime DateFrom)
        {
            using (MarwariContext db = new MarwariContext())
            {
                stdt = DateFrom;

                dateTimePicker1.Value = DateFrom;

                this.Text = "Registration Report, " + DateFrom.ToString(Database.dformat);

                bs.DataSource = db.Accounts.Select(rpt => new { Registration_No = rpt.Pat_id, Name = rpt.Name, Room_Number = rpt.Number, Gender = rpt.Sex, Age = rpt.Age, Date = rpt.RDate, Time = rpt.RTime, pay = rpt.Pay }).Where(p1 => p1.Date == stdt).OrderByDescending(d => d.Registration_No).ToList();

                

                dataGridView1.DataSource = bs;

                dataGridView1.Columns["Registration_No"].Width = 100;
                dataGridView1.Columns["Name"].Width = 150;
                dataGridView1.Columns["Room_Number"].Width = 110;
                dataGridView1.Columns["Gender"].Width = 130;
                dataGridView1.Columns["Age"].Width = 150;
                dataGridView1.Columns["Date"].Width = 100;
                dataGridView1.Columns["Time"].Width = 100;
                dataGridView1.Columns["Pay"].Width = 100;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateWiseReport(dateTimePicker1.Value);
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            string tPath = Path.GetTempPath() + DateTime.Now.ToString("yyMMddhmmssfff") + ".pdf";
            ExportToPdf(tPath);
            GC.Collect();
            PdfReader frm = new PdfReader();
            frm.LoadFile(tPath);
            frm.Visible = false;
            frm.axAcroPDF1.printWithDialog();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            string tPath = Path.GetTempPath() + DateTime.Now.ToString("yyMMddhmmssfff") + ".pdf";
            ExportToPdf(tPath);
            GC.Collect();
            PdfReader frm = new PdfReader();
            frm.LoadFile(tPath);
            frm.Show();
        }

        private void ExportToPdf(string tPath)
        {
            frmptyp2 = frmptyp;
            DecsOfReport2 = DecsOfReport;
            str2 = str;
            dataGridView2 = dataGridView1;



            FileStream fs = new FileStream(tPath, FileMode.Create, FileAccess.Write, FileShare.None);
            iTextSharp.text.Rectangle rec;
            Document document;
            int Twidth = 0;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Twidth += dataGridView1.Columns[i].Width;
            }
          
            document = new Document(PageSize.A4, 20f, 10f, 20f, 10f);
       

            Pagesize = "A4";
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            writer.PageEvent = new MainTextEventsHandler();
            document.Open();
            HTMLWorker hw = new HTMLWorker(document);
            str = "";
            str += @"<body> <font size='1'><table border=1> <tr>";
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                string align = "";
                string bold = "";
                int width = 0;

                if (Twidth == 2000)
                {
                    width = dataGridView1.Columns[i].Width / 20;
                }
                else
                {
                    width = dataGridView1.Columns[i].Width / 10;
                }

                if (dataGridView1.Columns[i].HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight)
                {
                    align = "text-align:right;";
                }

                bold = "font-weight: bold;";

                if (width != 0)
                {
                    str += "<th width=" + width + "%  style='" + align + bold + "'>" + dataGridView1.Columns[i].HeaderText.ToString() + "</th> ";
                }

            }

            str += "</tr>";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                str += "<tr> ";
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    int width = 0;
                    if (Twidth == 2000)
                    {
                        width = dataGridView1.Rows[i].Cells[j].Size.Width / 20;

                    }
                    else
                    {
                        width = dataGridView1.Rows[i].Cells[j].Size.Width / 10;
                    }

                    if (width != 0)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            string align = "";
                            string bold = "";
                            string colspan = "";

                            if (dataGridView1.Columns[j].DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                            {
                                align = "text-align:right;";
                            }

                            if (dataGridView1.Rows[i].Cells[j].Style.Font != null && dataGridView1.Rows[i].Cells[j].Style.Font.Bold == true)
                            {
                                bold = "font-weight: bold;";
                            }

                            if (j == 0 && dataGridView1.Rows[i].Cells[0].Value.ToString() != "" && dataGridView1.Rows[i].Cells[1].Value == null && dataGridView1.Rows[i].Cells[2].Value == null)
                            {
                                colspan = "colspan= '2'";
                            }


                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "")
                            {
                                str += "<td> &nbsp; </td>";
                            }
                            else
                            {
                                str += "<td " + colspan + "  style='" + align + bold + "'>" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "</td> ";
                            }

                            if (j == 0 && dataGridView1.Rows[i].Cells[0].Value.ToString() != "" && dataGridView1.Rows[i].Cells[1].Value == null && dataGridView1.Rows[i].Cells[2].Value == null)
                            {
                                j++;
                            }


                        }
                        else
                        {


                            str += "<td> &nbsp; </td>";

                        }
                    }
                }
                str += "</tr> ";
            }
            str += "</table></font></body>";

            StringReader sr = new StringReader(str);
            hw.Parse(sr);
            document.Close();

        }


        internal class MainTextEventsHandler : PdfPageEventHelper
        {
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);

                bool sta = false;
                
                PdfPTable table = new PdfPTable(1);
                PdfPCell cell = new PdfPCell();

                if (sta == false)
                {
                    cell.Phrase = new Phrase(comp_record.Name.ToString());
                    cell.BorderWidth = 0f;
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(comp_record.Address1.ToString());
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(comp_record.Address2.ToString());
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(Report.DecsOfReport2);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase("\n");
                    table.AddCell(cell);
                }
                else
                {
                    cell.Phrase = new Phrase("\n");
                    cell.BorderWidth = 0f;
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell);
                    cell.Phrase = new Phrase("\n");
                    table.AddCell(cell);
                    cell.Phrase = new Phrase("\n");
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(Report.DecsOfReport2);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase("\n");
                    table.AddCell(cell);
                }
                document.Add(table);
            }


            public override void OnEndPage(PdfWriter writer, Document document)
            {

                base.OnEndPage(writer, document);
                string text = "";
                text += "Page No-" + document.PageNumber;
                PdfContentByte cb = writer.DirectContent;
                cb.BeginText();
                BaseFont bf = BaseFont.CreateFont();
                cb.SetFontAndSize(bf, 8);
                if (Pagesize == "A4")
                {
                    cb.SetTextMatrix(530, 8);
                }


                cb.ShowText(text);
                cb.EndText();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "Adobe Acrobat(*.pdf) | *.pdf";

            if (DialogResult.OK == ofd.ShowDialog())
            {
                ExportToPdf(ofd.FileName);
                MessageBox.Show("Export Successfully!!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            Object misValue = System.Reflection.Missing.Value;
            Excel.Application apl = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook wb = (Excel.Workbook)apl.Workbooks.Add(misValue);
            Excel.Worksheet ws;
            ws = (Excel.Worksheet)wb.Worksheets[1];

            int lno = 1;
            DataTable dtExcel = new DataTable();

            ws.Cells[lno, 1] = comp_record.Name.ToString();
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Merge(Type.Missing);
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].Merge(Type.Missing);
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].Font.Bold = true;
            lno++;

            ws.Cells[lno, 1] = comp_record.Address1.ToString();
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].Merge(Type.Missing);
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].Font.Bold = true;
            lno++;

            ws.Cells[lno, 1] = comp_record.Address2.ToString();
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].Merge(Type.Missing);
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ws.Range[ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]].Font.Bold = true;
            lno++;



            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight)
                {
                    ws.Range[ws.Cells[5, i + 1], ws.Cells[5, i + 1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                }
                ws.Range[ws.Cells[i + 1, i + 1], ws.Cells[i + 1, i + 1]].ColumnWidth = dataGridView1.Columns[i].Width / 11.5;
                ws.Cells[5, i + 1] = dataGridView1.Columns[i].HeaderText.ToString();

            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Columns[j].HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight)
                    {
                        ws.Range[ws.Cells[i + 6, j + 1], ws.Cells[i + 6, j + 1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        ws.Range[ws.Cells[i + 6, j + 1], ws.Cells[i + 6, j + 1]].NumberFormat = "0,0.00";

                    }
                    else
                    {
                        ws.Range[ws.Cells[i + 6, j + 1], ws.Cells[i + 6, j + 1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    }

                    if (dataGridView1.Columns[j].DefaultCellStyle.Font != null)
                    {
                        ws.Range[ws.Cells[i + 6, j + 1], ws.Cells[i + 6, j + 1]].Font.Bold = true;
                    }

                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        ws.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().Replace(",", "");
                    }
                }
            }

            Excel.Range last = ws.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            ws.Range["A1", last].WrapText = true;


            DirectoryInfo dInfo = new System.IO.DirectoryInfo("D:\\Backup");

            if (dInfo.Exists == false)
            {
                Directory.CreateDirectory("D:\\Backup");

                wb.SaveAs("D:\\Backup\\Data" + dateTimePicker1.Value.ToString("yyyyMMdd"));


            }
            else
            {
                wb.SaveAs("D:\\Backup\\Data" + dateTimePicker1.Value.ToString("yyyyMMdd"));

            }


            apl.Visible = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
