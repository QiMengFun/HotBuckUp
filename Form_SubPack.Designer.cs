namespace HotBuckUp
{
    partial class Form_SubPack
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sub_pack_size = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.create_subpack_dir = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.sub_pack_cachesize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "分包可以有效防止压缩出来的ZIP过大导致不可以被备份的问题\r\n(如:百度网盘的4GB限制)\r\n解压软件推荐使用Bandizip6";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "每个包最大尺寸:";
            // 
            // sub_pack_size
            // 
            this.sub_pack_size.Location = new System.Drawing.Point(151, 69);
            this.sub_pack_size.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sub_pack_size.Name = "sub_pack_size";
            this.sub_pack_size.Size = new System.Drawing.Size(115, 25);
            this.sub_pack_size.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(275, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "MB";
            // 
            // create_subpack_dir
            // 
            this.create_subpack_dir.AutoSize = true;
            this.create_subpack_dir.Location = new System.Drawing.Point(15, 160);
            this.create_subpack_dir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.create_subpack_dir.Name = "create_subpack_dir";
            this.create_subpack_dir.Size = new System.Drawing.Size(322, 19);
            this.create_subpack_dir.TabIndex = 4;
            this.create_subpack_dir.Text = "分包时自动创建文件夹,将分包放入文件夹内";
            this.create_subpack_dir.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(170, 227);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "存储配置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "分割缓冲区大小:";
            // 
            // sub_pack_cachesize
            // 
            this.sub_pack_cachesize.Location = new System.Drawing.Point(151, 110);
            this.sub_pack_cachesize.Margin = new System.Windows.Forms.Padding(4);
            this.sub_pack_cachesize.Name = "sub_pack_cachesize";
            this.sub_pack_cachesize.Size = new System.Drawing.Size(115, 25);
            this.sub_pack_cachesize.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(274, 113);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "KB";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(308, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 26);
            this.button2.TabIndex = 9;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(308, 107);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(23, 26);
            this.button3.TabIndex = 10;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form_SubPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 268);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sub_pack_cachesize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.create_subpack_dir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sub_pack_size);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SubPack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分包配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_SubPack_FormClosing);
            this.Load += new System.EventHandler(this.Form_SubPack_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox sub_pack_size;
        public System.Windows.Forms.CheckBox create_subpack_dir;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox sub_pack_cachesize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}