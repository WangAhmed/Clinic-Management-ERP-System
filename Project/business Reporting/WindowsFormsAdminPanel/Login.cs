using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAdminPanel
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void registerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.registerBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Register' table. You can move, or remove it, as needed.
            this.registerTableAdapter.Fill(this.dataSet1.Register);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registerTableAdapter.FillByLOGIN(dataSet1.Register, textBox1.Text, textBox2.Text);

            int noofrows = dataSet1.Register.Rows.Count;

            if (noofrows > 0)
            {
                // Assuming the RegisterTypeID is a column in the dataSet1.Register table
                DataRow userRow = dataSet1.Register.Rows[0];
                int registerTypeID = (int)userRow["RegisterTypeID"];

                if (registerTypeID == 1)
                {
                    Form1 a = new Form1();
                    a.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("You do not have the necessary permissions to access this application.");
                }
            }
            else
            {
                MessageBox.Show("Invalid id/pwd");
            }
        }

        // Method to show the Login form again when logging out
        public void ShowLogin()
        {
            this.Show();
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
