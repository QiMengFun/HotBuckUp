using HotBuckUp.Func;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotBuckUp
{
    public partial class Form_Add : Form
    {
        public Form_Add()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String 源 = textBox1.Text;
            String 备 = textBox2.Text;
            BackupFileClass.WriteBackupList(源+"|"+ 备);
            BackupFileClass.ReadBackupList();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Form_添加备份目录_Load(object sender, EventArgs e)
        {

        }
    }
}
