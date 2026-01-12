using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace VideosYITDownloaderCsharp.Licensing
{
    internal static class HardwareIdProvider
    {
        public static string GetKeyCode()
        {
            var machineGuid = TryGetMachineGuid();
            if (!string.IsNullOrWhiteSpace(machineGuid))
                return To10Digits(machineGuid);

            var fallback = Environment.MachineName + Environment.UserName;
            return To10Digits(fallback);
        }

        private static string TryGetMachineGuid()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography"))
                {
                    return key?.GetValue("MachineGuid")?.ToString();
                }
            }
            catch
            {
                return null;
            }
        }

        private static string To10Digits(string source)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(source));
                ulong val = BitConverter.ToUInt64(bytes, 0);
                val %= 10000000000UL;
                return val.ToString("D10");
            }
        }
    }
}