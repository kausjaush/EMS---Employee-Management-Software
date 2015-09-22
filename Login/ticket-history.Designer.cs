namespace Login
{
    partial class ticket_history
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.empid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tickid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lreason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.till = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appldate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.appltime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file = new System.Windows.Forms.DataGridViewLinkColumn();
            this.aapproval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aadate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aatime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.half = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.emp_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 356);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.empid,
            this.tickid,
            this.lreason,
            this.from,
            this.till,
            this.appldate,
            this.appltime,
            this.file,
            this.aapproval,
            this.areason,
            this.aadate,
            this.aatime,
            this.nodays,
            this.half,
            this.type});
            this.dataGridView2.Location = new System.Drawing.Point(5, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(892, 347);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.Visible = false;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // empid
            // 
            this.empid.HeaderText = "Employee Number";
            this.empid.Name = "empid";
            // 
            // tickid
            // 
            this.tickid.HeaderText = "TW-ID";
            this.tickid.Name = "tickid";
            // 
            // lreason
            // 
            this.lreason.HeaderText = "Reason Of Leave";
            this.lreason.Name = "lreason";
            this.lreason.Width = 500;
            // 
            // from
            // 
            this.from.HeaderText = "From / On Date";
            this.from.Name = "from";
            // 
            // till
            // 
            this.till.HeaderText = "Till Date";
            this.till.Name = "till";
            // 
            // appldate
            // 
            this.appldate.HeaderText = "Application Date";
            this.appldate.Name = "appldate";
            this.appldate.Width = 200;
            // 
            // appltime
            // 
            this.appltime.HeaderText = "Application Time";
            this.appltime.Name = "appltime";
            this.appltime.ReadOnly = true;
            // 
            // file
            // 
            this.file.HeaderText = "Attachment";
            this.file.Name = "file";
            this.file.ReadOnly = true;
            this.file.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.file.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // aapproval
            // 
            this.aapproval.HeaderText = "Admin Approval";
            this.aapproval.Name = "aapproval";
            // 
            // areason
            // 
            this.areason.HeaderText = "Admin Reason";
            this.areason.Name = "areason";
            // 
            // aadate
            // 
            this.aadate.HeaderText = "Approval Date";
            this.aadate.Name = "aadate";
            // 
            // aatime
            // 
            this.aatime.HeaderText = "Approval Time";
            this.aatime.Name = "aatime";
            // 
            // nodays
            // 
            this.nodays.HeaderText = "Number of Days";
            this.nodays.Name = "nodays";
            // 
            // half
            // 
            this.half.HeaderText = "Half Day Leaves";
            this.half.Name = "half";
            // 
            // type
            // 
            this.type.HeaderText = "Type Of Application";
            this.type.Name = "type";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.emp_number,
            this.workid,
            this.comment,
            this.Date,
            this.Time,
            this.Reason});
            this.dataGridView1.Location = new System.Drawing.Point(4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(892, 347);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Visible = false;
            // 
            // emp_number
            // 
            this.emp_number.HeaderText = "Employee Number";
            this.emp_number.Name = "emp_number";
            // 
            // workid
            // 
            this.workid.HeaderText = "TW-ID";
            this.workid.Name = "workid";
            // 
            // comment
            // 
            this.comment.HeaderText = "Daily Reporting Statement";
            this.comment.Name = "comment";
            this.comment.Width = 500;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date of Report";
            this.Date.Name = "Date";
            // 
            // Time
            // 
            this.Time.HeaderText = "Time of Report Received";
            this.Time.Name = "Time";
            // 
            // Reason
            // 
            this.Reason.HeaderText = "Reason";
            this.Reason.Name = "Reason";
            this.Reason.Width = 200;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(898, 354);
            this.label1.TabIndex = 1;
            this.label1.Text = "----------------";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // ticket_history
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(924, 380);
            this.Controls.Add(this.panel1);
            this.Name = "ticket_history";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ticket Work History";
            this.Load += new System.EventHandler(this.ticket_history_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn emp_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn workid;
        private System.Windows.Forms.DataGridViewTextBoxColumn comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reason;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn empid;
        private System.Windows.Forms.DataGridViewTextBoxColumn tickid;
        private System.Windows.Forms.DataGridViewTextBoxColumn lreason;
        private System.Windows.Forms.DataGridViewTextBoxColumn from;
        private System.Windows.Forms.DataGridViewTextBoxColumn till;
        private System.Windows.Forms.DataGridViewTextBoxColumn appldate;
        private System.Windows.Forms.DataGridViewTextBoxColumn appltime;
        private System.Windows.Forms.DataGridViewLinkColumn file;
        private System.Windows.Forms.DataGridViewTextBoxColumn aapproval;
        private System.Windows.Forms.DataGridViewTextBoxColumn areason;
        private System.Windows.Forms.DataGridViewTextBoxColumn aadate;
        private System.Windows.Forms.DataGridViewTextBoxColumn aatime;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodays;
        private System.Windows.Forms.DataGridViewTextBoxColumn half;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}