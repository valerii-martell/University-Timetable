using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace FormTable
{
    static class Program
    {
        static Excel.Application xlobj;
        static string source = "example.htm";
        static string exfol = @"excel\";
        static string htmlfol = @"html\";
        static string[] days = new string[] {"mon", "tue", "wed", "thu", "fri", "sat"};

        [STAThread]
        static void Main()
        {
            if (!Directory.Exists(exfol))
            {
                Directory.CreateDirectory(exfol);
            }

            if (!Directory.Exists(htmlfol))
            {
                Directory.CreateDirectory(htmlfol);
                Directory.CreateDirectory(htmlfol + @"site_res\");
                File.Copy("style.css", htmlfol + @"site_res\style.css");
                File.Copy("bootstrap.min.css", htmlfol + @"site_res\bootstrap.min.css");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

        public static void generate(bool all, string[] list)
        {
            //////
            xlobj = new Excel.Application();

            string[] files = Directory.GetFiles(exfol);
            List<Excel.Workbook> wbs = new List<Excel.Workbook>();

            foreach (string s in files)
            {
                if (s.Contains("xls") || s.Contains("xlsx"))
                {
                    string s1 = AppDomain.CurrentDomain.BaseDirectory + @"\" + s;
                    wbs.Add(xlobj.Workbooks.Open(s1));
                }
            }

            List<Excel.Worksheet> wss = new List<Excel.Worksheet>();

            foreach (Excel.Workbook wb in wbs)
            {
                foreach (Excel.Worksheet ws in wb.Worksheets)
                {
                    if (ws.Name.Contains("Лист1")) wss.Add(ws);
                }
            }

            foreach (Excel.Worksheet ws in wss)
            {
                string[,] table = getTable(ws);

                Excel.Range xlRange = ws.UsedRange;
                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                for (int i = 4; i <= colCount; i++)
                {
                    if (all)
                    {
                        string str = xlRange.Cells[12, i].Text;

                        generateHTML(str, table, i - 1);
                    }
                    else
                    {
                        string str = xlRange.Cells[12, i].Text;

                        foreach (string ls in list)
                        {
                            if (ls.Contains(str))
                            {
                                generateHTML(str, table, i - 1);
                                break;
                            }
                        }
                    }

                }
            }
            xlobj.Quit();

        }

        static string[,] getTable(Excel.Worksheet ws)
        {

            try
            {
                Excel.Range xlRange = ws.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                string[,] tsReqs = new string[rowCount, colCount];

                for (int i = 1; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        string str = xlRange.Cells[i, j].Text;
                        tsReqs[i - 1, j - 1] = str;
                    }
                }


                ////////////
                for (int i = 13; i < rowCount; i+=2)
                {
                    for (int j = 4; j <= colCount; j++)
                    {
                        Excel.Range cell5 = (Excel.Range)xlRange.Cells[i, j];
                        Boolean merged5 = (Boolean)cell5.MergeCells;

                        Excel.Range cell6 = (Excel.Range)xlRange.Cells[i + 1, j];
                        Boolean merged6 = (Boolean)cell6.MergeCells;

                        if(merged5 && merged6)
                        {
                            tsReqs[i, j - 1] = tsReqs[i - 1, j - 1];
                        }
                    }
                }

                for (int i = 13; i <= rowCount; i ++)
                {
                    for (int j = 4; j < colCount; j++)
                    {
                        Excel.Range cell5 = (Excel.Range)xlRange.Cells[i, j];
                        Boolean merged5 = (Boolean)cell5.MergeCells;

                        Excel.Range cell6 = (Excel.Range)xlRange.Cells[i, j + 1];
                        Boolean merged6 = (Boolean)cell6.MergeCells;

                        if (merged5 && merged6 && !merged6.Equals("") && !merged6.Equals(" ") &&
                            xlRange.Cells[12, j].Text.Substring(0,4).Equals(xlRange.Cells[12, j+1].Text.Substring(0, 4)) &&
                            !xlRange.Cells[12, j + 1].Text.Contains("ін."))
                        {
                            tsReqs[i - 1, j] = tsReqs[i - 1, j - 1];

                        }
                    }
                }

                /////////////

                return tsReqs;
            }

            catch
            {
                return null;
            }
        }

        static void generateHTML(string name, string[,] table, int column)
        {
            String text = File.ReadAllText(source);
            text = text.Replace("group_name", name);

            int k1 = 0;
            int k2 = 1;
            for(int i = 12; i < 72; i+=10)
            {
                text = text.Replace(days[k1] + "1" + k2, table[i, column]);
                text = text.Replace(days[k1] + "2" + k2, table[i+1, column]);
                k2++;

                text = text.Replace(days[k1] + "1" + k2, table[i+2, column]);
                text = text.Replace(days[k1] + "2" + k2, table[i+3, column]);
                k2++;

                text = text.Replace(days[k1] + "1" + k2, table[i+4, column]);
                text = text.Replace(days[k1] + "2" + k2, table[i+5, column]);
                k2++;

                text = text.Replace(days[k1] + "1" + k2, table[i+6, column]);
                text = text.Replace(days[k1] + "2" + k2, table[i+7, column]);
                k2++;

                text = text.Replace(days[k1] + "1" + k2, table[i+8, column]);
                text = text.Replace(days[k1] + "2" + k2, table[i+9, column]);
                k2++;

                k1++;
                k2 = 1;
            }

            for(int i = 1; i <= 5; i++)
            {
                    for (int j = 0; j < days.GetLength(0); j++)
                    {
                        text = text.Replace(days[j] + "1" + i, "");
                        text = text.Replace(days[j] + "2" + i, "");
                    }
            }

            File.Create(htmlfol + name + ".htm").Close();
            File.WriteAllText(htmlfol + name + ".htm", text);

        }
    }
}
