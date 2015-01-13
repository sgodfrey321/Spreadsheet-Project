using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SS;

namespace SpreadsheetGUI
{
    public partial class SpreadsheetName : Form
    {
        public class CellToRedo
        {
            public string name { get; set; }
            public int col { get; set; }
            public int row { get; set; }

        }
        Spreadsheet spreadsheet;
        Dictionary<string, CellToRedo> cellsToRecalculate;
        private string contents;
        private string value;
        private int col, row;
        private string colString;
        private string currentCell;

        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private int currentCol { get { return col; } set { this.col = value; } }
        private string currentColString { get { return colString; } set { this.colString = value; } }
        private int currentRow { get { return row; } set { this.row = value; } }
        public string currentCellname { get { return currentCell; } set { this.currentCell = value; } }

        public SpreadsheetName()
        {
            spreadsheet = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            cellsToRecalculate = new Dictionary<string, CellToRedo>();

            InitializeComponent();
            CellName.Text = "A" + (1 + currentRow).ToString();
            currentCellname = "A" + (1 + currentRow).ToString();
            CellContents.Text = "";
            CellValue.Text = "";
            spreadsheetPanel1.SelectionChanged += displaySelection;
        }
        //public SpreadsheetName(Spreadsheet updateTo)
        //{
        //    InitializeComponent();

        //    CellName.Text = "A" + (1 + currentRow).ToString();
        //    currentCellname = "A" + (1 + currentRow).ToString();
        //    CellContents.Text = "";
        //    CellValue.Text = "";

        //    spreadsheetPanel1.SelectionChanged += displaySelection;

        //    spreadsheet = updateTo;
        //    cellsToRecalculate = new Dictionary<string, CellToRedo>();

        //    foreach (var item in spreadsheet.GetNamesOfAllNonemptyCells().ToList())
        //    {
        //        string[] cName = Regex.Split(item, @"[0-9]+");
        //        string[] rName = Regex.Split(item, @"[A-Za-z]+");
        //        string columnName = string.Join("", cName);
        //        string rowName = string.Join("", rName);
        //        int colNumber = letters.IndexOf(columnName);
        //        int rowNumber;
        //        int.TryParse(rowName, out rowNumber);

        //        spreadsheetPanel1.SetSelection(colNumber, rowNumber);
        //        spreadsheetPanel1.SetValue(colNumber, rowNumber - 1, spreadsheet.GetCellValue(item).ToString());
        //    }
        //}
        private void displaySelection(SpreadsheetPanel spreadsheetPanel)
        {

            /// Get the current selected cell
            spreadsheetPanel.GetSelection(out col, out row);
            /// Change the cell coordinates into the cell name
            currentColString = letters[currentCol].ToString();
            currentCellname = letters[currentCol].ToString() + ((1 + currentRow).ToString());

            string currentValue = string.Empty;
            if (spreadsheetPanel1.GetValue(currentCol, currentRow, out currentValue))
            {

                currentColString = letters[currentCol].ToString();
                currentCellname = ((currentColString) + (1 + currentRow).ToString());

                contents = spreadsheet.GetCellContents(currentCellname).ToString();
                value = currentValue;

                CellValue.Text = spreadsheet.GetCellValue(currentCellname).ToString();
                CellContents.Text = spreadsheet.GetCellContents(currentCellname).ToString();
            }
            else
            {
                setCellInsidesOnMouseClick();
                value = string.Empty;
                CellContents.Text = string.Empty;
            }
            CellName.Text = currentColString + (currentRow + 1).ToString();
        }
        private void setCellInsidesOnMouseClick()
        {
            foreach (var item in spreadsheet.GetNamesOfAllNonemptyCells().ToList())
            {
                if (spreadsheet.GetCellContents(item) is double)
                {

                }
                if (spreadsheet.GetCellContents(item) is string)
                {

                }
                if (spreadsheet.GetCellContents(item) is SpreadsheetUtilities.Formula)
                {
                    string[] cName = Regex.Split(item, @"[0-9]+");
                    string[] rName = Regex.Split(item, @"[A-Za-z]+");
                    string columnName = string.Join("", cName);
                    string rowName = string.Join("", rName);
                    int colNumber = letters.IndexOf(columnName);
                    int rowNumber;
                    int.TryParse(rowName, out rowNumber);
                    spreadsheetPanel1.SetValue(colNumber, rowNumber - 1, spreadsheet.GetCellValue(item).ToString());
                }
            }
            this.displaySelection(spreadsheetPanel1);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog mySave = new SaveFileDialog();
            mySave.Filter = "Spreadsheet files (*.sprd)|*.sprd|All files (*.*)|*.*";

            if (mySave.ShowDialog() == DialogResult.OK)
            {
                string saveName = mySave.FileName;
                spreadsheet.Save(saveName);
            }
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpreadsheetApplicationContext.getAppContext().RunForm(new SpreadsheetName());
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Spreadsheet Files (*.sprd)|*.sprd|All files (*.*)|*.*";

            if (loadFile.ShowDialog() == DialogResult.OK)
            {
                string loadName = loadFile.FileName;
                spreadsheet = new Spreadsheet(loadName, s => true, s => s, "default");

                foreach (var item in spreadsheet.GetNamesOfAllNonemptyCells().ToList())
                {
                    string[] cName = Regex.Split(item, @"[0-9]+");
                    string[] rName = Regex.Split(item, @"[A-Za-z]+");
                    string columnName = string.Join("", cName);
                    string rowName = string.Join("", rName);
                    int colNumber = letters.IndexOf(columnName);
                    int rowNumber;
                    int.TryParse(rowName, out rowNumber);

                    spreadsheetPanel1.SetSelection(colNumber, rowNumber);
                    spreadsheetPanel1.SetValue(colNumber, rowNumber - 1, spreadsheet.GetCellValue(item).ToString());
                }
                spreadsheetPanel1.SetSelection(0, 0);
            }

        }

        private void spreadsheetPanel1_KeyPress(object sender, KeyPressEventArgs e)
        {
            value += e.KeyChar.ToString();
            spreadsheetPanel1.SetValue(currentCol, currentRow, value);
            CellContents.Text = value;
            contents = value;
        }
        private void setCellInsides()
        {
            string previousContents = spreadsheet.GetCellContents(currentCellname).ToString();
            List<string> cellsToRelc = new List<string>();
            string upperCaseContents = contents.ToUpper();
            try
            {
                cellsToRelc = spreadsheet.SetContentsOfCell(currentCellname, contents.Replace("\r", string.Empty)).ToList();
            }
            catch (SpreadsheetUtilities.FormulaFormatException)
            {
                cellsToRelc = spreadsheet.SetContentsOfCell(currentCellname, previousContents.Replace("\r", string.Empty)).ToList();
            }
            catch (CircularException)
            {
                MessageBox.Show("You have created a circular exception");

                CellContents.Text = "";
                spreadsheetPanel1.SetSelection(currentCol, (currentRow));
                spreadsheetPanel1.SetValue(currentCol, currentRow, spreadsheet.GetCellValue(currentCellname).ToString());

                this.displaySelection(spreadsheetPanel1);
            }
            catch (NullReferenceException) { }

            CellContents.Text = spreadsheet.GetCellContents(currentCellname).ToString();
            CellValue.Text = spreadsheet.GetCellValue(currentCellname).ToString();

            spreadsheetPanel1.SetValue(currentCol, currentRow, spreadsheet.GetCellValue(currentCellname).ToString());

            /// Moves the current selection down a row
            currentRow += 1;
            currentColString = letters[currentCol].ToString();
            CellName.Text = currentColString + (currentRow + 1).ToString();
            spreadsheetPanel1.SetSelection(currentCol, (currentRow));

            this.displaySelection(spreadsheetPanel1);

            foreach (var item in cellsToRelc)
            {
                string[] cName = Regex.Split(item, @"[0-9]+");
                string[] rName = Regex.Split(item, @"[A-Za-z]+");
                string columnName = string.Join("", cName);
                string rowName = string.Join("", rName);
                int colNumber = letters.IndexOf(columnName);
                int rowNumber;
                int.TryParse(rowName, out rowNumber);
                spreadsheetPanel1.SetValue(colNumber, rowNumber - 1, spreadsheet.GetCellValue(item).ToString());
            }

        }
        private void spreadsheetPanel1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;
                try
                {
                    value = value.Remove(value.Length - 1);
                    CellContents.Text = value;
                    spreadsheetPanel1.SetValue(currentCol, currentRow, value);
                }
                catch (ArgumentOutOfRangeException) { }
                catch (NullReferenceException) { }
            }
            if (e.KeyData == Keys.Enter)
            {
                setCellInsides();
            }
        }


        private void CellContents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                setCellInsides();
            }
        }
        private void CellContents_TextChanged(object sender, EventArgs e)
        {
            value = CellContents.Text;
            spreadsheetPanel1.SetValue(currentCol, currentRow, value);
            CellContents.Text = value;
            contents = value;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();           
        }

        private void SpreadsheetName_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (spreadsheet.Changed == true)
            {
                DialogResult yesOrNo = MessageBox.Show("You Have Unsaved Work, Would You Like To Save?", "Unsaved Work Alert", MessageBoxButtons.YesNo);
                if (yesOrNo == DialogResult.Yes)
                {
                    SaveFileDialog mySave = new SaveFileDialog();
                    mySave.Filter = "Spreadsheet files (*.sprd)|*.sprd|All files (*.*)|*.*";

                    if (mySave.ShowDialog() == DialogResult.OK)
                    {
                        string saveName = mySave.FileName;
                        spreadsheet.Save(saveName);
                    }

                }
                if (yesOrNo == DialogResult.No)
                {
                    e.Cancel = false;
                }
            }
            if (spreadsheet.Changed != true)
            {
            }
        }
    }
}
