namespace ScanDB
{
    partial class DBScanWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.connStringTextBox = new System.Windows.Forms.TextBox();
            this.zoekConnStringButton = new System.Windows.Forms.Button();
            this.TablesDataGrid = new System.Windows.Forms.DataGridView();
            this.AttributesDataGrid = new System.Windows.Forms.DataGridView();
            this.AttributeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TablesDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttributesDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connectiestring";
            // 
            // connStringTextBox
            // 
            this.connStringTextBox.Location = new System.Drawing.Point(98, 13);
            this.connStringTextBox.Name = "connStringTextBox";
            this.connStringTextBox.Size = new System.Drawing.Size(553, 20);
            this.connStringTextBox.TabIndex = 1;
            this.connStringTextBox.Text = "Data Source=TSTEVADC01;Initial Catalog=EVA;User Id=tomw;Password=tomw;";
            // 
            // zoekConnStringButton
            // 
            this.zoekConnStringButton.Location = new System.Drawing.Point(15, 49);
            this.zoekConnStringButton.Name = "zoekConnStringButton";
            this.zoekConnStringButton.Size = new System.Drawing.Size(75, 23);
            this.zoekConnStringButton.TabIndex = 2;
            this.zoekConnStringButton.Text = "Zoek";
            this.zoekConnStringButton.UseVisualStyleBackColor = true;
            this.zoekConnStringButton.Click += new System.EventHandler(this.zoekConnStringButton_Click);
            // 
            // TablesDataGrid
            // 
            this.TablesDataGrid.AllowUserToAddRows = false;
            this.TablesDataGrid.AllowUserToDeleteRows = false;
            this.TablesDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablesDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablesDataGrid.Location = new System.Drawing.Point(15, 79);
            this.TablesDataGrid.MultiSelect = false;
            this.TablesDataGrid.Name = "TablesDataGrid";
            this.TablesDataGrid.ReadOnly = true;
            this.TablesDataGrid.RowHeadersVisible = false;
            this.TablesDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.TablesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TablesDataGrid.Size = new System.Drawing.Size(301, 579);
            this.TablesDataGrid.TabIndex = 3;
            this.TablesDataGrid.Visible = false;
            this.TablesDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TablesDataGrid_CellDoubleClick);
            // 
            // AttributesDataGrid
            // 
            this.AttributesDataGrid.AllowUserToAddRows = false;
            this.AttributesDataGrid.AllowUserToDeleteRows = false;
            this.AttributesDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.AttributesDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.AttributesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AttributesDataGrid.Location = new System.Drawing.Point(322, 79);
            this.AttributesDataGrid.MultiSelect = false;
            this.AttributesDataGrid.Name = "AttributesDataGrid";
            this.AttributesDataGrid.ReadOnly = true;
            this.AttributesDataGrid.RowHeadersVisible = false;
            this.AttributesDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.AttributesDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.AttributesDataGrid.Size = new System.Drawing.Size(362, 579);
            this.AttributesDataGrid.TabIndex = 4;
            this.AttributesDataGrid.Visible = false;
            this.AttributesDataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AttributesDataGrid_CellDoubleClick);
            // 
            // AttributeLabel
            // 
            this.AttributeLabel.AutoSize = true;
            this.AttributeLabel.Location = new System.Drawing.Point(319, 49);
            this.AttributeLabel.Name = "AttributeLabel";
            this.AttributeLabel.Size = new System.Drawing.Size(234, 26);
            this.AttributeLabel.TabIndex = 5;
            this.AttributeLabel.Text = "Dubbelklik om de tabel te selecteren.\r\nHet geselecteerde attribuut bepaalt de sor" +
    "tering.";
            this.AttributeLabel.Visible = false;
            // 
            // DBScanWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 670);
            this.Controls.Add(this.AttributeLabel);
            this.Controls.Add(this.AttributesDataGrid);
            this.Controls.Add(this.TablesDataGrid);
            this.Controls.Add(this.zoekConnStringButton);
            this.Controls.Add(this.connStringTextBox);
            this.Controls.Add(this.label1);
            this.Name = "DBScanWindow";
            this.Text = "DBScanWindow";
            ((System.ComponentModel.ISupportInitialize)(this.TablesDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttributesDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox connStringTextBox;
        private System.Windows.Forms.Button zoekConnStringButton;
        private System.Windows.Forms.DataGridView TablesDataGrid;
        private System.Windows.Forms.DataGridView AttributesDataGrid;
        private System.Windows.Forms.Label AttributeLabel;
    }
}