using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NZXTSharp;
using NZXTSharp.KrakenX;
using log4net;

namespace nzxtsharp_diagnostic_util
{
    public static class KrakenXTest
    {
        public static void runAllTests(KrakenX deviceInstance, ILog log)
        {
            log.Info("Starting KrakenX test battery...");
            testGetMethods(deviceInstance, log);
            testRGB(deviceInstance, log);
            testChannelToggling(deviceInstance, log);

            deviceInstance.ApplyEffect(deviceInstance.Both, new Fixed(new Color("#FFFFFF")));
        }

        static void testRGB(KrakenX device, ILog log)
        {
            Fixed R = new Fixed(new Color(255, 0, 0));
            Fixed G = new Fixed(new Color(0, 255, 0));
            Fixed B = new Fixed(new Color(0, 0, 255));

            log.Info("Testing solid fixed red on both channels...");
            device.ApplyEffect(device.Both, R);
            askQuestion("Are all LEDs on both Kraken channels displaying solid red? (Y/N): ", log);

            log.Info("Testing solid fixed green on both channels...");
            device.ApplyEffect(device.Both, G);
            askQuestion("Are all LEDs on both Kraken channels displaying solid green? (Y/N): ", log);

            log.Info("Testing solid fixed blue on both channels...");
            device.ApplyEffect(device.Both, B);
            askQuestion("Are all LEDs on both Kraken channels displaying solid blue? (Y/N): ", log);
        }

        static void testChannelToggling(KrakenX device, ILog log)
        {
            log.Info("Testing channel toggling...");
            device.Both.Off();
            askQuestion("Are both Kraken channels now off? (Y/N): ", log);

            device.Both.On();
            askQuestion("Are both Kraken channels now on? (Y/N): ", log);

            device.Logo.Off();
            askQuestion("Is the NZXT logo on the KrakenX now off? (Y/N): ", log);

            device.Ring.Off();
            askQuestion("Is the ring on the KrakenX now off? (Y/N): ", log);
        }

        static void testFanSpeed(KrakenX device, ILog log)
        {
            log.Info("Testing fan speed control...");

        }

        static void testGetMethods(KrakenX device, ILog log)
        {
            log.Info("Testing all get methods...");
            log.Info("    Fan Speed: " + device.GetFanSpeed());
            log.Info("    Pump Speed: " + device.GetPumpSpeed());
            log.Info("    Liquid Temp: " + device.GetLiquidTemp());
        }

        static void askQuestion(string question, ILog log)
        {
            string resp = "";
            bool isValid = false;
            log.Info(question);
            while (!isValid)
            {
                Console.Write(question);
                resp = Console.ReadLine().Trim().ToLower();
                if (resp == "y" || resp == "n")
                {
                    isValid = true;
                }
            }

            if (resp == "y")
            {
                log.Info("User response: true");
            }
            else
            {
                log.Error("Test Failed");
                Console.Write("Please write some information about how the lights behaved (press enter when done): ");
                log.Info("User account of incorrect effect display: " + Console.ReadLine());
            }
        }
    }
}
