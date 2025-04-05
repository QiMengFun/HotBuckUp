using HotBuckUp.Func;
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
            GlobalVar.LogForm.Visible = true;
            /*热备份文件*/
            HotBackup hb = new HotBackup();
            new Thread(hb.BackupAll).Start();
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

            /* 新建忽略配置 */
            if (!File.Exists(GlobalVar.IgnoreDirList))
            {
                File.WriteAllText(GlobalVar.IgnoreDirList, "每行一个文件夹名字,用于忽略(不压缩),不区分大小写\r\nSystem Volume Information\r\n$RECYCLE.BIN\r\nWindows.old");
            }
            if (!File.Exists(GlobalVar.IgnoreExtList))
            {
                File.WriteAllText(GlobalVar.IgnoreExtList, "每行一个后缀名字,用于忽略(不压缩),不区分大小写\r\n.tmp\r\n.temp\r\n.cache");
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
            if (INIConfig.读配置项("bin\\setting.ini", "setting", "SubPackMode", "False").Equals("True"))
            {
                sub_pack_mode.Checked = true;
            }
            else
            {
                sub_pack_mode.Checked = false;
            }
            //启动计时任务线程
            TimeBackupThread.thread.Start();
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
                GlobalVar.LogForm.Visible = true;
                /*热备份文件*/
                HotBackup hb = new HotBackup();
                new Thread(hb.BackupSelect).Start();
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (!File.Exists(GlobalVar.IgnoreDirList))
            {
                File.WriteAllText(GlobalVar.IgnoreDirList, "每行一个文件夹名字,用于忽略(不压缩)\r\nSystem Volume Information\r\n$RECYCLE.BIN\r\nWindows.old");
            }
            // 打开文件
            Process.Start(new ProcessStartInfo
            {
                FileName = GlobalVar.exeDirectory + "\\" + GlobalVar.IgnoreDirList,
                UseShellExecute = true // 使用系统默认程序打开文件
            });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!File.Exists(GlobalVar.IgnoreExtList))
            {
                File.WriteAllText(GlobalVar.IgnoreExtList, "每行一个后缀名字,用于忽略(不压缩)\r\n.tmp\r\n.temp\r\n.cache");
            }
            // 打开文件
            Process.Start(new ProcessStartInfo
            {
                FileName = GlobalVar.exeDirectory + "\\" + GlobalVar.IgnoreExtList,
                UseShellExecute = true // 使用系统默认程序打开文件
            });
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GlobalVar.LogForm.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GlobalVar.SubPackForm.Visible = true;
        }

        private void sub_pack_mode_CheckedChanged(object sender, EventArgs e)
        {
            INIConfig.写配置项("bin\\setting.ini", "setting", "SubPackMode", sub_pack_mode.Checked.ToString());
        }
    }
}
