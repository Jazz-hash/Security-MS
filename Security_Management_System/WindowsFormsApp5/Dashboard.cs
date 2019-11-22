using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using Bunifu.Framework;
using Bunifu.Framework.UI;
using Microsoft.Office.Interop.Excel;
using ExcelDataReader;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using CrystalDecisions.CrystalReports.Engine;
using Security_ms;

namespace WindowsFormsApp5
{

    public partial class Dashboard : MaterialForm
    {
        public static string user_name = Login.username_authenticate;
        public static string guard_name;
        public static string guard_com;
        public static string guard_com_br;
        public static string guard_timings;
        public static string guard_amount;
        public static string guard_from;
        public static string guard_to;
        public static string guard_time_to;
        public static string guard_time_from;
        // String connection to DATABASE
        static string con_string = ConfigurationManager.ConnectionStrings["WindowsFormsApp5.Properties.Settings.Setting"].ConnectionString;
        SqlConnection connection = new SqlConnection(con_string);
        public Dashboard()
        {
            InitializeComponent();
            // Tabcontrol sizing & Fetching data
            dashboard_functions();
        }

        private void login_functions()
        {
            // Human Resource
            if (Login.emp_categories == Login.category1)
            {
                ((Control)this.assign_tab).Enabled = false;
                ((Control)this.Appointments).Enabled = false;
                ((Control)this.attendance_tab).Enabled = false;
                ((Control)this.Salaries).Enabled = false;
                ((Control)this.Expenses).Enabled = false;
                ((Control)this.Reports).Enabled = false;
                Prepaid_tile.Enabled = false;
                operation_tile.Enabled = false;
                admission_tile.Enabled = false;
                employee_tile.Enabled = false;
                delete_emp_button.Enabled = false;
                delete_company_button.Enabled = false;
                delete_branch_data.Enabled = false;
            }
            // Manager
            else if (Login.emp_categories == Login.category2)
            {

                ((Control)this.Employees).Enabled = false;
                ((Control)this.mini_screen_com).Enabled = false;
                ((Control)this.attendance_tab).Enabled = false;
                ((Control)this.Salaries).Enabled = false;
                ((Control)this.Expenses).Enabled = false;
                ((Control)this.Reports).Enabled = false;
                Prepaid_tile.Enabled = false;
                operation_tile.Enabled = false;
                admission_tile.Enabled = false;
                employee_tile.Enabled = false;
                delete_assign_button.Enabled = false;
                delete_app_button.Enabled = false;
            }
            // Accountant
            else if (Login.emp_categories == Login.category3)
            {
                ((Control)this.Employees).Enabled = false;
                ((Control)this.mini_screen_com).Enabled = false;
                ((Control)this.assign_tab).Enabled = false;
                ((Control)this.Appointments).Enabled = false;
                ((Control)this.Reports).Enabled = false;
                Prepaid_tile.Enabled = false;
                operation_tile.Enabled = false;
                admission_tile.Enabled = false;
                employee_tile.Enabled = false;
                delete_payment_btn.Enabled = false;
                delete_pay_btn.Enabled = false;
                delete_exp_btn.Enabled = false;
            }
            // Prepaid Manager
            else if (Login.emp_categories == Login.category4)
            {
                Prepaid_tile.Enabled = false;
                operation_tile.Enabled = false;
                admission_tile.Enabled = false;
                employee_tile.Enabled = false;
            }
            // Operation Manager
            else if (Login.emp_categories == Login.category5)
            {
                Prepaid_tile.Enabled = false;
                operation_tile.Enabled = false;
                admission_tile.Enabled = false;
                employee_tile.Enabled = false;
            }
            // Admission Manager
            else if (Login.emp_categories == Login.category6)
            {
                Prepaid_tile.Enabled = false;
                operation_tile.Enabled = false;
                admission_tile.Enabled = false;
                employee_tile.Enabled = false;
            }
            else if (Login.emp_categories == Login.category7)
            {
                ((Control)this.Employees).Enabled = true;
                ((Control)this.mini_screen_com).Enabled = true;
                ((Control)this.assign_tab).Enabled = true;
                ((Control)this.Appointments).Enabled = true;
                ((Control)this.attendance_tab).Enabled = true;
                ((Control)this.Salaries).Enabled = true;
                ((Control)this.Expenses).Enabled = true;
                ((Control)this.Reports).Enabled = true;
                Prepaid_tile.Enabled = true;
                operation_tile.Enabled = true;
                admission_tile.Enabled = true;
                employee_tile.Enabled = true;
                delete_emp_button.Enabled = true;
                delete_company_button.Enabled = true;
                delete_branch_data.Enabled = true;
                delete_assign_button.Enabled = true;
                delete_app_button.Enabled = true;
                delete_pay_btn.Enabled = true;
                delete_exp_btn.Enabled = true;

            }
        }

        private void fetch_data_for_labels()
        {
            SqlDataAdapter fetch_emp_data = new SqlDataAdapter("SELECT COUNT(*) FROM EMPLOYEE_GUARD", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_emp_data.Fill(tb_data);

            SqlDataAdapter fetch_com_data = new SqlDataAdapter("SELECT COUNT(*) FROM COMPANY", connection);
            System.Data.DataTable tb_data_com = new System.Data.DataTable();
            fetch_com_data.Fill(tb_data_com);

            SqlDataAdapter fetch_app_data = new SqlDataAdapter("SELECT COUNT(*) FROM APPPOINTMENT", connection);
            System.Data.DataTable tb_data_app = new System.Data.DataTable();
            fetch_app_data.Fill(tb_data_app);

            SqlDataAdapter fetch_exp_data = new SqlDataAdapter("SELECT SUM(CAST(AMOUNT AS bigint)) FROM [dbo].[EXPENSE]", connection);
            System.Data.DataTable tb_data_exp = new System.Data.DataTable();
            fetch_exp_data.Fill(tb_data_exp);

            emp_no_label.Text = tb_data.Rows[0][0].ToString();
            app_no_label.Text = tb_data_app.Rows[0][0].ToString();
            com_no_label.Text = tb_data_com.Rows[0][0].ToString();
            string expense = tb_data_exp.Rows[0][0].ToString();
            if (expense == "")
            {
                exp_no_label.Text = "$ 0.00";
            }
            else
            {
                exp_no_label.Text = "$ " + tb_data_exp.Rows[0][0].ToString();

            }
        }

        private void comboboxes_data()
        {
            combobox_employee();
            combobox_company();
            combobox_company_branches();
            fetch_data_for_labels();
            fetch_prepaid_data();
            fetch_m_add_combo_data();
            fetch_m_operate_data();
        }


        private void combobox_employee()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[EMPLOYEE_GUARD]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            new_emp_salary_combo.DataSource = tb_data;
            new_emp_salary_combo.DisplayMember = "NAME";
            new_emp_salary_combo.ValueMember = "ID";
            update_emp_salary_combo.DataSource = tb_data;
            update_emp_salary_combo.DisplayMember = "NAME";
            update_emp_salary_combo.ValueMember = "ID";
            add_emp_to_app_combo.DataSource = tb_data;
            add_emp_to_app_combo.DisplayMember = "NAME";
            add_emp_to_app_combo.ValueMember = "ID";
            update_emp_of_app_combo.DataSource = tb_data;
            update_emp_of_app_combo.DisplayMember = "NAME";
            update_emp_of_app_combo.ValueMember = "ID";

            SqlDataAdapter fetch_emp_data = new SqlDataAdapter("SELECT * FROM EMPLOYEE_GUARD WHERE EMPLOYEE_GUARD.ID NOT IN ( SELECT REFERENCE.EMP_NAME FROM REFERENCE)", connection);
            System.Data.DataTable tb_data_emp = new System.Data.DataTable();
            fetch_emp_data.Fill(tb_data_emp);
            add_emp_data.DataSource = tb_data_emp;
            add_emp_data.DisplayMember = "NAME";
            add_emp_data.ValueMember = "ID";
        }
        private void combobox_company()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[COMPANY]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            company_branch_combo.DataSource = tb_data;
            company_branch_combo.DisplayMember = "COMPANY_NAME";
            company_branch_combo.ValueMember = "ID";
            delete_com_for_payment.DataSource = tb_data;
            delete_com_for_payment.DisplayMember = "COMPANY_NAME";
            delete_com_for_payment.ValueMember = "ID";
            update_company_branch_combo.DataSource = tb_data;
            update_company_branch_combo.DisplayMember = "COMPANY_NAME";
            update_company_branch_combo.ValueMember = "ID";
            delete_company_branch_combo.DataSource = tb_data;
            delete_company_branch_combo.DisplayMember = "COMPANY_NAME";
            delete_company_branch_combo.ValueMember = "ID";
            com_assign_combo.DataSource = tb_data;
            com_assign_combo.DisplayMember = "COMPANY_NAME";
            com_assign_combo.ValueMember = "ID";
            update_com_assign_combo.DataSource = tb_data;
            update_com_assign_combo.DisplayMember = "COMPANY_NAME";
            update_com_assign_combo.ValueMember = "ID";
            delete_com_assign_combo.DataSource = tb_data;
            delete_com_assign_combo.DisplayMember = "COMPANY_NAME";
            delete_com_assign_combo.ValueMember = "ID";
            add_com_to_app_combo.DataSource = tb_data;
            add_com_to_app_combo.DisplayMember = "COMPANY_NAME";
            add_com_to_app_combo.ValueMember = "ID";
            update_com_of_app_combo.DataSource = tb_data;
            update_com_of_app_combo.DisplayMember = "COMPANY_NAME";
            update_com_of_app_combo.ValueMember = "ID";
            delete_com_of_app_combo.DataSource = tb_data;
            delete_com_of_app_combo.DisplayMember = "COMPANY_NAME";
            delete_com_of_app_combo.ValueMember = "ID";
            edit_app_of_com.DataSource = tb_data;
            edit_app_of_com.DisplayMember = "COMPANY_NAME";
            edit_app_of_com.ValueMember = "ID";
            company_combo.DataSource = tb_data;
            company_combo.DisplayMember = "COMPANY_NAME";
            company_combo.ValueMember = "ID";
            update_com_for_payment.DataSource = tb_data;
            update_com_for_payment.DisplayMember = "COMPANY_NAME";
            update_com_for_payment.ValueMember = "ID";
        }

        //private void combobox_company_data_for_appointment()
        //{
        //    SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT[dbo].[COMPANY_BRANCHES_DETAIL].[BRANCH_NAME] FROM[dbo].[COMPANY_BRANCHES_DETAIL] WHERE[dbo].[COMPANY_BRANCHES_DETAIL].[COMPANY_NAME] = '" + edit_app_of_com.Text + "'", connection);
        //    System.Data.DataTable tb_data = new System.Data.DataTable();
        //    fetch_data.Fill(tb_data);
        //    edit_app_of_com_br.DataSource = tb_data;
        //    edit_app_of_com_br.DisplayMember = "COMPANY_NAME";
        //    edit_app_of_com_br.ValueMember = "ID";
        //}

        private void combobox_company_branches()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[COMPANY_BRANCHES_DETAIL]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            com_br_payment_combo.DataSource = tb_data;
            com_br_payment_combo.DisplayMember = "BRANCH_NAME";
            com_br_payment_combo.ValueMember = "ID";
            update_com_br_for_payment.DataSource = tb_data;
            update_com_br_for_payment.DisplayMember = "BRANCH_NAME";
            update_com_br_for_payment.ValueMember = "ID";
            delete_com_br_for_payment.DataSource = tb_data;
            delete_com_br_for_payment.DisplayMember = "BRANCH_NAME";
            delete_com_br_for_payment.ValueMember = "ID";
            add_com_br_to_app_combo.DataSource = tb_data;
            add_com_br_to_app_combo.DisplayMember = "BRANCH_NAME";
            add_com_br_to_app_combo.ValueMember = "ID";
            update_com_br_of_app_combo.DataSource = tb_data;
            update_com_br_of_app_combo.DisplayMember = "BRANCH_NAME";
            update_com_br_of_app_combo.ValueMember = "ID";
            delete_com_br_of_app_combo.DataSource = tb_data;
            delete_com_br_of_app_combo.DisplayMember = "BRANCH_NAME";
            delete_com_br_of_app_combo.ValueMember = "ID";
            com_br_assign_combo.DataSource = tb_data;
            com_br_assign_combo.DisplayMember = "BRANCH_NAME";
            com_br_assign_combo.ValueMember = "ID";
            update_com_br_assign_combo.DataSource = tb_data;
            update_com_br_assign_combo.DisplayMember = "BRANCH_NAME";
            update_com_br_assign_combo.ValueMember = "ID";
            delete_com_br_assign_combo.DataSource = tb_data;
            delete_com_br_assign_combo.DisplayMember = "BRANCH_NAME";
            delete_com_br_assign_combo.ValueMember = "ID";
            com_br_payment_combo.DataSource = tb_data;
            com_br_payment_combo.DisplayMember = "BRANCH_NAME";
            com_br_payment_combo.ValueMember = "ID";
        }

        private void dashboard_functions()
        {
            // Color settings
            theme();
            // screen sizing fix
            mini_screens_fix();
            // show clock
            show_time();
            // fetching data
            fetch_data();
            // fetching data for comboboxes
            comboboxes_data();
            // username
            user_text.Text = Login.username_authenticate;
            panel227.BackColor = Color.Transparent;
            panel228.BackColor = Color.Transparent;
            bunifuCustomLabel2.BackColor = Color.Transparent;
            bunifuCustomLabel7.BackColor = Color.Transparent;
            bunifuCustomLabel8.BackColor = Color.Transparent;
            bunifuCustomLabel9.BackColor = Color.Transparent;
            panel247.BackColor = Color.Transparent;
            panel246.BackColor = Color.Transparent;
            panel249.BackColor = Color.Transparent;
            panel248.BackColor = Color.Transparent;
            panel251.BackColor = Color.Transparent;
            panel250.BackColor = Color.Transparent;
        }

        private void fetch_data()
        {
            // Guards data
            fetch_data_emp();
            // Company data
            fetch_data_com();
            // Company branches details
            data_fetch_branches();
            // Expenses data
            fetch_expense_data();
            // Payments
            fetch_data_payment();
            // Salaries
            fetch_data_salary();
            // Assignments
            fetch_data_assignment();
            // Appointments
            fetch_data_app();
            // Attendance
            fetch_atten_data();
            // Employees
            fetch_data_employees_company();
            // Clear employees data
            data_clear_employees();
            // Combo_boxes
            comboboxes_data();
            // Status Data
            fetch_guards_data();
        }

        private void theme()
        {
            // color settings
            MaterialSkinManager skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            metroLabel306.ForeColor = Color.Black;

            SkinManager.ColorScheme = new ColorScheme(
                Primary.Purple50, Primary.Purple800,
                Primary.Purple100, Accent.Purple100,
                TextShade.BLACK
            );
            buttons_fix();
        }

        private void buttons_fix()
        {
            new_emp_button.IconVisible = true;
            update_emp_button.IconVisible = true;
            delete_emp_button.IconVisible = true;
            update_emp_more_data.IconVisible = true;
            new_company_button.IconVisible = true;
            update_company_button.IconVisible = true;
            delete_company_button.IconVisible = true;
            new_branch_btn.IconVisible = true;
            update_branch_data.IconVisible = true;
            delete_branch_data.IconVisible = true;
            new_assign_button.IconVisible = true;
            update_assign_button.IconVisible = true;
            delete_assign_button.IconVisible = true;
            new_app_button.IconVisible = true;
            update_app_button.IconVisible = true;
            delete_app_button.IconVisible = true;
            new_payment_btn.IconVisible = true;
            update_payment_btn.IconVisible = true;
            delete_pay_btn.IconVisible = true;
            new_salary_btn.IconVisible = true;
            update_salary_btn.IconVisible = true;
            delete_payment_btn.IconVisible = true;
            new_exp_btn.IconVisible = true;
            update_exp_btn.IconVisible = true;
            delete_exp_btn.IconVisible = true;
            bunifuFlatButton1.IconVisible = true;
            bunifuFlatButton2.IconVisible = true;
            bunifuFlatButton3.IconVisible = true;

        }

        private void mini_screens_fix()
        {
            // screen sizing fix
            mini_screen_emp.Appearance = TabAppearance.FlatButtons;
            mini_screen_emp.ItemSize = new Size(0, 1);
            mini_screen_emp.SizeMode = TabSizeMode.Fixed;

            mini_screen_company.Appearance = TabAppearance.FlatButtons;
            mini_screen_company.ItemSize = new Size(0, 1);
            mini_screen_company.SizeMode = TabSizeMode.Fixed;

            mini_screen_app.Appearance = TabAppearance.FlatButtons;
            mini_screen_app.ItemSize = new Size(0, 1);
            mini_screen_app.SizeMode = TabSizeMode.Fixed;

            mini_screen_expense.Appearance = TabAppearance.FlatButtons;
            mini_screen_expense.ItemSize = new Size(0, 1);
            mini_screen_expense.SizeMode = TabSizeMode.Fixed;

            mini_screen_payment_salaries.Appearance = TabAppearance.FlatButtons;
            mini_screen_payment_salaries.ItemSize = new Size(0, 1);
            mini_screen_payment_salaries.SizeMode = TabSizeMode.Fixed;

            mini_screen_assignment.Appearance = TabAppearance.FlatButtons;
            mini_screen_assignment.ItemSize = new Size(0, 1);
            mini_screen_assignment.SizeMode = TabSizeMode.Fixed;

            mini_screen_dash.Appearance = TabAppearance.FlatButtons;
            mini_screen_dash.ItemSize = new Size(0, 1);
            mini_screen_dash.SizeMode = TabSizeMode.Fixed;
        }

        async void show_time()
        {
            // clock
            while (true)
            {
                show_date.Text = DateTime.Now.ToString();
                await Task.Delay(1000);
            }
        }

        private void data_clear_all()
        {
            com_data_clear();
            data_clear();
            data_clear_app();
            data_clear_assign();
            data_clear_branches();
            data_clear_expense();
            data_clear_salary();
            data_clear_payment();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }


        private void data_clear()
        {
            delete_prepaid_combo.Text = "";
            name_text.Clear();
            father_text.Clear();
            present_add_richtext.Clear();
            permanent_add_richtext.Clear();
            cnic_text.Clear();
            kin_name_text.Clear();
            kin_rel_text.Clear();
            kin_add_richtext.Clear();
            picture_emp.Image = null;
            salary_text.Clear();
        }

        private void browse_image_Click(object sender, EventArgs e)
        {
            // browsing employees' images
            browse_image_emp();
        }
        OpenFileDialog file_dialog = new OpenFileDialog();
        private void browse_image_emp()
        {
            // Browsing Function
            file_dialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
            file_dialog.Title = "Select Employee Picture";
            // file_dialog.InitialDirectory

            if (file_dialog.ShowDialog() == DialogResult.OK)
            {

                picture_emp.Image = Image.FromFile(file_dialog.FileName);
            }
            else
            {
                picture_emp.Image = null;
            }
        }

        private void fetch_data_emp()
        {
            // fetching employee basic data
            SqlDataAdapter data_fetch = new SqlDataAdapter("SELECT * FROM [dbo].[EMPLOYEE_GUARD] AS EMP FULL JOIN [dbo].[SERVICE_DETAIL] AS SERV ON EMP.ID = SERV.EMP_NAME FULL JOIN [dbo].[REFERENCE] AS REFER ON EMP.ID = REFER.EMP_NAME ", connection);
            System.Data.DataTable tb_data_fetch = new System.Data.DataTable();
            data_fetch.Fill(tb_data_fetch);
            screen_update_emp.DataSource = tb_data_fetch;
            screen_delete_emp.DataSource = tb_data_fetch;
        }

        private void fetch_data_emp_for_update()
        {
            try
            {
                update_prepaid_combo.Text = screen_update_emp.CurrentRow.Cells[1].Value.ToString();
                update_m_operate_combo.Text = screen_update_emp.CurrentRow.Cells[2].Value.ToString();
                update_m_add_combo.Text = screen_update_emp.CurrentRow.Cells[3].Value.ToString();
                update_name_text.Text = screen_update_emp.CurrentRow.Cells[4].Value.ToString();
                update_status_combo.Text = screen_update_emp.CurrentRow.Cells[5].Value.ToString();
                update_father_text.Text = screen_update_emp.CurrentRow.Cells[6].Value.ToString();
                update_present_add_richtext.Text = screen_update_emp.CurrentRow.Cells[7].Value.ToString();
                update_permanent_add_richtext.Text = screen_update_emp.CurrentRow.Cells[8].Value.ToString();
                update_cnic_text.Text = screen_update_emp.CurrentRow.Cells[9].Value.ToString();
                update_m_status_text.Text = screen_update_emp.CurrentRow.Cells[10].Value.ToString();
                update_kin_name_text.Text = screen_update_emp.CurrentRow.Cells[11].Value.ToString();
                update_kin_rel_text.Text = screen_update_emp.CurrentRow.Cells[12].Value.ToString();
                update_kin_add_richtext.Text = screen_update_emp.CurrentRow.Cells[13].Value.ToString();
                update_dob_date.Value = Convert.ToDateTime(screen_update_emp.CurrentRow.Cells[14].Value);
                update_religion_text.Text = screen_update_emp.CurrentRow.Cells[15].Value.ToString();
                update_edu_text.Text = screen_update_emp.CurrentRow.Cells[16].Value.ToString();
                update_sect_combo.Text = screen_update_emp.CurrentRow.Cells[17].Value.ToString();
                update_doi_date.Value = Convert.ToDateTime(screen_update_emp.CurrentRow.Cells[19].Value);
                update_salary_text.Text = screen_update_emp.CurrentRow.Cells[20].Value.ToString();
                update_serv_details_richtext.Text = screen_update_emp.CurrentRow.Cells[23].Value.ToString();
                update_prev_depart_text.Text = screen_update_emp.CurrentRow.Cells[24].Value.ToString();
                update_city_text.Text = screen_update_emp.CurrentRow.Cells[27].Value.ToString();
                update_work_text.Text = screen_update_emp.CurrentRow.Cells[28].Value.ToString();
                update_refer_name_text.Text = screen_update_emp.CurrentRow.Cells[29].Value.ToString();
                update_refer_contact_text.Text = screen_update_emp.CurrentRow.Cells[30].Value.ToString();
                update_refer_cnic_text.Text = screen_update_emp.CurrentRow.Cells[31].Value.ToString();
                string date = screen_update_emp.CurrentRow.Cells[25].Value.ToString();

                string date_to = screen_update_emp.CurrentRow.Cells[26].Value.ToString();
                if (date == "")
                {
                    update_from_date.Value = DateTime.Now;
                }
                else
                {
                    update_from_date.Value = Convert.ToDateTime(screen_delete_emp.CurrentRow.Cells[25].Value);
                }
                if (date_to == "")
                {
                    update_to_date.Value = DateTime.Now;
                }
                else
                {
                    update_to_date.Value = Convert.ToDateTime(screen_delete_emp.CurrentRow.Cells[26].Value);
                }


                if (screen_update_emp.CurrentRow.Cells[31].Value == null || screen_update_emp.CurrentRow.Cells[31].Value == DBNull.Value)
                {
                    update_refer_name_text.Text = "";
                }
                else
                {
                    update_refer_name_text.Text = screen_update_emp.CurrentRow.Cells[31].Value.ToString();

                }
                if (screen_update_emp.CurrentRow.Cells[32].Value == null || screen_update_emp.CurrentRow.Cells[32].Value == DBNull.Value)
                {
                    update_refer_contact_text.Text = "";
                }
                else
                {
                    update_refer_contact_text.Text = screen_update_emp.CurrentRow.Cells[32].Value.ToString();
                }
                if (screen_update_emp.CurrentRow.Cells[33].Value == null || screen_update_emp.CurrentRow.Cells[33].Value == DBNull.Value)
                {

                    update_refer_cnic_text.Text = "";
                }
                else
                {

                    update_refer_cnic_text.Text = screen_update_emp.CurrentRow.Cells[33].Value.ToString();
                }
                if (screen_update_emp.CurrentRow.Cells[24].Value == null || screen_update_emp.CurrentRow.Cells[24].Value == DBNull.Value)
                {
                    update_prev_depart_text.Text = "";
                }
                else
                {

                    update_prev_depart_text.Text = screen_update_emp.CurrentRow.Cells[24].Value.ToString();
                }
                if (screen_update_emp.CurrentRow.Cells[28].Value == null || screen_update_emp.CurrentRow.Cells[28].Value == DBNull.Value)
                {

                    update_work_text.Text = "";
                }
                else
                {

                    update_work_text.Text = screen_update_emp.CurrentRow.Cells[28].Value.ToString();
                }
                if (screen_update_emp.CurrentRow.Cells[27].Value == null || screen_update_emp.CurrentRow.Cells[27].Value == DBNull.Value)
                {

                    update_city_text.Text = "";
                }
                else
                {

                    update_city_text.Text = screen_update_emp.CurrentRow.Cells[27].Value.ToString();
                }
                //if (screen_update_emp.CurrentRow.Cells[25].Value == null || screen_update_emp.CurrentRow.Cells[25].Value == DBNull.Value)
                //{

                //    update_from_date.Value = DateTime.Now;
                //}
                //else
                //{

                //    update_from_date.Value = Convert.ToDateTime(screen_update_emp.CurrentRow.Cells[25].Value);
                //}
                //if (screen_update_emp.CurrentRow.Cells[26].Value == null || screen_update_emp.CurrentRow.Cells[26].Value == DBNull.Value)
                //{

                //    update_to_date.Value = DateTime.Now;
                //}
                //else
                //{

                //    update_to_date.Value = Convert.ToDateTime(screen_update_emp.CurrentRow.Cells[26].Value);
                //}
                if (screen_update_emp.CurrentRow.Cells[23].Value == null || screen_update_emp.CurrentRow.Cells[23].Value == DBNull.Value)
                {

                    update_serv_details_richtext.Text = "";
                }
                else
                {

                    update_serv_details_richtext.Text = screen_update_emp.CurrentRow.Cells[23].Value.ToString();
                }
                SqlDataAdapter update_emp_data_image = new SqlDataAdapter("SELECT [EMP_IMAGE] FROM [dbo].[EMPLOYEE_GUARD] WHERE ID = '" + screen_update_emp.CurrentRow.Cells[0].Value + "'", connection);
                System.Data.DataTable tb_update_emp_image = new System.Data.DataTable();
                update_emp_data_image.Fill(tb_update_emp_image);
                byte[] emp_image = (byte[])tb_update_emp_image.Rows[0][0];
                Bitmap emp_image_to_box;
                using (MemoryStream memoryStream = new MemoryStream(emp_image))
                {
                    emp_image_to_box = new Bitmap(memoryStream);
                }
                update_emp_pic.Image = emp_image_to_box;
                SqlDataAdapter FETCH_DATA = new SqlDataAdapter("SELECT [DOC_FOLDER] FROM [dbo].[DOCUMENTS] WHERE EMP_NAME = '" + screen_update_emp.CurrentRow.Cells[0].Value + "'", connection);
                System.Data.DataTable tb_data = new System.Data.DataTable();
                FETCH_DATA.Fill(tb_data);
                update_doc_text.Text = tb_data.Rows[0][0].ToString();
            }
            catch (Exception error)
            {
                MetroFramework.MetroMessageBox.Show(this, error.Message + "\n" + "No service details found for this record !!");
            }
        }

        private void fetch_data_emp_for_delete()
        {
            // fetching data to delete
            try
            {

                delete_prepaid_combo.Text = screen_delete_emp.CurrentRow.Cells[1].Value.ToString();
                delete_m_operate_combo.Text = screen_delete_emp.CurrentRow.Cells[2].Value.ToString();
                delete_m_add_combo.Text = screen_delete_emp.CurrentRow.Cells[3].Value.ToString();
                delete_name.Text = screen_delete_emp.CurrentRow.Cells[4].Value.ToString();
                delete_status_combo.Text = screen_delete_emp.CurrentRow.Cells[5].Value.ToString();
                delete_fname_text.Text = screen_delete_emp.CurrentRow.Cells[6].Value.ToString();
                delete_present_add_richtext.Text = screen_delete_emp.CurrentRow.Cells[7].Value.ToString();
                delete_permanent_add_richtext.Text = screen_delete_emp.CurrentRow.Cells[8].Value.ToString();
                delete_cnic_text.Text = screen_delete_emp.CurrentRow.Cells[9].Value.ToString();
                delete_m_status_text.Text = screen_delete_emp.CurrentRow.Cells[10].Value.ToString();
                delete_kin_name_text.Text = screen_delete_emp.CurrentRow.Cells[11].Value.ToString();
                delete_kin_rel_text.Text = screen_delete_emp.CurrentRow.Cells[12].Value.ToString();
                delete_kin_add_richtext.Text = screen_delete_emp.CurrentRow.Cells[13].Value.ToString();
                delete_dob_date.Value = Convert.ToDateTime(screen_delete_emp.CurrentRow.Cells[14].Value);
                delete_religion_text.Text = screen_delete_emp.CurrentRow.Cells[15].Value.ToString();
                delete_edu_text.Text = screen_delete_emp.CurrentRow.Cells[16].Value.ToString();
                delete_sect_combo.Text = screen_delete_emp.CurrentRow.Cells[17].Value.ToString();
                delete_doi_date.Value = Convert.ToDateTime(screen_delete_emp.CurrentRow.Cells[19].Value);
                delete_salary_text.Text = screen_delete_emp.CurrentRow.Cells[20].Value.ToString();
                delete_serv_details_richtext.Text = screen_delete_emp.CurrentRow.Cells[23].Value.ToString();
                delete_prev_depart_text.Text = screen_delete_emp.CurrentRow.Cells[24].Value.ToString();
                string date = screen_delete_emp.CurrentRow.Cells[25].Value.ToString();

                string date_to = screen_delete_emp.CurrentRow.Cells[26].Value.ToString();
                if (date == "")
                {
                    delete_from_text.Value = DateTime.Now;
                }
                else
                {
                    delete_from_text.Value = Convert.ToDateTime(screen_delete_emp.CurrentRow.Cells[25].Value);
                }
                if (date_to == "")
                {
                    delete_to_text.Value = DateTime.Now;
                }
                else
                {
                    delete_to_text.Value = Convert.ToDateTime(screen_delete_emp.CurrentRow.Cells[26].Value);
                }


                delete_city_text.Text = screen_delete_emp.CurrentRow.Cells[27].Value.ToString();
                delete_work_text.Text = screen_delete_emp.CurrentRow.Cells[28].Value.ToString();
                delete_refer_name_text.Text = screen_delete_emp.CurrentRow.Cells[29].Value.ToString();
                delete_refer_contact_text.Text = screen_delete_emp.CurrentRow.Cells[30].Value.ToString();
                delete_refer_cnic_text.Text = screen_delete_emp.CurrentRow.Cells[31].Value.ToString();
                if (screen_delete_emp.CurrentRow.Cells[31].Value == null || screen_delete_emp.CurrentRow.Cells[31].Value == DBNull.Value)
                {
                    delete_refer_name_text.Text = "";
                }
                else
                {
                    delete_refer_name_text.Text = screen_delete_emp.CurrentRow.Cells[31].Value.ToString();

                }
                if (screen_delete_emp.CurrentRow.Cells[32].Value == null || screen_delete_emp.CurrentRow.Cells[32].Value == DBNull.Value)
                {
                    delete_refer_contact_text.Text = "";
                }
                else
                {
                    delete_refer_contact_text.Text = screen_delete_emp.CurrentRow.Cells[32].Value.ToString();
                }
                if (screen_delete_emp.CurrentRow.Cells[33].Value == null || screen_delete_emp.CurrentRow.Cells[33].Value == DBNull.Value)
                {

                    delete_refer_cnic_text.Text = "";
                }
                else
                {

                    delete_refer_cnic_text.Text = screen_delete_emp.CurrentRow.Cells[33].Value.ToString();
                }
                if (screen_delete_emp.CurrentRow.Cells[24].Value == null || screen_delete_emp.CurrentRow.Cells[24].Value == DBNull.Value)
                {
                    delete_prev_depart_text.Text = "";
                }
                else
                {

                    delete_prev_depart_text.Text = screen_delete_emp.CurrentRow.Cells[24].Value.ToString();
                }
                if (screen_delete_emp.CurrentRow.Cells[28].Value == null || screen_delete_emp.CurrentRow.Cells[28].Value == DBNull.Value)
                {

                    delete_work_text.Text = "";
                }
                else
                {

                    delete_work_text.Text = screen_delete_emp.CurrentRow.Cells[28].Value.ToString();
                }
                if (screen_delete_emp.CurrentRow.Cells[27].Value == null || screen_delete_emp.CurrentRow.Cells[27].Value == DBNull.Value)
                {

                    delete_city_text.Text = "";
                }
                else
                {

                    delete_city_text.Text = screen_delete_emp.CurrentRow.Cells[27].Value.ToString();
                }
                //if (screen_delete_emp.CurrentRow.Cells[25].Value == null || screen_delete_emp.CurrentRow.Cells[25].Value == DBNull.Value)
                //{

                //    delete_from_text.Value = DateTime.Now;
                //}
                //else
                //{

                //    delete_from_text.Value = Convert.ToDateTime(screen_delete_emp.CurrentRow.Cells[25].Value);
                //}
                //if (screen_delete_emp.CurrentRow.Cells[26].Value == null || screen_delete_emp.CurrentRow.Cells[26].Value == DBNull.Value)
                //{

                //    delete_to_text.Value = DateTime.Now;
                //}
                //else
                //{

                //    delete_to_text.Value = Convert.ToDateTime(screen_delete_emp.CurrentRow.Cells[26].Value);
                //}
                if (screen_delete_emp.CurrentRow.Cells[23].Value == null || screen_delete_emp.CurrentRow.Cells[23].Value == DBNull.Value)
                {

                    delete_serv_details_richtext.Text = "";
                }
                else
                {

                    delete_serv_details_richtext.Text = screen_delete_emp.CurrentRow.Cells[23].Value.ToString();
                }
                SqlDataAdapter delete_emp_data_image = new SqlDataAdapter("SELECT [EMP_IMAGE] FROM [dbo].[EMPLOYEE_GUARD] WHERE ID = '" + screen_delete_emp.CurrentRow.Cells[0].Value + "'", connection);
                System.Data.DataTable tb_delete_emp_image = new System.Data.DataTable();
                delete_emp_data_image.Fill(tb_delete_emp_image);
                byte[] emp_image1 = (byte[])tb_delete_emp_image.Rows[0][0];
                Bitmap emp_image_to_box1;
                using (MemoryStream memoryStream = new MemoryStream(emp_image1))
                {
                    emp_image_to_box1 = new Bitmap(memoryStream);
                }
                delete_emp_pic.Image = emp_image_to_box1;
                SqlDataAdapter FETCH_DATA = new SqlDataAdapter("SELECT [DOC_FOLDER] FROM [dbo].[DOCUMENTS] WHERE EMP_NAME = '" + screen_delete_emp.CurrentRow.Cells[0].Value + "'", connection);
                System.Data.DataTable tb_data = new System.Data.DataTable();
                FETCH_DATA.Fill(tb_data);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }
        }

        private void screen_update_emp_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_emp.SelectedTab = update_emp_data_tab;
            fetch_data_emp_for_update();
        }

        private void update_emp_pic_btn_Click(object sender, EventArgs e)
        {
            browse_image_emp();
            update_emp_pic.Image = Image.FromFile(file_dialog.FileName);
        }


        private void update_emp_data_btn_Click(object sender, EventArgs e)
        {

            if (update_emp_pic.Image == null)
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_dob_date.Value == DateTime.Now)
            {
                MetroFramework.MetroMessageBox.Show(this, "Nobody born today can work here. Enter the correct date of birth !!", "Error");
            }
            else if (update_prepaid_combo.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_m_operate_combo.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_m_add_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_status_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_father_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_cnic_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_m_status_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_m_status_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_present_add_richtext.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_permanent_add_richtext.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_religion_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_edu_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_sect_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_salary_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_kin_name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_kin_rel_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_kin_rel_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else
            {
                if (File.Exists(file_dialog.FileName))
                {
                    SqlCommand update_emp_data = new SqlCommand("UPDATE [dbo].[EMPLOYEE_GUARD] SET [PREPAID_BY] = '" + update_prepaid_combo.Text + "',[MANAGER_OPERATION] = '" + update_m_operate_combo.Text + "', [MANAGER_ADMISSION] = '" + update_m_add_combo.Text + "', [NAME] = '" + update_name_text.Text + "', [STATUS] = '" + update_status_combo.Text + "', [FATHER_NAME] = '" + update_father_text.Text + "', [PRESENT_ADDRESS] = '" + update_present_add_richtext.Text + "', [PERMANENT_ADDRESS] = '" + update_permanent_add_richtext.Text + "', [CNIC] = '" + update_cnic_text.Text + "', [MARTIAL_STATUS] = '" + update_m_status_text.Text + "', [NEXT_OF_KIN_NAME] = '" + update_kin_name_text.Text + "', [NEXT_OF_KIN_RELATION] = '" + update_kin_rel_text.Text + "', [NEXT_OF_KIN_ADDRESS] = '" + update_kin_add_richtext.Text + "', [DOB] = '" + update_dob_date.Text + "', [RELIGION] = '" + update_religion_text.Text + "', [EDUCATION] = '" + update_edu_text.Text + "', [SECTION] = '" + update_sect_combo.Text + "', [EMP_IMAGE] = @update_img, [DATE_OF_ENROLLMENT] = '" + update_doi_date.Text + "', [SALARY] = '" + update_salary_text.Text + "' FROM [dbo].[EMPLOYEE_GUARD] E JOIN [dbo].[REFERENCE] R ON R.EMP_NAME = E.ID JOIN [dbo].[SERVICE_DETAIL] S ON S.EMP_NAME = E.ID WHERE E.ID = '" + screen_update_emp.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    FileStream fileStream = new FileStream(file_dialog.FileName, FileMode.Open, FileAccess.Read);
                    byte[] image_update = new byte[fileStream.Length];
                    fileStream.Read(image_update, 0, Convert.ToInt32(fileStream.Length));
                    fileStream.Close();
                    SqlParameter parameter = new SqlParameter("@update_img", SqlDbType.VarBinary, image_update.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image_update);
                    update_emp_data.Parameters.Add(parameter);
                    update_emp_data.ExecuteNonQuery();
                    connection.Close();
                    data_clear();
                    fetch_data_emp();
                    comboboxes_data();
                    mini_screen_emp.SelectedTab = update_refer;
                    update_id_emp();
                    fetch_data();
                }
                else
                {
                    SqlCommand update_emp_data = new SqlCommand("UPDATE [dbo].[EMPLOYEE_GUARD] SET [PREPAID_BY] = '" + update_prepaid_combo.Text + "',[MANAGER_OPERATION] = '" + update_m_operate_combo.Text + "', [MANAGER_ADMISSION] = '" + update_m_add_combo.Text + "', [NAME] = '" + update_name_text.Text + "', [STATUS] = '" + update_status_combo.Text + "', [FATHER_NAME] = '" + update_father_text.Text + "', [PRESENT_ADDRESS] = '" + update_present_add_richtext.Text + "', [PERMANENT_ADDRESS] = '" + update_permanent_add_richtext.Text + "', [CNIC] = '" + update_cnic_text.Text + "', [MARTIAL_STATUS] = '" + update_m_status_text.Text + "', [NEXT_OF_KIN_NAME] = '" + update_kin_name_text.Text + "', [NEXT_OF_KIN_RELATION] = '" + update_kin_rel_text.Text + "', [NEXT_OF_KIN_ADDRESS] = '" + update_kin_add_richtext.Text + "', [DOB] = '" + update_dob_date.Text + "', [RELIGION] = '" + update_religion_text.Text + "', [EDUCATION] = '" + update_edu_text.Text + "', [SECTION] = '" + update_sect_combo.Text + "', [DATE_OF_ENROLLMENT] = '" + update_doi_date.Text + "', [SALARY] = '" + update_salary_text.Text + "' FROM [dbo].[EMPLOYEE_GUARD] E JOIN [dbo].[REFERENCE] R ON R.EMP_NAME = E.ID JOIN [dbo].[SERVICE_DETAIL] S ON S.EMP_NAME = E.ID WHERE E.ID = '" + screen_update_emp.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_emp_data.ExecuteNonQuery();
                    connection.Close();
                    data_clear();
                    fetch_data_emp();
                    comboboxes_data();
                    mini_screen_emp.SelectedTab = update_refer;
                    update_id_emp();
                    fetch_data();
                }
            }
        }

        private void delete_emp_data_btn_Click(object sender, EventArgs e)
        {
            fetch_data_emp();
            fetch_data_emp();
            mini_screen_emp.SelectedTab = delete_refer;
            delete_id_emp();
            fetch_data();
        }


        private void fetch_data_com()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [ID],[COMPANY_NAME],[COMPANY_ADDRESS],[CITY],[CONTACT_PERSON_NAME],[CONTACT_PERSON_DESIGNATION],[CONTACT_PERSON_EMAIL],[CONTACT_PERSON_CELL_NUMBER],[COMPANY_PHONE_ONE],[COMPANY_PHONE_TWO],[REGISTRATION_DATE] FROM [dbo].[COMPANY] WHERE [ON_STATUS] = 1", connection);
            System.Data.DataTable tb_com_data = new System.Data.DataTable();
            fetch_data.Fill(tb_com_data);
            screen_delete_company.DataSource = tb_com_data;
            update_screen_company.DataSource = tb_com_data;

        }

        private void fetch_com_data_to_update()
        {
            try
            {
                update_company_name_text.Text = update_screen_company.CurrentRow.Cells[1].Value.ToString();
                update_company_add_text.Text = update_screen_company.CurrentRow.Cells[2].Value.ToString();
                update_company_city_text.Text = update_screen_company.CurrentRow.Cells[3].Value.ToString();
                update_company_person_name_text.Text = update_screen_company.CurrentRow.Cells[4].Value.ToString();
                update_company_person_designation_text.Text = update_screen_company.CurrentRow.Cells[5].Value.ToString();
                update_company_person_email_text.Text = update_screen_company.CurrentRow.Cells[6].Value.ToString();
                update_company_person_cell_text.Text = update_screen_company.CurrentRow.Cells[7].Value.ToString();
                update_company_phone_1_text.Text = update_screen_company.CurrentRow.Cells[8].Value.ToString();
                update_company_phone_2_text.Text = update_screen_company.CurrentRow.Cells[9].Value.ToString();
                update_company_register_date.Value = Convert.ToDateTime(update_screen_company.CurrentRow.Cells[10].Value);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }
        }

        private void com_data_clear()
        {
            company_name_text.Clear();
            company_add_text.Clear();
            contact_person_name_com_text.Clear();
            contact_person_email_com_text.Clear();
            contact_person_cell_com_text.Clear();
            company_phone_1_text.Clear();
            company_phone_2_text.Clear();
            update_company_name_text.Clear();
            update_company_add_text.Clear();
            update_company_person_name_text.Clear();
            update_company_person_email_text.Clear();
            update_company_person_cell_text.Clear();
            update_company_phone_1_text.Clear();
            update_company_phone_2_text.Clear();
            delete_company_name_text.Clear();
            delete_company_add_richtext.Clear();
            delete_company_city_text.Clear();
            delete_company_person_name_text.Clear();
            delete_company_person_designation_text.Clear();
            delete_company_person_mail_text.Clear();
            delete_company_person_cell_text.Clear();
            delete_company_phone_1_text.Clear();
            delete_company_phone_2_text.Clear();
        }


        private void fetch_com_data_to_delete()
        {
            delete_company_name_text.Text = screen_delete_company.CurrentRow.Cells[1].Value.ToString();
            delete_company_add_richtext.Text = screen_delete_company.CurrentRow.Cells[2].Value.ToString();
            delete_company_city_text.Text = screen_delete_company.CurrentRow.Cells[3].Value.ToString();
            delete_company_person_name_text.Text = screen_delete_company.CurrentRow.Cells[4].Value.ToString();
            delete_company_person_designation_text.Text = screen_delete_company.CurrentRow.Cells[5].Value.ToString();
            delete_company_person_mail_text.Text = screen_delete_company.CurrentRow.Cells[6].Value.ToString();
            delete_company_person_cell_text.Text = screen_delete_company.CurrentRow.Cells[7].Value.ToString();
            delete_company_phone_1_text.Text = screen_delete_company.CurrentRow.Cells[8].Value.ToString();
            delete_company_phone_2_text.Text = screen_delete_company.CurrentRow.Cells[9].Value.ToString();
            delete_company_register_date.Value = Convert.ToDateTime(screen_delete_company.CurrentRow.Cells[10].Value);

        }

        private void save_emp_data_btn_Click(object sender, EventArgs e)
        {
            if (refer_name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (refer_contact_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (refer_cnic_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (prev_depart_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (work_as_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (serv_city_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (five_years_serv_details_richtext.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (doc_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else
            {

                try
                {
                    SqlCommand add_refer_emp_data = new SqlCommand("INSERT INTO [dbo].[REFERENCE] ([EMP_NAME],[REFER_NAME],[REFER_CONTACT],[REFER_CNIC]) VALUES ('" + emp_id + "','" + refer_name_text.Text + "','" + refer_contact_text.Text + "','" + refer_cnic_text.Text + "')", connection);
                    connection.Open();
                    add_refer_emp_data.ExecuteNonQuery();
                    connection.Close();
                    SqlCommand add_serv_details_emp_data = new SqlCommand("INSERT INTO [dbo].[SERVICE_DETAIL]([EMP_NAME],[FIVE_YEAR_SERVICE_DETAIL],[PREVIOUS_DEPARTMENT],[FROM_TIME],[TO_TIME],[CITY],[WORK_AS]) VALUES('" + emp_id + "','" + five_years_serv_details_richtext.Text + "','" + prev_depart_text.Text + "','" + serv_from_date.Text + "','" + serve_to_date.Text + "','" + serv_city_text.Text + "','" + work_as_text.Text + "')", connection);
                    connection.Open();
                    add_serv_details_emp_data.ExecuteNonQuery();
                    connection.Close();
                    fetch_data_emp();
                    comboboxes_data();
                    SqlCommand add_data = new SqlCommand("INSERT INTO [dbo].[DOCUMENTS] ([EMP_NAME],[DOC_FOLDER]) VALUES('" + emp_id + "','" + doc_text.Text + "')", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record updated !!", "Success");
                    mini_screen_emp.SelectedTab = new_emp_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }



        private void update_emp_button_Click(object sender, EventArgs e)
        {
            mini_screen_emp.SelectedTab = update_emp_tab;
            data_clear_all();
            fetch_data();
        }

        private void delete_emp_button_Click(object sender, EventArgs e)
        {
            mini_screen_emp.SelectedTab = delete_emp_tab;
            data_clear_all();
            fetch_data();
        }

        private void new_emp_button_Click(object sender, EventArgs e)
        {
            mini_screen_emp.SelectedTab = new_emp_tab;
            data_clear_all();
            fetch_data();
        }

        private void emp_basic_data_add_btn_Click_1(object sender, EventArgs e)
        {
            if (prepaid_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (m_operate_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");
            }
            else if (m_add_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (father_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (cnic_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (m_status_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (present_add_richtext.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (permanent_add_richtext.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (religion_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (edu_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (salary_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (kin_name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (kin_rel_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (kin_rel_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the detailsssss !!", "Error");

            }
            else if (dob_date.Value == DateTime.Now)
            {
                MetroFramework.MetroMessageBox.Show(this, "Nobody born today can work here. Enter the correct date of birth !!", "Error");
            }
            else
            {

                try
                {
                    // function to insert data in employee database
                    if (File.Exists(file_dialog.FileName))
                    {
                        SqlCommand add_emp_data = new SqlCommand("INSERT INTO [dbo].[EMPLOYEE_GUARD]([PREPAID_BY],[MANAGER_OPERATION],[MANAGER_ADMISSION],[NAME],[STATUS],[FATHER_NAME],[PRESENT_ADDRESS],[PERMANENT_ADDRESS],[CNIC],[MARTIAL_STATUS],[NEXT_OF_KIN_NAME],[NEXT_OF_KIN_RELATION],[NEXT_OF_KIN_ADDRESS],[DOB],[RELIGION],[EDUCATION],[SECTION],[EMP_IMAGE],[DATE_OF_ENROLLMENT],[SALARY],[ON_STATUS]) VALUES ('" + prepaid_combo.Text + "','" + m_operate_combo.Text + "','" + m_add_combo.Text + "','" + name_text.Text + "','" + status_combo.Text + "','" + father_text.Text + "','" + present_add_richtext.Text + "','" + permanent_add_richtext.Text + "','" + cnic_text.Text + "','" + m_status_text.Text + "','" + kin_name_text.Text + "','" + kin_rel_text.Text + "','" + kin_add_richtext.Text + "','" + dob_date.Text + "','" + religion_text.Text + "','" + edu_text.Text + "','" + sect_combo.Text + "',@img,'" + doi_date.Text + "','" + salary_text.Text + "','" + 1 + "')", connection);
                        connection.Open();
                        FileStream fileStream = new FileStream(file_dialog.FileName, FileMode.Open, FileAccess.Read);
                        byte[] image_upload = new byte[fileStream.Length];
                        fileStream.Read(image_upload, 0, Convert.ToInt32(fileStream.Length));
                        fileStream.Close();
                        SqlParameter parameter = new SqlParameter("@img", SqlDbType.VarBinary, image_upload.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image_upload);
                        add_emp_data.Parameters.Add(parameter);
                        add_emp_data.ExecuteNonQuery();
                        connection.Close();
                        MetroFramework.MetroMessageBox.Show(this, "Record updated !!", "Success");
                        comboboxes_data();
                        mini_screen_emp.SelectedTab = reference_emp_tab;
                        data_clear();
                        emp_id_fetch_for_more_details();
                        fetch_data();
                    }
                    else
                    {
                        MetroFramework.MetroMessageBox.Show(this, "Please insert your picture !!", "Error");
                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error");
                }
            }
        }

        private void update_refer_data_btn_Click(object sender, EventArgs e)
        {
            if (update_refer_name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_refer_contact_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_refer_cnic_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_prev_depart_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_work_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_city_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_serv_details_richtext.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_doc_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {

                try
                {
                    SqlCommand update_serv_data = new SqlCommand("UPDATE [dbo].[SERVICE_DETAIL] SET [FIVE_YEAR_SERVICE_DETAIL] = '" + update_serv_details_richtext.Text + "',[PREVIOUS_DEPARTMENT] = '" + update_prev_depart_text.Text + "',[FROM_TIME] = '" + update_from_date.Text + "',[TO_TIME] = '" + update_to_date.Text + "',[CITY] = '" + update_city_text.Text + "',[WORK_AS] = '" + update_work_text.Text + "' FROM [dbo].[SERVICE_DETAIL] S JOIN [dbo].[EMPLOYEE_GUARD] E ON E.ID = S.EMP_NAME JOIN [dbo].[REFERENCE] R ON E.ID = R.EMP_NAME WHERE E.ID = '" + screen_update_emp.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_serv_data.ExecuteNonQuery();
                    connection.Close();

                    SqlCommand update_refer_data = new SqlCommand("UPDATE [dbo].[REFERENCE] SET[REFER_NAME] = '" + update_refer_name_text.Text + "',[REFER_CONTACT] = '" + update_refer_contact_text.Text + "',[REFER_CNIC] = '" + update_refer_cnic_text.Text + "' FROM [dbo].[REFERENCE] R JOIN [dbo].[EMPLOYEE_GUARD] E ON E.ID = R.EMP_NAME JOIN [dbo].[SERVICE_DETAIL] S ON S.EMP_NAME = E.ID WHERE E.ID = '" + screen_update_emp.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_refer_data.ExecuteNonQuery();
                    connection.Close();

                    SqlCommand update_data = new SqlCommand("UPDATE [dbo].[DOCUMENTS] SET [DOC_FOLDER] = '" + update_doc_text.Text + "' WHERE EMP_NAME = " + update_emp_id + "", connection);
                    connection.Open();
                    update_data.ExecuteNonQuery();
                    connection.Close();


                    data_clear();
                    MetroFramework.MetroMessageBox.Show(this, "Record updated !!", "Success");
                    comboboxes_data();
                    mini_screen_emp.SelectedTab = update_emp_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void delete_emp_full_data_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                try
                {
                    SqlCommand delete_emp_data = new SqlCommand("UPDATE [dbo].[EMPLOYEE_GUARD] SET [ON_STATUS] = 0 WHERE [ID] = '" + screen_delete_emp.CurrentRow.Cells[0].Value + "' ", connection);
                    connection.Open();
                    delete_emp_data.ExecuteNonQuery();
                    connection.Close();

                    SqlCommand delete_data = new SqlCommand("UPDATE [dbo].[DOCUMENTS] SET [ON_STATUS] = 0 WHERE EMP_NAME = '" + screen_delete_emp.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    delete_data.ExecuteNonQuery();
                    connection.Close();

                    MetroFramework.MetroMessageBox.Show(this, "Record deleted", "Success");
                    comboboxes_data();
                    fetch_data();
                    mini_screen_emp.SelectedTab = delete_emp_tab;

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error");
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");
            }
            // deleting employee data

            data_clear();
            fetch_data();

        }


        private void new_app_button_Click(object sender, EventArgs e)
        {
            mini_screen_app.SelectedTab = new_app_tab;
            fetch_data_app();
            fetch_data();
        }

        private void update_app_button_Click(object sender, EventArgs e)
        {
            mini_screen_app.SelectedTab = update_app_tab;
            fetch_data_app();
            fetch_data();

        }

        private void delete_app_button_Click(object sender, EventArgs e)
        {
            mini_screen_app.SelectedTab = delete_app_tab;
            fetch_data_app();
            fetch_data();

        }

        private void screen_delete_app_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_app.SelectedTab = delete_app_data;
            fetch_data_to_delete();
            fetch_data();
        }

        private void screen_update_app_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_app.SelectedTab = update_app_data;
            fetch_data_to_update();
            fetch_data();
        }

        private void new_com_button_Click_1(object sender, EventArgs e)
        {
            mini_screen_comp.SelectedTab = new_com_tab;
            fetch_data_com();
            fetch_data();
        }

        private void update_com_button_Click(object sender, EventArgs e)
        {
            mini_screen_comp.SelectedTab = update_com_tab;
            fetch_data_com();
            fetch_data();
        }

        private void delete_com_button_Click(object sender, EventArgs e)
        {
            mini_screen_comp.SelectedTab = delete_com_tab;
            fetch_data_com();
            fetch_data();
        }

        private void reporting_com_button_Click(object sender, EventArgs e)
        {
            mini_screen_comp.SelectedTab = reporting_com_tab;
            fetch_data_com();
            fetch_data();
        }

        private void new_expense_btn_Click(object sender, EventArgs e)
        {
            if (items_expense_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill all the fields !!", "Error");
            }
            else if (payment_mode_expense.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill all the fields !!", "Error");

            }
            else
            {
                try
                {
                    SqlCommand add_data = new SqlCommand("INSERT INTO [dbo].[EXPENSE] ([ITEMS],[DATE],[AMOUNT],[PAYMENT_TYPE],[STATUS]) VALUES ('" + items_expense_text.Text + "','" + date_expense_date.Text + "','" + amount_expense_text.Text + "','" + payment_mode_expense.Text + "','" + 1 + "')", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record updated", "Success");
                    data_clear_expense();
                    fetch_expense_data();
                    comboboxes_data();
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

        }

        private void fetch_expense_data()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [ID],[ITEMS],[DATE],[AMOUNT],[PAYMENT_TYPE] FROM [dbo].[EXPENSE] WHERE [STATUS] = 1", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_expense_screen.DataSource = tb_data;
            expense_delete_screen.DataSource = tb_data;
        }

        private void expense_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                mini_screen_expense.SelectedTab = update_expense_data_tab;
                update_items_text.Text = update_expense_screen.CurrentRow.Cells[1].Value.ToString();
                update_date_expense.Value = Convert.ToDateTime(update_expense_screen.CurrentRow.Cells[2].Value);
                update_amount_expense_text.Text = update_expense_screen.CurrentRow.Cells[3].Value.ToString();
                update_payment_mode_expense.Text = update_expense_screen.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void data_clear_expense()
        {
            items_expense_text.Clear();
            amount_expense_text.Clear();
            update_items_text.Clear();
            update_amount_expense_text.Clear();
            delete_items_expense.Clear();
            delete_amount_expense_text.Clear();
        }

        private void update_expense_btn_Click(object sender, EventArgs e)
        {
            if (update_items_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill all the fields !!", "Error");
            }
            else if (update_payment_mode_expense.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill all the fields !!", "Error");

            }
            else
            {

                try
                {
                    SqlCommand update_data = new SqlCommand("UPDATE [dbo].[EXPENSE] SET [ITEMS] = '" + update_items_text.Text + "',[DATE] = '" + update_date_expense.Text + "',[AMOUNT] = '" + update_amount_expense_text.Text + "',[PAYMENT_TYPE] = '" + update_payment_mode_expense.Text + "' WHERE ID = '" + update_expense_screen.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record updated", "Success");
                    data_clear_expense();
                    fetch_expense_data();
                    comboboxes_data();
                    mini_screen_expense.SelectedTab = update_expense_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void expense_delete_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                mini_screen_expense.SelectedTab = delete_expense_data_tab;
                delete_items_expense.Text = expense_delete_screen.CurrentRow.Cells[1].Value.ToString();
                delete_date_expense.Value = Convert.ToDateTime(expense_delete_screen.CurrentRow.Cells[2].Value);
                delete_amount_expense_text.Text = expense_delete_screen.CurrentRow.Cells[3].Value.ToString();
                delete_payment_mode_text.Text = expense_delete_screen.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static bool allow_user = false;


        private void delete_expense_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                try
                {
                    SqlCommand update_data = new SqlCommand("UPDATE [dbo].[EXPENSE] SET [STATUS] = 0 WHERE ID = '" + expense_delete_screen.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record deleted", "Success");
                    fetch_expense_data();
                    data_clear_expense();
                    comboboxes_data();
                    mini_screen_expense.SelectedTab = delete_expense_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");
            }

        }



        private void new_exp_btn_Click(object sender, EventArgs e)
        {
            mini_screen_expense.SelectedTab = new_expense_tab;
            fetch_expense_data();
            data_clear_all();
            fetch_data();
        }

        private void update_exp_btn_Click(object sender, EventArgs e)
        {
            mini_screen_expense.SelectedTab = update_expense_tab;
            fetch_expense_data();
            data_clear_all();
            fetch_data();

        }

        private void delete_exp_btn_Click(object sender, EventArgs e)
        {
            mini_screen_expense.SelectedTab = delete_expense_tab;
            fetch_expense_data();
            data_clear_all();
            fetch_data();
        }

        private void fetch_data_app()
        {
            SqlDataAdapter data_fetch = new SqlDataAdapter("SELECT [ID],[COMPANY],[COMPANY_BRANCH],[NO_OF_PERSON],[FROM_TIME],[TO_TIME],[TIMINGS],[AMOUNT_PER_PERSON],[IN_TIME],[OUT_TIME] FROM [dbo].[APPPOINTMENT] WHERE [ON_STATUS] = 1", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            data_fetch.Fill(tb_data);
            screen_update_app.DataSource = tb_data;
            screen_delete_app.DataSource = tb_data;
        }

        private void fetch_data_to_update()
        {
            update_com_of_app_combo.Text = screen_update_app.CurrentRow.Cells[1].Value.ToString();
            update_com_br_of_app_combo.Text = screen_update_app.CurrentRow.Cells[2].Value.ToString();
            update_emp_of_app_combo.Text = screen_update_app.CurrentRow.Cells[3].Value.ToString();
            update_amount_person_text.Text = screen_update_app.CurrentRow.Cells[7].Value.ToString();
            update_from_time_date.Value = Convert.ToDateTime(screen_update_app.CurrentRow.Cells[4].Value);
            update_to_time_date.Value = Convert.ToDateTime(screen_update_app.CurrentRow.Cells[5].Value);
            update_app_timings.Text = screen_update_app.CurrentRow.Cells[6].Value.ToString();
            update_in_time_app_text.Text = screen_update_app.CurrentRow.Cells[8].Value.ToString();
            update_out_time_app_text.Text = screen_update_app.CurrentRow.Cells[9].Value.ToString();
        }

        private void fetch_data_to_delete()
        {
            delete_com_of_app_combo.Text = screen_delete_app.CurrentRow.Cells[1].Value.ToString();
            delete_com_br_of_app_combo.Text = screen_delete_app.CurrentRow.Cells[2].Value.ToString();
            delete_emp_of_app_combo.Text = screen_delete_app.CurrentRow.Cells[3].Value.ToString();
            delete_amount_person_text.Text = screen_delete_app.CurrentRow.Cells[4].Value.ToString();
            delete_from_app_date.Value = Convert.ToDateTime(screen_delete_app.CurrentRow.Cells[5].Value);
            delete_to_app_date.Value = Convert.ToDateTime(screen_delete_app.CurrentRow.Cells[6].Value);
            delete_timing_text.Text = screen_delete_app.CurrentRow.Cells[7].Value.ToString();
            delete_in_time_app_text.Text = screen_delete_app.CurrentRow.Cells[8].Value.ToString();
            delete_out_time_app_text.Text = screen_delete_app.CurrentRow.Cells[9].Value.ToString();
        }

        private void data_clear_app()
        {
            amount_person_text.Clear();
            in_time_app_text.Clear();
            out_time_app_text.Clear();
            update_amount_person_text.Clear();
            update_in_time_app_text.Clear();
            update_out_time_app_text.Clear();
            delete_amount_person_text.Clear();
            delete_timing_text.Clear();
            delete_in_time_app_text.Clear();
            delete_out_time_app_text.Clear();
        }

        private void new_app_button_Click_1(object sender, EventArgs e)
        {
            mini_screen_app.SelectedTab = new_app_tab;
            fetch_data_app();
            data_clear_all();
            fetch_data();
        }

        private void update_app_button_Click_1(object sender, EventArgs e)
        {
            mini_screen_app.SelectedTab = update_app_tab;
            fetch_data_app();
            data_clear_all();
            fetch_data();
        }

        private void delete_app_button_Click_1(object sender, EventArgs e)
        {

            mini_screen_app.SelectedTab = delete_app_tab;
            fetch_data_app();
            fetch_data();
            data_clear_all();
        }

        private void fetch_data_salary()
        {
            SqlDataAdapter data_fetch = new SqlDataAdapter("SELECT [ID],[EMPLOYEE],[FROM_TIME],[TO_TIME],[SALARY],[OVERTIME],[TOTAL_AMOUNT] FROM [dbo].[SALARY]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            data_fetch.Fill(tb_data);
            update_salary_screen.DataSource = tb_data;
            delete_salary_screen.DataSource = tb_data;
        }

        private void new_payment_btn_Click(object sender, EventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = new_payment_tab;
            fetch_data_payment();
            data_clear_all();
            fetch_data();
        }

        private void update_payment_btn_Click(object sender, EventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = update_payment_tab;
            fetch_data_payment();
            data_clear_all();
            fetch_data();
        }

        private void new_salary_btn_Click(object sender, EventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = new_salary_tab;
            fetch_data_salary();
            data_clear_all();
            fetch_data();
        }

        private void update_salary_btn_Click(object sender, EventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = update_salary_tab;
            fetch_data_salary();
            data_clear_all();
            fetch_data();
        }

        private void update_app_btn_Click(object sender, EventArgs e)
        {
            if (update_com_of_app_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_com_br_of_app_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_emp_of_app_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_amount_person_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_app_timings.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_in_time_app_text.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_out_time_app_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                try
                {
                    SqlCommand update_data = new SqlCommand("UPDATE [dbo].[APPPOINTMENT] SET [COMPANY] = '" + update_com_of_app_combo.Text + "',[COMPANY_BRANCH] = '" + update_com_br_of_app_combo.Text + "',[NO_OF_PERSON] = '" + update_amount_person_text.Text + "',[FROM_TIME] = '" + update_from_time_date.Text + "',[TO_TIME] = '" + update_to_time_date.Text + "',[TIMINGS] = '" + update_app_timings.Text + "',[AMOUNT_PER_PERSON] = '" + update_amount_person_text.Text + "',[IN_TIME] = '" + update_in_time_app_text.Text + "',[OUT_TIME] = '" + update_out_time_app_text.Text + "' WHERE ID = '" + screen_update_app.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record updated", "Success");
                    data_clear_app();
                    fetch_data_app();
                    comboboxes_data();
                    mini_screen_app.SelectedTab = update_app_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void delete_app_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                try
                {
                    SqlCommand delete_data = new SqlCommand("UPDATE [dbo].[APPPOINTMENT] SET [ON_STATUS] = 0 WHERE ID = '" + screen_delete_app.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    delete_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record updated", "Success");
                    data_clear_app();
                    fetch_data_app();
                    comboboxes_data();
                    mini_screen_app.SelectedTab = delete_app_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");
            }

        }

        private void fetch_data_payment()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [ID],[DATE],[COMPANY],[COMPANY_BRANCH],[BRANCH_OFFICER],[AMOUNT_RATE],[AMOUNT_QUANTITY],[GST_RATE],[GST_AMOUNT],[GST_WITHHELD_RATE],[GST_WITHHELD_AMOUNT],[REMARKS] FROM [dbo].[PAYMENTS] WHERE [STATUS] = 1", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_payment_screen.DataSource = tb_data;
            delete_payment_screen.DataSource = tb_data;
        }

        private void fetch_data_for_payment()
        {
            update_date_for_payment.Text = update_payment_screen.CurrentRow.Cells[1].Value.ToString();
            update_com_for_payment.Text = update_payment_screen.CurrentRow.Cells[2].Value.ToString();
            update_com_br_for_payment.Text = update_payment_screen.CurrentRow.Cells[3].Value.ToString();
            update_br_off_for_payment.Text = update_payment_screen.CurrentRow.Cells[4].Value.ToString();
            update_am_rate_for_payment.Text = update_payment_screen.CurrentRow.Cells[5].Value.ToString();
            update_am_quantity_for_payment.Text = update_payment_screen.CurrentRow.Cells[6].Value.ToString();
            update_gst_rate_for_payment.Text = update_payment_screen.CurrentRow.Cells[7].Value.ToString();
            update_gst_amount_for_payment.Text = update_payment_screen.CurrentRow.Cells[8].Value.ToString();
            update_gst_w_rate_for_payment.Text = update_payment_screen.CurrentRow.Cells[9].Value.ToString();
            update_gst_w_amount_for_payment.Text = update_payment_screen.CurrentRow.Cells[10].Value.ToString();
            update_remarks_for_payment.Text = update_payment_screen.CurrentRow.Cells[11].Value.ToString();

            //update_com_br_payment_combo.Text = update_payment_screen.CurrentRow.Cells[1].Value.ToString();
            //update_payment_date.Value = Convert.ToDateTime(update_payment_screen.CurrentRow.Cells[2].Value.ToString());
            //update_amount_due_payment_text.Text = update_payment_screen.CurrentRow.Cells[4].Value.ToString();
            //update_amount_receive_payment_text.Text = update_payment_screen.CurrentRow.Cells[5].Value.ToString();
            //update_total_amount_payment_text.Text = update_payment_screen.CurrentRow.Cells[3].Value.ToString();
            //update_mode_payment_text.Text = update_payment_screen.CurrentRow.Cells[6].Value.ToString();

        }

        private void fetch_data_for_salary()
        {
            update_emp_salary_combo.Text = update_salary_screen.CurrentRow.Cells[1].Value.ToString();
            update_from_salary_date.Value = Convert.ToDateTime(update_salary_screen.CurrentRow.Cells[2].Value.ToString());
            update_to_salary_date.Value = Convert.ToDateTime(update_salary_screen.CurrentRow.Cells[3].Value.ToString());
            update_salary_emp_text.Text = update_salary_screen.CurrentRow.Cells[4].Value.ToString();
            update_overtime_salary_text.Text = update_salary_screen.CurrentRow.Cells[5].Value.ToString();
            update_total_am_salary_text.Text = update_salary_screen.CurrentRow.Cells[6].Value.ToString();
        }

        private void data_clear_payment()
        {
            com_emp_txt.Clear();
            amount_rate.Clear();
            amount_qty.Clear();
            gst_rate.Clear();
            gst_amount.Clear();
            gst_w_rate.Clear();
            gst_w_amount.Clear();
            remarks_payment.Clear();


            //update_amount_due_payment_text.Clear();
            //update_amount_receive_payment_text.Clear();
            //update_total_amount_payment_text.Clear();
            //delete_amount_due_payment_text.Clear();
            //delete_amount_receive_payment_text.Clear();
            //delete_amount_remain_payment_text.Clear();

            ////update_total_amount_payment_text.Enabled = true;
            //delete_amount_remain_payment_text.Clear();
            //delete_amount_remain_payment_text.Enabled = true;

        }

        private void data_clear_salary()
        {
            salary_emp_text.Clear();
            salary_emp_text.Enabled = true;
            overtime_salary_text.Clear();
            overtime_salary_text.Enabled = true;
            total_am_salary_text.Clear();
            total_am_salary_text.Enabled = true;
            update_salary_emp_text.Clear();
            update_overtime_salary_text.Clear();
            update_total_am_salary_text.Clear();
            delete_salary_sal_text.Clear();
            delete_overtime_text.Clear();
            delete_total_am_salary_text.Clear();
            update_overtime_salary_text.Enabled = true;
            update_salary_emp_text.Enabled = true;
            update_total_am_salary_text.Enabled = true;
        }

        private void new_pay_btn_Click(object sender, EventArgs e)
        {
            if (date_payment_date.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (company_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (com_br_payment_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (com_emp_txt.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (amount_rate.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }

            else if (amount_qty.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }

            else if (gst_rate.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }

            else if (gst_amount.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }

            else if (gst_w_rate.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }

            else if (gst_w_amount.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }

            else if (remarks_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                try
                {
                    SqlCommand add_data = new SqlCommand("INSERT INTO[dbo].[PAYMENTS] ([DATE],[COMPANY],[COMPANY_BRANCH],[BRANCH_OFFICER],[AMOUNT_RATE],[AMOUNT_QUANTITY],[GST_RATE],[GST_AMOUNT],[GST_WITHHELD_RATE],[GST_WITHHELD_AMOUNT],[REMARKS],[STATUS]) VALUES('" + date_payment_date.Value + "', '" + company_combo.Text + "', '" + com_br_payment_combo.Text + "', '" + com_emp_txt.Text + "', '" + amount_rate.Text + "', '" + amount_qty.Text + "', '" + gst_rate.Text + "', '" + gst_amount.Text + "', '" + gst_w_rate.Text + "', '" + gst_w_amount.Text + "','" + remarks_payment.Text + "','" + 1 + "')", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record updated", "Success");
                    data_clear_payment();
                    fetch_data_payment();
                    comboboxes_data();
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void update_payment_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = update_payment_data_tab;
            bill_no.Text = "Bill # " + update_payment_screen.CurrentRow.Cells[0].Value.ToString();
            fetch_data_for_payment();
            fetch_data();
        }

        private void update_pay_btn_Click(object sender, EventArgs e)
        {
            if (update_com_br_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_date_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_com_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_br_off_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_am_rate_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_am_quantity_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_gst_rate_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_gst_amount_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_gst_w_rate_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_gst_w_amount_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_remarks_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                try
                {
                    //SqlCommand update_data = new SqlCommand("UPDATE [dbo].[PAYMENTS] SET [COMPANY_BRANCH] = '" + update_com_br_payment_combo.Text + "',[DATE] = '" + update_payment_date.Text + "',[TOTAL_AMOUNT] = '" + update_total_amount_payment_text.Text + "',[AMOUNT_DUE] = '" + update_amount_due_payment_text.Text + "',[AMOUNT_RECEIVE] = '" + update_amount_receive_payment_text.Text + "',[PAYMENT_MODE] = '" + update_mode_payment_text.Text + "' WHERE ID = '" + update_payment_screen.CurrentRow.Cells[0].Value + "'", connection);
                    //connection.Open();
                    //update_data.ExecuteNonQuery();
                    //connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record updated", "Success");
                    data_clear_payment();
                    comboboxes_data();
                    fetch_data_payment();
                    mini_screen_payment_salaries.SelectedTab = update_payment_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }


        private void new_sal_btn_Click(object sender, EventArgs e)
        {
            if (new_emp_salary_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (salary_emp_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (overtime_salary_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (total_am_salary_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                try
                {
                    SqlCommand add_data = new SqlCommand("INSERT INTO [dbo].[SALARY] ([EMPLOYEE],[FROM_TIME],[TO_TIME],[SALARY],[OVERTIME],[TOTAL_AMOUNT]) VALUES ('" + new_emp_salary_combo.Text + "','" + from_salary_date.Text + "','" + to_salary_date.Text + "','" + salary_emp_text.Text + "','" + overtime_salary_text.Text + "','" + total_am_salary_text.Text + "')", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record updated", "Success");
                    data_clear_salary();
                    fetch_data_salary();
                    comboboxes_data();
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void update_salary_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = update_salary_data_tab;
            fetch_data_for_salary();
            fetch_data();
        }

        private void update_sal_btn_Click(object sender, EventArgs e)
        {
            if (update_emp_salary_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_salary_emp_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_overtime_salary_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_total_am_salary_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                try
                {
                    SqlCommand update_data = new SqlCommand("UPDATE [dbo].[SALARY] SET [EMPLOYEE] = '" + update_emp_salary_combo.Text + "',[FROM_TIME] = '" + update_from_salary_date.Text + "',[TO_TIME] = '" + update_to_salary_date.Text + "',[SALARY] = '" + update_salary_emp_text.Text + "',[OVERTIME] = '" + update_overtime_salary_text.Text + "',[TOTAL_AMOUNT] = '" + update_total_am_salary_text.Text + "' WHERE ID = '" + update_salary_screen.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record updated", "Success");
                    data_clear_salary();
                    fetch_data_salary();
                    comboboxes_data();
                    fetch_data();
                    mini_screen_payment_salaries.SelectedTab = update_salary_tab;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void delete_pay_btn_Click(object sender, EventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = delete_payment_tab;
            fetch_data_payment();
            data_clear_all();
            fetch_data();
        }

        private void delete_payment_btn_Click(object sender, EventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = delete_salary_tab;
            fetch_data_payment();
            data_clear_all();
            fetch_data();
        }

        private void fetch_salary_data_to_delete()
        {
            delete_emp_salary_combo.Text = delete_salary_screen.CurrentRow.Cells[1].Value.ToString();

            delete_from_salary_date.Value = Convert.ToDateTime(delete_salary_screen.CurrentRow.Cells[2].Value.ToString());
            delete_to_salary_date.Value = Convert.ToDateTime(delete_salary_screen.CurrentRow.Cells[3].Value.ToString());
            delete_salary_sal_text.Text = delete_salary_screen.CurrentRow.Cells[4].Value.ToString();
            delete_overtime_text.Text = delete_salary_screen.CurrentRow.Cells[5].Value.ToString();
            delete_total_am_salary_text.Text = delete_salary_screen.CurrentRow.Cells[6].Value.ToString();
        }

        private void delete_salary_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = delete_salary_data_tab;
            fetch_salary_data_to_delete();
            fetch_data();

        }

        private void delete_salary_data_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                try
                {
                    SqlCommand delete_data = new SqlCommand("DELETE FROM [dbo].[SALARY] WHERE ID = '" + delete_salary_screen.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    delete_data.ExecuteNonQuery();
                    connection.Close();
                    comboboxes_data();
                    MetroFramework.MetroMessageBox.Show(this, "record deleted", "Success");
                    fetch_data_salary();
                    data_clear_salary();
                    mini_screen_payment_salaries.SelectedTab = delete_salary_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");
            }

        }

        private void fetch_payment_data_to_delete()
        {
            delete_date_for_payment.Text = delete_payment_screen.CurrentRow.Cells[1].Value.ToString();
            delete_com_for_payment.Text = delete_payment_screen.CurrentRow.Cells[2].Value.ToString();
            delete_com_br_for_payment.Text = delete_payment_screen.CurrentRow.Cells[3].Value.ToString();
            delete_br_off_for_payment.Text = delete_payment_screen.CurrentRow.Cells[4].Value.ToString();
            delete_am_rate_for_payment.Text = delete_payment_screen.CurrentRow.Cells[5].Value.ToString();
            delete_am_quantity_for_payment.Text = delete_payment_screen.CurrentRow.Cells[6].Value.ToString();
            delete_gst_rate_for_payment.Text = delete_payment_screen.CurrentRow.Cells[7].Value.ToString();
            delete_gst_amount_for_payment.Text = delete_payment_screen.CurrentRow.Cells[8].Value.ToString();
            delete_gst_w_rate_for_payment.Text = delete_payment_screen.CurrentRow.Cells[9].Value.ToString();
            delete_gst_w_amount_for_payment.Text = delete_payment_screen.CurrentRow.Cells[10].Value.ToString();
            delete_remarks_for_payment.Text = delete_payment_screen.CurrentRow.Cells[11].Value.ToString();

            delete_tax.Text = "Rs " + (Convert.ToDecimal(delete_am_rate_for_payment.Text) * Convert.ToDecimal(delete_am_quantity_for_payment.Text)).ToString();
            delete_gst.Text = "Rs " + (Convert.ToDecimal(delete_gst_rate_for_payment.Text) * Convert.ToDecimal(delete_gst_amount_for_payment.Text)).ToString();
            delete_gst_w.Text = "Rs " + (Convert.ToDecimal(delete_gst_w_rate_for_payment.Text) * Convert.ToDecimal(delete_gst_w_amount_for_payment.Text)).ToString();

            delete_total.Text = "Rs " + ((Convert.ToDecimal(delete_gst_w_rate_for_payment.Text) * Convert.ToDecimal(delete_gst_w_amount_for_payment.Text)) + (Convert.ToDecimal(delete_gst_rate_for_payment.Text) * Convert.ToDecimal(delete_gst_amount_for_payment.Text)) + (Convert.ToDecimal(delete_am_rate_for_payment.Text) * Convert.ToDecimal(delete_am_quantity_for_payment.Text))).ToString();


            //delete_com_br_payment_combobox.Text = delete_payment_screen.CurrentRow.Cells[1].Value.ToString();
            //delete_date_payment_date.Value = Convert.ToDateTime(delete_payment_screen.CurrentRow.Cells[2].Value.ToString());
            //delete_amount_due_payment_text.Text = delete_payment_screen.CurrentRow.Cells[4].Value.ToString();
            //delete_amount_receive_payment_text.Text = delete_payment_screen.CurrentRow.Cells[5].Value.ToString();
            //delete_amount_remain_payment_text.Text = delete_payment_screen.CurrentRow.Cells[3].Value.ToString();
            //delete_payment_mode_combo.Text = delete_payment_screen.CurrentRow.Cells[6].Value.ToString();
        }

        private void delete_payment_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_payment_salaries.SelectedTab = delete_payment_data_tab;
            delete_bill.Text = "Bill # " + delete_payment_screen.CurrentRow.Cells[0].Value.ToString();
            fetch_payment_data_to_delete();
        }

        private void delete_payment_data_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                try
                {
                    SqlCommand delete_data = new SqlCommand("UPDATE [dbo].[PAYMENTS] SET [STATUS] = 0 WHERE [ID] = '" + delete_payment_screen.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    delete_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record deleted", "Success");
                    fetch_data_payment();
                    data_clear_payment();
                    comboboxes_data();
                    mini_screen_payment_salaries.SelectedTab = delete_payment_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");
            }

        }

        private void new_assign_button_Click(object sender, EventArgs e)
        {
            mini_screen_assignment.SelectedTab = new_assign_tab;
            data_clear_all();
            fetch_data();
        }

        private void update_assign_button_Click(object sender, EventArgs e)
        {
            mini_screen_assignment.SelectedTab = update_assign_tab;
            data_clear_all();
            fetch_data();
        }

        private void delete_assign_button_Click(object sender, EventArgs e)
        {
            mini_screen_assignment.SelectedTab = delete_assign_tab;
            data_clear_all();
            fetch_data();
        }

        private void fetch_data_assignment()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [ID],[COMPANY],[COMPANY_BRANCH],[COMPANY_BRANCH_EMPLOYEE],[FROM_TIME],[TO_TIME],[TIMING] FROM [dbo].[ASSIGNMENT] WHERE [ON_STATUS] = 1 ", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_assign_screen.DataSource = tb_data;
            delete_assign_screen.DataSource = tb_data;
        }

        private void update_assign_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_assignment.SelectedTab = update_assign_data_tab;
            data_fetch_assign_to_update();
            fetch_data();
        }

        private void delete_assign_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_assignment.SelectedTab = delete_assign_data_tab;
            data_fetch_assign_to_delete();
            fetch_data();
        }

        private void com_branch_add_combo_Click(object sender, EventArgs e)
        {
            mini_screen_comp.SelectedTab = new_com_br_tab;
            fetch_data();
        }

        private void update_com_br_button_Click(object sender, EventArgs e)
        {
            mini_screen_comp.SelectedTab = update_com_br_tab;
            fetch_data();
        }

        private void delete_com_br_button_Click(object sender, EventArgs e)
        {
            mini_screen_comp.SelectedTab = delete_com_br_tab;
            fetch_data();
        }

        private void fetch_data_branches()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [ID],[COMPANY_NAME],[BRANCH_NAME],[COMPANY_ADDRESS],[CITY],[CONTACT_PERSON_NAME],[CONTACT_PERSON_DESIGNATION],[CONTACT_PERSON_EMAIL],[CONTACT_PERSON_CELL_NUMBER],[COMPANY_PHONE_ONE],[COMPANY_PHONE_TWO],[REGISTRATION_DATE] FROM [dbo].[COMPANY_BRANCHES_DETAIL] WHERE [ON_STATUS] = 1", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_branches_screen.DataSource = tb_data;
            delete_branches_screen.DataSource = tb_data;
        }

        private void update_com_br_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_comp.SelectedTab = update_com_br_data_tab;
            fetch_data_branches();
            fetch_data();
        }

        private void delete_com_br_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_comp.SelectedTab = delete_com_br_data_tab;
            fetch_data_branches();
            fetch_data();
        }

        private void data_clear_assign()
        {
            com_emp_assign_txt.Clear();
            update_com_emp_assign_txt.Clear();
            delete_com_emp_assign_txt.Clear();
            delete_timings_assign_text.Clear();
            com_emp_assign_txt.Enabled = true;
            update_com_emp_assign_txt.Enabled = true;

        }

        private void data_fetch_assign_to_update()
        {
            update_com_assign_combo.Text = update_assign_screen.CurrentRow.Cells[1].Value.ToString();
            update_com_br_assign_combo.Text = update_assign_screen.CurrentRow.Cells[2].Value.ToString();
            update_com_emp_assign_txt.Text = update_assign_screen.CurrentRow.Cells[3].Value.ToString();
            update_from_time_assign_date.Value = Convert.ToDateTime(update_assign_screen.CurrentRow.Cells[4].Value);
            update_to_time_assign_date.Value = Convert.ToDateTime(update_assign_screen.CurrentRow.Cells[5].Value);
            update_timings_assign_text.Text = update_assign_screen.CurrentRow.Cells[6].Value.ToString();
        }

        private void data_fetch_assign_to_delete()
        {
            delete_com_assign_combo.Text = delete_assign_screen.CurrentRow.Cells[1].Value.ToString();
            delete_com_br_assign_combo.Text = delete_assign_screen.CurrentRow.Cells[2].Value.ToString();
            delete_com_emp_assign_txt.Text = delete_assign_screen.CurrentRow.Cells[3].Value.ToString();
            delete_from_time_assign_date.Value = Convert.ToDateTime(delete_assign_screen.CurrentRow.Cells[4].Value);
            delete_to_time_assign_date.Value = Convert.ToDateTime(delete_assign_screen.CurrentRow.Cells[5].Value);
            delete_timings_assign_text.Text = delete_assign_screen.CurrentRow.Cells[6].Value.ToString();
        }

        private void new_assign_data_Click(object sender, EventArgs e)
        {
            if (com_assign_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (com_br_assign_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (com_emp_assign_txt.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (timings_assign_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                try
                {
                    SqlCommand add_data = new SqlCommand("INSERT INTO [dbo].[ASSIGNMENT]([COMPANY],[COMPANY_BRANCH],[COMPANY_BRANCH_EMPLOYEE],[FROM_TIME],[TO_TIME],[TIMING],[ON_STATUS])VALUES ('" + com_assign_combo.Text + "','" + com_br_assign_combo.Text + "','" + com_emp_assign_txt.Text + "','" + from_time_assign_date.Text + "','" + to_time_assign_date.Text + "','" + timings_assign_text.Text + "','" + 1 + "')", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record updated !!", "Success");
                    data_clear_assign();
                    fetch_data_assignment();
                    comboboxes_data();
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void update_assign_data_Click(object sender, EventArgs e)
        {
            if (update_com_assign_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_com_br_assign_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_com_emp_assign_txt.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_timings_assign_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                try
                {
                    SqlCommand add_data = new SqlCommand("UPDATE [dbo].[ASSIGNMENT] SET [COMPANY] = '" + update_com_assign_combo.Text + "',[COMPANY_BRANCH] = '" + update_com_br_assign_combo.Text + "',[COMPANY_BRANCH_EMPLOYEE] = '" + update_com_emp_assign_txt.Text + "',[FROM_TIME] = '" + update_from_time_assign_date.Text + "',[TO_TIME] = '" + update_to_time_assign_date.Text + "',[TIMING] = '" + update_timings_assign_text.Text + "' WHERE [ID] = " + update_assign_screen.CurrentRow.Cells[0].Value + "", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record updated !!", "Success");
                    data_clear_assign();
                    fetch_data_assignment();
                    mini_screen_assignment.SelectedTab = update_assign_tab;
                    comboboxes_data();
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void delete_assign_data_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                try
                {
                    SqlCommand add_data = new SqlCommand("UPDATE [dbo].[ASSIGNMENT] SET [ON_STATUS] = 0 WHERE [ID] = '" + delete_assign_screen.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record deleted !!", "Success");
                    data_clear_assign();
                    fetch_data_assignment();
                    comboboxes_data();
                    fetch_data();
                    mini_screen_assignment.SelectedTab = delete_assign_tab;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");
            }

        }

        private void delete_company_button_Click(object sender, EventArgs e)
        {
            mini_screen_company.SelectedTab = delete_company_tab;
            fetch_data_com();
            data_clear_all();
            fetch_data();
        }

        private void new_company_data_button_Click(object sender, EventArgs e)
        {
            if (company_name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (company_city_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (company_phone_1_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (contact_person_name_com_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (contact_person_designation_com_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (contact_person_email_com_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (contact_person_cell_com_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (company_register_date.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (company_add_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {

                try
                {
                    SqlCommand new_com = new SqlCommand("INSERT INTO [dbo].[COMPANY]([COMPANY_NAME],[COMPANY_ADDRESS],[CITY],[CONTACT_PERSON_NAME],[CONTACT_PERSON_DESIGNATION],[CONTACT_PERSON_EMAIL],[CONTACT_PERSON_CELL_NUMBER],[COMPANY_PHONE_ONE],[COMPANY_PHONE_TWO],[REGISTRATION_DATE],[ON_STATUS]) VALUES ('" + company_name_text.Text + "','" + company_add_text.Text + "','" + company_city_text.Text + "','" + contact_person_name_com_text.Text + "','" + contact_person_designation_com_text.Text + "','" + contact_person_email_com_text.Text + "','" + contact_person_cell_com_text.Text + "','" + company_phone_1_text.Text + "','" + company_phone_2_text.Text + "','" + company_register_date.Text + "','" + 1 + "')", connection);
                    connection.Open();
                    new_com.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record updated", "Success");
                    com_data_clear();
                    fetch_data();
                    comboboxes_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error");
                }
            }
        }

        private void screen_delete_emp_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_emp.SelectedTab = delete_emp_data_tab;
            fetch_data_emp_for_delete();
            fetch_data();
        }

        private void screen_delete_com_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_company.SelectedTab = delete_company_data_tab;
            fetch_com_data_to_delete();
            fetch_data();
        }

        private void screen_update_com_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_company.SelectedTab = update_company_data_tab;
            fetch_com_data_to_update();
            fetch_data();
        }


        private void update_com_data_btn_Click(object sender, EventArgs e)
        {

            if (update_company_name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_company_city_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_company_phone_1_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_company_person_name_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_company_person_designation_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_company_person_email_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_company_person_cell_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_company_register_date.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_company_add_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {

                try
                {
                    SqlCommand update_data = new SqlCommand("UPDATE [dbo].[COMPANY] SET [COMPANY_NAME] = '" + update_company_name_text.Text + "',[COMPANY_ADDRESS] = '" + update_company_add_text.Text + "',[CITY] = '" + update_company_city_text.Text + "',[CONTACT_PERSON_NAME] = '" + update_company_person_name_text.Text + "',[CONTACT_PERSON_DESIGNATION] = '" + update_company_person_designation_text.Text + "',[CONTACT_PERSON_EMAIL] = '" + update_company_person_email_text.Text + "' ,[CONTACT_PERSON_CELL_NUMBER] = '" + update_company_person_cell_text.Text + "',[COMPANY_PHONE_ONE] = '" + update_company_phone_1_text.Text + "' ,[COMPANY_PHONE_TWO] = '" + update_company_phone_1_text.Text + "',[REGISTRATION_DATE] = '" + update_company_phone_2_text.Text + "' WHERE [ID] = '" + update_screen_company.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record Updated", "Success");
                    com_data_clear();
                    comboboxes_data();
                    mini_screen_company.SelectedTab = update_company_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error");
                }
            }
        }

        private void delete_com_data_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                try
                {
                    SqlCommand update_data = new SqlCommand("UPDATE [dbo].[COMPANY] SET [ON_STATUS] = 0 WHERE [ID] = '" + screen_delete_company.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    update_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record Deleted", "Success");
                    com_data_clear();
                    comboboxes_data();
                    mini_screen_company.SelectedTab = delete_company_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error");
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");
            }

        }

        private void new_company_button_Click(object sender, EventArgs e)
        {
            mini_screen_company.SelectedTab = new_company_tab;
            fetch_data_com();
            data_clear_all();
            fetch_data();
        }

        private void update_company_button_Click(object sender, EventArgs e)
        {
            mini_screen_company.SelectedTab = update_company_tab;
            fetch_data_com();
            data_clear_all();
            fetch_data();
        }

        private void data_fetch_branches()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [ID],[COMPANY_NAME],[BRANCH_NAME],[COMPANY_ADDRESS],[CITY],[CONTACT_PERSON_NAME],[CONTACT_PERSON_DESIGNATION],[CONTACT_PERSON_EMAIL],[CONTACT_PERSON_CELL_NUMBER],[COMPANY_PHONE_ONE],[COMPANY_PHONE_TWO],[REGISTRATION_DATE] FROM [dbo].[COMPANY_BRANCHES_DETAIL]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_branches_screen.DataSource = tb_data;
            delete_branches_screen.DataSource = tb_data;
        }

        private void data_clear_branches()
        {
            branch_name.Clear();
            branch_phone_1.Clear();
            branch_phone_2.Clear();
            branch_person_name.Clear();
            branch_person_email.Clear();
            branch_person_cell.Clear();
            branch_address.Clear();
            update_branch_name.Clear();
            update_branch_phone_1.Clear();
            update_branch_phone_2.Clear();
            update_branch_person_name.Clear();
            update_branch_person_email.Clear();
            update_branch_person_cell.Clear();
            update_branch_address.Clear();
            delete_branch_name.Clear();
            delete_branch_phone_1.Clear();
            delete_branch_phone_2.Clear();
            delete_branch_person_name.Clear();
            delete_branch_person_designation.Clear();
            delete_branch_person_email.Clear();
            delete_branch_person_cell.Clear();
            delete_branch_address.Clear();
        }

        private void fetch_data_branch_to_update()
        {
            update_company_branch_combo.Text = update_branches_screen.CurrentRow.Cells[2].Value.ToString();
            update_branch_name.Text = update_branches_screen.CurrentRow.Cells[2].Value.ToString();
            update_branch_city.Text = update_branches_screen.CurrentRow.Cells[4].Value.ToString();
            update_branch_phone_1.Text = update_branches_screen.CurrentRow.Cells[9].Value.ToString();
            update_branch_phone_2.Text = update_branches_screen.CurrentRow.Cells[10].Value.ToString();
            update_branch_person_name.Text = update_branches_screen.CurrentRow.Cells[5].Value.ToString();
            update_branch_person_designation.Text = update_branches_screen.CurrentRow.Cells[6].Value.ToString();
            update_branch_person_email.Text = update_branches_screen.CurrentRow.Cells[7].Value.ToString();
            update_branch_person_cell.Text = update_branches_screen.CurrentRow.Cells[8].Value.ToString();
            update_branch_register_date.Value = Convert.ToDateTime(update_branches_screen.CurrentRow.Cells[11].Value);
            update_branch_address.Text = update_branches_screen.CurrentRow.Cells[3].Value.ToString();
        }
        private void fetch_data_branch_to_delete()
        {
            delete_company_branch_combo.Text = update_branches_screen.CurrentRow.Cells[1].Value.ToString();
            delete_branch_name.Text = update_branches_screen.CurrentRow.Cells[2].Value.ToString();
            delete_branch_city.Text = update_branches_screen.CurrentRow.Cells[4].Value.ToString();
            delete_branch_phone_1.Text = update_branches_screen.CurrentRow.Cells[9].Value.ToString();
            delete_branch_phone_2.Text = update_branches_screen.CurrentRow.Cells[10].Value.ToString();
            delete_branch_person_name.Text = update_branches_screen.CurrentRow.Cells[5].Value.ToString();
            delete_branch_person_designation.Text = update_branches_screen.CurrentRow.Cells[6].Value.ToString();
            delete_branch_person_email.Text = update_branches_screen.CurrentRow.Cells[7].Value.ToString();
            delete_branch_person_cell.Text = update_branches_screen.CurrentRow.Cells[8].Value.ToString();
            delete_branch_register_date.Value = Convert.ToDateTime(update_branches_screen.CurrentRow.Cells[11].Value);
            delete_branch_address.Text = update_branches_screen.CurrentRow.Cells[3].Value.ToString();
        }

        private void new_branch_btn_Click(object sender, EventArgs e)
        {
            mini_screen_company.SelectedTab = new_company_br_tab;
            data_fetch_branches();
            data_clear_all();
            fetch_data();
        }

        private void update_branch_data_Click(object sender, EventArgs e)
        {
            mini_screen_company.SelectedTab = update_company_br_tab;
            data_fetch_branches();
            data_clear_all();
            fetch_data();
        }

        private void delete_branch_data_Click(object sender, EventArgs e)
        {
            mini_screen_company.SelectedTab = delete_company_br_tab;
            data_fetch_branches();
            data_clear_all();
            fetch_data();
        }

        private void update_branches_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_company.SelectedTab = update_company_br_data_tab;
            fetch_data_branch_to_update();
            fetch_data();
        }

        private void delete_branches_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mini_screen_company.SelectedTab = delete_company_br_data_tab;
            fetch_data_branch_to_delete();
            fetch_data();
        }

        private void new_branch_data_btn_Click(object sender, EventArgs e)
        {
            if (company_branch_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (branch_name.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (branch_city.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (branch_phone_1.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (branch_person_name.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (branch_person_designation.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (branch_person_email.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (branch_person_cell.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (branch_address.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {

                try
                {
                    SqlCommand add_data = new SqlCommand("INSERT INTO [dbo].[COMPANY_BRANCHES_DETAIL]([COMPANY_NAME],[BRANCH_NAME],[COMPANY_ADDRESS],[CITY],[CONTACT_PERSON_NAME],[CONTACT_PERSON_DESIGNATION],[CONTACT_PERSON_EMAIL],[CONTACT_PERSON_CELL_NUMBER],[COMPANY_PHONE_ONE],[COMPANY_PHONE_TWO],[REGISTRATION_DATE],[ON_STATUS])VALUES ('" + company_branch_combo.Text + "','" + branch_name.Text + "','" + branch_address.Text + "','" + branch_city.Text + "','" + branch_person_name.Text + "','" + branch_person_designation.Text + "','" + branch_person_email.Text + "','" + branch_person_cell.Text + "','" + branch_phone_1.Text + "','" + branch_phone_2.Text + "','" + branch_register_date.Text + "','" + 1 + "')", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record updated", "Success");
                    data_clear_branches();
                    fetch_data_branches();
                    comboboxes_data();
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void update_branch_data_button_Click(object sender, EventArgs e)
        {

            if (update_company_branch_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_branch_name.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_branch_city.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_branch_phone_1.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_branch_phone_2.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_branch_person_designation.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_branch_person_email.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_branch_person_cell.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_branch_address.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else
            {

                try
                {
                    SqlCommand add_data = new SqlCommand("UPDATE [dbo].[COMPANY_BRANCHES_DETAIL] SET [COMPANY_NAME] = '" + update_company_branch_combo.Text + "',[BRANCH_NAME] = '" + update_branch_name.Text + "',[COMPANY_ADDRESS] = '" + update_branch_address.Text + "',[CITY] = '" + update_branch_city.Text + "',[CONTACT_PERSON_NAME] = '" + update_branch_person_name.Text + "',[CONTACT_PERSON_DESIGNATION] = '" + update_branch_person_designation.Text + "',[CONTACT_PERSON_EMAIL] = '" + update_branch_person_email.Text + "',[CONTACT_PERSON_CELL_NUMBER] = '" + update_branch_person_cell.Text + "',[COMPANY_PHONE_ONE] = '" + update_branch_phone_1.Text + "',[COMPANY_PHONE_TWO] = '" + update_branch_phone_2.Text + "' ,[REGISTRATION_DATE] = '" + update_branch_register_date.Text + "' WHERE [ID] = " + update_branches_screen.CurrentRow.Cells[0].Value + "", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record updated", "Success");
                    data_clear_branches();
                    fetch_data_branches();
                    comboboxes_data();
                    mini_screen_company.SelectedTab = update_company_br_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }


        private void delete_branch_data_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                try
                {
                    SqlCommand add_data = new SqlCommand("UPDATE [dbo].[COMPANY_BRANCHES_DETAIL] SET [ON_STATUS] = 0 WHERE [ID] = " + delete_branches_screen.CurrentRow.Cells[0].Value + "", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record deleted", "Success");
                    data_clear_branches();
                    fetch_data_branches();
                    comboboxes_data();
                    mini_screen_company.SelectedTab = delete_company_br_tab;
                    fetch_data();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");
            }

        }

        private void amount_expense_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_amount_expense_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void amount_remain_payment_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_amount_receive_payment_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void salary_emp_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_overtime_salary_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void contact_person_cell_com_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_company_person_cell_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void branch_person_cell_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_branch_phone_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void salary_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_cnic_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        string emp_id;
        private void emp_id_fetch_for_more_details()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT TOP 1 ID FROM [dbo].[EMPLOYEE_GUARD] ORDER BY ID DESC", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            emp_id = tb_data.Rows[0][0].ToString();
        }

        string update_emp_id;
        private void update_id_emp()
        {
            update_emp_id = screen_update_emp.CurrentRow.Cells[0].Value.ToString();
        }

        private void delete_id_emp()
        {
            delete_id_text.Text = screen_delete_emp.CurrentRow.Cells[0].Value.ToString();
        }

        private void search_update_emp_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[EMPLOYEE_GUARD] WHERE [PREPAID_BY] = '" + search_update_emp.Text + "' OR [MANAGER_OPERATION] = '" + search_update_emp.Text + "' OR [MANAGER_ADMISSION] = '" + search_update_emp.Text + "' OR [NAME] = '" + search_update_emp.Text + "' OR [STATUS] = '" + search_update_emp.Text + "' OR [FATHER_NAME] = '" + search_update_emp.Text + "' OR [PRESENT_ADDRESS] = '" + search_update_emp.Text + "' OR [PERMANENT_ADDRESS] = '" + search_update_emp.Text + "' OR [CNIC] = '" + search_update_emp.Text + "' OR [MARTIAL_STATUS] = '" + search_update_emp.Text + "' OR [NEXT_OF_KIN_NAME] = '" + search_update_emp.Text + "' OR [NEXT_OF_KIN_RELATION] = '" + search_update_emp.Text + "' OR [NEXT_OF_KIN_ADDRESS] = '" + search_update_emp.Text + "' OR [DOB] = '" + search_update_emp.Text + "' OR [RELIGION] = '" + search_update_emp.Text + "' OR [EDUCATION] = '" + search_update_emp.Text + "' OR [SECTION] = '" + search_update_emp.Text + "' OR [DATE_OF_ENROLLMENT] = '" + search_update_emp.Text + "' OR [SALARY] = '" + search_update_emp.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            screen_update_emp.DataSource = tb_data;
            if (search_update_emp.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_delete_emp_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[EMPLOYEE_GUARD] WHERE [PREPAID_BY] = '" + search_delete_emp.Text + "' OR [MANAGER_OPERATION] = '" + search_delete_emp.Text + "' OR [MANAGER_ADMISSION] = '" + search_delete_emp.Text + "' OR [NAME] = '" + search_delete_emp.Text + "' OR [STATUS] = '" + search_delete_emp.Text + "' OR [FATHER_NAME] = '" + search_delete_emp.Text + "' OR [PRESENT_ADDRESS] = '" + search_delete_emp.Text + "' OR [PERMANENT_ADDRESS] = '" + search_delete_emp.Text + "' OR [CNIC] = '" + search_delete_emp.Text + "' OR [MARTIAL_STATUS] = '" + search_delete_emp.Text + "' OR [NEXT_OF_KIN_NAME] = '" + search_delete_emp.Text + "' OR [NEXT_OF_KIN_RELATION] = '" + search_delete_emp.Text + "' OR [NEXT_OF_KIN_ADDRESS] = '" + search_delete_emp.Text + "' OR [DOB] = '" + search_delete_emp.Text + "' OR [RELIGION] = '" + search_delete_emp.Text + "' OR [EDUCATION] = '" + search_delete_emp.Text + "' OR [SECTION] = '" + search_delete_emp.Text + "' OR [DATE_OF_ENROLLMENT] = '" + search_delete_emp.Text + "' OR [SALARY] = '" + search_delete_emp.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            screen_delete_emp.DataSource = tb_data;
            if (search_delete_emp.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_update_company_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[COMPANY] WHERE  [COMPANY_NAME] = '" + search_update_company.Text + "' OR [COMPANY_ADDRESS] = '" + search_update_company.Text + "' OR [CITY] = '" + search_update_company.Text + "' OR [CONTACT_PERSON_NAME] = '" + search_update_company.Text + "' OR [CONTACT_PERSON_DESIGNATION] = '" + search_update_company.Text + "' OR [CONTACT_PERSON_EMAIL] = '" + search_update_company.Text + "' OR [CONTACT_PERSON_CELL_NUMBER] = '" + search_update_company.Text + "' OR [COMPANY_PHONE_ONE] = '" + search_update_company.Text + "' OR [COMPANY_PHONE_TWO] = '" + search_update_company.Text + "' OR [REGISTRATION_DATE] = '" + search_update_company.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_screen_company.DataSource = tb_data;
            if (search_update_company.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_delete_company_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[COMPANY] WHERE  [COMPANY_NAME] = '" + search_delete_company.Text + "' OR [COMPANY_ADDRESS] = '" + search_delete_company.Text + "' OR [CITY] = '" + search_delete_company.Text + "' OR [CONTACT_PERSON_NAME] = '" + search_delete_company.Text + "' OR [CONTACT_PERSON_DESIGNATION] = '" + search_delete_company.Text + "' OR [CONTACT_PERSON_EMAIL] = '" + search_delete_company.Text + "' OR [CONTACT_PERSON_CELL_NUMBER] = '" + search_delete_company.Text + "' OR [COMPANY_PHONE_ONE] = '" + search_delete_company.Text + "' OR [COMPANY_PHONE_TWO] = '" + search_delete_company.Text + "' OR [REGISTRATION_DATE] = '" + search_delete_company.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            screen_delete_company.DataSource = tb_data;
            if (search_delete_company.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_update_company_branch_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT* FROM [dbo].[COMPANY_BRANCHES_DETAIL] WHERE [COMPANY_NAME] = '" + search_update_company_branch.Text + "' OR [BRANCH_NAME] = '" + search_update_company_branch.Text + "' OR [COMPANY_ADDRESS] = '" + search_update_company_branch.Text + "' OR [CITY] = '" + search_update_company_branch.Text + "' OR [CONTACT_PERSON_NAME] = '" + search_update_company_branch.Text + "' OR [CONTACT_PERSON_DESIGNATION] = '" + search_update_company_branch.Text + "' OR [CONTACT_PERSON_EMAIL] = '" + search_update_company_branch.Text + "' OR [CONTACT_PERSON_CELL_NUMBER] = '" + search_update_company_branch.Text + "' OR [COMPANY_PHONE_ONE] = '" + search_update_company_branch.Text + "' OR [COMPANY_PHONE_TWO] = '" + search_update_company_branch.Text + "' OR [REGISTRATION_DATE] = '" + search_update_company_branch.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_branches_screen.DataSource = tb_data;
            if (search_update_company_branch.Text == "")
            {
                this.fetch_data();
            }
        }

        private void bunifuMaterialTextbox27_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT* FROM [dbo].[COMPANY_BRANCHES_DETAIL] WHERE [COMPANY_NAME] = '" + search_delete_com_branch.Text + "' OR [BRANCH_NAME] = '" + search_delete_com_branch.Text + "' OR [COMPANY_ADDRESS] = '" + search_delete_com_branch.Text + "' OR [CITY] = '" + search_delete_com_branch.Text + "' OR [CONTACT_PERSON_NAME] = '" + search_delete_com_branch.Text + "' OR [CONTACT_PERSON_DESIGNATION] = '" + search_delete_com_branch.Text + "' OR [CONTACT_PERSON_EMAIL] = '" + search_delete_com_branch.Text + "' OR [CONTACT_PERSON_CELL_NUMBER] = '" + search_delete_com_branch.Text + "' OR [COMPANY_PHONE_ONE] = '" + search_delete_com_branch.Text + "' OR [COMPANY_PHONE_TWO] = '" + search_delete_com_branch.Text + "' OR [REGISTRATION_DATE] = '" + search_delete_com_branch.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            delete_branches_screen.DataSource = tb_data;
            if (search_delete_com_branch.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_delete_assignment_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[ASSIGNMENT] WHERE [COMPANY] = '" + search_delete_assignment.Text + "' OR [COMPANY_BRANCH] = '" + search_delete_assignment.Text + "' OR [COMPANY_BRANCH_EMPLOYEE] = '" + search_delete_assignment.Text + "' OR [FROM_TIME] = '" + search_delete_assignment.Text + "' OR [TO_TIME] = '" + search_delete_assignment.Text + "' OR [TIMING] = '" + search_delete_assignment.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            delete_assign_screen.DataSource = tb_data;
            if (search_delete_assignment.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_update_assignment_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[ASSIGNMENT] WHERE [COMPANY] = '" + search_update_assignment.Text + "' OR [COMPANY_BRANCH] = '" + search_update_assignment.Text + "' OR [COMPANY_BRANCH_EMPLOYEE] = '" + search_update_assignment.Text + "' OR [FROM_TIME] = '" + search_update_assignment.Text + "' OR [TO_TIME] = '" + search_update_assignment.Text + "' OR [TIMING] = '" + search_update_assignment.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_assign_screen.DataSource = tb_data;
            if (search_update_assignment.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_update_app_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[APPPOINTMENT] WHERE [COMPANY] = '" + search_update_app.Text + "' OR [COMPANY_BRANCH] = '" + search_update_app.Text + "' OR [NO_OF_PERSON] = '" + search_update_app.Text + "' OR [FROM_TIME] = '" + search_update_app.Text + "' OR [TO_TIME] = '" + search_update_app.Text + "' OR [TIMINGS] = '" + search_update_app.Text + "' OR [AMOUNT_PER_PERSON] = '" + search_update_app.Text + "' OR [IN_TIME] = '" + search_update_app.Text + "' OR [OUT_TIME] = '" + search_update_app.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            screen_update_app.DataSource = tb_data;
            if (search_update_app.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_delete_app_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[APPPOINTMENT] WHERE [COMPANY] = '" + search_delete_app.Text + "' OR [COMPANY_BRANCH] = '" + search_delete_app.Text + "' OR [NO_OF_PERSON] = '" + search_delete_app.Text + "' OR [FROM_TIME] = '" + search_delete_app.Text + "' OR [TO_TIME] = '" + search_delete_app.Text + "' OR [TIMINGS] = '" + search_delete_app.Text + "' OR [AMOUNT_PER_PERSON] = '" + search_delete_app.Text + "' OR [IN_TIME] = '" + search_delete_app.Text + "' OR [OUT_TIME] = '" + search_delete_app.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            screen_delete_app.DataSource = tb_data;
            if (search_delete_app.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_update_payment_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[PAYMENTS] WHERE [COMPANY] = '" + search_update_payment.Text + "' OR [COMPANY_BRANCH]  = '" + search_update_payment.Text + "' OR [BRANCH_OFFICER]  = '" + search_update_payment.Text + "' OR [AMOUNT_RATE] = '" + search_update_payment.Text + "' OR [AMOUNT_QUANTITY] = '" + search_update_payment.Text + "' OR [GST_RATE] = '" + search_update_payment.Text + "' OR [GST_AMOUNT]  = '" + search_update_payment.Text + "' OR [GST_WITHHELD_RATE]  = '" + search_update_payment.Text + "' OR [GST_WITHHELD_AMOUNT] = '" + search_update_payment.Text + "' OR [REMARKS]  = '" + search_update_payment.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_payment_screen.DataSource = tb_data;
            if (search_update_payment.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_delete_payment_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[PAYMENTS] WHERE [COMPANY] = '" + search_delete_payment.Text + "' OR [COMPANY_BRANCH]  = '" + search_delete_payment.Text + "' OR [BRANCH_OFFICER]  = '" + search_delete_payment.Text + "' OR [AMOUNT_RATE] = '" + search_delete_payment.Text + "' OR [AMOUNT_QUANTITY] = '" + search_delete_payment.Text + "' OR [GST_RATE] = '" + search_delete_payment.Text + "' OR [GST_AMOUNT]  = '" + search_delete_payment.Text + "' OR [GST_WITHHELD_RATE]  = '" + search_delete_payment.Text + "' OR [GST_WITHHELD_AMOUNT] = '" + search_delete_payment.Text + "' OR [REMARKS]  = '" + search_delete_payment.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            delete_payment_screen.DataSource = tb_data;
            if (search_delete_payment.Text == "")
            {
                this.fetch_data();
            }
        }
        private void search_update_salary_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[SALARY] WHERE [EMPLOYEE] = '" + search_update_salary.Text + "' OR [FROM_TIME] = '" + search_update_salary.Text + "' OR [TO_TIME] = '" + search_update_salary.Text + "' OR [SALARY] = '" + search_update_salary.Text + "' OR [OVERTIME] = '" + search_update_salary.Text + "' OR [TOTAL_AMOUNT] = '" + search_update_salary.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_salary_screen.DataSource = tb_data;
            if (search_update_salary.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_delete_salary_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[SALARY] WHERE [EMPLOYEE] = '" + search_delete_salary.Text + "' OR [FROM_TIME] = '" + search_delete_salary.Text + "' OR [TO_TIME] = '" + search_delete_salary.Text + "' OR [SALARY] = '" + search_delete_salary.Text + "' OR [OVERTIME] = '" + search_delete_salary.Text + "' OR [TOTAL_AMOUNT] = '" + search_delete_salary.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            delete_salary_screen.DataSource = tb_data;
            if (search_delete_salary.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_update_expense_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[EXPENSE] WHERE [ITEMS] = '" + search_update_expense.Text + "' OR [DATE] = '" + search_update_expense.Text + "' OR [AMOUNT] = '" + search_update_expense.Text + "' OR [PAYMENT_TYPE] = '" + search_update_expense.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_expense_screen.DataSource = tb_data;
            if (search_update_expense.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_delete_expense_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[EXPENSE] WHERE [EMP_NAME] = '" + search_delete_expense.Text + "' OR [ITEMS] = '" + search_delete_expense.Text + "' OR [DATE] = '" + search_delete_expense.Text + "' OR [AMOUNT] = '" + search_delete_expense.Text + "' OR [PAYMENT_TYPE] = '" + search_delete_expense.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            expense_delete_screen.DataSource = tb_data;
            if (search_delete_expense.Text == "")
            {
                this.fetch_data();
            }
        }

        int wage = 1000;

        private void overtime_salary_text_Click(object sender, EventArgs e)
        {

        }

        private void total_am_salary_text_Click(object sender, EventArgs e)
        {
        }

        private void update_total_am_salary_text_Click(object sender, EventArgs e)
        {
        }

        private void update_overtime_salary_text_Click(object sender, EventArgs e)
        {
        }

        private void update_salary_emp_text_Click(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT SUM(CAST([DUTY_HOURS] AS decimal)) FROM [dbo].[ATTENDANCE] WHERE EMP_NAME = '" + new_emp_salary_combo.Text + "'", connection);
                System.Data.DataTable tb_data = new System.Data.DataTable();
                fetch_data.Fill(tb_data);

                SqlDataAdapter fetch_data_count = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[ATTENDANCE] WHERE EMP_NAME = '" + new_emp_salary_combo.Text + "'", connection);
                System.Data.DataTable tb_data_count = new System.Data.DataTable();
                fetch_data_count.Fill(tb_data_count);

                string days = tb_data_count.Rows[0][0].ToString();
                string minutes = tb_data.Rows[0][0].ToString();
                Decimal date = Convert.ToDecimal(minutes);
                Decimal hour = Convert.ToDecimal((decimal)date / 60);
                float hours = float.Parse(hour.ToString());
                string limit_no = (8 * Convert.ToInt32(days)).ToString();
                float limit = float.Parse(limit_no);
                //* float.Parse(days);
                float overtime_hours;
                if ((overtime_hours = hours - limit) < 0)
                {
                    update_salary_emp_text.Text = hours.ToString();
                    update_overtime_salary_text.Text = "0";
                    update_total_am_salary_text.Text = ((float.Parse(update_salary_emp_text.Text) + (float.Parse(update_overtime_salary_text.Text))) * wage).ToString();

                }
                else if (hours > 8)
                {
                    update_salary_emp_text.Text = limit.ToString();
                    update_overtime_salary_text.Text = (hours - limit).ToString();
                    update_total_am_salary_text.Text = ((float.Parse(update_salary_emp_text.Text) + (float.Parse(update_overtime_salary_text.Text))) * wage).ToString();


                }
                else
                {
                    update_salary_emp_text.Text = hours.ToString();
                    update_overtime_salary_text.Text = "0";
                    update_total_am_salary_text.Text = ((float.Parse(update_salary_emp_text.Text) + (float.Parse(update_overtime_salary_text.Text))) * wage).ToString();


                }
                update_overtime_salary_text.Enabled = false;
                update_salary_emp_text.Enabled = false;
                update_total_am_salary_text.Enabled = false;
            }
            catch (Exception error)
            {
                MetroFramework.MetroMessageBox.Show(this, error.Message + " Couldn't find attendance sheet or employee details !!", "Error");
            }
        }

        private void salary_emp_text_Click(object sender, EventArgs e)
        {
            try
            {

                SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT SUM(CAST([DUTY_HOURS] AS decimal)) FROM [dbo].[ATTENDANCE] WHERE EMP_NAME = '" + new_emp_salary_combo.Text + "'", connection);
                System.Data.DataTable tb_data = new System.Data.DataTable();
                fetch_data.Fill(tb_data);

                SqlDataAdapter fetch_data_count = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[ATTENDANCE] WHERE EMP_NAME = '" + new_emp_salary_combo.Text + "'", connection);
                System.Data.DataTable tb_data_count = new System.Data.DataTable();
                fetch_data_count.Fill(tb_data_count);

                string days = tb_data_count.Rows[0][0].ToString();
                string minutes = tb_data.Rows[0][0].ToString();
                Decimal date = Convert.ToDecimal(minutes);
                Decimal hour = Convert.ToDecimal((decimal)date / 60);
                float hours = float.Parse(hour.ToString());
                string limit_no = (8 * Convert.ToInt32(days)).ToString();
                float limit = float.Parse(limit_no);
                //* float.Parse(days);
                float overtime_hours;
                if ((overtime_hours = hours - limit) < 0)
                {
                    salary_emp_text.Text = hours.ToString();
                    overtime_salary_text.Text = "0";
                    total_am_salary_text.Text = ((float.Parse(salary_emp_text.Text) + (float.Parse(overtime_salary_text.Text))) * wage).ToString();

                }
                else if (hours > 8)
                {
                    salary_emp_text.Text = limit.ToString();
                    overtime_salary_text.Text = (hours - limit).ToString();
                    total_am_salary_text.Text = ((float.Parse(salary_emp_text.Text) + (float.Parse(overtime_salary_text.Text))) * wage).ToString();


                }
                else
                {
                    salary_emp_text.Text = hours.ToString();
                    overtime_salary_text.Text = "0";
                    total_am_salary_text.Text = ((float.Parse(salary_emp_text.Text) + (float.Parse(overtime_salary_text.Text))) * wage).ToString();


                }
                overtime_salary_text.Enabled = false;
                salary_emp_text.Enabled = false;
                total_am_salary_text.Enabled = false;
            }
            catch (Exception error)
            {
                MetroFramework.MetroMessageBox.Show(this, error.Message + " Couldn't find attendance sheet or employee details !!", "Error");
            }
        }

        private void new_app_btn_Click_1(object sender, EventArgs e)
        {

            if (add_com_to_app_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (add_com_br_to_app_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (add_emp_to_app_combo.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (amount_person_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (app_timing_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (in_time_app_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (out_time_app_text.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                try
                {
                    SqlCommand add_data = new SqlCommand("INSERT INTO [dbo].[APPPOINTMENT] ([COMPANY],[COMPANY_BRANCH],[NO_OF_PERSON],[FROM_TIME],[TO_TIME],[TIMINGS],[AMOUNT_PER_PERSON],[IN_TIME],[OUT_TIME],[STATUS],[ON_STATUS]) VALUES ('" + add_com_to_app_combo.Text + "','" + add_com_br_to_app_combo.Text + "','" + add_emp_to_app_combo.Text + "','" + app_from_date.Text + "','" + app_to_date.Text + "','" + app_timing_text.Text + "','" + amount_person_text.Text + "','" + in_time_app_text.Text + "','" + out_time_app_text.Text + "','On-Duty','" + 1 + "')", connection);
                    connection.Open();
                    add_data.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "record updated", "Success");
                    data_clear_app();
                    comboboxes_data();
                    fetch_data_app();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MetroFramework.MetroMessageBox.Show(this, "Are you sure you want to logout ?", "Question", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Login login = new Login();
                this.Hide();
                login.Show();
            }
            else
            {

            }


        }

        private void browse_documents_Click(object sender, EventArgs e)
        {
            // Browsing Function
            file_dialog.Filter = "ZIP Files (*.rar)|*.rar|(*.zip)|*.zip|All Files (*.*)|*.*";
            file_dialog.Title = "Select Employee Documents";
            // file_dialog.InitialDirectory
            if (file_dialog.ShowDialog() == DialogResult.OK)
            {
                string st_filename = file_dialog.FileName;
                doc_text.Text = st_filename;

            }
            else
            {
                doc_text.Text = "";
            }
        }

        private void update_browse_doc_Click(object sender, EventArgs e)
        {
            // Browsing Function
            file_dialog.Filter = "ZIP Files (*.rar)|*.rar|(*.zip)|*.zip|All Files (*.*)|*.*";
            file_dialog.Title = "Select Employee Documents";
            // file_dialog.InitialDirectory
            if (file_dialog.ShowDialog() == DialogResult.OK)
            {
                string st_filename = file_dialog.FileName;
                update_doc_text.Text = st_filename;

            }
            else
            {
                update_doc_text.Text = "";
            }

        }

        private void metroLabel305_Click(object sender, EventArgs e)
        {
            mini_screen_emp.SelectedTab = reference_emp_tab;
            data_clear_all();
            fetch_data();
        }

        private void update_emp_more_data_Click(object sender, EventArgs e)
        {
            mini_screen_emp.SelectedTab = add_refer_data_to_employee;
            data_clear_all();
            fetch_data();
        }

        private void metroLabel306_Move(object sender, EventArgs e)
        {
            metroLabel306.ForeColor = Color.Blue;

        }

        private void metroLabel306_Click(object sender, EventArgs e)
        {
            mini_screen_emp.SelectedTab = new_emp_tab;
            data_clear_all();
            fetch_data();
        }

        private void update_employee_refer_cno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }


        private void update_employee_refer_cnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_doc_employee_Click(object sender, EventArgs e)
        {
            // Browsing Function
            file_dialog.Filter = "ZIP Files (*.rar)|*.rar|(*.zip)|*.zip|All Files (*.*)|*.*";
            file_dialog.Title = "Select Employee Documents";
            // file_dialog.InitialDirectory
            if (file_dialog.ShowDialog() == DialogResult.OK)
            {
                string st_filename = file_dialog.FileName;
                update_doc_folder_string.Text = st_filename;

            }
            else
            {
                update_doc_folder_string.Text = "";
            }
        }

        private void update_data_reference_Click(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [ID] FROM [dbo].[EMPLOYEE_GUARD] WHERE [NAME] = '" + add_emp_data.Text + "' ", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            SqlCommand update_data_1 = new SqlCommand("INSERT INTO [dbo].[REFERENCE] ([EMP_NAME],[REFER_NAME],[REFER_CONTACT],[REFER_CNIC]) VALUES('" + tb_data.Rows[0][0].ToString() + "','" + update_employee_refer_name.Text + "','" + update_employee_refer_cno.Text + "','" + update_employee_refer_cnic.Text + "')", connection);
            connection.Open();
            update_data_1.ExecuteNonQuery();
            connection.Close();

            SqlCommand update_data_2 = new SqlCommand("INSERT INTO [dbo].[SERVICE_DETAIL] ([EMP_NAME],[FIVE_YEAR_SERVICE_DETAIL],[PREVIOUS_DEPARTMENT],[FROM_TIME],[TO_TIME],[CITY],[WORK_AS])VALUES ('" + tb_data.Rows[0][0].ToString() + "','" + update_employee_serv_details.Text + "','" + update_employee_prev_depart.Text + "','" + update_employee_from_date.Text + "','" + update_employee_to_date.Text + "','" + update_employee_city.Text + "','" + update_employee_work_as.Text + "')", connection);
            connection.Open();
            update_data_2.ExecuteNonQuery();
            connection.Close();
            SqlCommand update_data_3 = new SqlCommand("INSERT INTO [dbo].[DOCUMENTS] ([EMP_NAME],[DOC_FOLDER]) VALUES('" + tb_data.Rows[0][0].ToString() + "','" + update_doc_folder_string.Text + "')", connection);
            connection.Open();
            update_data_3.ExecuteNonQuery();
            connection.Close();
            MetroFramework.MetroMessageBox.Show(this, "Record updated !!", "Success");

        }

        private void refer_cnic_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_refer_cnic_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_refer_contact_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void refer_contact_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }


        private void fetch_prepaid_data()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[PREPAID]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            prepaid_combo.DataSource = tb_data;
            prepaid_combo.DisplayMember = "NAME";
            prepaid_combo.ValueMember = "ID";
            update_prepaid_combo.DataSource = tb_data;
            update_prepaid_combo.DisplayMember = "NAME";
            update_prepaid_combo.ValueMember = "ID";
        }

        private void fetch_m_operate_data()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[MANAGER_OPERATION]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            m_operate_combo.DataSource = tb_data;
            m_operate_combo.DisplayMember = "NAME";
            m_operate_combo.ValueMember = "ID";
            update_m_operate_combo.DataSource = tb_data;
            update_m_operate_combo.DisplayMember = "NAME";
            update_m_operate_combo.ValueMember = "ID";
        }

        private void fetch_m_add_combo_data()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[MANAGER_ADMISSION]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            m_add_combo.DataSource = tb_data;
            m_add_combo.DisplayMember = "NAME";
            m_add_combo.ValueMember = "ID";
            update_m_add_combo.DataSource = tb_data;
            update_m_add_combo.DisplayMember = "NAME";
            update_m_add_combo.ValueMember = "ID";
        }

        private void com_emp_assign_txt_Click(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [CONTACT_PERSON_NAME] FROM [dbo].[COMPANY] WHERE [dbo].[COMPANY].[COMPANY_NAME] = '" + com_assign_combo.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            com_emp_assign_txt.Text = tb_data.Rows[0][0].ToString();
            com_emp_assign_txt.Enabled = false;
        }

        private void update_com_emp_assign_txt_Click(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [CONTACT_PERSON_NAME] FROM [dbo].[COMPANY] WHERE [dbo].[COMPANY].[COMPANY_NAME] = '" + update_com_assign_combo.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            update_com_emp_assign_txt.Text = tb_data.Rows[0][0].ToString();
            update_com_emp_assign_txt.Enabled = false;
        }

        private void in_time_app_text_Click(object sender, EventArgs e)
        {
            if (app_timing_text.Text == "8 am to 5 pm")
            {
                in_time_app_text.Text = "8 am";
                out_time_app_text.Text = "5 pm";
                in_time_app_text.Enabled = false;
                out_time_app_text.Enabled = false;
            }
            else if (app_timing_text.Text == "5 pm to 1 am")
            {
                in_time_app_text.Text = "5 pm";
                out_time_app_text.Text = "1 am";
                in_time_app_text.Enabled = false;
                out_time_app_text.Enabled = false;
            }
            else if (app_timing_text.Text == "1 am to 8 am")
            {
                in_time_app_text.Text = "1 am";
                out_time_app_text.Text = "8 am";
                in_time_app_text.Enabled = false;
                out_time_app_text.Enabled = false;
            }
        }

        private void update_in_time_app_text_Click(object sender, EventArgs e)
        {
            if (update_app_timings.Text == "8 am to 5 pm")
            {
                update_in_time_app_text.Text = "8 am";
                update_out_time_app_text.Text = "5 pm";
                update_in_time_app_text.Enabled = false;
                update_out_time_app_text.Enabled = false;
            }
            else if (update_app_timings.Text == "5 pm to 1 am")
            {
                update_in_time_app_text.Text = "5 pm";
                update_out_time_app_text.Text = "1 am";
                update_in_time_app_text.Enabled = false;
                update_out_time_app_text.Enabled = false;
            }
            else if (update_app_timings.Text == "1 am to 8 am")
            {
                update_in_time_app_text.Text = "1 am";
                update_out_time_app_text.Text = "8 am";
                update_in_time_app_text.Enabled = false;
                update_out_time_app_text.Enabled = false;
            }
        }

        private void update_amount_due_payment_text_Click(object sender, EventArgs e)
        {
            //update_amount_due_payment_text.Text = (Convert.ToDouble(update_total_amount_payment_text.Text) - Convert.ToDouble(update_amount_receive_payment_text.Text)).ToString();
            //update_amount_due_payment_text.Enabled = false;
        }

        DataClasses1DataContext x_connection = new DataClasses1DataContext();
        OpenFileDialog file_excel_dialoge = new OpenFileDialog();

        string excel_sheet_location;
        private void browse_excel_sheet_Click(object sender, EventArgs e)
        {
            //Browsing Function
            file_excel_dialoge.Filter = "Excel Files (*.xlsx)|*.xlsx";
            file_excel_dialoge.Title = "Select employee attendance sheet";
            // file_dialog.InitialDirectory
            if (file_excel_dialoge.ShowDialog() == DialogResult.Cancel)

            {
                MetroFramework.MetroMessageBox.Show(this, "Couldn't import attendance sheet. Try again  !!", "Error");
                return;
            }
            else
            {
                FileStream stream = new FileStream(file_excel_dialoge.FileName, FileMode.Open);
                excel_sheet_location = file_excel_dialoge.FileName;
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DataSet result = excelDataReader.AsDataSet();

                foreach (System.Data.DataTable table in result.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        string hours = Convert.ToString(dr[3]);
                        TimeSpan ts = new TimeSpan(int.Parse(hours.Split(':')[0]),    // hours
                        int.Parse(hours.Split(':')[1]),    // minutes
                        0); // seconds
                        string minutes = ((decimal)ts.Hours * 60 + (decimal)ts.Minutes).ToString();

                        ATTENDANCE add_table = new ATTENDANCE()
                        {
                            EMP_NAME = Convert.ToString(dr[0]),
                            IN_TIME = Convert.ToString(dr[1]),
                            OUT_TIME = Convert.ToString(dr[2]),
                            DUTY_HOURS = minutes.ToString()
                        };
                        x_connection.ATTENDANCEs.InsertOnSubmit(add_table);
                    }

                }
                x_connection.SubmitChanges();
                excelDataReader.Close();
                stream.Close();
                fetch_atten_data();
                MetroFramework.MetroMessageBox.Show(this, "Attendance sheet imported successfully !!", "Success");
            }

        }
        private void fetch_atten_data()
        {
            x_sheet_txt.Text = excel_sheet_location;

            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT [ID],[EMP_NAME],[IN_TIME],[OUT_TIME],[DUTY_HOURS] FROM [dbo].[ATTENDANCE]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            atten_sheet_show.DataSource = tb_data;



            //set autosize mode
            atten_sheet_show.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            atten_sheet_show.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            atten_sheet_show.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i <= atten_sheet_show.Columns.Count - 1; i++)
            {
                //store autosized widths
                int colw = atten_sheet_show.Columns[i].Width;
                //remove autosizing
                atten_sheet_show.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                atten_sheet_show.Columns[i].Width = colw;
            }
        }

        private void Prepaid_tile_Click(object sender, EventArgs e)
        {
            mini_screen_dash.SelectedTab = Prepaid;
            bunifuImageButton2.Visible = true;
            //bunifuImageButton2.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/Ok_64px.png");
            //bunifuImageButton13.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/error.png");
            bunifuImageButton13.Visible = false;
        }

        private void operation_tile_Click(object sender, EventArgs e)
        {
            mini_screen_dash.SelectedTab = Operation;
            //bunifuImageButton9.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/Ok_64px.png");
            //bunifuImageButton12.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/error.png");
            bunifuImageButton9.Visible = true;
            bunifuImageButton12.Visible = false;
        }

        private void admission_tile_Click(object sender, EventArgs e)
        {
            mini_screen_dash.SelectedTab = Admission;
            //bunifuImageButton11.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/Ok_64px.png");
            //bunifuImageButton10.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/error.png");
            bunifuImageButton11.Visible = true;
            bunifuImageButton10.Visible = false;

        }

        private void employee_tile_Click(object sender, EventArgs e)
        {
            mini_screen_dash.SelectedTab = Employee;
            //bunifuImageButton8.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/Ok_64px.png");
            //bunifuImageButton8.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/error.png");
            bunifuImageButton1.Visible = true;
            bunifuImageButton8.Visible = false;
        }

        private void all_emp_screen_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            metroTextBox1.Text = all_emp_screen.CurrentRow.Cells[1].Value.ToString();
            metroTextBox2.Text = all_emp_screen.CurrentRow.Cells[2].Value.ToString();
            metroTextBox3.Text = all_emp_screen.CurrentRow.Cells[3].Value.ToString();
            metroComboBox1.Text = all_emp_screen.CurrentRow.Cells[4].Value.ToString();
            metroDateTime4.Value = Convert.ToDateTime(all_emp_screen.CurrentRow.Cells[5].Value);
            metroTextBox5.Text = all_emp_screen.CurrentRow.Cells[6].Value.ToString();
            metroTextBox4.Text = all_emp_screen.CurrentRow.Cells[7].Value.ToString();
            metroTextBox6.Text = all_emp_screen.CurrentRow.Cells[7].Value.ToString();
            richTextBox1.Text = all_emp_screen.CurrentRow.Cells[8].Value.ToString();
            bunifuImageButton1.Enabled = false;
            bunifuImageButton8.Enabled = true;
            bunifuImageButton1.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/error.png");
            bunifuImageButton8.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/Ok_64px.png");

        }

        private void back_to_main_screen(object sender, EventArgs e)
        {
            mini_screen_dash.SelectedTab = Main;
        }

        private void delete_employee_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[ADMIN_USER] WHERE [ID] = " + all_emp_screen.CurrentRow.Cells[0].Value + "", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MetroFramework.MetroMessageBox.Show(this, "Record Deleted !!", "Success");
                fetch_data();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");

            }
        }

        private void Delete_prepaid_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[PREPAID] WHERE [ID] = " + metroGrid4.CurrentRow.Cells[0].Value + "", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MetroFramework.MetroMessageBox.Show(this, "Record Deleted !!", "Success");
                fetch_data();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");

            }
        }

        private void fetch_data_employees_company()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[PREPAID]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            metroGrid4.DataSource = tb_data;
            SqlDataAdapter fetch_data1 = new SqlDataAdapter("SELECT * FROM [dbo].[MANAGER_OPERATION]", connection);
            System.Data.DataTable tb_data1 = new System.Data.DataTable();
            fetch_data1.Fill(tb_data1);
            metroGrid3.DataSource = tb_data1;
            SqlDataAdapter fetch_data2 = new SqlDataAdapter("SELECT * FROM [dbo].[MANAGER_ADMISSION]", connection);
            System.Data.DataTable tb_data2 = new System.Data.DataTable();
            fetch_data2.Fill(tb_data2);
            metroGrid2.DataSource = tb_data2;
            SqlDataAdapter fetch_data3 = new SqlDataAdapter("SELECT * FROM [dbo].[ADMIN_USER]", connection);
            System.Data.DataTable tb_data3 = new System.Data.DataTable();
            fetch_data3.Fill(tb_data3);
            all_emp_screen.DataSource = tb_data3;
        }

        private void Delete_operate_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[MANAGER_OPERATION] WHERE [ID] = " + metroGrid3.CurrentRow.Cells[0].Value + "", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MetroFramework.MetroMessageBox.Show(this, "Record Deleted !!", "Success");
                fetch_data();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");

            }
        }

        private void Delete_admission_btn_Click(object sender, EventArgs e)
        {
            Admin_authentication authentication = new Admin_authentication();
            authentication.ShowDialog();
            if (Admin_authentication.allow_user == true)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[MANAGER_ADMISSION] WHERE [ID] = " + metroGrid2.CurrentRow.Cells[0].Value + "", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MetroFramework.MetroMessageBox.Show(this, "Record Deleted !!", "Success");
                fetch_data();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Wrong password. You are not allowed to do the prefered function");

            }
        }

        private void metroGrid4_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            metroTextBox12.Text = metroGrid4.CurrentRow.Cells[1].Value.ToString();
            metroTextBox10.Text = metroGrid4.CurrentRow.Cells[2].Value.ToString();
            metroTextBox9.Text = metroGrid4.CurrentRow.Cells[3].Value.ToString();
            metroDateTime1.Value = Convert.ToDateTime(metroGrid4.CurrentRow.Cells[4].Value);
            metroTextBox11.Text = metroGrid4.CurrentRow.Cells[5].Value.ToString();
            metroTextBox7.Text = metroGrid4.CurrentRow.Cells[6].Value.ToString();
            metroTextBox8.Text = metroGrid4.CurrentRow.Cells[6].Value.ToString();
            richTextBox2.Text = metroGrid4.CurrentRow.Cells[7].Value.ToString();
            bunifuImageButton2.Visible = false;
            bunifuImageButton13.Visible = true;
            //bunifuImageButton2.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/error.png");
            //bunifuImageButton13.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/Ok_64px.png");
        }

        public void data_clear_employees()
        {
            metroTextBox12.Clear();
            metroTextBox10.Clear();
            metroTextBox9.Clear();
            metroTextBox11.Clear();
            metroTextBox7.Clear();
            metroTextBox8.Clear();
            richTextBox2.Clear();
            metroTextBox18.Clear();
            metroTextBox16.Clear();
            metroTextBox15.Clear();
            metroTextBox17.Clear();
            metroTextBox13.Clear();
            metroTextBox14.Clear();
            richTextBox3.Clear();
            metroTextBox24.Clear();
            metroTextBox22.Clear();
            metroTextBox21.Clear();
            metroTextBox23.Clear();
            metroTextBox19.Clear();
            metroTextBox20.Clear();
            richTextBox4.Clear();
            metroTextBox1.Clear();
            metroTextBox2.Clear();
            metroTextBox3.Clear();
            metroTextBox5.Clear();
            metroTextBox4.Clear();
            metroTextBox6.Clear();
            richTextBox1.Clear();
        }

        private void metroGrid3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            metroTextBox18.Text = metroGrid3.CurrentRow.Cells[1].Value.ToString();
            metroTextBox16.Text = metroGrid3.CurrentRow.Cells[2].Value.ToString();
            metroTextBox15.Text = metroGrid3.CurrentRow.Cells[3].Value.ToString();
            metroDateTime2.Value = Convert.ToDateTime(metroGrid3.CurrentRow.Cells[4].Value);
            metroTextBox17.Text = metroGrid3.CurrentRow.Cells[5].Value.ToString();
            metroTextBox13.Text = metroGrid3.CurrentRow.Cells[6].Value.ToString();
            metroTextBox14.Text = metroGrid3.CurrentRow.Cells[6].Value.ToString();
            richTextBox3.Text = metroGrid3.CurrentRow.Cells[7].Value.ToString();
            bunifuImageButton9.Enabled = false;
            bunifuImageButton12.Enabled = true;
            bunifuImageButton9.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/error.png");
            bunifuImageButton12.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/Ok_64px.png");
        }

        private void metroGrid2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            metroTextBox24.Text = metroGrid2.CurrentRow.Cells[1].Value.ToString();
            metroTextBox22.Text = metroGrid2.CurrentRow.Cells[2].Value.ToString();
            metroTextBox21.Text = metroGrid2.CurrentRow.Cells[3].Value.ToString();
            metroDateTime3.Value = Convert.ToDateTime(metroGrid2.CurrentRow.Cells[4].Value);
            metroTextBox23.Text = metroGrid2.CurrentRow.Cells[5].Value.ToString();
            metroTextBox19.Text = metroGrid2.CurrentRow.Cells[6].Value.ToString();
            metroTextBox20.Text = metroGrid2.CurrentRow.Cells[6].Value.ToString();
            richTextBox4.Text = metroGrid2.CurrentRow.Cells[7].Value.ToString();
            bunifuImageButton11.Enabled = false;
            bunifuImageButton10.Enabled = true;
            bunifuImageButton11.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/error.png");
            bunifuImageButton10.Image = Image.FromFile("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/Images/Ok_64px.png");
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            string pass;
            if (metroTextBox12.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox10.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (metroTextBox9.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox11.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox7.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox8.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (richTextBox2.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {
                if (metroTextBox7.Text == metroTextBox8.Text)
                {
                    pass = metroTextBox8.Text;
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PREPAID] ([NAME],[CONTACT_NUMBER],[CNIC_NUMBER],[DATE_OF_BIRTH],[EMAIL],[PASSWORD],[OTHER]) VALUES('" + metroTextBox12.Text + "','" + metroTextBox10.Text + "','" + metroTextBox9.Text + "','" + metroDateTime1.Text + "','" + metroTextBox11.Text + "','" + pass + "','" + richTextBox2.Text + "')", connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record Inserted !!", "Success");
                    fetch_data();
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Password doesn't match !!", "Error");
                }
            }

        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {

            string pass;
            if (metroTextBox18.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox16.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox15.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox17.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox13.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox14.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }

            else if (richTextBox3.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {


                if (metroTextBox13.Text == metroTextBox14.Text)
                {
                    pass = metroTextBox14.Text;
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[MANAGER_OPERATION] ([NAME],[CONTACT_NUMBER],[CNIC_NUMBER],[DATE_OF_BIRTH],[EMAIL],[PASSWORD],[OTHER]) VALUES('" + metroTextBox18.Text + "','" + metroTextBox16.Text + "','" + metroTextBox15.Text + "','" + metroDateTime2.Text + "','" + metroTextBox17.Text + "','" + pass + "','" + richTextBox3.Text + "')", connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record Inserted !!", "Success");
                    fetch_data();
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Password doesn't match !!", "Error");
                }
            }

        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            string pass;
            if (metroTextBox24.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox22.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox21.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox23.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox19.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox20.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (richTextBox4.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {
                if (metroTextBox19.Text == metroTextBox20.Text)
                {
                    pass = metroTextBox20.Text;
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[MANAGER_ADMISSION] ([NAME],[CONTACT_NUMBER],[CNIC_NUMBER],[DATE_OF_BIRTH],[EMAIL],[PASSWORD],[OTHER]) VALUES('" + metroTextBox24.Text + "','" + metroTextBox22.Text + "','" + metroTextBox21.Text + "','" + metroDateTime3.Text + "','" + metroTextBox23.Text + "','" + pass + "','" + richTextBox4.Text + "')", connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record Inserted !!", "Success");
                    fetch_data();
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Password doesn't match !!", "Error");
                }
            }

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            string pass;
            if (metroTextBox1.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox2.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }

            else if (metroTextBox3.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox5.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox4.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroTextBox6.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (richTextBox1.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {

                if (metroTextBox4.Text == metroTextBox4.Text)
                {
                    pass = metroTextBox6.Text;
                    SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[ADMIN_USER] ([NAME],[CONTACT_NUMBER],[CNIC_NUMBER],[DESIGNATION],[DATE_OF_BIRTH],[EMAIL],[PASSWORD],[OTHER]) VALUES('" + metroTextBox1.Text + "','" + metroTextBox2.Text + "','" + metroTextBox3.Text + "','" + metroComboBox1.Text + "','" + metroDateTime4.Text + "','" + metroTextBox5.Text + "','" + pass + "','" + richTextBox1.Text + "')", connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Record Inserted !!", "Success");
                    fetch_data();
                }
                else
                {
                    MetroFramework.MetroMessageBox.Show(this, "Password doesn't match !!", "Error");
                }
            }
        }

        private void bunifuImageButton13_Click(object sender, EventArgs e)
        {
            string pass;
            if (metroTextBox4.Text == metroTextBox4.Text)
            {
                pass = metroTextBox6.Text;
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[PREPAID] SET [NAME] = '" + metroTextBox12.Text + "',[CONTACT_NUMBER] = '" + metroTextBox10.Text + "',[CNIC_NUMBER] = '" + metroTextBox9.Text + "',[DATE_OF_BIRTH] = '" + metroDateTime1.Text + "',[EMAIL] = '" + metroTextBox11.Text + "',[PASSWORD] = '" + pass + "',[OTHER] = '" + richTextBox2.Text + "' WHERE ID = '" + metroGrid4.CurrentRow.Cells[0].Value + "'", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MetroFramework.MetroMessageBox.Show(this, "Record Updated !!", "Success");
                fetch_data();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Password doesn't match !!", "Error");
            }
        }

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            string pass;
            if (metroTextBox4.Text == metroTextBox4.Text)
            {
                pass = metroTextBox6.Text;
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[MANAGER_OPERATION] SET [NAME] = '" + metroTextBox18.Text + "',[CONTACT_NUMBER] = '" + metroTextBox16.Text + "',[CNIC_NUMBER] = '" + metroTextBox15.Text + "',[DATE_OF_BIRTH] = '" + metroDateTime2.Text + "',[EMAIL] = '" + metroTextBox17.Text + "',[PASSWORD] = '" + pass + "',[OTHER] = '" + richTextBox3.Text + "' WHERE ID = '" + metroGrid3.CurrentRow.Cells[0].Value + "'", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MetroFramework.MetroMessageBox.Show(this, "Record Updated !!", "Success");
                fetch_data();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Password doesn't match !!", "Error");
            }
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            string pass;
            if (metroTextBox4.Text == metroTextBox4.Text)
            {
                pass = metroTextBox6.Text;
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[MANAGER_ADMISSION] SET [NAME] = '" + metroTextBox24.Text + "',[CONTACT_NUMBER] = '" + metroTextBox22.Text + "',[CNIC_NUMBER] = '" + metroTextBox21.Text + "',[DATE_OF_BIRTH] = '" + metroDateTime3.Text + "',[EMAIL] = '" + metroTextBox23.Text + "',[PASSWORD] = '" + pass + "',[OTHER] = '" + richTextBox4.Text + "' WHERE ID = '" + metroGrid2.CurrentRow.Cells[0].Value + "'", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MetroFramework.MetroMessageBox.Show(this, "Record Updated !!", "Success");
                fetch_data();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Password doesn't match !!", "Error");
            }
        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            string pass;
            if (metroTextBox4.Text == metroTextBox4.Text)
            {
                pass = metroTextBox6.Text;
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[ADMIN_USER] SET [NAME] = '" + metroTextBox1.Text + "',[CONTACT_NUMBER] = '" + metroTextBox2.Text + "',[CNIC_NUMBER] = '" + metroTextBox3.Text + "',[DESIGNATION] = '" + metroComboBox1.Text + "',[DATE_OF_BIRTH] = '" + metroDateTime4.Text + "',[EMAIL] = '" + metroTextBox5.Text + "',[PASSWORD] = '" + pass + "',[OTHER] = '" + richTextBox1.Text + "' WHERE ID = '" + all_emp_screen.CurrentRow.Cells[0].Value + "'", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MetroFramework.MetroMessageBox.Show(this, "Record Updated !!", "Success");
                fetch_data();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Password doesn't match !!", "Error");
            }
        }

        ReportDocument report = new ReportDocument();

        private void Ok_Click(object sender, EventArgs e)
        {
            if (category_reports.Text == "Guards")
            {
                report.Load("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/WindowsFormsApp5/Crystal_Reports/Guards.rpt");
            }
            if (category_reports.Text == "Employees")
            {
                report.Load("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/WindowsFormsApp5/Crystal_Reports/Employees.rpt");
            }

            if (category_reports.Text == "Assignments")
            {
                report.Load("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/WindowsFormsApp5/Crystal_Reports/Assignment.rpt");
            }

            if (category_reports.Text == "Appointments")
            {
                report.Load("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/WindowsFormsApp5/Crystal_Reports/Appointment.rpt");
            }

            if (category_reports.Text == "Companies")
            {
                report.Load("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/WindowsFormsApp5/Crystal_Reports/Companies.rpt");
            }
            if (category_reports.Text == "Company Branches")
            {
                report.Load("C:/Users/JAZZEL/Desktop/Main/WindowsFormsApp5/WindowsFormsApp5/Crystal_Reports/Branches.rpt");
            }
            panel266.Enabled = true;
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroCheckBox1.Checked == true)
            {
                metroTextBox25.Enabled = true;
                metroDateTime1.Enabled = false;
                metroDateTime2.Enabled = false;
            }
            else
            {
                metroTextBox25.Enabled = false;
                metroDateTime1.Enabled = true;
                metroDateTime2.Enabled = true;
            }
        }

        private void Search_btn_Click(object sender, EventArgs e)
        {

            string stringFormula;
            DateTime FirstDate = metroDateTime5.Value;
            DateTime SecondDate = metroDateTime6.Value;

            crystalReportViewer1.ReportSource = report;

            if (metroCheckBox1.Checked == true)
            {
                if (!string.IsNullOrEmpty(this.metroTextBox25.Text))
                {
                    if (category_reports.Text == "Guards")
                    {
                        stringFormula = "{EMPLOYEE_GUARD.PREPAID_BY} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.MANAGER_OPERATION} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.MANAGER_ADMISSION} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.NAME} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.STATUS} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.PRESENT_ADDRESS} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.PERMANENT_ADDRESS} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.CNIC} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.MARTIAL_STATUS} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.DOB} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.RELIGION} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.EDUCATION} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.SECTION} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {EMPLOYEE_GUARD.SALARY} Like '*" + metroTextBox25.Text.ToString() + "*'";
                    }
                    else if (category_reports.Text == "Employees")
                    {
                        stringFormula = "{ADMIN_USER.NAME} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {ADMIN_USER.CONTACT_NUMBER} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {ADMIN_USER.CNIC_NUMBER} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {ADMIN_USER.DESIGNATION} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {ADMIN_USER.DATE_OF_BIRTH} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {ADMIN_USER.EMAIL} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {ADMIN_USER.PASSWORD} Like '*" + metroTextBox25.Text.ToString() + "*'";
                    }

                    else if (category_reports.Text == "Assignments")
                    {
                        stringFormula = "{ASSIGNMENT.COMPANY} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {ASSIGNMENT.COMPANY_BRANCH} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {ASSIGNMENT.COMPANY_BRANCH_EMPLOYEE} Like '*" + metroTextBox25.Text.ToString() + "*'";
                    }

                    else if (category_reports.Text == "Appointments")
                    {
                        stringFormula = "{APPOINTMENT.COMPANY} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {APPOINTMENT.COMPANY_BRANCH} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {APPOINTMENT.NO_OF_PERSON} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {APPOINTMENT.AMOUNT_PER_PERSON} Like '*" + metroTextBox25.Text.ToString() + "*'";
                    }

                    else if (category_reports.Text == "Companies")
                    {
                        stringFormula = "{COMPANY.COMPANY_NAME} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY.COMPANY_ADDRESS} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY.CITY} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY.CONTACT_PERSON_NAME} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY.CONTACT_PERSON_DESIGNATION} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY.CONTACT_PERSON_EMAIL} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY.CONTACT_PERSON_CELL_NUMBER} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY.COMPANY_PHONE_ONE} Like '*" + metroTextBox25.Text.ToString() + "*'";


                    }
                    else if (category_reports.Text == "Company Branches")
                    {
                        stringFormula = "OR {COMPANY_BRANCHES_DETAIL.COMPANY_NAME} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY_BRANCHES_DETAIL.BRANCH_NAME} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY_BRANCHES_DETAIL.COMPANY_ADDRESS} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY_BRANCHES_DETAIL.CITY} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY_BRANCHES_DETAIL.CONTACT_PERSON_NAME} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY_BRANCHES_DETAIL.CONTACT_PERSON_DESIGNATION} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY_BRANCHES_DETAIL.CONTACT_PERSON_EMAIL} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY_BRANCHES_DETAIL.CONTACT_PERSON_CELL_NUMBER} Like '*" + metroTextBox25.Text.ToString() + "*'";
                        stringFormula += "OR {COMPANY_BRANCHES_DETAIL.CONTACT_PERSON_PHONE_ONE} Like '*" + metroTextBox25.Text.ToString() + "*'";
                    }
                    else
                    {
                        stringFormula = "";
                    }

                }
                else
                {

                    stringFormula = "";


                }
            }
            else
            {
                string textBox1 = metroDateTime5.Value.ToLongDateString();
                string textBox2 = metroDateTime6.Value.ToLongDateString();
                if (category_reports.Text == "Guards")
                {
                    stringFormula = "{EMPLOYEE_GUARD.DATE_OF_ENROLLMENT} >= '" + textBox1.ToString() + "' and {EMPLOYEE_GUARD.DATE_OF_ENROLLMENT} <= '" + textBox2.ToString() + "'";

                }

                else if (category_reports.Text == "Assignments")
                {
                    stringFormula = "{ASSIGNMENT.FROM_TIME} >= '" + textBox1.ToString() + "' and {ASSIGNMENT.FROM_TIME} <= '" + textBox2.ToString() + "'";
                    stringFormula += "OR {ASSIGNMENT.TO_TIME} >= '" + textBox1.ToString() + "' and {ASSIGNMENT.TO_TIME} <= '" + textBox2.ToString() + "'";
                }

                else if (category_reports.Text == "Appointments")
                {
                    stringFormula = "{APPOINTMENT.FROM_TIME} >= '" + textBox1.ToString() + "' and {APPOINTMENT.FROM_TIME} <= '" + textBox2.ToString() + "'";
                    stringFormula += "OR {APPOINTMENT.TO_TIME} >= '" + textBox1.ToString() + "' and {APPOINTMENT.TO_TIME} <= '" + textBox2.ToString() + "'";
                }

                else if (category_reports.Text == "Companies")
                {
                    stringFormula = "{COMPANY.REGISTRATION_DATE} >= '" + textBox1.ToString() + "' and {COMPANY.REGISTRATION_DATE} <= '" + textBox2.ToString() + "'";

                }
                else if (category_reports.Text == "Company Branches")
                {
                    stringFormula = "{COMPANY_BRANCHES_DETAIL.REGISTRATION_DATE} >= '" + textBox1.ToString() + "' and {COMPANY_BRANCHES_DETAIL.REGISTRATION_DATE} <= '" + textBox2.ToString() + "'";
                }
                else
                {
                    stringFormula = "";
                }

            }
            crystalReportViewer1.SelectionFormula = stringFormula;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.RefreshReport();
            panel267.Enabled = true;
        }

        private void category_reports_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel266.Enabled = false;
            panel267.Enabled = false;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            login_functions();
            theme();
            payment_load();
        }

        private void payment_load()
        {
            SqlDataAdapter fetch_number = new SqlDataAdapter("SELECT COUNT(*) FROM [dbo].[PAYMENTS]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_number.Fill(tb_data);
            int bill = Convert.ToInt32(tb_data.Rows[0][0].ToString());
            bill++;
            bill_txt.Text = "Bill # " + bill;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void metroTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void metroTextBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void metroTextBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void metroTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {
            if (metroDateTime1.Value == DateTime.Now)
            {
                MetroFramework.MetroMessageBox.Show(this, "Nobody born today can work here. Enter the correct date of birth !!");
            }
        }

        private void metroDateTime2_ValueChanged(object sender, EventArgs e)
        {

            if (metroDateTime2.Value == DateTime.Now)
            {
                MetroFramework.MetroMessageBox.Show(this, "Nobody born today can work here. Enter the correct date of birth !!");
            }
        }

        private void metroDateTime3_ValueChanged(object sender, EventArgs e)
        {

            if (metroDateTime3.Value == DateTime.Now)
            {
                MetroFramework.MetroMessageBox.Show(this, "Nobody born today can work here. Enter the correct date of birth !!");
            }
        }

        private void metroDateTime4_ValueChanged(object sender, EventArgs e)
        {

            if (metroDateTime4.Value == DateTime.Now)
            {
                MetroFramework.MetroMessageBox.Show(this, "Nobody born today can work here. Enter the correct date of birth !!");
            }
        }

        private void dob_date_ValueChanged(object sender, EventArgs e)
        {
            if (dob_date.Value == DateTime.Now)
            {
                MetroFramework.MetroMessageBox.Show(this, "Nobody born today can work here. Enter correct date of birth !!");
            }
        }

        private void update_dob_date_ValueChanged(object sender, EventArgs e)
        {
            if (update_dob_date.Value == DateTime.Now)
            {
                MetroFramework.MetroMessageBox.Show(this, "Nobody born today can work here. Enter correct date of birth !!");
            }
        }

        private void edit_app_of_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[COMPANY_BRANCHES_DETAIL] WHERE [dbo].[COMPANY_BRANCHES_DETAIL].[COMPANY_NAME] = '" + edit_app_of_com.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            edit_app_of_com_br.DataSource = tb_data;
            edit_app_of_com_br.DisplayMember = "BRANCH_NAME";
            edit_app_of_com_br.ValueMember = "ID";
        }

        private void data_fetch()
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[APPPOINTMENT] WHERE [dbo].[APPPOINTMENT].[COMPANY] = '" + edit_app_of_com.Text + "' AND [dbo].[APPPOINTMENT].[COMPANY_BRANCH] = '" + edit_app_of_com_br.Text + "' AND [dbo].[APPPOINTMENT].[STATUS] = 'On-Duty'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            show_app_update_data.DataSource = tb_data;
        }

        private void edit_app_of_com_br_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void search_guards_btn_Click(object sender, EventArgs e)
        {
            data_fetch();
        }


        private void show_app_update_data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guard_com = show_app_update_data.CurrentRow.Cells[1].Value.ToString();
            guard_com_br = show_app_update_data.CurrentRow.Cells[2].Value.ToString();
            guard_name = show_app_update_data.CurrentRow.Cells[3].Value.ToString();
            guard_from = ((DateTime.Now).ToLongDateString()).ToString();
            guard_to = show_app_update_data.CurrentRow.Cells[5].Value.ToString();
            guard_timings = show_app_update_data.CurrentRow.Cells[6].Value.ToString();
            guard_amount = show_app_update_data.CurrentRow.Cells[7].Value.ToString();
            guard_time_from = show_app_update_data.CurrentRow.Cells[8].Value.ToString();
            guard_time_to = show_app_update_data.CurrentRow.Cells[9].Value.ToString();
            AlternateGuard alternate = new AlternateGuard();
            alternate.ShowDialog();
            if (AlternateGuard.check == false)
            {
                MetroFramework.MetroMessageBox.Show(this, "No change in appointments occured !!", "Success");
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Leave application for the prefered guard uploaded. Now you can send an alternate guard !!", "Success");
                LeaveApplication leave = new LeaveApplication();
                leave.ShowDialog();
                MetroFramework.MetroMessageBox.Show(this, "Alternate guard hired !!", "Success");
                data_fetch();
            }
        }

        private void fetch_guards_data()
        {
            SqlDataAdapter fetch = new SqlDataAdapter("SELECT * FROM [dbo].[APPPOINTMENT]", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch.Fill(tb_data);
            metroGrid5.DataSource = tb_data;
        }

        private void guard_search_bar_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[APPPOINTMENT] WHERE [dbo].[APPPOINTMENT].[NO_OF_PERSON] = '" + guard_search_bar.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            metroGrid5.DataSource = tb_data;
            if (search_update_emp.Text == "")
            {
                this.fetch_data();
            }
        }

        private void search_guards_bar_OnValueChanged(object sender, EventArgs e)
        {
            SqlDataAdapter fetch_data = new SqlDataAdapter("SELECT * FROM [dbo].[APPPOINTMENT] WHERE [dbo].[APPPOINTMENT].[NO_OF_PERSON] = '" + search_guards_bar.Text + "'", connection);
            System.Data.DataTable tb_data = new System.Data.DataTable();
            fetch_data.Fill(tb_data);
            show_app_update_data.DataSource = tb_data;
            if (search_update_emp.Text == "")
            {
                this.fetch_data();
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            mini_screen_assignment.SelectedTab = status_emp_tab;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            mini_screen_app.SelectedTab = show_current_emp;
        }


        private void guard_status_btn(object sender, EventArgs e)
        {
            if (metroComboBox2.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (metroComboBox3.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[APPPOINTMENT] SET [dbo].[APPPOINTMENT].[STATUS] = 'Offline' WHERE ID = '" + metroGrid5.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Guard is now active to do the duty again !!", "Success");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void amount_rate_Click(object sender, EventArgs e)
        {

        }

        private void amount_rate_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = amount_rate.Text;
            string qty_txt = amount_qty.Text;
            decimal rate;
            decimal qty;
            if (amount_rate.Text == "")
            {
                amount_rate.Text = "10";
            }
            else
            {
                if (qty_txt == "")
                {
                    amount_qty.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(rate_txt);
                    decimal answer = rate * qty;

                    pre_tax.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    pre_tax.Text = "Rs " + answer.ToString();
                }
            }

        }

        private void amount_qty_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = amount_rate.Text;
            string qty_txt = amount_qty.Text;
            decimal rate;
            decimal qty;
            if (amount_qty.Text == "")
            {
                amount_qty.Text = "10";
            }
            else
            {
                if (rate_txt == "")
                {
                    amount_rate.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    pre_tax.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    pre_tax.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void gst_rate_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = gst_rate.Text;
            string qty_txt = gst_amount.Text;
            decimal rate;
            decimal qty;
            if (amount_rate.Text == "")
            {
                amount_rate.Text = "10";
            }
            else
            {
                if (qty_txt == "")
                {
                    gst_amount.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(rate_txt);
                    decimal answer = rate * qty;

                    gst.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    gst.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void gst_amount_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = gst_rate.Text;
            string qty_txt = gst_amount.Text;
            decimal rate;
            decimal qty;
            if (gst_amount.Text == "")
            {
                gst_amount.Text = "10";
            }
            else
            {
                if (rate_txt == "")
                {
                    gst_rate.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    gst.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    gst.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void gst_w_rate_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = gst_w_rate.Text;
            string qty_txt = gst_w_amount.Text;
            decimal rate;
            decimal qty;
            if (gst_w_rate.Text == "")
            {
                gst_w_rate.Text = "10";
            }
            else
            {
                if (qty_txt == "")
                {
                    gst_w_amount.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(rate_txt);
                    decimal answer = rate * qty;

                    gst_w.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    gst_w.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void gst_w_amount_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = gst_w_rate.Text;
            string qty_txt = gst_w_amount.Text;
            decimal rate;
            decimal qty;
            if (gst_w_amount.Text == "")
            {
                gst_w_amount.Text = "10";
            }
            else
            {
                if (rate_txt == "")
                {
                    gst_w_rate.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    gst_w.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    gst_w.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void pre_tax_TextChanged(object sender, EventArgs e)
        {

            string pre_taxt_value = "";
            String[] tax_array = pre_tax.Text.Split();
            foreach (var item in tax_array)
            {
                pre_taxt_value = item;
            }
            decimal tax_amount = Convert.ToDecimal(pre_taxt_value);

            string gst_value = "";
            String[] gst_array = gst.Text.Split();
            foreach (var item in gst_array)
            {
                gst_value = item;
            }
            decimal gst_amount = Convert.ToDecimal(gst_value);

            string gst_w_value = "";
            String[] gst_w_array = gst_w.Text.Split();
            foreach (var item in gst_w_array)
            {
                gst_w_value = item;
            }
            decimal gst_w_amount = Convert.ToDecimal(gst_w_value);

            total.Text = "Rs " + (tax_amount + gst_amount + gst_w_amount).ToString();
        }

        private void com_br_payment_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                SqlDataAdapter search_officer = new SqlDataAdapter("SELECT [CONTACT_PERSON_NAME] FROM [dbo].[COMPANY_BRANCHES_DETAIL] WHERE [dbo].[COMPANY_BRANCHES_DETAIL].[BRANCH_NAME] = '" + com_br_payment_combo.Text + "'", connection);


                System.Data.DataTable tb_data = new System.Data.DataTable();
                search_officer.Fill(tb_data);
                com_emp_txt.Text = tb_data.Rows[0][0].ToString();
            }
            catch (Exception error)
            {

            }
        }

        private void update_am_rate_for_payment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_am_rate_for_payment_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = update_am_rate_for_payment.Text;
            string qty_txt = update_am_quantity_for_payment.Text;
            decimal rate;
            decimal qty;
            if (update_am_rate_for_payment.Text == "")
            {
                update_am_rate_for_payment.Text = "10";
            }
            else
            {
                if (qty_txt == "")
                {
                    update_am_quantity_for_payment.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(rate_txt);
                    decimal answer = rate * qty;

                    updated_pre_tax.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;
                    updated_pre_tax.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void update_am_quantity_for_payment_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = update_am_rate_for_payment.Text;
            string qty_txt = update_am_quantity_for_payment.Text;
            decimal rate;
            decimal qty;
            if (update_am_quantity_for_payment.Text == "")
            {
                update_am_quantity_for_payment.Text = "10";
            }
            else
            {
                if (rate_txt == "")
                {
                    update_am_rate_for_payment.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    updated_pre_tax.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    updated_pre_tax.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void update_gst_rate_for_payment_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = update_gst_rate_for_payment.Text;
            string qty_txt = update_gst_amount_for_payment.Text;
            decimal rate;
            decimal qty;
            if (update_gst_rate_for_payment.Text == "")
            {
                update_gst_rate_for_payment.Text = "10";
            }
            else
            {
                if (qty_txt == "")
                {
                    update_gst_amount_for_payment.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(rate_txt);
                    decimal answer = rate * qty;

                    updated_gst.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    updated_gst.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void update_gst_amount_for_payment_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = update_gst_rate_for_payment.Text;
            string qty_txt = update_gst_amount_for_payment.Text;
            decimal rate;
            decimal qty;
            if (update_gst_amount_for_payment.Text == "")
            {
                update_gst_amount_for_payment.Text = "10";
            }
            else
            {
                if (rate_txt == "")
                {
                    update_gst_rate_for_payment.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    updated_gst.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    updated_gst.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void update_gst_w_rate_for_payment_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = update_gst_w_rate_for_payment.Text;
            string qty_txt = update_gst_w_amount_for_payment.Text;
            decimal rate;
            decimal qty;
            if (update_gst_w_rate_for_payment.Text == "")
            {
                update_gst_w_rate_for_payment.Text = "10";
            }
            else
            {
                if (qty_txt == "")
                {
                    update_gst_w_amount_for_payment.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(rate_txt);
                    decimal answer = rate * qty;

                    updated_gst_w.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    updated_gst_w.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void update_gst_w_amount_for_payment_TextChanged(object sender, EventArgs e)
        {
            string rate_txt = update_gst_w_rate_for_payment.Text;
            string qty_txt = update_gst_w_amount_for_payment.Text;
            decimal rate;
            decimal qty;
            if (update_gst_w_amount_for_payment.Text == "")
            {
                update_gst_w_amount_for_payment.Text = "10";
            }
            else
            {
                if (rate_txt == "")
                {
                    update_gst_w_rate_for_payment.Text = "10";
                    qty = 10;
                    rate = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    updated_gst_w.Text = "Rs " + answer.ToString();
                }
                else
                {

                    rate = Convert.ToDecimal(rate_txt);
                    qty = Convert.ToDecimal(qty_txt);
                    decimal answer = rate * qty;

                    updated_gst_w.Text = "Rs " + answer.ToString();
                }
            }
        }

        private void updated_pre_tax_TextChanged(object sender, EventArgs e)
        {

            string pre_taxt_value = "";
            String[] tax_array = updated_pre_tax.Text.Split();
            foreach (var item in tax_array)
            {
                pre_taxt_value = item;
            }
            decimal tax_amount = Convert.ToDecimal(pre_taxt_value);

            string gst_value = "";
            String[] gst_array = updated_gst.Text.Split();
            foreach (var item in gst_array)
            {
                gst_value = item;
            }
            decimal gst_amount = Convert.ToDecimal(gst_value);

            string gst_w_value = "";
            String[] gst_w_array = updated_gst_w.Text.Split();
            foreach (var item in gst_w_array)
            {
                gst_w_value = item;
            }
            decimal gst_w_amount = Convert.ToDecimal(gst_w_value);

            updated_total.Text = "Rs " + (tax_amount + gst_amount + gst_w_amount).ToString();
        }

        private void update_payment_details_btn_Click(object sender, EventArgs e)
        {
            if (update_date_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_com_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_com_br_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_br_off_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_am_rate_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_am_quantity_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_gst_rate_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_gst_amount_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_gst_w_rate_for_payment.Text == "")
            {

                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");
            }
            else if (update_gst_w_amount_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else if (update_remarks_for_payment.Text == "")
            {
                MetroFramework.MetroMessageBox.Show(this, "Please fill out all the details !!", "Error");

            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[PAYMENTS] SET [DATE] = '" + update_date_for_payment.Value + "',[COMPANY] = '" + update_com_for_payment.Text + "',[COMPANY_BRANCH] = '" + update_com_br_for_payment.Text + "',[BRANCH_OFFICER] = '" + update_br_off_for_payment.Text + "',[AMOUNT_RATE] = '" + update_am_rate_for_payment.Text + "',[AMOUNT_QUANTITY] = '" + update_am_quantity_for_payment.Text + "',[GST_RATE] = '" + update_gst_rate_for_payment.Text + "',[GST_AMOUNT] = '" + update_gst_amount_for_payment.Text + "',[GST_WITHHELD_RATE] = '" + update_gst_w_rate_for_payment.Text + "',[GST_WITHHELD_AMOUNT] = '" + update_gst_w_amount_for_payment.Text + "',[REMARKS] = '" + update_remarks_for_payment.Text + "' WHERE [ID] = '" + update_payment_screen.CurrentRow.Cells[0].Value + "'", connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Payment details updated !!", "Success");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void delete_payment_details_btn_Click(object sender, EventArgs e)
        {
            int delete_id = Convert.ToInt32(delete_payment_screen.CurrentRow.Cells[0].Value.ToString());

        }
    }
}