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
    public partial class Login : Form
    {
        public static string emp_categories;
        public static string username_authenticate;
        public static string category1 = "Human Resource";
        public static string category2 = "Manager";
        public static string category3 = "Accountant";
        public static string category4 = "Prepaid Manager";
        public static string category5 = "Operation Manager";
        public static string category6 = "Admission Manager";
        public static string category7 = "Super Admin";
        // String connection to DATABASE
        static string con_string = ConfigurationManager.ConnectionStrings["WindowsFormsApp5.Properties.Settings.Setting"].ConnectionString;
        SqlConnection connection = new SqlConnection(con_string);
        public Login()
        {
            InitializeComponent();
            login_tab.Appearance = TabAppearance.FlatButtons;
            login_tab.ItemSize = new Size(0, 1);
            login_tab.SizeMode = TabSizeMode.Fixed;

        }
        

        private void login_btn_Click(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data_for_super_admin = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[SUPER_ADMIN] WHERE [MASTER_EMAIL] = '"+username_txt.Text+"' AND [MASTER_PASS] = '"+password_text.Text+"'", connection);
            DataTable tb_data = new DataTable();
            fetch_data_for_super_admin.Fill(tb_data);

            SqlDataAdapter fetch_data_for_super_admin_pass = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[SUPER_ADMIN] WHERE [MASTER_EMAIL] = '" + username_txt.Text + "'", connection);
            DataTable tb_data_password = new DataTable();
            fetch_data_for_super_admin_pass.Fill(tb_data_password);
            
            SqlDataAdapter fetch_data_for_admin_user = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[ADMIN_USER] WHERE [EMAIL] = '" + username_txt.Text+"'", connection);
            DataTable tb_data_username = new DataTable();
            fetch_data_for_admin_user.Fill(tb_data_username);

            SqlDataAdapter fetch_data_for_admin = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[ADMIN_USER] WHERE [EMAIL] = '" + username_txt.Text+"' AND PASSWORD = '"+password_text.Text+"'", connection);
            DataTable tb_data_user = new DataTable();
            fetch_data_for_admin.Fill(tb_data_user);

            SqlDataAdapter fetch_data_for_prepaid_user = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[PREPAID] WHERE [EMAIL] = '" + username_txt.Text + "'", connection);
            DataTable tb_data_user_prepaid = new DataTable();
            fetch_data_for_prepaid_user.Fill(tb_data_user_prepaid);

            SqlDataAdapter fetch_data_for_prepaid = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[PREPAID] WHERE [EMAIL] = '" + username_txt.Text + "' AND PASSWORD = '" + password_text.Text + "'", connection);
            DataTable tb_data_user_pre = new DataTable();
            fetch_data_for_prepaid.Fill(tb_data_user_pre);

            SqlDataAdapter fetch_data_for_admission_user = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[MANAGER_ADMISSION] WHERE [EMAIL] = '" + username_txt.Text + "'", connection);
            DataTable tb_data_user_admission = new DataTable();
            fetch_data_for_admission_user.Fill(tb_data_user_admission);

            SqlDataAdapter fetch_data_for_admission = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[MANAGER_ADMISSION] WHERE [EMAIL] = '" + username_txt.Text + "' AND PASSWORD = '" + password_text.Text + "'", connection);
            DataTable tb_data_user_ad = new DataTable();
            fetch_data_for_admission.Fill(tb_data_user_ad);

            SqlDataAdapter fetch_data_for_operation_user = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[MANAGER_OPERATION] WHERE [EMAIL] = '" + username_txt.Text + "'", connection);
            DataTable tb_data_user_operation = new DataTable();
            fetch_data_for_operation_user.Fill(tb_data_user_operation);

            SqlDataAdapter fetch_data_for_operation = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[MANAGER_OPERATION] WHERE [EMAIL] = '" + username_txt.Text + "' AND PASSWORD = '" + password_text.Text + "'", connection);
            DataTable tb_data_user_op = new DataTable();
            fetch_data_for_operation.Fill(tb_data_user_op);
            
            if (tb_data.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Welcome Super admin !!", "Success");
                username_authenticate = username_txt.Text;
                Cursor = Cursors.WaitCursor;
                Dashboard db_main = new Dashboard();
                Cursor = Cursors.Arrow;
                emp_categories = category7;
                this.Hide();
                db_main.Show();
            }
            else if (tb_data_password.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. Try again or contact admin if you forgot password !!", "Error"); 
            }
            else if (tb_data_user_pre.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Welcome Prepaid Manager", "Success");
                username_authenticate = username_txt.Text;
                emp_categories = category4;
                Cursor = Cursors.WaitCursor;
                Dashboard db_main = new Dashboard();
                Cursor = Cursors.Arrow;
                this.Hide();
                db_main.Show();
            }
            else if (tb_data_user_prepaid.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. Try again or contact admin if you forgot password !!", "Error");
            }


            else if (tb_data_user_op.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Welcome Operation Manager", "Success");
                username_authenticate = username_txt.Text;
                emp_categories = category5;
                Cursor = Cursors.WaitCursor;
                Dashboard db_main = new Dashboard();
                Cursor = Cursors.Arrow;
                this.Hide();
                db_main.Show();
            }
            else if (tb_data_user_operation.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. Try again or contact admin if you forgot password !!", "Error");
            }
            else if (tb_data_user_ad.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Welcome Admission Manager", "Success");
                username_authenticate = username_txt.Text;
                emp_categories = category6;
                Cursor = Cursors.WaitCursor;
                Dashboard db_main = new Dashboard();
                Cursor = Cursors.Arrow;
                this.Hide();
                db_main.Show();
            }
            else if (tb_data_user_admission.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. Try again or contact admin if you forgot password !!", "Error");
            }
            else if (tb_data_user.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Welcome user", "Success");
                username_authenticate = username_txt.Text;
                SqlDataAdapter FETCH_DATA = new SqlDataAdapter("SELECT [DESIGNATION] FROM [dbo].[ADMIN_USER] WHERE [dbo].[ADMIN_USER].[EMAIL] = '" + username_authenticate + "'", connection);
                DataTable TB_DATA = new DataTable();
                FETCH_DATA.Fill(TB_DATA);
                if (category1 == TB_DATA.Rows[0][0].ToString())
                {
                    emp_categories = category1;
                }
                else if(TB_DATA.Rows[0][0].ToString() == category2)
                {
                    emp_categories = category2;

                }

                else if (TB_DATA.Rows[0][0].ToString() == category3)
                {
                    emp_categories = category3;

                }
                Cursor = Cursors.WaitCursor;
                Dashboard db_main = new Dashboard();
                Cursor = Cursors.Arrow;
                this.Hide();
                db_main.Show();
            }
            else if (tb_data_username.Rows[0][0].ToString() == "1")
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. Try again or contact admin if you forgot password !!", "Error");
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Couldn't find your account !!");
            }

        }
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MetroFramework.MetroMessageBox.Show(this, "Are you sure you want to exit ? ", "Notification", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {


                Application.ExitThread();
            }
            else
            {

            }
        }

        private void username_txt_Click(object sender, EventArgs e)
        {
        }

        private void password_text_Click(object sender, EventArgs e)
        {
        }
    }
}
