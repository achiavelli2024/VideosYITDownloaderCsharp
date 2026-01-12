using System;
using System.Windows.Forms;
using VideosYITDownloaderCsharp.Licensing;

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

            Application.Run(new frmMain(license));
        }
    }
}