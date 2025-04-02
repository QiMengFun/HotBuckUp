using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBuckUp
{
    internal class GlobalVar
    {
        public static Form_Main MainForm = null;//主窗口
        public static Form_Log LogForm = null;//Log窗口

        public static String BackupDirList = "bin/backuplist.txt";
        public static String IgnoreDirList = "bin/IgnoreDirList.txt";
        public static String IgnoreExtList = "bin/IgnoreExtList.txt";

        public static int SelectedID = -1;

        public static string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
    }
}
