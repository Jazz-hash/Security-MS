using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApp5
{
    public partial class Admin_authentication : Form
    {
        public static bool allow_user;
        // String connection to DATABASE
        static string con_string = ConfigurationManager.ConnectionStrings["WindowsFormsApp5.Properties.Settings.Setting"].ConnectionString;
        SqlConnection connection = new SqlConnection(con_string);

        public static string username_text;
        public static string name_text;

        public Admin_authentication()
        {
            InitializeComponent();
            if (Dashboard.user_name == "admin")
            {
                username_label.Text = Dashboard.user_name;
                name_label.Text = "ADMIN";

            }
            else
            {
                username_label.Text = Dashboard.user_name;
                SqlDataAdapter fetch = new SqlDataAdapter("SELECT [NAME] FROM [dbo].[ADMIN_USER] WHERE [EMAIL] = '" + Dashboard.user_name + "'", connection);
                DataTable tb_data = new DataTable();
                fetch.Fill(tb_data);
                SqlDataAdapter fetch2 = new SqlDataAdapter("SELECT [NAME] FROM [dbo].[PREPAID] WHERE [EMAIL] = '" + Dashboard.user_name + "'", connection);
                DataTable tb_data2 = new DataTable();
                fetch2.Fill(tb_data2);
                SqlDataAdapter fetch3 = new SqlDataAdapter("SELECT [NAME] FROM [dbo].[MANAGER_ADMISSION] WHERE [EMAIL] = '" + Dashboard.user_name + "'", connection);
                DataTable tb_data3 = new DataTable();
                fetch3.Fill(tb_data3);
                SqlDataAdapter fetch4 = new SqlDataAdapter("SELECT [NAME] FROM [dbo].[MANAGER_OPERATION] WHERE [EMAIL] = '" + Dashboard.user_name + "'", connection);
                DataTable tb_data4 = new DataTable();
                fetch4.Fill(tb_data4);

                SqlDataAdapter fetch5 = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[ADMIN_USER] WHERE [EMAIL] = '" + Dashboard.user_name + "'", connection);
                DataTable tb_data_count = new DataTable();
                fetch5.Fill(tb_data_count);
                SqlDataAdapter fetch6 = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[PREPAID] WHERE [EMAIL] = '" + Dashboard.user_name + "'", connection);
                DataTable tb_data_count2 = new DataTable();
                fetch6.Fill(tb_data_count2);
                SqlDataAdapter fetch7 = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[MANAGER_ADMISSION] WHERE [EMAIL] = '" + Dashboard.user_name + "'", connection);
                DataTable tb_data_count3 = new DataTable();
                fetch7.Fill(tb_data_count3);
                SqlDataAdapter fetch8 = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[MANAGER_OPERATION] WHERE [EMAIL] = '" + Dashboard.user_name + "'", connection);
                DataTable tb_data_count4 = new DataTable();
                fetch8.Fill(tb_data_count4);

                if (tb_data_count.Rows[0][0].ToString() == "1")
                {
                    name_label.Text = tb_data.Rows[0][0].ToString();
                }
                else if (tb_data_count2.Rows[0][0].ToString() == "1"){
                    name_label.Text = tb_data2.Rows[0][0].ToString();

                }

                else if (tb_data_count3.Rows[0][0].ToString() == "1")
                {
                    name_label.Text = tb_data3.Rows[0][0].ToString();

                }
                else if (tb_data_count4.Rows[0][0].ToString() == "1")
                {
                    name_label.Text = tb_data4.Rows[0][0].ToString();

                }
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        string email;
        private void fetch_admin_data()
        {
            SqlDataAdapter fetch_data_for_super_admin_pass = new SqlDataAdapter("SELECT [MASTER_EMAIL] FROM [dbo].[SUPER_ADMIN] WHERE ID = '1'", connection);
            DataTable tb_data_password = new DataTable();
            fetch_data_for_super_admin_pass.Fill(tb_data_password);
            email = tb_data_password.Rows[0][0].ToString();
        }

        private void Yes_Click(object sender, EventArgs e)
        {
            fetch_admin_data();
            SqlDataAdapter fetch_data_for_super_admin_pass = new SqlDataAdapter("SELECT [MASTER_PASS] FROM [dbo].[SUPER_ADMIN] WHERE [MASTER_EMAIL] = '"+email+"'", connection);
            DataTable tb_data_password = new DataTable();
            fetch_data_for_super_admin_pass.Fill(tb_data_password);
            if (pass_confirm_txt.Text == tb_data_password.Rows[0][0].ToString())
            {
                this.Hide();
                allow_user = true;
            }
            else
            {
                allow_user = false;
                this.Hide();
            }
        }

        private void No_Click(object sender, EventArgs e)
        {
            this.Hide();
            allow_user = false;
        }
    }
}
