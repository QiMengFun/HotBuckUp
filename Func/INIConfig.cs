using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HotBuckUp.Func
{
    internal class INIConfig
    {
        /// <summary>
        /// 写入配置项(自动创建目录) 为空则删除相关配置
        /// </summary>
        /// <param name="带路径文件名"></param>
        /// <param name="区"></param>
        /// <param name="键"></param>
        /// <param name="值"></param>
        /// <returns></returns>
        public static bool 写配置项(String 带路径文件名, String 区, String 键 = "", String 值 = "")
        {
            if (带路径文件名 == null)
            {
                return false;//提供文件名为空
            }
#pragma warning disable CS8600 // 可能返回 null 引用。
            String Dir = Path.GetDirectoryName(带路径文件名);
#pragma warning disable CS8600 // 可能返回 null 引用。
            if (Dir == null)
            {
                return false;//目录为空
            }
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);//目录不存在则创建
            }
            //判断文件是否存在
            if (!File.Exists(带路径文件名))
            {
                File.Create(带路径文件名).Close();//创建INI文件
            }

            //判断删除
            if (键 == "")
            {
                if (DeleteSection(区, 带路径文件名) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            if (值 == "")
            {
                if (DeleteKey(区, 键, 带路径文件名) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }


            if (Write(区, 键, 值, 带路径文件名) == 0)
            {
                return false;//写入配置项失败
            }
            else
            {
                return true;//写入配置项成功
            }
        }

        /// <summary>
        /// 读取配置项 文件不存在或配置不存在将返回 默认返回值
        /// </summary>
        /// <param name="带路径文件名"></param>
        /// <param name="区"></param>
        /// <param name="键"></param>
        /// <param name="默认返回值"></param>
        /// <returns></returns>
        public static String 读配置项(String 带路径文件名, String 区, String 键, String 默认返回值)
        {

            if (!File.Exists(带路径文件名))
            {
                return 默认返回值;//文件不存在 返回默认返回值
            }
            return Read(区, 键, 默认返回值, 带路径文件名);
        }

        /// <summary>
        /// 为INI文件中指定的节点取得字符串
        /// </summary>
        /// <param name="lpAppName">欲在其中查找关键字的节点名称</param>
        /// <param name="lpKeyName">欲获取的项名</param>
        /// <param name="lpDefault">指定的项没有找到时返回的默认值</param>
        /// <param name="lpReturnedString">指定一个字串缓冲区，长度至少为nSize</param>
        /// <param name="nSize">指定装载到lpReturnedString缓冲区的最大字符数量</param>
        /// <param name="lpFileName">INI文件完整路径</param>
        /// <returns>复制到lpReturnedString缓冲区的字节数量，其中不包括那些NULL中止字符</returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        /// <summary>
        /// 修改INI文件中内容
        /// </summary>
        /// <param name="lpApplicationName">欲在其中写入的节点名称</param>
        /// <param name="lpKeyName">欲设置的项名</param>
        /// <param name="lpString">要写入的新字符串</param>
        /// <param name="lpFileName">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public static string Read(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, filePath);
            return sb.ToString();
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的新字符串</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public static int Write(string section, string key, string value, string filePath)
        {
            return WritePrivateProfileString(section, key, value, filePath);
        }

        /// <summary>
        /// 删除节
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public static int DeleteSection(string section, string filePath)
        {
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
            return Write(section, null, null, filePath);
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
        }

        /// <summary>
        /// 删除键的值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键名</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public static int DeleteKey(string section, string key, string filePath)
        {
#pragma warning disable CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
            return Write(section, key, null, filePath);
#pragma warning restore CS8625 // 无法将 null 字面量转换为非 null 的引用类型。
        }
    }
}
