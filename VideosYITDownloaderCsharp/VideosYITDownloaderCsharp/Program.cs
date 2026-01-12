using System;
using System.Windows.Forms;
using VideosYITDownloaderCsharp.Licensing;
using VideosYITDownloaderCsharp.Utils;

namespace VideosYITDownloaderCsharp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var license = LicenseManager.EnsureLicense();
            if (license == null)
            {
                return;
            }

            var logger = new FileLogger();
            Application.Run(new frmMain(license, logger));
        }
    }
}