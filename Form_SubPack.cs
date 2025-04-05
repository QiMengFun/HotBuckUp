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
    public partial class Form_SubPack: Form
    {
        public Form_SubPack()
        {
            InitializeComponent();
        }

        private void Form_SubPack_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            e.Cancel = true;
        }

        private void Form_SubPack_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Func.INIConfig.写配置项("bin\\setting.ini", "setting", "SubPackSize", sub_pack_size.Text);
            INIConfig.写配置项("bin\\setting.ini", "setting", "TempMode", create_subpack_dir.Checked.ToString());
            Visible = false;
        }
    }
}
