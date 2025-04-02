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
    public partial class Form_Log : Form
    {
        public Form_Log()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form_Log_Load(object sender, EventArgs e)
        {

        }

        

        // 清空日志
        public void ClearLog()
        {
            if (richTextBoxLog.InvokeRequired)
            {
                richTextBoxLog.Invoke(new Action(ClearLog));
                return;
            }

            richTextBoxLog.Clear();
        }
    }
}
