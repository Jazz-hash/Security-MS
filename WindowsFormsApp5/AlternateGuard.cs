using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp5;

namespace Security_ms
{
    public partial class AlternateGuard : Form
    {
       
        // String connection to DATABASE
        static string con_string = ConfigurationManager.ConnectionStrings["WindowsFormsApp5.Properties.Settings.Setting"].ConnectionString;
        SqlConnection connection = new SqlConnection(con_string);
        public AlternateGuard()
        {
            InitializeComponent();
            guard_reason_name_txt.Text = Dashboard.guard_name;
            guard_reason_name_txt.Enabled = false;
        }

        private void next_btn_1_Click(object sender, EventArgs e)
        {
            if (guard_reason_name_txt.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (guard_reason_txt.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else
            {

                check = true;
                SqlCommand add_data = new SqlCommand("INSERT INTO [dbo].[LEAVE] ([EMP_NAME],[REASON]) VALUES ('" + guard_reason_name_txt.Text + "', '" + guard_reason_txt.Text + "')", connection);
                connection.Open();
                add_data.ExecuteNonQuery();
                connection.Close();
                this.Hide();
            }
            
        }

        private void Close_Paint(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, "No change in appointments occured !!", "Warning");
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, "No change in appointments occured !!", "Warning");
            this.Hide();
        }

        public static bool check;

        private void next_btn_2_Click(object sender, EventArgs e)
        {
            check = false;
            this.Hide();
        }
    }
}
