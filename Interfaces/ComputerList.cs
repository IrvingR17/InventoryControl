using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InventoryControl.Interfaces
{
    public partial class ComputerList : Form
    {
        private readonly string connection = ConfigurationManager.ConnectionStrings["MSSQLLocalDB"].ConnectionString;

        public ComputerList()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            LoadComputers();
        }

        public ComputerList(string searchTerm)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            Search(searchTerm);
        }

        private void LoadComputers()
        {
            quantityLabel2.Text = GetRecordsQuantity().ToString();
            string query = "SELECT Id, Usuario, Status, Model, Hostname, Type, SerialNumber, Date, Department, Cost, Area, CECO " +
                           "FROM Computers";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader["Id"], reader["Status"], reader["Model"], reader["Usuario"], reader["Hostname"], reader["Type"],
                                               reader["SerialNumber"], reader["Date"], reader["Department"], reader["Cost"], reader["Area"], reader["CECO"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los equipos: " + ex.Message);
                }
            }
        }

        private void Search(string searchTerm)
        {
            quantityLabel2.Text = GetRecordsQuantity().ToString();
            string query = "SELECT Id, Usuario, Status, Model, Hostname, Type, SerialNumber, Date, Department, Cost, Area, CECO " +
                           "FROM Computers " +
                           "WHERE Id LIKE @searchTerm OR Status LIKE @searchTerm OR Type LIKE @searchTerm OR Department LIKE @searchTerm OR Area " +
                                    "LIKE @searchTerm OR Model LIKE @searchTerm OR SerialNumber LIKE @searchTerm OR Usuario LIKE @searchTerm OR CECO " +
                                    "LIKE @searchTerm OR Hostname LIKE @searchTerm OR Date LIKE @searchTerm OR Cost LIKE @searchTerm";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                try
                {
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    dataGridView1.Rows.Clear(); 

                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader["Id"], reader["Usuario"], reader["Status"], reader["Model"], reader["Hostname"], reader["Type"],
                                               reader["SerialNumber"], reader["Date"], reader["Department"], reader["Cost"], reader["Area"], reader["CECO"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar los equipos: " + ex.Message);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                button3.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Index index = new Index();
            this.Close();
            index.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string[] rowData = new string[selectedRow.Cells.Count];
            for (int i = 0; i < selectedRow.Cells.Count; i++)
            {
                rowData[i] = selectedRow.Cells[i].Value.ToString();
            }

            Edit edit = new Edit(rowData);
            this.Close();
            edit.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    if (selectedRow.Cells.Count > 0)
                    {
                        string[] rowData = new string[selectedRow.Cells.Count];
                        for (int i = 0; i < selectedRow.Cells.Count; i++)
                        {
                            rowData[i] = selectedRow.Cells[i].Value.ToString();
                        }

                        Details details = new Details(rowData);
                        this.Close();
                        details.Show();
                    }
                    else
                    {
                        MessageBox.Show("No hay celdas seleccionadas en la fila.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No hay filas seleccionadas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Error al obtener los datos de la fila seleccionada: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetRecordsQuantity()
        {
            int quantity = 0;

            string query = "SELECT COUNT(*) FROM Computers";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);

                try
                {
                    sqlConnection.Open();
                    quantity = (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener el total de equipos: " + ex.Message);
                }
            }

            return quantity;
        }

    }
}
