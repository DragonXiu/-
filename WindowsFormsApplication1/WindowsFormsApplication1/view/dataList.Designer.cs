namespace HILYCode.view
{
    partial class dataList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dataList));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.orderTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePic1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePic2 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butCho = new System.Windows.Forms.Button();
            this.butLo = new System.Windows.Forms.Button();
            this.radio02917 = new System.Windows.Forms.RadioButton();
            this.radio03304 = new System.Windows.Forms.RadioButton();
            this.radio02916 = new System.Windows.Forms.RadioButton();
            this.radio03305 = new System.Windows.Forms.RadioButton();
            this.radio04560 = new System.Windows.Forms.RadioButton();
            this.radio04559 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 147);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 200;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(991, 610);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(908, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // orderTxt
            // 
            this.orderTxt.BackColor = System.Drawing.SystemColors.Menu;
            this.orderTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.orderTxt.Location = new System.Drawing.Point(128, 97);
            this.orderTxt.Name = "orderTxt";
            this.orderTxt.Size = new System.Drawing.Size(273, 21);
            this.orderTxt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(29, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "工单号：";
            // 
            // dateTimePic1
            // 
            this.dateTimePic1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePic1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePic1.Location = new System.Drawing.Point(128, 40);
            this.dateTimePic1.Name = "dateTimePic1";
            this.dateTimePic1.Size = new System.Drawing.Size(88, 21);
            this.dateTimePic1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(29, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "开始时间：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(222, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "结束时间：";
            // 
            // dateTimePic2
            // 
            this.dateTimePic2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePic2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePic2.Location = new System.Drawing.Point(310, 37);
            this.dateTimePic2.Name = "dateTimePic2";
            this.dateTimePic2.Size = new System.Drawing.Size(91, 21);
            this.dateTimePic2.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.butCho);
            this.groupBox1.Controls.Add(this.butLo);
            this.groupBox1.Controls.Add(this.radio02917);
            this.groupBox1.Controls.Add(this.radio03304);
            this.groupBox1.Controls.Add(this.radio02916);
            this.groupBox1.Controls.Add(this.radio03305);
            this.groupBox1.Controls.Add(this.radio04560);
            this.groupBox1.Controls.Add(this.radio04559);
            this.groupBox1.Location = new System.Drawing.Point(432, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 100);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择";
            // 
            // butCho
            // 
            this.butCho.BackColor = System.Drawing.Color.Transparent;
            this.butCho.Enabled = false;
            this.butCho.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butCho.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butCho.Location = new System.Drawing.Point(377, 59);
            this.butCho.Name = "butCho";
            this.butCho.Size = new System.Drawing.Size(87, 23);
            this.butCho.TabIndex = 16;
            this.butCho.Text = "重复查询";
            this.butCho.UseVisualStyleBackColor = false;
            this.butCho.Click += new System.EventHandler(this.butCho_Click);
            // 
            // butLo
            // 
            this.butLo.BackColor = System.Drawing.Color.Transparent;
            this.butLo.Enabled = false;
            this.butLo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butLo.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butLo.Location = new System.Drawing.Point(377, 16);
            this.butLo.Name = "butLo";
            this.butLo.Size = new System.Drawing.Size(87, 23);
            this.butLo.TabIndex = 10;
            this.butLo.Text = "漏扫查询";
            this.butLo.UseVisualStyleBackColor = false;
            this.butLo.Click += new System.EventHandler(this.butLo_Click);
            // 
            // radio02917
            // 
            this.radio02917.AutoSize = true;
            this.radio02917.BackColor = System.Drawing.Color.Transparent;
            this.radio02917.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio02917.Location = new System.Drawing.Point(106, 62);
            this.radio02917.Name = "radio02917";
            this.radio02917.Size = new System.Drawing.Size(71, 20);
            this.radio02917.TabIndex = 15;
            this.radio02917.TabStop = true;
            this.radio02917.Text = "02917";
            this.radio02917.UseVisualStyleBackColor = false;
            this.radio02917.CheckedChanged += new System.EventHandler(this.radio03304_CheckedChanged);
            // 
            // radio03304
            // 
            this.radio03304.AutoSize = true;
            this.radio03304.BackColor = System.Drawing.Color.Transparent;
            this.radio03304.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio03304.Location = new System.Drawing.Point(16, 19);
            this.radio03304.Name = "radio03304";
            this.radio03304.Size = new System.Drawing.Size(71, 20);
            this.radio03304.TabIndex = 10;
            this.radio03304.TabStop = true;
            this.radio03304.Text = "03304";
            this.radio03304.UseVisualStyleBackColor = false;
            this.radio03304.CheckedChanged += new System.EventHandler(this.radio03304_CheckedChanged);
            // 
            // radio02916
            // 
            this.radio02916.AutoSize = true;
            this.radio02916.BackColor = System.Drawing.Color.Transparent;
            this.radio02916.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio02916.Location = new System.Drawing.Point(20, 62);
            this.radio02916.Name = "radio02916";
            this.radio02916.Size = new System.Drawing.Size(71, 20);
            this.radio02916.TabIndex = 14;
            this.radio02916.TabStop = true;
            this.radio02916.Text = "02916";
            this.radio02916.UseVisualStyleBackColor = false;
            this.radio02916.CheckedChanged += new System.EventHandler(this.radio03304_CheckedChanged);
            // 
            // radio03305
            // 
            this.radio03305.AutoSize = true;
            this.radio03305.BackColor = System.Drawing.Color.Transparent;
            this.radio03305.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio03305.Location = new System.Drawing.Point(106, 19);
            this.radio03305.Name = "radio03305";
            this.radio03305.Size = new System.Drawing.Size(71, 20);
            this.radio03305.TabIndex = 11;
            this.radio03305.TabStop = true;
            this.radio03305.Text = "03305";
            this.radio03305.UseVisualStyleBackColor = false;
            this.radio03305.CheckedChanged += new System.EventHandler(this.radio03304_CheckedChanged);
            // 
            // radio04560
            // 
            this.radio04560.AutoSize = true;
            this.radio04560.BackColor = System.Drawing.Color.Transparent;
            this.radio04560.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio04560.Location = new System.Drawing.Point(269, 19);
            this.radio04560.Name = "radio04560";
            this.radio04560.Size = new System.Drawing.Size(71, 20);
            this.radio04560.TabIndex = 13;
            this.radio04560.TabStop = true;
            this.radio04560.Text = "04560";
            this.radio04560.UseVisualStyleBackColor = false;
            this.radio04560.CheckedChanged += new System.EventHandler(this.radio03304_CheckedChanged);
            // 
            // radio04559
            // 
            this.radio04559.AutoSize = true;
            this.radio04559.BackColor = System.Drawing.Color.Transparent;
            this.radio04559.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio04559.Location = new System.Drawing.Point(192, 19);
            this.radio04559.Name = "radio04559";
            this.radio04559.Size = new System.Drawing.Size(71, 20);
            this.radio04559.TabIndex = 12;
            this.radio04559.TabStop = true;
            this.radio04559.Text = "04559";
            this.radio04559.UseVisualStyleBackColor = false;
            this.radio04559.CheckedChanged += new System.EventHandler(this.radio03304_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(192, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 21);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dataList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1027, 769);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dateTimePic2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePic1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.orderTxt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dataList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据查询";
            this.Load += new System.EventHandler(this.dataList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox orderTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePic1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePic2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio02917;
        private System.Windows.Forms.RadioButton radio03304;
        private System.Windows.Forms.RadioButton radio02916;
        private System.Windows.Forms.RadioButton radio03305;
        private System.Windows.Forms.RadioButton radio04560;
        private System.Windows.Forms.RadioButton radio04559;
        private System.Windows.Forms.Button butCho;
        private System.Windows.Forms.Button butLo;
        private System.Windows.Forms.TextBox textBox1;
    }
}