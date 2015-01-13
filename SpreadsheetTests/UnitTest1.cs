using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SpreadsheetUtilities;
using SS;

namespace SpreadsheetTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateNewSpreadsheet()
        {
            AbstractSpreadsheet test = new Spreadsheet();
        }
        /// <summary>
        /// Checks that the return value from an undefined cell is ""
        /// </summary>
        [TestMethod]
        public void ReturnEmptyCellValue()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            Assert.AreEqual(test.GetCellContents("A1"), "");         
        }
        /// <summary>
        /// Returns a cell contetns with a string
        /// </summary>
        [TestMethod]
        public void ReturnCellValueString()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("A1", "hello");
            Assert.AreEqual(test.GetCellContents("A1"), "hello");
        }
        /// <summary>
        /// Returns a cell contents with a double
        /// </summary>
        [TestMethod]
        public void ReturnCellValueDouble()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("A1", 5);
            Assert.AreEqual(test.GetCellContents("A1"), (double)5);
        }
        /// <summary>
        /// Checks that replace string works
        /// </summary>
        [TestMethod]
        public void ReplaceCellValueString()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("A1", "hello");
            Assert.AreEqual(test.GetCellContents("A1"), "hello");
            test.SetCellContents("A1", "world");
            Assert.AreEqual(test.GetCellContents("A1"), "world");
        }
        /// <summary>
        /// Checks that replace double works
        /// </summary>
        [TestMethod]
        public void ReplaceCellValueDouble()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("A1", 5);
            Assert.AreEqual(test.GetCellContents("A1"), (double)5);
            test.SetCellContents("A1", 6);
            Assert.AreEqual(test.GetCellContents("A1"), (double)6);
        }
        /// <summary>
        /// Checks that replace formula works
        /// </summary>
        [TestMethod]
        public void ReplaceCellValueFormula()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("A1", new Formula("2+2"));
            test.SetCellContents("B1", new Formula("2+2"));
            Assert.AreEqual(test.GetCellContents("A1"), new Formula("2+2"));
            test.SetCellContents("A1", new Formula("B1+B1"));
            Assert.AreEqual(test.GetCellContents("A1"), new Formula("B1+B1"));
        }
        /// <summary>
        /// Replaces a formula with one that would create a circular exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void ReplaceCellValueFormulaWithCircularRelationship()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("A1", new Formula("2+2"));
            test.SetCellContents("B1", new Formula("A1+2"));
            Assert.AreEqual(test.GetCellContents("A1"), new Formula("2+2"));
            test.SetCellContents("A1", new Formula("B1+B1"));
            Assert.AreEqual(test.GetCellContents("A1"), new Formula("B1+B1"));
        }
        /// <summary>
        /// Returns a list of all nonempty cells
        /// </summary>
        [TestMethod]
        public void GetAllNoneEmptyCells()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            List<string> nonEmpty1 = new List<string>();
            test.SetCellContents("A1", 5);
            test.SetCellContents("C1", 5);
            test.SetCellContents("B1", 5);
            nonEmpty1.AddRange(test.GetNamesOfAllNonemptyCells());
            List<string> nonEmpty = new List<string>() { "A1", "C1", "B1" };
            CollectionAssert.AreEqual(nonEmpty, nonEmpty1);
        }
        /// <summary>
        /// Checks a circular relationship formula
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void AddCircularException()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("A1", new Formula("A1+3"));
        }
        /// <summary>
        /// Checks that 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void AddInvalidName()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("12x", 5);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddInvalidName1()
        {
            AbstractSpreadsheet test = new Spreadsheet();
            test.SetCellContents("X1", "");
        }
    }
}
