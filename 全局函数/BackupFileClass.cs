using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotBuckUp.全局函数
{
    internal class BackupFileClass
    {
        public static void WriteBackupList(String LineText)
        {
            String 列表 = "";
            if (File.Exists(GlobalVar.BackupDirList))
            {
               列表 = File.ReadAllText(GlobalVar.BackupDirList);
            }
            if (列表.Equals(""))
            {
                File.AppendAllText(GlobalVar.BackupDirList, LineText);
            }
            else
            {
                File.AppendAllText(GlobalVar.BackupDirList, "\r\n"+LineText);
            }
        }
        public static void WriteBackupList(ListBox listbox)
        {
            // 使用using语句确保正确关闭StreamWriter
            File.WriteAllText(GlobalVar.BackupDirList, "");
            using (StreamWriter writer = new StreamWriter(GlobalVar.BackupDirList))
            {
                foreach (var item in listbox.Items)
                {
                    // 写入项并添加换行符
                    writer.WriteLine(item.ToString());
                }
            }
        }
        public static void ReadBackupList()
        {
            
            String 列表 = "";
            if (File.Exists(GlobalVar.BackupDirList))
            {
                列表 = File.ReadAllText(GlobalVar.BackupDirList);
                String[] 列表数组 = 列表.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                GlobalVar.MainForm.listBox1.Items.Clear();
                foreach(String str in 列表数组)
                {
                    GlobalVar.MainForm.listBox1.Items.Add(str);
                }
            }
        }
    }
}
