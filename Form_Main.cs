using HotBuckUp.全局函数;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotBuckUp
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Add form_ = new Form_Add();
            form_.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int sel = listBox1.SelectedIndex;
            if (sel != -1)
            {
                if (MessageBox.Show("是否删除?", "请注意", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    listBox1.Items.RemoveAt(sel);
                    BackupFileClass.WriteBackupList(listBox1);
                    BackupFileClass.ReadBackupList();
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的目录.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalVar.LogForm = new Form_Log();
            GlobalVar.LogForm.Show();
            /*热备份文件*/
            new Thread(HotBackup.BackupAll).Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* 初始化目录 */
            if (!Directory.Exists("temp"))
            {
                Directory.CreateDirectory("temp");
            }
            if (!Directory.Exists("bin"))
            {
                Directory.CreateDirectory("bin");
            }
            // 读取配置项
            BackupFileClass.ReadBackupList();
            if (INIConfig.读配置项("bin\\setting.ini", "setting", "TempMode", "False").Equals("True"))
            {
                temp_mode.Checked = true;
            }
            else
            {
                temp_mode.Checked = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GlobalVar.SelectedID = listBox1.SelectedIndex;
            if (GlobalVar.SelectedID != -1)
            {
                GlobalVar.LogForm = new Form_Log();
                GlobalVar.LogForm.Show();
                /*热备份文件*/
                new Thread(HotBackup.BackupSelect).Start();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("可能因为某些需求需要这个功能,\r\n比如目标是网盘自动备份目录的情况下可以防止网盘提示文件被占用.\r\n需要在开始备份前勾选或取消.\r\nTemp模式就是先把文件压缩到temp目录然后再从temp目录移动到目标目录.");
        }

        private void temp_mode_CheckedChanged(object sender, EventArgs e)
        {
            INIConfig.写配置项("bin\\setting.ini", "setting", "TempMode", temp_mode.Checked.ToString());
        }
    }
}
