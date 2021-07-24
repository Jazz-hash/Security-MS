namespace Security_ms
{
    partial class AlternateGuard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlternateGuard));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuElipse2 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.OK = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.guard_reason_name_txt = new MetroFramework.Controls.MetroTextBox();
            this.guard_reason_txt = new System.Windows.Forms.RichTextBox();
            this.Cancel = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 0;
            this.bunifuElipse1.TargetControl = this;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gray;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(790, 35);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 405);
            this.panel4.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 405);
            this.panel3.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 440);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 10);
            this.panel2.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(790, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(10, 35);
            this.panel5.TabIndex = 5;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(12, 8);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 20);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(322, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Leave Application";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.pictureBox4);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(752, 35);
            this.panel6.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 35);
            this.panel1.TabIndex = 13;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.ElipseRadius = 0;
            this.bunifuElipse2.TargetControl = this;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.Cancel);
            this.panel7.Controls.Add(this.OK);
            this.panel7.Controls.Add(this.metroLabel2);
            this.panel7.Controls.Add(this.metroLabel1);
            this.panel7.Controls.Add(this.guard_reason_name_txt);
            this.panel7.Controls.Add(this.guard_reason_txt);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(10, 35);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(780, 405);
            this.panel7.TabIndex = 17;
            // 
            // OK
            // 
            this.OK.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.OK.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.OK.Location = new System.Drawing.Point(473, 322);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 29);
            this.OK.TabIndex = 25;
            this.OK.Text = "Next";
            this.OK.UseSelectable = true;
            this.OK.Click += new System.EventHandler(this.next_btn_1_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.White;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(181, 158);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(71, 25);
            this.metroLabel2.TabIndex = 23;
            this.metroLabel2.Text = "Reason:";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.White;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(181, 93);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(63, 25);
            this.metroLabel1.TabIndex = 24;
            this.metroLabel1.Text = "Guard:";
            // 
            // guard_reason_name_txt
            // 
            // 
            // 
            // 
            this.guard_reason_name_txt.CustomButton.Image = null;
            this.guard_reason_name_txt.CustomButton.Location = new System.Drawing.Point(224, 1);
            this.guard_reason_name_txt.CustomButton.Name = "";
            this.guard_reason_name_txt.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.guard_reason_name_txt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.guard_reason_name_txt.CustomButton.TabIndex = 1;
            this.guard_reason_name_txt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.guard_reason_name_txt.CustomButton.UseSelectable = true;
            this.guard_reason_name_txt.CustomButton.Visible = false;
            this.guard_reason_name_txt.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.guard_reason_name_txt.Lines = new string[0];
            this.guard_reason_name_txt.Location = new System.Drawing.Point(296, 93);
            this.guard_reason_name_txt.MaxLength = 32767;
            this.guard_reason_name_txt.Name = "guard_reason_name_txt";
            this.guard_reason_name_txt.PasswordChar = '\0';
            this.guard_reason_name_txt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.guard_reason_name_txt.SelectedText = "";
            this.guard_reason_name_txt.SelectionLength = 0;
            this.guard_reason_name_txt.SelectionStart = 0;
            this.guard_reason_name_txt.ShortcutsEnabled = true;
            this.guard_reason_name_txt.Size = new System.Drawing.Size(252, 29);
            this.guard_reason_name_txt.TabIndex = 22;
            this.guard_reason_name_txt.UseSelectable = true;
            this.guard_reason_name_txt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.guard_reason_name_txt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // guard_reason_txt
            // 
            this.guard_reason_txt.Location = new System.Drawing.Point(296, 158);
            this.guard_reason_txt.Name = "guard_reason_txt";
            this.guard_reason_txt.Size = new System.Drawing.Size(252, 134);
            this.guard_reason_txt.TabIndex = 21;
            this.guard_reason_txt.Text = "";
            // 
            // Cancel
            // 
            this.Cancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Cancel.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.Cancel.Location = new System.Drawing.Point(388, 322);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 29);
            this.Cancel.TabIndex = 25;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseSelectable = true;
            this.Cancel.Click += new System.EventHandler(this.next_btn_2_Click);
            // 
            // AlternateGuard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AlternateGuard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlternateGuard";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel7;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox guard_reason_name_txt;
        private System.Windows.Forms.RichTextBox guard_reason_txt;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse2;
        private MetroFramework.Controls.MetroButton OK;
        private MetroFramework.Controls.MetroButton Cancel;
    }
}