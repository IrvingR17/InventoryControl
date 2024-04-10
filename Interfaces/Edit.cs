using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InventoryControl.Interfaces
{
    public partial class Edit : Form
    {
        private string connection = ConfigurationManager.ConnectionStrings["MSSQLLocalDB"].ConnectionString;

        public Edit(String[] rowData)
        {
            InitializeComponent();
            DisplayData(rowData);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ComputerList list = new ComputerList();
            this.Close();
            list.Show();
        }

        private void DisplayData(string[] rowData)
        {
            idLabel.Text = rowData[0];
            statusTextBox.Text = rowData[1];
            modelTextBox.Text = rowData[2];
            userTextBox.Text = rowData[3];
            hostnameTextBox.Text = rowData[4];
            typeTextBox.Text = rowData[5];
            serialNumberTextBox.Text = rowData[6];
            dateTextBox.Text = rowData[7];
            departmentTextBox.Text = rowData[8];
            costTextBox.Text = rowData[9];
            areaTextBox.Text = rowData[10];
            CECOTextBox.Text = rowData[11];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = idLabel.Text.Trim();
            string status = statusTextBox.Text.Trim();
            string type = typeTextBox.Text.Trim();
            string department = departmentTextBox.Text.Trim();
            string area = areaTextBox.Text.Trim();
            string model = modelTextBox.Text.Trim();
            string serialNumber = serialNumberTextBox.Text.Trim();
            string user = userTextBox.Text.Trim();
            string CECO = CECOTextBox.Text.Trim();
            string hostname = hostnameTextBox.Text.Trim();
            string date = dateTextBox.Text.Trim();
            string cost = costTextBox.Text.Trim();

            string updateQuery = "UPDATE Computers " +
                                 "SET Status = @Status, Model = @Model, Hostname = @Hostname, Type = @Type, " +
                                 "SerialNumber = @SerialNumber, Date = @Date, Department = @Department, Usuario = @Usuario, Cost = @Cost, " +
                                 "Area = @Area, CECO = @CECO WHERE Id = @Id";

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                SqlCommand sqlCommand = new SqlCommand(updateQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Status", status);
                sqlCommand.Parameters.AddWithValue("@Type", type);
                sqlCommand.Parameters.AddWithValue("@Department", department);
                sqlCommand.Parameters.AddWithValue("@Area", area);
                sqlCommand.Parameters.AddWithValue("@Model", model);
                sqlCommand.Parameters.AddWithValue("@SerialNumber", serialNumber);
                sqlCommand.Parameters.AddWithValue("@Usuario", user);
                sqlCommand.Parameters.AddWithValue("@CECO", CECO);
                sqlCommand.Parameters.AddWithValue("@Hostname", hostname);
                sqlCommand.Parameters.AddWithValue("@Date", date);
                sqlCommand.Parameters.AddWithValue("@Cost", cost);
                sqlCommand.Parameters.AddWithValue("@Id", id);

                try
                {
                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Datos editados y guardados correctamente.");
                        this.Close();
                        ComputerList list = new ComputerList();
                        list.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar los datos editados: " + ex.Message);
                }
            }
        }
    }
}
