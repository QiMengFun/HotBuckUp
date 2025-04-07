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
            INIConfig.写配置项("bin\\setting.ini", "setting", "SubPackSize", sub_pack_size.Text);
            INIConfig.写配置项("bin\\setting.ini", "setting", "SubPackCacheSize", sub_pack_cachesize.Text);
            INIConfig.写配置项("bin\\setting.ini", "setting", "SubPackCreateDir", create_subpack_dir.Checked.ToString());
            
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("拆分zip包后每个zip包的最大大小");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("拆分zip包时缓冲区的大小(注意4K对齐)" +
                "\r\n机械硬盘(HDD)建议: 32/64/128/256KB" +
                "\r\n固态硬盘(SSD)建议: 256/512KB" +
                "\r\nPCIE硬盘(NVME)建议: 256/512/1024KB" +
                "\r\n根据硬盘性能合理填入.");
        }
    }
}
