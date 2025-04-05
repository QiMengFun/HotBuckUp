using HotBuckUp.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HotBuckUp
{
    class TimeBackupThread
    {
        public static Thread thread = new Thread(CheckTimeBackup);
        public static void CheckTimeBackup()
        {
            while (true)
            {
                new Thread(SubThread).Start();//另开线程防止占用计时器
                Thread.Sleep(1000);
            }
        }
        public static void SubThread()
        {
            DateTime dateTime = DateTime.Now;
            foreach (String STR in GlobalVar.MainForm.listBox1.Items)
            {
                String[] Arr = STR.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (Arr.Length == 5)//启用了定时任务
                {
                    if (Arr[2].Equals("1"))//月备份
                    {
                        if (dateTime.Day.ToString().Equals(Arr[3]))
                        {
                            if (dateTime.Hour.ToString().Equals(Arr[4]) && dateTime.Minute.ToString().Equals(Arr[5]) && dateTime.Second == 1)
                            {
                                HotBackup hb = new HotBackup();
                                hb.BackupSrcToDir(Arr[0], Arr[1]);
                            }
                        }
                    }
                    if (Arr[2].Equals("2"))//周备份
                    {
                        if (GetDayOfWeekNumber() == int.Parse(Arr[3]))
                        {
                            if (dateTime.Hour.ToString().Equals(Arr[4]) && dateTime.Minute.ToString().Equals(Arr[5]) && dateTime.Second == 1)
                            {
                                HotBackup hb = new HotBackup();
                                hb.BackupSrcToDir(Arr[0], Arr[1]);
                            }
                        }
                    }
                    if (Arr[2].Equals("3"))//每天备份
                    {
                        if (dateTime.Hour.ToString().Equals(Arr[4]) && dateTime.Minute.ToString().Equals(Arr[5]) && dateTime.Second == 1)
                        {
                            HotBackup hb = new HotBackup();
                            hb.BackupSrcToDir(Arr[0], Arr[1]);
                        }
                    }
                }
            }
        }

        private static int GetDayOfWeekNumber()
        {
            // 获取当前日期是周几
            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;

            // 将DayOfWeek枚举转换为1到7的数值
            // 注意：周一为1，周日为7，所以我们需要做适当的转换
            int dayNumber = (int)dayOfWeek + 1; // 因为周一对应的值是2（0是周日），所以需要+1
            if (dayNumber == 8) // 如果今天是周日，则调整为7
            {
                dayNumber = 7;
            }
            return dayNumber;
        }
    }
}
