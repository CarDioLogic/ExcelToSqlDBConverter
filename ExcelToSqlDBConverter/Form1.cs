using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace ExcelToSqlDBConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OnClickViewExcelFile(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(excelFilePath.Text))
            {
                MessageBox.Show("The input field for the path of the excel file must not be empty!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string excelPath = excelFilePath.Text;
                (List<string> headers, List<List<string>> dataRows) = ReadExcel(excelPath);
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                if (headers == null)
                {

                    MessageBox.Show("Error reading from Excel file! File empty or not found!",
                      "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    foreach (string header in headers)
                    {
                        DataGridViewColumn newColumn = new DataGridViewTextBoxColumn
                        {
                            Name = header,
                            HeaderText = header,
                            ReadOnly = true
                        };

                        dataGridView1.Columns.Add(newColumn);
                    }

                    foreach (List<string> row in dataRows)
                    {
                        dataGridView1.Rows.Add(row.ToArray());
                    }
                }
            }
        }

        private void OnClickConvertToDb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(excelFilePath.Text))
            {
                MessageBox.Show("The input field for the path of the excel file must not be empty!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(inputDatabaseName.Text) || string.IsNullOrEmpty(inputTableName.Text))
            {
                MessageBox.Show("Database name and table name both can't be empty",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                CreateDatabase();
                PopulateDatabase();
            }
        }

        private (List<string> headers, List<List<string>> dataRows) ReadExcel(string excelPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            try
            {
                using (var package = new ExcelPackage(new FileInfo(excelPath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount;

                    if (worksheet.Dimension != null)
                    {
                        rowCount = worksheet.Dimension.Rows;

                        int colCount = worksheet.Dimension.Columns;

                        List<string> headers = new List<string>();

                        for (int col = 1; col <= colCount; col++)
                        {
                            headers.Add(worksheet.Cells[1, col].Text);
                        }

                        List<List<string>> dataRows = new List<List<string>>();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            List<string> rowData = new List<string>();
                            for (int col = 1; col <= colCount; col++)
                            {
                                rowData.Add(worksheet.Cells[row, col].Text);
                            }
                            dataRows.Add(rowData);
                        }

                        return (headers, dataRows);
                    }
                    else
                    {
                        MessageBox.Show("Error reading from Excel file! File empty or not found!",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return (null, null);
                    }
                }
            }
            catch
            {
                //Create a warning message here!!
                return (null, null);
            }
        }

        private void OnClickClearAll(object sender, EventArgs e)
        {
            excelFilePath.Text = "";
            inputDatabaseName.Text = "";
            inputTableName.Text = "";
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
        }

        public void CreateDatabase()
        {
            string databaseName = inputDatabaseName.Text;
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{databaseName}') CREATE DATABASE {databaseName}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void PopulateDatabase()
        {
            bool tableExistsInDb = TableExistsInDatabase();

            if (tableExistsInDb == false)
            {
                string databaseName = inputDatabaseName.Text;
                string tableName = inputTableName.Text;
                string connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database='{databaseName}';Trusted_Connection=True";
                string createTableQuery = $"CREATE TABLE {tableName} (";
                string insertDataQuery = $"INSERT INTO {tableName} (";

                string excelPath = excelFilePath.Text;
                (List<string> headers, List<List<string>> dataRows) = ReadExcel(excelPath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var header in headers)
                    {
                        createTableQuery += $"{header} NVARCHAR(MAX), ";
                        insertDataQuery += $"{header}, ";
                    }

                    createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";
                    insertDataQuery = insertDataQuery.TrimEnd(',', ' ') + ") VALUES (";

                    using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"Table '{tableName}' created successfully.");
                    }

                    foreach (var row in dataRows)
                    {
                        string insertDataQueryFinal = insertDataQuery;

                        foreach (var cell in row)
                        {
                            insertDataQueryFinal += $"'{cell}', ";
                        }

                        insertDataQueryFinal = insertDataQueryFinal.TrimEnd(',', ' ') + ")";

                        using (SqlCommand command = new SqlCommand(insertDataQueryFinal, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("File converted succesfuly.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Table already exists in the current database! Choose a different table name",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool TableExistsInDatabase()
        {
            string databaseName = inputDatabaseName.Text;
            string tableName = inputTableName.Text;
            string connectionString = $"Server=(localdb)\\MSSQLLocalDB;Database='{databaseName}';Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand checkTableCommand = new SqlCommand($"SELECT OBJECT_ID('dbo.{tableName}')", connection))
                {
                    var result = checkTableCommand.ExecuteScalar();

                    if (result == DBNull.Value)
                    {
                        return false; // Create table
                    }
                    else
                    {
                        return true; //Table already exists in Db
                    }
                }
            }
        }
    }
}