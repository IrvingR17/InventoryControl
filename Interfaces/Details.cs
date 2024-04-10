using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InventoryControl.Interfaces
{
    public partial class Details : Form
    {
        private string connection = ConfigurationManager.ConnectionStrings["MSSQLLocalDB"].ConnectionString;

        private string[] rowData;

        public Details(string[] rowData)
        {
            InitializeComponent();
            this.rowData = rowData;
            DisplayData(rowData);
        }

        private void DisplayData(string[] rowData)
        {
            idLabel.Text = rowData[0];
            statusLabel.Text = rowData[1];
            modelLabel.Text = rowData[2];
            userLabel.Text = rowData[3];
            hostnameLabel.Text = rowData[4];
            typeLabel.Text = rowData[5];
            serialNumberLabel.Text = rowData[6];
            dateLabel.Text = rowData[7];
            departmentLabel.Text = rowData[8];
            costLabel.Text = rowData[9];
            areaLabel.Text = rowData[10];
            CECOLabel.Text = rowData[11];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que deseas eliminar este registro?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(rowData[0]);

                string deleteQuery = "DELETE FROM Computers WHERE Id = @Id";

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    SqlCommand sqlCommand = new SqlCommand(deleteQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        sqlConnection.Open();
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registro eliminado correctamente.");
                            this.Close(); 
                            ComputerList computerList = new ComputerList();
                            computerList.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el registro: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComputerList list = new ComputerList();
            this.Close();
            list.Show();
        }
    }
}
