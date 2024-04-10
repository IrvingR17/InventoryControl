using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InventoryControl.Interfaces
{
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SaveRecord())
            {
                statusTextBox.Text = "";
                userTextBox.Text = "";
                modelTextbox.Text = "";
                hostnameTextBox.Text = "";
                serialNumberTextBox.Text = "";
                typeTextBox.Text = "";
                dateTimePicker1.Text = "";
                departmentTextBox.Text = "";
                costTextBox.Text = "";
                areaTextBox.Text = "";
                CECOTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Error al guardar el registro. Por favor, inténtalo de nuevo.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Index index = new Index();
            this.Close();
            index.Show();
        }

        private bool SaveRecord()
        {
            string status = statusTextBox.Text.Trim();
            string type = typeTextBox.Text.Trim();
            string department = departmentTextBox.Text.Trim();
            string area = areaTextBox.Text.Trim();
            string model = modelTextbox.Text.Trim();
            string serialNumber = serialNumberTextBox.Text.Trim();
            string user = userTextBox.Text.Trim();
            string CECO = CECOTextBox.Text.Trim();
            string hostname = hostnameTextBox.Text.Trim();
            string date = dateTimePicker1.Text.Trim();
            string cost = costTextBox.Text.Trim();

            string query = "INSERT INTO Computers (Status, Type, Department, Area, Model, SerialNumber, Usuario, CECO, Hostname, Date, Cost) VALUES (@Status, @Type, @Department, @Area, @Model, @SerialNumber, @Usuario, @CECO, @Hostname, @Date, @Cost)";

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLLocalDB"].ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
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

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }

            MessageBox.Show("El registro ha sido guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }
    }
}
