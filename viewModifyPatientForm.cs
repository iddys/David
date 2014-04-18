using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace sqltest3
{
    public partial class viewModifyPatientForm : Form
    {
        SqlCeConnection cn = new SqlCeConnection(@"Data Source=C:\temp\Hospital.sdf");

        public viewModifyPatientForm(string id)
        {
            InitializeComponent();

            cn.Close(); //closes any existing session
            cn.Open(); //opens a new connection

            SqlCeCommand cm = new SqlCeCommand("select * from patients where patientId ='" + id + "' ", cn);
            SqlCeDataReader dr = cm.ExecuteReader(); //sql reader
            idTextBox.Text = "HI";


            while (dr.Read()) //reads and assigns rows to the listbox
            {
                idTextBox.Text = dr["patientId"].ToString();
                nameTextBox.Text = dr["firstName"].ToString();
                surnameTextBox.Text = dr["surname"].ToString();
                textBox1.Text = dr["address"].ToString();
                textBox2.Text = dr["suburb"].ToString();
                textBox3.Text = dr["postcode"].ToString();
                textBox4.Text = dr["telephone_home"].ToString(); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 stuff2 = new Form1();
            stuff2.Show();
        }
    }
}
