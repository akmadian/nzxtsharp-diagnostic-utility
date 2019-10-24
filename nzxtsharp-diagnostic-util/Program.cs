using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using log4net;

using NZXTSharp;
using NZXTSharp.HuePlus;
using NZXTSharp.KrakenX;

namespace nzxtsharp_diagnostic_util
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting NZXTSharp diagnostic utility...");
            Console.WriteLine("Some of these tests will be automated, some will " +
                "ask for your input to verify that colors and effects are displaying properly.\n\n\n");
            DeviceLoader loader = new DeviceLoader();
            
            logSysInfo();
            stopCAM();
            logNzxtSharpInfo(loader);
            HuePlusTest.runAllTests(loader.HuePlus, log);
            KrakenXTest.runAllTests(loader.KrakenX, log);

            Console.WriteLine("\n\n\nAll tests complete. If anything did not work properly, please create an issue " +
                "at github.com/akmadian/NZXTSharp. Be sure to include a pastebin (or other) link to the log file (can be found in the logs/ " +
                "directory in the same directory as this exe.)");
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static void logSysInfo()
        {
            System.Reflection.Assembly Assembly = System.Reflection.Assembly.GetEntryAssembly();
            log.Info("Assembly.GetName()");
            log.Info("    GetName - " + Assembly.GetName().ToString());
            log.Info("    Name - " + Assembly.GetName().Name);
            log.Info("    Version - " + Assembly.GetName().Version);
            log.Info("    VersionCompatibility - " + Assembly.GetName().VersionCompatibility);
            log.Info("    FullName - " + Assembly.FullName);
            log.Info("    HostContext - " + Assembly.HostContext.ToString());
            log.Info("    IsFullyTrusted - " + Assembly.IsFullyTrusted.ToString());
            log.Info("System.Environment");
            log.Info("    OSVersion - " + Environment.OSVersion);
            log.Info("    Version - " + Environment.Version);
            log.Info("    CurrentManagedThreadID - " + Environment.CurrentManagedThreadId.ToString());
            log.Info("    Is64BitOS - " + Environment.Is64BitOperatingSystem.ToString());
            log.Info("    Is64BitProcess - " + Environment.Is64BitProcess.ToString());
            log.Info("    WorkingSet - " + Environment.WorkingSet.ToString());
            log.Info("Target Framework - " + AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName);
        }

        private static void stopCAM()
        {
            log.Info("Attempting to stop running CAM instances...");
            Process.Start("cmd.exe", "/c taskkill /IM \"NZXT CAM.exe\"");
            Process.Start("cmd.exe", "/c taskkill /IM cam_helper.exe");
            log.Info("Running CAM instances stopped.");
        }

        private static void logNzxtSharpInfo(DeviceLoader loader)
        {
            log.Info("Discovered Devices - " + loader.NumDevices);

            INZXTDevice[] devices = loader.Devices.ToArray();
            log.Info(devices);

            foreach(INZXTDevice device in devices)
            {
                log.Info("Info for device of type: " + device.Type);
                log.Info("    Firmware Version - " + device.GetFirmwareVersion());
            }
        }
    }
}
