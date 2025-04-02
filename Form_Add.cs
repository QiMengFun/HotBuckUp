using HotBuckUp.Func;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            if (源.Equals("") || !Directory.Exists(源))
            {
                MessageBox.Show("源 目录不存在或为空.");
                return;
            }
            if (备.Equals("") || !Directory.Exists(备))
            {
                MessageBox.Show("备 目录不存在或为空.");
                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {
                BackupFileClass.WriteBackupList(源 + "|" + 备);
            }
            else
            {
                if(comboBox1.SelectedIndex == 1 || comboBox1.SelectedIndex == 2)
                {
                    if (textBox3.Text.Equals("")|| textBox4.Text.Equals("") || textBox5.Text.Equals(""))
                    {
                        MessageBox.Show("计划任务时间编辑框不能为空,请检查.");
                        return;
                    }
                    try
                    {
                        int.Parse(textBox3.Text);
                        int.Parse(textBox4.Text);
                        int.Parse(textBox5.Text);
                    }
                    catch
                    {
                        MessageBox.Show("计划任务时间编辑框必须为数字,请检查.");
                        return;
                    }
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    if (textBox4.Text.Equals("") || textBox5.Text.Equals(""))
                    {
                        MessageBox.Show("计划任务时间编辑框不能为空,请检查.");
                        return;
                    }
                    try
                    {
                        int.Parse(textBox4.Text);
                        int.Parse(textBox5.Text);
                    }
                    catch
                    {
                        MessageBox.Show("计划任务时间编辑框必须为数字,请检查.");
                        return;
                    }
                }
                String cycle = comboBox1.SelectedIndex.ToString();//周期
                BackupFileClass.WriteBackupList(源 + "|" + 备 + "|"+ cycle+"|"+ textBox3.Text + "|"+ textBox4.Text + "|"+ textBox5.Text);
            }
            BackupFileClass.ReadBackupList();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Form_添加备份目录_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                label4.Text = "__";
                label5.Text = "__";
                label6.Text = "__";
                label7.Text = "__";
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                label4.Text = "第";
                label5.Text = "天";
                label6.Text = "时";
                label7.Text = "分";
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                label4.Text = "第";
                label5.Text = "天";
                label6.Text = "时";
                label7.Text = "分";
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                label4.Text = "__";
                label5.Text = "__";
                label6.Text = "时";
                label7.Text = "分";
                textBox3.Enabled = false;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
        }
    }
}
