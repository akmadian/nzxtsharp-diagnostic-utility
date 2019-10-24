using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NZXTSharp;
using NZXTSharp.HuePlus;
using log4net;

namespace nzxtsharp_diagnostic_util
{
    static class HuePlusTest
    {
        public static void runAllTests(HuePlus deviceInstance, ILog log)
        {
            log.Info("Starting HuePlus test battery...");
            testRGB(deviceInstance, log);
        }

        static void testRGB(HuePlus device, ILog log)
        {
            Fixed R = new Fixed(new Color(255, 0, 0));
            Fixed G = new Fixed(new Color(0, 255, 0));
            Fixed B = new Fixed(new Color(0, 0, 255));

            log.Info("Testing solid fixed red on both channels...");
            device.ApplyEffect(device.Both, R);
            Console.Write("Are all LEDs on both Hue+ channels displaying solid red? (Y/N): ");
            Console.WriteLine(Console.ReadLine());
            bool resp = Console.ReadLine().Trim().ToLower() == "y";
            log.Info("User response: " + resp);
        }
    }
}
