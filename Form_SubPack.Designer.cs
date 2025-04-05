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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "分包可以有效防止压缩出来的ZIP过大导致不可以被备份的问题\r\n(如:百度网盘的4GB限制)\r\n解压软件推荐使用Bandizip6";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "每个包最大尺寸:";
            // 
            // sub_pack_size
            // 
            this.sub_pack_size.Location = new System.Drawing.Point(113, 55);
            this.sub_pack_size.Name = "sub_pack_size";
            this.sub_pack_size.Size = new System.Drawing.Size(87, 21);
            this.sub_pack_size.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "MB";
            // 
            // create_subpack_dir
            // 
            this.create_subpack_dir.AutoSize = true;
            this.create_subpack_dir.Location = new System.Drawing.Point(11, 84);
            this.create_subpack_dir.Name = "create_subpack_dir";
            this.create_subpack_dir.Size = new System.Drawing.Size(258, 16);
            this.create_subpack_dir.TabIndex = 4;
            this.create_subpack_dir.Text = "分包时自动创建文件夹,将分包放入文件夹内";
            this.create_subpack_dir.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 22);
            this.button1.TabIndex = 5;
            this.button1.Text = "存储配置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form_SubPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 172);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.create_subpack_dir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sub_pack_size);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
    }
}