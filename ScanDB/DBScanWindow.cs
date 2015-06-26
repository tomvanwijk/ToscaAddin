using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScanDB
{
    public partial class DBScanWindow : Form
    {

        private const string TABLEQUERY = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES order by TABLE_NAME";
        private const string ATTRIBQUERYBEGIN = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '";
        private const string ATTRIBQUERYEND =  "' ORDER BY COLUMN_NAME";
        private string CellValue = string.Empty;

        public XModuleEntity XModule { get; set; }

        public DBScanWindow()
        {
            InitializeComponent();
        }

        private void zoekConnStringButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(connStringTextBox.Text))
                return;
            if (!IsValidSqlConnectionString(connStringTextBox.Text))
                return;
            DataTable dt = getAllTables(connStringTextBox.Text, TABLEQUERY);

            BindingSource SBind = new BindingSource();
            SBind.DataSource = dt;
            TablesDataGrid.AutoGenerateColumns = true;
            TablesDataGrid.DataSource = SBind;
            TablesDataGrid.Refresh();
            TablesDataGrid.Visible = true;
            

        }

        private DataTable getAllTables(string connString, string query)
        {
            DataTable dt = null;
            dt = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                using (SqlDataAdapter adpt = new SqlDataAdapter(query, sqlConn))
                {
                    adpt.SelectCommand.CommandTimeout = sqlConn.ConnectionTimeout; ;
                    adpt.Fill(dt);
                }
            }
            return dt;
        }

        private Boolean IsValidSqlConnectionString( string connString)
        {
            try
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch { return false; }
        }

        private void TablesDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView s = sender as DataGridView;
            CellValue = s.CurrentCell.FormattedValue.ToString() ;

            DataTable dt = getAllTables(connStringTextBox.Text, ATTRIBQUERYBEGIN + CellValue + ATTRIBQUERYEND);

            BindingSource SBind = new BindingSource();
            SBind.DataSource = dt;
            AttributesDataGrid.AutoGenerateColumns = true;
            AttributesDataGrid.DataSource = SBind;
            AttributesDataGrid.Refresh();
            AttributesDataGrid.Visible = true;
            AttributeLabel.Visible = true;
        }

        private void AttributesDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (String.IsNullOrEmpty(CellValue))
                return;
            DataTable dt = getAllTables(connStringTextBox.Text, ATTRIBQUERYBEGIN + CellValue + ATTRIBQUERYEND);
            DataGridView s = sender as DataGridView;


            XModule = new XModuleEntity();
            XModule.Name = CellValue;
            XModule.ConnectionString = connStringTextBox.Text;
            XModule.Attributes = dt.AsEnumerable().Select(x => x[0].ToString()).ToList();
            XModule.OrderAttribute = s.CurrentCell.FormattedValue.ToString();
            this.Close();
        }

    }
}
