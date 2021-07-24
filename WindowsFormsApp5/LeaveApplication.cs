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
    public partial class LeaveApplication : Form
    {
        public static string guard_com = Dashboard.guard_com;
        public static string guard_com_br = Dashboard.guard_com_br;
        public static string guard_timings = Dashboard.guard_timings;
        public static string guard_amount = Dashboard.guard_amount;
        // String connection to DATABASE
        static string con_string = ConfigurationManager.ConnectionStrings["WindowsFormsApp5.Properties.Settings.Setting"].ConnectionString;
        SqlConnection connection = new SqlConnection(con_string);
        public LeaveApplication()
        {
            InitializeComponent();
            guard_time_to_txt.Text = Dashboard.guard_time_to;
            guard_time_from_txt.Text = Dashboard.guard_time_from;
            guard_from_txt.Text = Dashboard.guard_from;
            guard_to_txt.Text = Dashboard.guard_to;
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[EMPLOYEE_GUARD]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            metroComboBox1.DataSource = tb_data;
            metroComboBox1.DisplayMember = "NAME";
            metroComboBox1.ValueMember = "ID";
        }

        private void next_btn_1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(guard_from_txt.Text) > Convert.ToDateTime(guard_to_txt.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, "Current date cannot be greator than working date !!", "Error");
            }
            else
            {
                try
                {
                    SqlCommand add_data = new SqlCommand("INSERT INTO [dbo].[APPPOINTMENT] ([COMPANY],[COMPANY_BRANCH],[NO_OF_PERSON],[FROM_TIME],[TO_TIME],[TIMINGS],[AMOUNT_PER_PERSON],[IN_TIME],[OUT_TIME],[STATUS]) VALUES ('" + guard_com + "','" + guard_com_br + "','" + metroComboBox1.Text + "','" + guard_from_txt.Text + "','" + guard_to_txt.Text + "','" + guard_timings + "','" + guard_amount + "','" + guard_time_from_txt.Text + "','" + guard_time_to_txt.Text + "','On-Duty')", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    this.Hide();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, "You can't close at this stage !!", "Warning");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, "You can't close at this stage !!", "Warning");
        }
    }
}
