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
    public partial class Form1 : Form
    {
        SqlCeConnection cn = new SqlCeConnection(@"Data Source=C:\temp\Hospital.sdf");



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            viewPatientButton.Enabled = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); //clears data from previous search
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            cn.Close(); //closes any existing session
            cn.Open(); //opens a new connection

            if (searchNameTextBox.Text != "") //checks if text exists
                viewPatientButton.Enabled = true;
            {
                SqlCeCommand cm = new SqlCeCommand("select * from patients where firstName like '%" + searchNameTextBox.Text + "%' ", cn); //basic sql query with textbox variable
                SqlCeDataReader dr = cm.ExecuteReader(); //sql reader
                while (dr.Read()) //reads and assigns rows to the listbox
                {
                    listBox1.Items.Add(dr["patientId"].ToString());
                    listBox2.Items.Add(dr["firstName"].ToString());
                    listBox3.Items.Add(dr["surname"].ToString());
                }
            }
             cn.Close(); //closes connection
        }

        private void viewPatientButton_Click(object sender, EventArgs e)
        {
            //string id = listBox1.SelectedItem.ToString();
            //viewModifyPatientForm viewForm = new viewModifyPatientForm(id);
            //this.Hide();
            //viewForm.Show();

            cn.Open(); //opens a new connection
            //if (searchNameTextBox.Text != "") //checks if text exists
            //{
                string view_id = listBox1.SelectedItem.ToString(); 
                SqlCeCommand cm = new SqlCeCommand("select * from patients where patientId = '"+view_id+"'", cn); //basic sql query 
                SqlCeDataReader dr = cm.ExecuteReader(); //sql reader
                while (dr.Read()) //reads and assigns rows to the listbox
                {
                    idTextBox.Text = dr["patientId"].ToString();
                    nameTextBox.Text = dr["firstName"].ToString();
                    surnameTextBox.Text = dr["surname"].ToString();
                    addressTextBox.Text = dr["address"].ToString();
                    suburbTextBox.Text = dr["suburb"].ToString();
                    postcodeTextBox.Text = dr["postcode"].ToString();
                    homeTelephoneTextBox.Text = dr["telephone_home"].ToString();
                    mobilePhoneTextBox.Text = dr["telephone_mobile"].ToString();
                    dobTextBox.Text = dr["date_of_birth"].ToString();
                    ageTextBox.Text = dr["age"].ToString();
                    genderTextBox.Text = dr["sex"].ToString();
                    countryOfBirthTextBox.Text = dr["country_of_birth"].ToString();
                    contactNameTextBox.Text = dr["contact_name"].ToString();
                    contactRelationshipTextBox.Text = dr["contact_relationship"].ToString();
                    contactAddressTextBox.Text = dr["contact_address"].ToString();
                    contactSuburbTextBox.Text = dr["contact_suburb"].ToString();
                    contactPostcodeTextBox.Text = dr["contact_postcode"].ToString();
                    contactMobileNumberTextBox.Text = dr["contact_telephone_mobile"].ToString();
                    contactTelephoneNumberTextBox.Text = dr["contact_telephone_home"].ToString();
                    medicareNumberTextBox.Text = dr["entitlement_medicare_number"].ToString();
                    medicareReferenceNumberTextBox.Text = dr["entitlement_medicare_reference_number"].ToString();
                    medicareExpiryDateTextBox.Text = dr["entitlement_medicare_expiry_date"].ToString();
                    privateHealthNumberTextBox.Text = dr["privatehealth_number"].ToString();
                    privateHealthJoinDateTextBox.Text = dr["privatehealth_join_date"].ToString();
                    privateHealthExpiryDateTextBox.Text = dr["privatehealth_expiry_date"].ToString();
                    privateHealthCoverageLevelTextBox.Text = dr["privatehealth_coverage_level"].ToString();
                    heightTextBox.Text = dr["patient_height"].ToString();
                    weightTextBox.Text = dr["patient_weight"].ToString();
                }
                tabControl1.SelectTab(1);
          //  }     
            cn.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox l = sender as ListBox;
            if (l.SelectedIndex !=-1)
            {
                listBox1.SelectedIndex = l.SelectedIndex;
                listBox2.SelectedIndex = l.SelectedIndex;
                listBox3.SelectedIndex = l.SelectedIndex;
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {

        }

        
        private void modifyButton_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCeCommand cm = new SqlCeCommand(@"update patients set 
                                    firstName = '"+nameTextBox.Text
                                    +"',surname = '"+surnameTextBox.Text
                                    +"',address = '"+addressTextBox.Text
                                    +"',suburb = '"+suburbTextBox.Text
                                    +"',date_of_birth = '"+dobTextBox.Text
                                    +"',age = '"+ageTextBox.Text
                                    +"',sex = '"+genderTextBox.Text
                                    +"',country_of_birth = '"+countryOfBirthTextBox.Text
                                    +"',contact_name = '"+contactNameTextBox.Text
                                    +"',contact_relationship = '"+contactRelationshipTextBox.Text
                                    +"',contact_address = '"+contactAddressTextBox.Text
                                    +"',contact_suburb = '"+contactSuburbTextBox.Text
                                    +"',contact_postcode = '"+contactPostcodeTextBox.Text
                                    +"',contact_telephone_mobile = '"+contactMobileNumberTextBox.Text
                                    +"',contact_telephone_home = '"+contactTelephoneNumberTextBox.Text
                                    +"',entitlement_medicare_number = '"+medicareNumberTextBox.Text
                                    +"',entitlement_medicare_reference_number = '"+medicareReferenceNumberTextBox.Text
                                    +"',entitlement_medicare_expiry_date = '"+medicareExpiryDateTextBox.Text
                                    +"',privatehealth_number = '"+privateHealthNumberTextBox.Text
                                    +"',privatehealth_join_date = '"+privateHealthJoinDateTextBox.Text
                                    +"',privatehealth_expiry_date = '"+privateHealthExpiryDateTextBox.Text
                                    +"',privatehealth_coverage_level = '"+privateHealthCoverageLevelTextBox.Text
                                    +"',patient_height ='"+heightTextBox.Text
                                    +"',patient_weight = '"+weightTextBox.Text
                                    +"',postcode = '"+postcodeTextBox.Text
                                    +"',telephone_home = '"+homeTelephoneTextBox.Text
                                    +"',telephone_mobile = '"+mobilePhoneTextBox.Text
                                    +"' where patientId = '"+idTextBox.Text.ToString()+"'  ", cn); //update query. On second thought, we could do a "field modified" option
            cm.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Patient has been Updated", "Townsville Hospital");

        }

        private void createNewPatientButton_Click(object sender, EventArgs e) //located on search page
        {
            tabControl1.SelectTab(2); 
        }

        private void createPatientButton_Click(object sender, EventArgs e) //located on the actual patient creation page
        {
            cn.Open();
            SqlCeCommand cm = new SqlCeCommand(@"insert into Patients 
                                    (patientId, firstName, surname,
                                    address, suburb, date_of_birth,
                                    age, sex, country_of_birth,
                                    contact_name, contact_relationship, 
                                    contact_address, contact_suburb, 
                                    contact_postcode, contact_telephone_mobile,
                                    contact_telephone_home, entitlement_medicare_number,
                                    entitlement_medicare_reference_number, 
                                    entitlement_medicare_expiry_date,
                                    privatehealth_number, privatehealth_join_date,
                                    privatehealth_expiry_date, privatehealth_coverage_level,
                                    patient_height, patient_weight, postcode,telephone_home,
                                    telephone_mobile) 
                                    values 
                                   ('" + create_IdTB.Text 
                                       + "', '" + create_NameTB.Text 
                                       + "', '" + create_SurnameTB.Text
                                       + "', '" + create_AddressTB.Text
                                       + "', '" + create_SuburbTB.Text
                                       + "', '" + create_DoBTB.Text
                                       + "', '" + create_AgeTB.Text
                                       + "', '" + create_GenderTB.Text
                                       + "', '" + create_CountryTB.Text
                                       + "', '" + create_ContactNameTB.Text
                                       + "', '" + create_ContactRelationshipTB.Text
                                       + "', '" + create_ContactAddressTB.Text
                                       + "', '" + create_ContactSuburbTB.Text
                                       + "', '" + create_ContactPostcodeTB.Text
                                       + "', '" + create_ContactMobileTB.Text
                                       + "', '" + create_ContactPhoneTB.Text
                                       + "', '" + create_MedicareTB.Text
                                       + "', '" + create_MedicareReferenceTB.Text
                                       + "', '" + create_MedicareExpiryTB.Text
                                       + "', '" + create_PHNumberTB.Text
                                       + "', '" + create_PHJoinDateTB.Text
                                       + "', '" + create_PHJoinDateTB.Text
                                       + "', '" + create_PHExpiryDateTB.Text
                                       + "', '" + create_HeightTB.Text
                                       + "', '" + create_WeightTB.Text
                                       + "', '" + create_PostcodeTB.Text
                                       + "', '" + create_HomeTB.Text
                                       + "', '" + create_MobileTB.Text
                                       + "' )", cn);
            cm.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Patient has been created", "Townsville Hospital");
        }
    }
}
