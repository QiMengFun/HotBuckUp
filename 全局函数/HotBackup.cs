using System;
using System.IO;
using System.Linq;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace HotBuckUp.全局函数
{
    internal class HotBackup
    {

        private static long totalBytesToBackup = 0;
        private static long backedUpBytes = 0;
        private static bool isBackupCompleted = false;
        private const int MaxRetryCount = 5;
        private const int RetryDelayMilliseconds = 3000;
        private const int 缓冲区大小 = 2 * 1024 * 1024; // 2MB
        private const int 压缩等级 = 1;

        // 定义需要忽略的文件夹名称列表
        private static readonly HashSet<string> IgnoredFolders = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase)
        {
            "System Volume Information",
            "$RECYCLE.BIN",
            "Windows.old",
            //"Temp",
            //"Temporary Internet Files",
            //"AppData"
        };

        // 定义需要忽略的文件扩展名列表
        private static readonly HashSet<string> IgnoredFileExtensions = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase)
        {
            ".tmp",
            ".temp",
            //".log",
            //".bak",
            ".cache"
        };
        public static void BackupAll()
        {
            foreach (String STR in GlobalVar.MainForm.listBox1.Items)
            {
                String[] 列表数组 = STR.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string rootDir = 列表数组[0];
                string toDir = 列表数组[1];

                BackupS(rootDir,toDir);
            }
        }
        public static void BackupSelect()
        {
            String[] 列表数组 = GlobalVar.MainForm.listBox1.Items[GlobalVar.SelectedID].ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string rootDir = 列表数组[0];
            string toDir = 列表数组[1];
            BackupS(rootDir, toDir);
        }

        static void BackupS(string rootDir, string toDir)
        {
            string rootDirName = new DirectoryInfo(rootDir).Name;
            if (rootDir.Length <= 3)
            {
                rootDirName = rootDir.Substring(0, 1);
            }
            string ZipName = $"{rootDirName}_{DateTime.Now:yyyy-MM-dd HH-mm-ss}.zip";

            LogClass.AppendLog("开始压缩 " + rootDir + " -> " + ZipName, Color.Green);
            GlobalVar.LogForm.label1.Text = "数据预估中...(并非未响应状态)";
            if (GlobalVar.MainForm.temp_mode.Checked)
            {
                String tempzip = Path.Combine("temp", ZipName);
                BackupSub(rootDir, tempzip);
                LogClass.AppendLog("开始拷贝 " + ZipName + " -> " + toDir, Color.Green);
                File.Copy(tempzip, Path.Combine(toDir, ZipName));
                LogClass.AppendLog("开始删除 temp->" + ZipName, Color.Green);
                File.Delete(tempzip);
            }
            else
            {
                BackupSub(rootDir, Path.Combine(toDir, ZipName));
            }
            LogClass.AppendLog("完成!", Color.Green);
        }

        // 修改后的备份方法
        static void BackupSub(string sourceDirectory, string backupZipFilePath)
        {
            try
            {
                EnableBackupPrivileges(); // 启用备份特权

                totalBytesToBackup = CalculateDirectorySize(sourceDirectory);
                backedUpBytes = 0;
                isBackupCompleted = false;

                using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(backupZipFilePath)))
                {
                    zipStream.SetLevel(压缩等级);
                    BackupDirectory(sourceDirectory, sourceDirectory, zipStream);
                    isBackupCompleted = true;
                }
                LogClass.AppendLog("备份完成！", Color.Green);
            }
            catch (Exception ex)
            {
                LogClass.AppendLog($"备份过程中出现错误: {ex.Message}\r\n{ex.StackTrace}", Color.Red);
            }
        }

        // 修改后的文件备份方法
        static void BackupFile(string file, string rootPath, ZipOutputStream zipStream)
        {
            bool success = false;
            int retryCount = 0;

            while (!success && retryCount < MaxRetryCount)
            {
                IntPtr fileHandle = IntPtr.Zero;
                try
                {
                    // 尝试常规方式打开文件
                    using (FileStream fs = new FileStream(
                        file,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite))
                    {
                        success = WriteFileToZip(fs, file, rootPath, zipStream);
                    }
                }
                catch (IOException)
                {
                    LogClass.AppendLog("处理时发现占用文件 -> "+ file, Color.Orange);
                    // 常规方式失败，尝试使用备份模式
                    try
                    {
                        fileHandle = CreateFileW(
                            file,
                            GENERIC_READ,
                            FILE_SHARE_READ | FILE_SHARE_WRITE | FILE_SHARE_DELETE,
                            IntPtr.Zero,
                            OPEN_EXISTING,
                            FILE_FLAG_BACKUP_SEMANTICS,
                            IntPtr.Zero);

                        if (fileHandle.ToInt64() == -1) // INVALID_HANDLE_VALUE
                            throw new Win32Exception(Marshal.GetLastWin32Error());

                        using (var backupStream = new Win32FileStream(fileHandle))
                        {
                            success = WriteFileToZip(backupStream, file, rootPath, zipStream);
                        }
                        LogClass.AppendLog("转为VSS方式读取文件成功 -> " + file, Color.Green);
                    }
                    catch (Exception ex)
                    {
                        LogClass.AppendLog($"备份模式访问失败: {file} - {ex.Message}", Color.Orange);
                        retryCount++;
                        Thread.Sleep(RetryDelayMilliseconds);
                    }
                    finally
                    {
                        if (fileHandle != IntPtr.Zero)
                            CloseHandle(fileHandle);
                    }
                }
            }

            if (!success)
            {
                LogClass.AppendLog($"最终无法备份文件: {file}, 已跳过。", Color.Red);
            }
        }

        private static bool WriteFileToZip(Stream fileStream, string filePath, string rootPath, ZipOutputStream zipStream)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string entryName = filePath.Substring(Path.GetFullPath(rootPath).TrimEnd(Path.DirectorySeparatorChar).Length + 1).Replace('\\', '/');
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fileInfo.LastWriteTime;

                zipStream.PutNextEntry(newEntry);

                byte[] buffer = new byte[缓冲区大小];
                int bytesRead;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    zipStream.Write(buffer, 0, bytesRead);
                    Interlocked.Add(ref backedUpBytes, bytesRead);
                    ShowProgress();
                }

                zipStream.CloseEntry();
                return true;
            }
            catch (Exception ex)
            {
                LogClass.AppendLog($"写入ZIP失败: {filePath} - {ex.Message}", Color.Red);
                return false;
            }
        }

        // 修改后的备份目录方法
        static void BackupDirectory(string rootPath, string currentPath, ZipOutputStream zipStream)
        {
            try
            {
                // 处理文件
                foreach (string file in Directory.GetFiles(currentPath))
                {
                    string extension = Path.GetExtension(file);
                    if (IgnoredFileExtensions.Contains(extension)) continue;

                    BackupFile(file, rootPath, zipStream);
                }

                // 处理子目录
                foreach (string directory in Directory.GetDirectories(currentPath))
                {
                    string dirName = new DirectoryInfo(directory).Name;
                    if (IgnoredFolders.Contains(dirName)) continue;

                    BackupDirectory(rootPath, directory, zipStream);
                }
            }
            catch (IOException ex)
            {
                LogClass.AppendLog($"目录访问错误: {currentPath} - {ex.Message}", Color.Orange);
            }
        }


        static long CalculateDirectorySize(string path)
        {
            if (!Directory.Exists(path))
                return 0;

            long size = 0;
            try
            {
                // 处理当前目录文件
                foreach (string file in Directory.GetFiles(path))
                {
                    string extension = Path.GetExtension(file);
                    if (IgnoredFileExtensions.Contains(extension))
                    {
                        continue;
                    }

                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        size += fileInfo.Length;
                    }
                    catch (IOException)
                    {
                        // 文件正在被占用或其他IO错误，忽略该文件
                    }
                }

                // 递归处理子目录
                foreach (string directory in Directory.GetDirectories(path))
                {
                    string dirName = new DirectoryInfo(directory).Name;
                    if (IgnoredFolders.Contains(dirName))
                    {
                        continue;
                    }

                    size += CalculateDirectorySize(directory);
                }
            }
            catch (IOException)
            {
                // 目录访问错误，忽略
            }
            return size;
        }


        static void ShowProgress()
        {
            if (GlobalVar.LogForm != null)
            {
                if (totalBytesToBackup > 0)
                {
                    double progress = (double)backedUpBytes / totalBytesToBackup * 100;
                    GlobalVar.LogForm.label1.Text = $"备份进度: {progress:F2}% 已备份: {backedUpBytes / 1024 / 1024:F2} MB / {totalBytesToBackup / 1024 / 1024:F2} MB";
                }
                if (isBackupCompleted)
                {
                    GlobalVar.LogForm.label1.Text = "备份已完成！";
                }
            }
        }

        //--------------------------------------------------------------------------------------

        // 辅助类：用于包装Win32文件句柄的Stream
        private class Win32FileStream : Stream
        {
            private readonly IntPtr _fileHandle;
            private long _position;

            public Win32FileStream(IntPtr fileHandle)
            {
                _fileHandle = fileHandle;
                if (_fileHandle.ToInt64() == -1)
                    throw new IOException("Invalid file handle");
            }

            public override bool CanRead => true;
            public override bool CanSeek => false;
            public override bool CanWrite => false;
            public override long Length => throw new NotSupportedException();

            public override long Position
            {
                get => _position;
                set => throw new NotSupportedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                uint bytesRead = 0;
                byte[] internalBuffer = new byte[count];

                if (!ReadFile(_fileHandle, internalBuffer, (uint)count, out bytesRead, IntPtr.Zero))
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                Array.Copy(internalBuffer, 0, buffer, offset, (int)bytesRead);
                _position += bytesRead;
                return (int)bytesRead;
            }

            public override void Flush() { }
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
            public override void SetLength(long value) => throw new NotSupportedException();
            public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

            protected override void Dispose(bool disposing)
            {
                CloseHandle(_fileHandle);
                base.Dispose(disposing);
            }
        }

        // 新增的Win32 API声明
        private const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
        private const uint FILE_SHARE_READ = 0x00000001;
        private const uint FILE_SHARE_WRITE = 0x00000002;
        private const uint FILE_SHARE_DELETE = 0x00000004;
        private const uint GENERIC_READ = 0x80000000;
        private const uint OPEN_EXISTING = 3;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFileW(
            [MarshalAs(UnmanagedType.LPWStr)] string filename,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadFile(
            IntPtr hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToRead,
            out uint lpNumberOfBytesRead,
            IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        // 安全令牌相关声明
        private const string SE_BACKUP_NAME = "SeBackupPrivilege";
        private const string SE_RESTORE_NAME = "SeRestorePrivilege";

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AdjustTokenPrivileges(
            IntPtr TokenHandle,
            [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            uint BufferLength,
            IntPtr PreviousState,
            IntPtr ReturnLength);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(
            IntPtr ProcessHandle,
            uint DesiredAccess,
            out IntPtr TokenHandle);

        [StructLayout(LayoutKind.Sequential)]
        private struct TOKEN_PRIVILEGES
        {
            public uint PrivilegeCount;
            public LUID Luid;
            public uint Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        private const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        private const uint TOKEN_QUERY = 0x0008;
        private const uint SE_PRIVILEGE_ENABLED = 0x00000002;

        // 启用备份特权
        private static void EnableBackupPrivileges()
        {
            try
            {
                IntPtr tokenHandle;
                if (!OpenProcessToken(
                    System.Diagnostics.Process.GetCurrentProcess().Handle,
                    TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY,
                    out tokenHandle))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                EnablePrivilege(tokenHandle, SE_BACKUP_NAME);
                EnablePrivilege(tokenHandle, SE_RESTORE_NAME);
                CloseHandle(tokenHandle);
            }
            catch (Exception ex)
            {
                LogClass.AppendLog($"启用备份权限失败: {ex.Message}", Color.Red);
            }
        }

        private static void EnablePrivilege(IntPtr tokenHandle, string privilege)
        {
            TOKEN_PRIVILEGES tkp = new TOKEN_PRIVILEGES();
            tkp.PrivilegeCount = 1;
            tkp.Attributes = SE_PRIVILEGE_ENABLED;

            if (!LookupPrivilegeValue(null, privilege, out tkp.Luid))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!AdjustTokenPrivileges(
                tokenHandle,
                false,
                ref tkp,
                0,
                IntPtr.Zero,
                IntPtr.Zero))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(
            string lpSystemName,
            string lpName,
            out LUID lpLuid);

    }
}