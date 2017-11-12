namespace XML2Table
{
    partial class Xml2Db
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Xml2Db));
            this.txtBox = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_box_clean = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBx = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblNewStatus = new System.Windows.Forms.Label();
            this.openXmlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radio_btn_full_run = new System.Windows.Forms.RadioButton();
            this.radio_btn_test_run = new System.Windows.Forms.RadioButton();
            this.btn_processMediaMigration = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBox
            // 
            this.txtBox.Location = new System.Drawing.Point(47, 83);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(734, 38);
            this.txtBox.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(803, 67);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(229, 68);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse XML";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_box_clean);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.checkBx);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnUpload);
            this.groupBox1.Controls.Add(this.txtBox);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Location = new System.Drawing.Point(45, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1071, 343);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FW XML to Classification Table Export";
            // 
            // chk_box_clean
            // 
            this.chk_box_clean.AutoSize = true;
            this.chk_box_clean.Checked = true;
            this.chk_box_clean.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_box_clean.Location = new System.Drawing.Point(58, 271);
            this.chk_box_clean.Name = "chk_box_clean";
            this.chk_box_clean.Size = new System.Drawing.Size(278, 36);
            this.chk_box_clean.TabIndex = 6;
            this.chk_box_clean.Text = "Clean Sequences";
            this.chk_box_clean.UseVisualStyleBackColor = true;
            this.chk_box_clean.CheckedChanged += new System.EventHandler(this.chk_box_clean_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(464, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(247, 92);
            this.button1.TabIndex = 5;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // checkBx
            // 
            this.checkBx.AutoSize = true;
            this.checkBx.Location = new System.Drawing.Point(58, 197);
            this.checkBx.Name = "checkBx";
            this.checkBx.Size = new System.Drawing.Size(214, 36);
            this.checkBx.TabIndex = 3;
            this.checkBx.Text = "Include Logs";
            this.checkBx.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select XML file";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(760, 215);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(272, 92);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Text = "Upload to Db";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1522, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblNewStatus);
            this.groupBox2.Location = new System.Drawing.Point(45, 633);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1071, 130);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Upload Status";
            // 
            // lblNewStatus
            // 
            this.lblNewStatus.AutoSize = true;
            this.lblNewStatus.Location = new System.Drawing.Point(26, 63);
            this.lblNewStatus.Name = "lblNewStatus";
            this.lblNewStatus.Size = new System.Drawing.Size(23, 32);
            this.lblNewStatus.TabIndex = 4;
            this.lblNewStatus.Text = ".";
            // 
            // openXmlFileDialog
            // 
            this.openXmlFileDialog.FileName = "openXmlFileDialog";
            this.openXmlFileDialog.Filter = "XML files (*.xml)|*.xml";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radio_btn_full_run);
            this.groupBox3.Controls.Add(this.radio_btn_test_run);
            this.groupBox3.Controls.Add(this.btn_processMediaMigration);
            this.groupBox3.Location = new System.Drawing.Point(45, 433);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1071, 175);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Media Migration";
            // 
            // radio_btn_full_run
            // 
            this.radio_btn_full_run.AutoSize = true;
            this.radio_btn_full_run.Location = new System.Drawing.Point(530, 73);
            this.radio_btn_full_run.Name = "radio_btn_full_run";
            this.radio_btn_full_run.Size = new System.Drawing.Size(158, 36);
            this.radio_btn_full_run.TabIndex = 2;
            this.radio_btn_full_run.Text = "Full Run";
            this.radio_btn_full_run.UseVisualStyleBackColor = true;
            // 
            // radio_btn_test_run
            // 
            this.radio_btn_test_run.AutoSize = true;
            this.radio_btn_test_run.Checked = true;
            this.radio_btn_test_run.Location = new System.Drawing.Point(35, 73);
            this.radio_btn_test_run.Name = "radio_btn_test_run";
            this.radio_btn_test_run.Size = new System.Drawing.Size(460, 36);
            this.radio_btn_test_run.TabIndex = 1;
            this.radio_btn_test_run.TabStop = true;
            this.radio_btn_test_run.Text = "Test Run (Process 100 Records)";
            this.radio_btn_test_run.UseVisualStyleBackColor = true;
            // 
            // btn_processMediaMigration
            // 
            this.btn_processMediaMigration.Location = new System.Drawing.Point(760, 43);
            this.btn_processMediaMigration.Name = "btn_processMediaMigration";
            this.btn_processMediaMigration.Size = new System.Drawing.Size(272, 96);
            this.btn_processMediaMigration.TabIndex = 0;
            this.btn_processMediaMigration.Text = "Process";
            this.btn_processMediaMigration.UseVisualStyleBackColor = true;
            this.btn_processMediaMigration.Click += new System.EventHandler(this.btn_processMediaMigration_Click);
            // 
            // Xml2Db
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 813);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Xml2Db";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xml2Db";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Xml2Db_FormClosing);
            this.Load += new System.EventHandler(this.Xml2Db_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        //private static System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.OpenFileDialog openXmlFileDialog;
        private System.Windows.Forms.CheckBox checkBx;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblNewStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radio_btn_full_run;
        private System.Windows.Forms.RadioButton radio_btn_test_run;
        private System.Windows.Forms.Button btn_processMediaMigration;
        private System.Windows.Forms.CheckBox chk_box_clean;
    }
}

