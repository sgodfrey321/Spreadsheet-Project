/// Sam Godfrey
/// U0467570
/// Comit 145
/*
 * Overall changes from PS4:
 * Got rid of some pointless functions, I see now what you meant
 * Created a Lookup function to pass to the cell formula evaluator
 * Moved all memeber variables inside the constructor
 * Put my name on everything
 * Trying to be better about comments
 * Changed alot in the Cell class
 *      - Changed all the member variables to use properties
 *      - Added the ability for a formula value to hold an FormulaException
 *      - Made it look a ton prettier
 * 
 * Things I Do Not Understand For PS5:
 * I need to figure out the huge time jump in test 37, it happend after I added a recalculateCell method
 * to the new cell constructor(string, formula) but I need to update all the cells that depend on a new formula
 * so I dont know what to do yet
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using SpreadsheetUtilities;
using Cell;

namespace SS
{
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Dictionary for the cell name and the associated value/content etc...
        /// </summary>
        private Dictionary<string, Cell.Cell> cells;
        /// <summary>
        /// Dependency Graph to keep track of all the cells and their relations
        /// </summary>
        private DependencyGraph cellDependancy;
        /* ********************************************************* */
        private string _pathToFile;
        private bool _changed;
        private string version;
        /* ********************************************************* */
        /// <summary>
        /// The changed variable that indicates if the spreadsheet has been modified in anyway, ie add
        /// a new cell, replaced a cell
        /// Is false after Spreadsheet.Save is called
        /// </summary>
        public override bool Changed
        {
            get
            {
                return this._changed;
            }
            protected set
            {
                this._changed = value;
            }
        }
        /* ********************************************************* */
        /// <summary>
        /// Default 0 argument spreadsheet constructor
        /// </summary>
        public Spreadsheet()
            : base(s => true, s => s, "default") /// simple spelling error
        {

            cells = new Dictionary<string, Cell.Cell>();
            cellDependancy = new DependencyGraph();
        }
        /// <summary>
        /// 3 argument spreadsheet constructor
        /// </summary>
        /// <param name="isValid">The rules for what a valid variable is, matches the IsValid
        /// delegate, created by user</param>
        /// <param name="normalize">The rules for normalizing a varaibles, matches the Normalize
        /// delegate, created by user </param>
        /// <param name="version">The version information of the new Spreadsheet</param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            cells = new Dictionary<string, Cell.Cell>();
            cellDependancy = new DependencyGraph();
        }
        /// <summary>
        /// A 4 argument spreadsheet constructor
        /// </summary>
        /// <param name="pathToFile">The path of the saved file to load and create a new spreadsheet
        /// with</param>
        /// <param name="isValid">The rules for what a valid variable is, matches the IsValid
        /// delegate, created by user</param>
        /// <param name="normalize">The rules for normalizing a varaibles, matches the Normalize
        /// delegate, created by user </param>
        /// <param name="version">The version information of the new Spreadsheet</param>
        public Spreadsheet(string pathToFile, Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            cells = new Dictionary<string, Cell.Cell>();
            cellDependancy = new DependencyGraph();

            this._pathToFile = pathToFile;
            if (File.Exists(pathToFile)) { }
            else { throw new SpreadsheetReadWriteException(""); }

            XmlDocument doc = new XmlDocument();
            /// Try and load the XML document, if not we throw SpreadsheetReadWriteException
            /// but we first have to catch the FileNotFoundException
            try
            {
                doc.Load(_pathToFile);
                XmlElement root = doc.DocumentElement;
                if (root.HasAttribute("version"))
                {
                    /// If the version of the loaded XML document matches that of are new constructor
                    /// we write all of the cells in the XML document into our new spreadsheet
                    string ver = root.GetAttribute("version");
                    if (ver == version)
                    {
                        writeCells(pathToFile);
                    }
                    /// Otherwise we throw a SpreadsheetReadWriteException
                    else if (ver != version)
                    {
                        throw new SpreadsheetReadWriteException("You have entered a different version that what is in the saved file");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new SpreadsheetReadWriteException("There is no file matching the filepath");
            }
            catch (XmlException)
            {
                throw new SpreadsheetReadWriteException("There is a file already here");
            }
        }
        /// <summary>
        /// Returns the version information of the requested filename
        /// </summary>
        /// <param name="filename">File to get saved version of</param>
        /// <returns>version info</returns>
        public override string GetSavedVersion(String filename)
        {

            XmlDocument doc = new XmlDocument();
            /// Try and load the file given by filename, throws a FileNotFoundException if the file
            /// does not exist
            try
            {
                doc.Load(filename + ".xml");
                XmlElement root = doc.DocumentElement;
                /// Make sure the loaded XML file has an attribute called version, if not we throw 
                /// NullReferenceException, which is caught and then we throw SpreadsheetReadWriteException
                try
                {
                    if (root.HasAttribute("version"))
                    {
                        /// If the file has attribute version we load the attribute and return it
                        version = root.GetAttribute("version");
                    }
                }
                catch (NullReferenceException)
                {
                    throw new SpreadsheetReadWriteException("Something is wrong with the XML format");
                }
            }
            catch (FileNotFoundException)
            {
                throw new SpreadsheetReadWriteException("There is no file matching the filepath");
            }
            return version;
        }
        /// <summary>
        /// Saves the spreadsheet as an XML file in the given filename
        /// </summary>
        /// <param name="filename">filename to save XML as</param>
        public override void Save(String filename)
        {
            /// To start, we only save a file if we have changed the file
            if (this.Changed == true)
            {
                try
                {
                    /// We create the given XML file
                    using (XmlWriter writer = XmlWriter.Create(filename))
                    {
                        /// Start writing the document
                        writer.WriteStartDocument();
                        /// Add spreadsheet element
                        writer.WriteStartElement("spreadsheet");
                        /// Add version attribute with the given version as the attribute value
                        writer.WriteAttributeString("version", Version);
                        /// We then loop through all the cells we have
                        foreach (var item in cells)
                        {
                            /// Create element called cell
                            writer.WriteStartElement("cell");
                            /// Create element called name
                            writer.WriteStartElement("name");
                            /// Add the name of the current cell as a string value to the XML doc
                            writer.WriteString(item.Key.ToString());
                            /// End name element
                            writer.WriteEndElement();
                            /// Create element called contents
                            writer.WriteStartElement("contents");
                            /// We first need to check what the actuall contents are:
                            /// If they are a formula we need to add an = sign back infront so they cells
                            /// can be properly read back into the spreadsheet when the XML file is loaded
                            if (item.Value.Contents is Formula)
                            {
                                string contentsWithEqual = "=" + item.Value.Contents.ToString();
                                writer.WriteString(contentsWithEqual);
                            }
                            /// Otherwise we just add the Contents of the given cell to the XML doc as a string
                            else
                            {
                                writer.WriteString(item.Value.Contents.ToString());
                            }
                            /// We end all of the elements we have opened, namely contents and cell
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                        /// End spreadsheet element and close the document
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                }
                catch (XmlException)
                {
                    throw new SpreadsheetReadWriteException("");
                }
                /// change Changed to false because everything is current and there is no need to save again
                this.Changed = false;
            }
            else
            {
                throw new SpreadsheetReadWriteException("");
            }
        }

        /// <summary>
        /// Returns the Value of a cell
        /// </summary>
        /// <param name="name">Cell to return value of</param>
        /// <returns>Returns an object, it can be anything: a string, a formula error, a double, it all
        /// depends on what the cell originally contains</returns>
        public override object GetCellValue(String name)
        {
            /// Check to make sure the cell name is valid
            isValidAndNotNull(name);
            object value = "";
            /// return the cell.Value as a var, we dont know what it is yet
            try
            {
                value = cells[Normalize(name)].Value;
            }
            catch (KeyNotFoundException)
            {

            }
            /// If value is a double, we return a double
            if (value is double)
            {
                return (double)value;
            }
            /// If value is a string, we return a string
            else if (value is string)
            {
                return (string)value;
            }
            /// If value is a FormulaError, we return a FormulaError
            else if (value is FormulaFormatException || value is FormatException || value is FormulaError)
            {
                return new FormulaError();
            }
            return null;
        }
        /// <summary>
        /// Sets the contents of the cell based upon the inputs provided
        /// </summary>
        /// <param name="name">The name of the cell to set</param>
        /// <param name="content">What to set the contents of the cell as, can be a string, a double
        /// or a formula</param>
        /// <returns>A list of all the cells dependent on name</returns>
        public override ISet<String> SetContentsOfCell(String name, String content)
        {
            HashSet<string> cellsToRelc = new HashSet<string>();
            /// We are adding something to the spreadsheet so we need to know to save it
            this.Changed = true;
            /// Check if cell name is valid
            isValidAndNotNull(name);
            double x = 0;
            /// If we can parse content to a double we can set the cell as (string, double)
            if (double.TryParse(content, out x))
            {
                cellsToRelc = new HashSet<string>(SetCellContents(name, x));
            }
            /// If the leading character of content is =, we can set the cell as (string, Formula)
            if (Regex.IsMatch(content, "[=]"))
            {
                try
                {
                    Formula formula = new Formula(content.Replace(" ", string.Empty), Normalize, isValidForm);
                    cellsToRelc = new HashSet<string>(SetCellContents(name, formula));
                }
                catch (FormulaFormatException)
                {
                    new FormulaError();
                }
            }
            /// Else if both of those statements are false then we must set the cell as (string, string)
            if (!double.TryParse(content, out x) && !Regex.IsMatch(content, "[=]"))
            {
                if (content == "")
                {
                    cellsToRelc = new HashSet<string>(GetCellsToRecalculate(name));
                    if (cells.ContainsKey(name))
                    {
                        if (cells[name].Contents is string)
                        {
                            cells.Remove(name);
                        }
                        if (cells[name].Contents is Formula)
                        {
                            foreach (var item in cells[name].Variables.ToList())
                            {
                                cellDependancy.RemoveDependency(name, item);
                            }
                            cells.Remove(name);
                        }
                        if (cells[name].Contents is double)
                        {
                            cells.Remove(name);
                            recalculateCell(GetCellsToRecalculate(name));
                        }                      
                    }
                }
                else
                {
                    cellsToRelc = new HashSet<string>(SetCellContents(name, content));
                }
            }
            return cellsToRelc;
        }
        /// <summary>
        /// Gets a list of all the names of the non empty cells
        /// </summary>
        /// <returns>List of all non-empty cells</returns>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            List<string> List1 = new List<string>();
            List1.AddRange(cells.Keys.ToList<string>());
            return List1;
        }
        /// <summary>
        /// Gets the contents of a cell
        /// </summary>
        /// <param name="name">Cell to return contents of</param>
        /// <returns>Contents of name, can be double, string or formula</returns>
        public override object GetCellContents(String name)
        {
            /// If the dictionary does not contain name cell we return ""
            if (!cells.ContainsKey(Normalize(name)))
            {
                return "";
            }
            /// Return cell contents, not cell value
            else
            {
                if (cells[Normalize(name)].Contents is Formula)
                {
                    return "=" + (cells[Normalize(name)].Contents);
                }
                else
                {
                    return cells[Normalize(name)].Contents;
                }
            }
        }
        /// <summary>
        /// Sets up a new cell with name and contents
        /// </summary>
        /// <param name="name">The name of the cell</param>
        /// <param name="number">The double which will be both the contents and value of the cell</param>
        /// <returns>List of all cells dependent on the given cell</returns>
        protected override ISet<String> SetCellContents(String name, double number)
        {
            /// If the dictionary contains the given cell name we clear the contents and add the cell again with
            /// the new input
            if (cells.Keys.Contains(name))
            {
                cells.Remove(name);
                cells.Add(name, new Cell.Cell(number));
            }
            /// If the dictionary does not contain the given cell name, we add it and the corresponding values 
            /// to the dictionary
            else
            {
                cells.Add(name, new Cell.Cell(number));
            }
            /// Call the helper function that will recalculate any cells that need to have their value updated
            /// since the new cell is a double and could affect other cells
            recalculateCell(GetCellsToRecalculate(name));
            return new HashSet<string>(GetCellsToRecalculate(name));
        }
        /// <summary>
        /// Creates a cell with a name and a string as the content
        /// </summary>
        /// <param name="name">Name of the given cell</param>
        /// <param name="text">String value of the given cell</param>
        /// <returns></returns>
        protected override ISet<String> SetCellContents(String name, String text)
        {
            /// Clear the list, it is a 'global' variabel so we need to clear it with every use
            /// Checks if text input is null or an empty string
            if (text == null || text == "")
            {
                throw new ArgumentNullException("You have not entered any text");
            }
            /// If the dictionary contains the given cell name we clear the contents and add the cell again with
            /// the new input
            if (cells.Keys.Contains(name))
            {
                cells.Remove(name);
                cells.Add(name, new Cell.Cell(text));
            }
            /// If the dictionary does not contain the given cell name, we add it and the corresponding values 
            /// to the dictionary
            else
            {
                cells.Add(name, new Cell.Cell(text));
            }
            /// We fill the list with all cells that are dependees of the given cell name, although this list 
            /// should be empty
            return new HashSet<string>(GetCellsToRecalculate(name));
        }
        /// <summary>
        /// Sets the contents of the given cell to be a Formula
        /// </summary>
        /// <param name="name">Name of new cell</param>
        /// <param name="formula">Formula to be stored as the cells.Contents</param>
        /// <returns>Returns list of all cells dependent on the new cell</returns>
        protected override ISet<String> SetCellContents(String name, Formula formula)
        {
            /// Used to store the cells current content
            object previousContents;
            HashSet<string> returnSet;

            // declare a set to contain cells to recalculate

            // grab current contents
            // set the contents of the given cell to the parameter formula

            // try{
            // see if a circ exception was thrown
            // populate set of cells to recalculate
            // }
            // catch(CircularException){
            // undo what you did by restoring previous contents
            // continue to throw circ exception
            // }

            // return the set of cells to recalculate
            /// Check if cell name is valid
            isValidForm(name);
            /// Store the current cells content in previousObject object
            if (cells.ContainsKey(name))
            {
                previousContents = cells[name].Contents;
            }
            /// Else stores an empty string
            else
            {
                previousContents = "";
            }
            /// Try block to try and set cell contents to a formula
            try
            {
                /// If the cell already exists we remove the dependancies and the cell
                if (cells.Keys.Contains(name))
                {
                    /// Checks if the formula is null
                    if (!object.ReferenceEquals(formula, null))
                    {
                        /// If the previous content is a double we do not need to remove dependancies
                        if (previousContents is double)
                        {

                        }
                        if (previousContents is string)
                        {

                        }
                        else
                        {
                            foreach (var item in cells[name].Variables.ToList())
                            {
                                cellDependancy.RemoveDependency(name, item);
                            }
                        }
                        cells.Remove(name);
                        /// Add in the new cell contents and add new dependancies
                        cells.Add(name, new Cell.Cell(formula, Lookup));
                        foreach (var item in cells[name].Variables.ToList())
                        {
                            cellDependancy.AddDependency(name, item);
                        }
                    }
                    /// If null throw ArgumentNullException
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                /// If cell does not exist we just add the new contents
                else
                {
                    if (!object.ReferenceEquals(formula, null))
                    {
                        cells.Add(name, new Cell.Cell(formula, Lookup));
                        foreach (var item in cells[name].Variables.ToList())
                        {
                            cellDependancy.AddDependency(name, item);
                        }

                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                returnSet = new HashSet<string>(GetCellsToRecalculate(name));
            }
            catch (CircularException)
            {
                foreach (var item in cells[name].Variables.ToList())
                {
                    cellDependancy.RemoveDependency(name, item);
                }
                cells.Remove(name);
                if (previousContents is string)
                {
                    cells.Add(name, new Cell.Cell((string)previousContents));
                }
                if (previousContents is double)
                {
                    cells.Add(name, new Cell.Cell((double)previousContents));
                }
                if (previousContents is Formula)
                {
                    cells.Add(name, new Cell.Cell((Formula)previousContents, Lookup));
                }
                throw new CircularException();
            }
            //catch (ArgumentNullException)
            //{
            //    throw new ArgumentNullException();
            //}
            recalculateCell(GetCellsToRecalculate(name));
            return returnSet;
        }
        /// <summary>
        /// Returns the Direct Dependees of the given cell name
        /// </summary>
        /// <param name="name">Name to get dependees of</param>
        /// <returns>Dependees of the given cell</returns>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            return cellDependancy.GetDependees(name);
        }
        /// <summary>
        /// Takes in a list of names to check
        /// </summary>
        /// <param name="names">List of cells that need to be recalculated</param>
        /// <returns></returns>
        protected IEnumerable<String> GetCellsToRecalculate(ISet<String> names)
        {
            LinkedList<String> changed = new LinkedList<String>();
            HashSet<String> visited = new HashSet<String>();
            /// For each item in the names list, that is the list of ALL dependancies, direct and indirect
            foreach (String name in names)
            {
                /// If the current dependant is not in the list of cells visited we call the Visit function
                if (!visited.Contains(name))
                {
                    Visit(name, name, visited, changed);
                }
            }
            return changed;
        }
        /// <summary>
        /// Gets cells to recalculate with just a single cell as the starting point,
        /// this can be used as the above version because by starting at cell 'name' you can get all the indirect
        /// dependents through a getDependee call making it super easy to move down the list
        /// </summary>
        /// <param name="name">Cell to start from</param>
        /// <returns></returns>
        protected IEnumerable<String> GetCellsToRecalculate(String name)
        {
            return GetCellsToRecalculate(new HashSet<String>() { name });
        }
        /// <summary>
        /// Creates a list of visited cells, or cells that have been 'changed'
        /// </summary>
        /// <param name="start">First cell to look at</param>
        /// <param name="name">First cell to look at, called something else</param>
        /// <param name="visited">List of visited cells</param>
        /// <param name="changed">List of changed cells</param>
        private void Visit(String start, String name, ISet<String> visited, LinkedList<String> changed)
        {
            /// Add current cell to list of 'cells' visited
            visited.Add(name);
            /// For each 'cell' that is a dependant of the current cell
            foreach (String n in GetDirectDependents(name))
            {
                /// if the current 'cell name' is at the front of the list that means the cell is dependant upon
                /// itself and we have a circular dependancy
                if (n.Equals(start))
                {
                    throw new CircularException();
                }
                /// if we have not visited the current dependant we call Visit again
                else if (!visited.Contains(n))
                {
                    Visit(start, n, visited, changed);
                }
            }
            /// we than add to the list changed the current 'cell' because we have 'changed' it (not really but we
            /// have built a mechanism for changing it
            changed.AddFirst(name);
        }
        /* *********************************************************************************** */
        /* Private Helper Methods */
        /// <summary>
        /// Checks if the cell name is valid
        /// </summary>
        /// <param name="t">Variable to check</param>
        /// <returns>Returns true if variable is of valid form, returns false if otherwise</returns>
        private bool isValidForm(string t)
        {
            bool validVariable = false;
            double n;
            if (IsValid(t) && !double.TryParse(t, out n))
            {
                Match valid = Regex.Match(t, @"^[A-Za-z]+[A-Za-z0-9_]+$");
                if (valid.Success)
                {
                    validVariable = true;
                }
                else
                {
                    validVariable = false;
                }
                return validVariable;
            }
            else if (double.TryParse(t, out n))
            {
                validVariable = true;
            }
            return validVariable;
        }
        /// <summary>
        /// Checks if current cell name is valid or null
        /// </summary>
        /// <param name="name">Current Cell to check</param>
        private void isValidAndNotNull(string name)
        {
            if (name == null || !isValidForm(name))
            {
                throw new InvalidNameException();
            }
        }

        /* *********************************************************************************** */
        /// <summary>
        /// The lookup function that will be passed to the Formula.Evaluate function,
        /// tries to convert the value of a cell to a double, if so we pass the double to the evaluator function
        /// if not we throw a FormulaFormatException
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        private double Lookup(string cellName)
        {
            double x;
            try
            {
                x = Convert.ToDouble(cells[Normalize(cellName)].Value);
            }
            catch
            {
                throw new FormulaFormatException("The variable does not have a valid value associated with it");
            }
            return x;
        }
        /* *********************************************************************************** */
        /// <summary>
        /// Writes the cells from the loaded saved xml files into the newly created spreadshet
        /// </summary>
        /// <param name="pathToFile">The path to the saved file to be loaded</param>
        private void writeCells(string pathToFile)
        {
            /// Load the saved xml file into a XDocument
            var xDoc = XDocument.Load(pathToFile);
            /// Create a list of all the nodes that start with the element "cell"
            try
            {
                var nodes = xDoc.Descendants("cell").ToList();
                /// Loop through each item in the list and
                foreach (var item in nodes)
                {
                    /// Retrive the value of the "name" element
                    try
                    {
                        string name = item.Element("name").Value;
                        /// Make sure that the cell names in the saved spreadsheet are valid for our purposes
                        if (!isValidForm(name))
                        {
                            throw new SpreadsheetReadWriteException("You have saved invalid variables");
                        }
                        /// Retrive the value of the "contents" element
                        string content = item.Element("contents").Value;
                        /// Set a cell with the given name and contents, but first we need to check that the
                        /// saved contents and name are valid, whos to say that I saved this
                        try
                        {
                            /// If content is a formula, we check by looking for a leading =
                            if (Regex.IsMatch(content, "[=]"))
                            {
                                Formula formula = new Formula(content.Replace(" ", string.Empty), Normalize, isValidForm);
                            }
                            /// If content is null or an empty string we throw a new ArgumentNullException
                            if (content == null || content == "")
                            {
                                throw new ArgumentNullException("The saved version has invalid contents of a cell");
                            }

                        }
                        catch (CircularException)
                        {
                            throw new SpreadsheetReadWriteException("A saved formula has a circular relationship exception");
                        }
                        SetContentsOfCell(name, content);
                    }
                    catch (NullReferenceException)
                    {
                        throw new SpreadsheetReadWriteException("Something is wrong wit the XML format");
                    }
                }
            }
            catch (NullReferenceException)
            {
                throw new SpreadsheetReadWriteException("Soemthing is wrong with the XML format");
            }
        }
        /// <summary>
        /// A private method to recalculate all the cells that are dependant on a cell that was just
        /// created
        /// </summary>
        /// <param name="cellsToRec">List of all the cells that need to be recalculated</param>
        private void recalculateCell(IEnumerable<string> cellsToRec)
        {
            /// Loop through the list and
            foreach (var item in cellsToRec)
            {
                /// If the cell.Contents is a formula we remove the old formula, we need to 
                /// remove it since the value is an Error, and add the formula again and pass
                /// in the new Lookup function which will have the value of the new cell
                try
                {
                    if (cells[item].Contents is Formula)
                    {
                        /// Create a new formula that does not have any of the old formulas errors or values
                        /// but still has the same string as input and delegate functions
                        Formula formula1 = new Formula(cells[item].Contents.ToString(), Normalize, isValidForm);
                        /// Remove the old cell
                        cells.Remove(item);
                        /// Add the new cell
                        cells.Add(item, new Cell.Cell(formula1, Lookup));
                    }
                }
                catch (KeyNotFoundException) { }
            }
        }

    }
}
