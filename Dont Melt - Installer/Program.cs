using System;
using System.IO;
using System.Reflection;
using System.IO.Compression;
using System.Diagnostics;
namespace Installer
{
    public static class Program
    {
        public const string ApplicationName = "DontMelt";
        public const string ExecutableSubpath = "DontMelt.exe";
        public const string PayloadResourceName = "Installer.Payload.zip";
        //Weather or not to delete the installer after it finishes.
        public static bool DeleteAfterInstallation = true;
        //Weather or not to run the newly installed program after installation.
        public static bool RunAfterInstallation = true;
        //Weather or not to create a shortcut on the users desktop to the program.
        public static bool CreateDesktopShortcut = false;
        //Weather or not to create a start menu shortcut.
        public static bool CreateStartMenuShortcut = true;
        [STAThread]
        public static void Main()
        {
            if (IsInstalled())
            {
                Uninstall();
            }
            else
            {
                Install();
            }
        }
        public static bool IsInstalled()
        {
            if (Directory.Exists(InstallRoot()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string InstallRoot()
        {
            return @"C:\Program Files\" + ApplicationName;
        }
        public static string DesktopShortcut()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + ApplicationName + ".lnk";
        }
        public static string StartMenuShortcut()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) + @"\" + ApplicationName + ".lnk";
        }
        public static string TempPath()
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            string assemblyLocation = assembly.Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            return assemblyDirectory + @"\Temp";
        }
        public static void Install()
        {
            if (IsInstalled())
            {
                return;
            }
            Directory.CreateDirectory(InstallRoot());

            Assembly assembly = Assembly.GetCallingAssembly();

            Stream payloadStream = assembly.GetManifestResourceStream(PayloadResourceName);
            byte[] payloadBytes = new byte[payloadStream.Length];
            payloadStream.Read(payloadBytes, 0, (int)payloadStream.Length);
            payloadStream.Dispose();

            Directory.CreateDirectory(TempPath());

            File.WriteAllBytes(TempPath() + @"\Payload.zip", payloadBytes);

            ZipFile.ExtractToDirectory(TempPath() + @"\Payload.zip", InstallRoot());

            Directory.Delete(TempPath(), true);

            if (DeleteAfterInstallation)
            {
                ProcessStartInfo cmdStartInfo = new ProcessStartInfo("powershell.exe", $"-Command \"del \'{assembly.Location}\'\"");
                Process.Start(cmdStartInfo);
                Process.GetCurrentProcess().Kill();
            }
        }
        public static void Uninstall()
        {
            if (!IsInstalled())
            {
                return;
            }
            Directory.Delete(InstallRoot(), true);
        }
    }
}
