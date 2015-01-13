/// Sam Godfrey
/// U0467570

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;
using System.Text.RegularExpressions;

namespace SS
{
    class Program
    {
        static Dictionary<string, double> variables = new Dictionary<string, double>();
        static void Main(string[] args)
        {
            List<string> set = new List<string>();

            AbstractSpreadsheet test = new Spreadsheet(IsValid, Normalize, "1");
            test.SetContentsOfCell("A1", "=5 3");
            test.SetContentsOfCell("B1", "3");
            test.SetContentsOfCell("C1", "3");
            test.Save("MySpreadsheet");
            test.SetContentsOfCell("D1", "Hello");
            test.Save("MySpreadsheet");
            Console.ReadLine();
        }

        static string Normalize(string s)
        {
            string upper = s.ToUpper();
            return upper;
        }
        static bool IsValid(string t)
        {
            bool validVariable;
            Match valid = Regex.Match(t, @"^[A-Za-z_]+[0-9]+$");
            if (valid.Success)
            { validVariable = true; }
            else { validVariable = false; }
            return validVariable;
        }
    }
}
