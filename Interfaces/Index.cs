using System;
using System.Windows.Forms;

namespace InventoryControl.Interfaces
{
    public partial class Index : Form
    {
        public Index()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Create createForm = new Create();
            createForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            ComputerList list = new ComputerList();
            list.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchTerm = textBox1.Text.Trim();
                ComputerList computerList = new ComputerList(searchTerm);
                this.Close();
                computerList.Show();
            }
        }
    }
}
