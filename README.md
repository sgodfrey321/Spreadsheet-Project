# Spreadsheet-Project
A 7 week project involving the creation of multiple libraries and tying them all together with a Windows Form Application.

This project was spread over 7 weeks, with new functionality being added every week. We started with a very basic formula evaluator
that took a formula and evaluated the equation using Regex and then processed the fomula for the correct answer. We then created
a formula syntax evaluator using Regex to check if the formula had correct syntax. Next we created a cell structure for the 
spreadsheet: I created cells that can hold a formula, double or a string and then created a mechanism for checking that cells had
correct dependancies (that is no cell created a circular dependancy). I then created the main spreadsheet class that allowed for
editing and updating cells. The last part was creating a simple Windows Form Application to display the spreadsheet.
