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
        static void Main(string[] args)
        {
            string pathToFile = "../../../ConsoleApplication1/bin/Debug/MySpreadsheet.xml";
            AbstractSpreadsheet test = new Spreadsheet(pathToFile, s=> true, s => s, "1");
            test.Save("MySpreadSheet1");
            Console.ReadLine();
        }
    }
}
