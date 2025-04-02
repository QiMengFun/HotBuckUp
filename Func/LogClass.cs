using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBuckUp.Func
{
    internal class LogClass
    {
        // 添加带颜色的日志
        public static void AppendLog(string message, Color color)
        {
            if (GlobalVar.LogForm.richTextBoxLog.InvokeRequired)
            {
                GlobalVar.LogForm.richTextBoxLog.Invoke(new Action(() => AppendLog(message, color)));
                return;
            }

            GlobalVar.LogForm.richTextBoxLog.SelectionStart = GlobalVar.LogForm.richTextBoxLog.TextLength;
            GlobalVar.LogForm.richTextBoxLog.SelectionLength = 0;
            GlobalVar.LogForm.richTextBoxLog.SelectionColor = color;
            GlobalVar.LogForm.richTextBoxLog.AppendText(message + Environment.NewLine);

            if (GlobalVar.LogForm.checkBoxAutoScroll.Checked)
            {
                GlobalVar.LogForm.richTextBoxLog.ScrollToCaret();
            }
        }
    }
}
