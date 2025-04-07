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
        public static Form_SubPack SubPackForm = null;//分包窗口

        public static String BackupDirList = "bin/backuplist.txt";
        public static String IgnoreDirList = "bin/IgnoreDirList.txt";
        public static String IgnoreFileList = "bin/IgnoreFileList.txt";
        public static String IgnoreExtList = "bin/IgnoreExtList.txt";

        public static String Def_IgnoreDirList = "每行一个文件夹名字,用于忽略(不压缩),不区分大小写\r\nSystem Volume Information\r\n$RECYCLE.BIN\r\nWindows.old";
        public static String Def_IgnoreFileList = "每行一个文件名字,用于忽略(不压缩),不区分大小写,可以是带后缀或者不带后缀的文件名\r\n(如果文件含有这个字符串就会被忽略)\r\npagefile.sys\r\n无需备份的文件A\r\n无需备份的文件B.tmp";
        public static String Def_IgnoreExtList = "每行一个后缀名字,用于忽略(不压缩),不区分大小写\r\n.tmp\r\n.temp\r\n.cache";

        public static int SelectedID = -1;

        public static string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
    }
}
