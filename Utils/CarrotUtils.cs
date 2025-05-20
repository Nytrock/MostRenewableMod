using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace EverythingRenewableNow.Utils {
    public static class CarrotUtils {
        public static void UnlockCarrot() {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                UnlockCarrotOnWindows();
            else
                UnlockCarrotOnUnix();
        }

        public static void LockCarrot() {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                LockCarrotOnWindows();
            else
                LockCarrotOnUnix();
        }

        private static void UnlockCarrotOnWindows() {
            RegistryKey terrariaRegistry = LoadWindowsRegistry();
            if (terrariaRegistry.GetValueNames().Contains("IsBunnyFake"))
                return;

            bool isBunnyFake = !terrariaRegistry.GetValueNames().Contains("Bunny");
            if (isBunnyFake)
                terrariaRegistry.SetValue("Bunny", "1");
            terrariaRegistry.SetValue("IsBunnyFake", Convert.ToInt32(isBunnyFake).ToString());
        }

        private static void LockCarrotOnWindows() {
            RegistryKey terrariaRegistry = LoadWindowsRegistry();
            bool isBunnyFake = Convert.ToInt32(terrariaRegistry.GetValue("IsBunnyFake")) == 1;

            terrariaRegistry.DeleteValue("IsBunnyFake");
            if (!isBunnyFake)
                return;

            terrariaRegistry.DeleteValue("Bunny");
        }

        private static RegistryKey LoadWindowsRegistry() {
            return Registry.CurrentUser.OpenSubKey("Software", true).OpenSubKey("Terraria", true);
        }

        private static void UnlockCarrotOnUnix() {
            XDocument document = new();
            XAttribute bunnyElement = new("Bunny", "1");

            document.Add(bunnyElement);
            document.Save("~/.mono/registry/CurrentUser/software/terraria/values.xml");
        }

        private static void LockCarrotOnUnix() {
            File.Delete("~/.mono/registry/CurrentUser/software/terraria/values.xml");
        }
    }
}
