/// Sam Godfrey
/// U0467570

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetUtilities;

namespace Cell
{
    public class Cell
    {
        /// <summary>
        /// The contents of a cell, ie what is in the cell
        /// </summary>
        private object _contents;
        /// <summary>
        /// 
        /// </summary>
        public object Contents
        {
            get { return this._contents; }
            private set { this._contents = value; }
        }
        /// <summary>
        /// The value of a cell, ie what is the value of the contents of a cell
        /// </summary>
        /// 
        private object _value;
        public object Value
        {
            get { return this._value; }
            private set { this._value = value; }
        }
        /// <summary>
        /// The variables that a cell can have, ie the variables of the function of the cell contents
        /// </summary>
        private IEnumerable<string> _variables;
        public IEnumerable<string> Variables
        {
            get { return this._variables; }
            private set { this._variables = value; }
        }
        /// <summary>
        /// Creates a cell with a string as the contents and value
        /// </summary>
        /// <param name="t">The string that will become the contents and the value of the cell</param>
        public Cell(string t)
        {
            Contents = t;
            Value = t;

        }
        /// <summary>
        /// Creates a cell with a double as the contents and values
        /// </summary>
        /// <param name="t"></param>
        public Cell(double t)
        {
            Contents = t;
            Value = t;
        }
        /// <summary>
        /// Creates a cell with a formula as the contents and the value is the evaluated formula
        /// </summary>
        /// <param name="t">Formula to be stored, including delegate functions</param>
        /// <param name="LookUp">Lookup delegate function for the evaluate portion of the formula</param>
        public Cell(Formula t, Func<string, double> LookUp)
        {
            /// Store all the variables in the formula as a list for easy access
            Variables = t.GetVariables().ToList();
            /// The contents of the cell are just the original formula
            Contents = t;
            /// Placeholder object for what ever comes out of the formula evaluator
            object evaluate;
            /// We try and evaluate the formula with the lookup function.
            /// There are a couple things that can happen
            /// 1- Either the evalute is successful and we get a double or
            try
            {
                evaluate = t.Evaluate(LookUp);
            }
            /// 2- We catch a formula format exception, thus we catch it and store it in 
            /// the place holder value or
            catch (FormulaFormatException e)
            {
                evaluate = e;
            }
            /// 3- We catch a format exception and store it in the place holder value
            catch (FormatException e)
            {
                evaluate = e;
            }
            /// The value of the cell will then become what ever comes out of the formula evaluator
            /// wither it be an exception or double, it is stilled stored
            Value = evaluate;
        }
    }
}
