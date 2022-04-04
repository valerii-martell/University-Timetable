using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversalTimetable
{
    static class Coincidence
    {
        public static string ReplaceToLatina(this string str)
        {
            str = str.Replace('і', 'i');
            str = str.Replace('І', 'I');
            str = str.Replace('е', 'e');
            str = str.Replace('Е', 'E');
            str = str.Replace('м', 'm');
            str = str.Replace('Н', 'H');
            str = str.Replace('о', 'o');
            str = str.Replace('О', 'O');
            str = str.Replace('Р', 'P');
            str = str.Replace('х', 'x');
            str = str.Replace('Х', 'X');
            str = str.Replace('Т', 'T');
            str = str.Replace('у', 'y');
            str = str.Replace('р', 'p');
            str = str.Replace('а', 'a');
            str = str.Replace('А', 'A');
            str = str.Replace('к', 'k');
            str = str.Replace('К', 'K');
            str = str.Replace('с', 'c');
            str = str.Replace('С', 'C');
            str = str.Replace('В', 'B');
            return str;
        }

        public static string ReplaceToCyrilla(this string str)
        {
            str = str.Replace('i', 'і');
            str = str.Replace('I', 'І');
            str = str.Replace('e', 'е');
            str = str.Replace('E', 'Е');
            str = str.Replace('m', 'м');
            str = str.Replace('H', 'Н');
            str = str.Replace('o', 'о');
            str = str.Replace('O', 'О');
            str = str.Replace('P', 'Р');
            str = str.Replace('x', 'х');
            str = str.Replace('X', 'Х');
            str = str.Replace('T', 'Т');
            str = str.Replace('y', 'у');
            str = str.Replace('p', 'р');
            str = str.Replace('a', 'а');
            str = str.Replace('A', 'А');
            str = str.Replace('k', 'к');
            str = str.Replace('K', 'К');
            str = str.Replace('c', 'с');
            str = str.Replace('C', 'С');
            str = str.Replace('B', 'В');
            return str;
        }
    }

    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
     
        protected void Button1_Click(object sender, EventArgs e)
        {
            string groupName = TextBoxGroupName.Text;
            Response.Redirect(Page.ResolveUrl("~/Table/html/"+groupName+".htm"));
        }
    }
}