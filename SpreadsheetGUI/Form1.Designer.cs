namespace SpreadsheetGUI
{
    partial class SpreadsheetName
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spreadsheetPanel1 = new SS.SpreadsheetPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CellName = new System.Windows.Forms.Label();
            this.SavePanel = new System.Windows.Forms.Panel();
            this.CurrentCell = new System.Windows.Forms.Label();
            this.SaveNameBox = new System.Windows.Forms.TextBox();
            this.SaveNamePrompt = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CurrentContents = new System.Windows.Forms.Label();
            this.CurrentValue = new System.Windows.Forms.Label();
            this.CellValue = new System.Windows.Forms.Label();
            this.CellContents = new System.Windows.Forms.TextBox();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 24);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(1184, 668);
            this.spreadsheetPanel1.TabIndex = 0;
            this.spreadsheetPanel1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.spreadsheetPanel1_KeyDown);
            this.spreadsheetPanel1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.spreadsheetPanel1_KeyPress);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolMenuItem
            // 
            this.fileToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolMenuItem.Name = "fileToolMenuItem";
            this.fileToolMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolMenuItem.Text = "File";
            // 
            // createNewToolStripMenuItem
            // 
            this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            this.createNewToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.createNewToolStripMenuItem.Text = "CreateNew";
            this.createNewToolStripMenuItem.Click += new System.EventHandler(this.createNewToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // CellName
            // 
            this.CellName.AutoSize = true;
            this.CellName.Location = new System.Drawing.Point(201, 3);
            this.CellName.Name = "CellName";
            this.CellName.Size = new System.Drawing.Size(55, 13);
            this.CellName.TabIndex = 2;
            this.CellName.Text = "Cell Name";
            // 
            // SavePanel
            // 
            this.SavePanel.Location = new System.Drawing.Point(0, 0);
            this.SavePanel.Margin = new System.Windows.Forms.Padding(2);
            this.SavePanel.Name = "SavePanel";
            this.SavePanel.Size = new System.Drawing.Size(55, 24);
            this.SavePanel.TabIndex = 3;
            // 
            // CurrentCell
            // 
            this.CurrentCell.AutoSize = true;
            this.CurrentCell.Location = new System.Drawing.Point(138, 3);
            this.CurrentCell.Name = "CurrentCell";
            this.CurrentCell.Size = new System.Drawing.Size(64, 13);
            this.CurrentCell.TabIndex = 3;
            this.CurrentCell.Text = "Current Cell:";
            // 
            // SaveNameBox
            // 
            this.SaveNameBox.Location = new System.Drawing.Point(0, 0);
            this.SaveNameBox.Name = "SaveNameBox";
            this.SaveNameBox.Size = new System.Drawing.Size(100, 20);
            this.SaveNameBox.TabIndex = 0;
            // 
            // SaveNamePrompt
            // 
            this.SaveNamePrompt.Location = new System.Drawing.Point(0, 0);
            this.SaveNamePrompt.Name = "SaveNamePrompt";
            this.SaveNamePrompt.Size = new System.Drawing.Size(100, 23);
            this.SaveNamePrompt.TabIndex = 0;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(0, 0);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(0, 0);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            // 
            // CurrentContents
            // 
            this.CurrentContents.AutoSize = true;
            this.CurrentContents.Location = new System.Drawing.Point(262, 3);
            this.CurrentContents.Name = "CurrentContents";
            this.CurrentContents.Size = new System.Drawing.Size(89, 13);
            this.CurrentContents.TabIndex = 5;
            this.CurrentContents.Text = "Current Contents:";
            // 
            // CurrentValue
            // 
            this.CurrentValue.AutoSize = true;
            this.CurrentValue.Location = new System.Drawing.Point(448, 3);
            this.CurrentValue.Name = "CurrentValue";
            this.CurrentValue.Size = new System.Drawing.Size(74, 13);
            this.CurrentValue.TabIndex = 6;
            this.CurrentValue.Text = "Current Value:";
            // 
            // CellValue
            // 
            this.CellValue.AutoSize = true;
            this.CellValue.Location = new System.Drawing.Point(543, 3);
            this.CellValue.Name = "CellValue";
            this.CellValue.Size = new System.Drawing.Size(54, 13);
            this.CellValue.TabIndex = 7;
            this.CellValue.Text = "Cell Value";
            // 
            // CellContents
            // 
            this.CellContents.Location = new System.Drawing.Point(343, 0);
            this.CellContents.Name = "CellContents";
            this.CellContents.Size = new System.Drawing.Size(100, 20);
            this.CellContents.TabIndex = 8;
            this.CellContents.TextChanged += new System.EventHandler(this.CellContents_TextChanged);
            this.CellContents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CellContents_KeyDown);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // SpreadsheetName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 692);
            this.Controls.Add(this.CellContents);
            this.Controls.Add(this.CurrentCell);
            this.Controls.Add(this.CellName);
            this.Controls.Add(this.CellValue);
            this.Controls.Add(this.CurrentValue);
            this.Controls.Add(this.CurrentContents);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.SavePanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SpreadsheetName";
            this.Text = "Spreadsheet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpreadsheetName_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel spreadsheetPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.Label CellName;
        private System.Windows.Forms.Panel SavePanel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox SaveNameBox;
        private System.Windows.Forms.Label SaveNamePrompt;
        private System.Windows.Forms.Label CurrentCell;
        private System.Windows.Forms.Label CurrentContents;
        private System.Windows.Forms.Label CurrentValue;
        private System.Windows.Forms.Label CellValue;
        private System.Windows.Forms.TextBox CellContents;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

