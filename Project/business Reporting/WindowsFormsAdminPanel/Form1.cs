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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void estaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptStaff a = new rptStaff();
            a.MdiParent = this;
            a.Show();
        }

        private void categoryInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptCat a = new rptCat();
            a.MdiParent = this;
            a.Show();
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the current form (Form1)
            this.Close();

            // Show the Login form
            Login loginForm = new Login();
            loginForm.ShowLogin();
        }

        private void eclassenrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptenrollment a = new rptenrollment();
            a.MdiParent = this;
            a.Show();
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptstudent a = new rptstudent();
            a.MdiParent = this; 
            a.Show();
        }

        private void eSubjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptsubject a = new rptsubject();
            a.MdiParent = this;
            a.Show();
        }

        private void eLecSemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptlsp a = new rptlsp();
            a.MdiParent = this;
            a.Show();
        }

        private void sappointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptappointment a = new rptappointment();
            a.MdiParent = this;
            a.Show();
        }

        private void labtestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptlabtest a = new rptlabtest();
            a.MdiParent = this;
            a.Show();
        }

        private void patientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptpatient a = new rptpatient();
            a.MdiParent = this;
            a.Show();
        }

        private void doctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptdoctor a = new rptdoctor();
            a.MdiParent = this;
            a.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptcustomer a = new rptcustomer();
            a.MdiParent = this;
            a.Show();

        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptorder a = new rptorder();
            a.MdiParent = this;
            a.Show();
        }

        private void orderDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptorderdetails a = new rptorderdetails();
            a.MdiParent = this;
            a.Show();
        }

        private void medicineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptmedicine a = new rptmedicine();
            a.MdiParent = this;
            a.Show();

        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rpttransaction a = new rpttransaction();
            a.MdiParent = this;
            a.Show();
        }
    }
}
