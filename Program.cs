using HotBuckUp.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotBuckUp
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GlobalVar.MainForm = new Form_Main();
            GlobalVar.LogForm = new Form_Log();
            GlobalVar.SubPackForm = new Form_SubPack();
            /* 初始化子窗口内容 */
            GlobalVar.SubPackForm.sub_pack_size.Text = Func.INIConfig.读配置项("bin\\setting.ini", "setting", "SubPackSize", "4096");
            if (INIConfig.读配置项("bin\\setting.ini", "setting", "SubPackDir", "False").Equals("True"))
            {
                GlobalVar.SubPackForm.create_subpack_dir.Checked = true;
            }
            else
            {
                GlobalVar.SubPackForm.create_subpack_dir.Checked = false;
            }

            Application.Run(GlobalVar.MainForm);
        }
    }
}
