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
        static string[] days = new string[] {"mon", "tue", "wed", "thu", "fri"};

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
                    if (all)
                    {
                        if (!ws.Name.Contains("roz_group")) wss.Add(ws);
                    } else
                    {
                        foreach(string s1 in list)
                        {
                            if (s1.Trim().Equals(ws.Name))
                            {
                                wss.Add(ws);
                                break;
                            }
                        }
                    }
                }
            }

            //Dictionary<string, string[,]> tables = new Dictionary<string, string[,]>();

            foreach (Excel.Worksheet ws in wss)
            {
                generateHTML(ws.Name, getTable(ws));
                //tables.Add(ws.Name, getTable(ws));
            }

            /////
            foreach (Excel.Workbook wb1 in wbs) wb1.Close(0);
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
                for (int i = 6; i <= rowCount; i++)
                {
                    Excel.Range cell5 = (Excel.Range)xlRange.Cells[i, 1];
                    Boolean merged5 = (Boolean)cell5.MergeCells;


                if (merged5) {
                        for (int j = 3; j <= colCount; j++)
                        {
                            Excel.Range cell = (Excel.Range)xlRange.Cells[i, j];
                            Boolean merged = (Boolean)cell.MergeCells;

                            if (merged)
                            {
                                string str = xlRange.Cells[i, j].Text;
                                tsReqs[i, j - 1] = str;
                            }
                        }

                        i++;
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

        static void generateHTML(string name, string[,] table)
        {
            String text = File.ReadAllText(source);
            text = text.Replace("group_name", name);

            for(int i = 5; i < table.GetLength(0); i++)
            {
                try
                {
                    int g, k, k2;
                    Int32.TryParse(table[i, 0], out g);
                    Int32.TryParse(table[i, 1], out k);

                    if (i + 1 < table.GetLength(0)) Int32.TryParse(table[i + 1, 1], out k2);
                    else k2 = 0;

                    for(int j = 0; j < days.GetLength(0); j++)
                    {
                        /*
                        text = text.Replace(days[j] + "1" + g, table[i, j+2]);
                        try
                        {
                            text = text.Replace(days[j] + "2" + g, table[i + 1, j + 2]);
                        } catch
                        {
                            text = text.Replace(days[j] + "2" + g, "");
                        }*/

                        if (k == 1 && k2 == 2)
                        {
                            text = text.Replace(days[j] + "1" + g, table[i, j + 2]);
                            try
                            {
                                text = text.Replace(days[j] + "2" + g, table[i + 1, j + 2]);
                            }
                            catch
                            {
                                text = text.Replace(days[j] + "2" + g, "");
                            }

                        } else if (k == 2)
                        {
                            text = text.Replace(days[j] + "2" + g, table[i, j + 2]);
                        } else if (k == 1)
                        {
                            text = text.Replace(days[j] + "1" + g, table[i, j + 2]);
                        }

                    }

                } catch
                {

                }
            }

            for(int i = 1; i <= 6; i++)
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
