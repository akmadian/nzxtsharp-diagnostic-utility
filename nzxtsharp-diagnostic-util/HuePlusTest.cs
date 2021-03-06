﻿using System;
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
            testUnitLight(deviceInstance, log);
            testChannelToggling(deviceInstance, log);

            deviceInstance.ApplyEffect(deviceInstance.Both, new Fixed(new Color("#FFFFFF")));
        }

        static void testRGB(HuePlus device, ILog log)
        {
            Fixed R = new Fixed(new Color(255, 0, 0));
            Fixed G = new Fixed(new Color(0, 255, 0));
            Fixed B = new Fixed(new Color(0, 0, 255));

            log.Info("Testing solid fixed red on both channels...");
            device.ApplyEffect(device.Both, R);
            askQuestion("Are all LEDs on both Hue+ channels displaying solid red? (Y/N): ", log);

            log.Info("Testing solid fixed green on both channels...");
            device.ApplyEffect(device.Both, G);
            askQuestion("Are all LEDs on both Hue+ channels displaying solid green? (Y/N): ", log);

            log.Info("Testing solid fixed blue on both channels...");
            device.ApplyEffect(device.Both, B);
            askQuestion("Are all LEDs on both Hue+ channels displaying solid blue? (Y/N): ", log);
        }

        static void testUnitLight(HuePlus device, ILog log)
        {
            log.Info("Testing unit light control, setting unit LED to off...");
            device.SetUnitLed(false);
            askQuestion("Is the Hue+ unit LED off? (Y/N): ", log);

            device.UnitLedOn();
            askQuestion("Is the Hue+ unit LED now on? (Y/N): ", log);

            device.UnitLedOff();
            askQuestion("Is the Hue+ unit LED now off? (Y/N): ", log);
        }

        static void testChannelToggling(HuePlus device, ILog log)
        {
            log.Info("Testing channel toggling...");
            device.Both.Off();
            askQuestion("Are both Hue+ channels now off? (Y/N): ", log);

            device.Both.On();
            askQuestion("Are both Hue+ channels now on? (Y/N): ", log);
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
            } else
            {
                log.Error("Test Failed");
                Console.Write("Please write some information about how the lights behaved (press enter when done): ");
                log.Info("User account of incorrect effect display: " + Console.ReadLine());
            }
        }
    }
}
