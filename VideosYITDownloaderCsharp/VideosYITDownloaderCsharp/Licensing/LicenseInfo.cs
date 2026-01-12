using System;

namespace VideosYITDownloaderCsharp.Licensing
{
    public class LicenseInfo
    {
        public string KeyCode { get; set; } = "";
        public string LicenseKey { get; set; } = "";
        public DateTime? TrialStartUtc { get; set; }
        public DateTime? LastDownloadUtc { get; set; }

        public bool IsDeveloperLicense => LicenseKey == LicenseManager.DeveloperLicense;
        public bool IsFullLicense => LicenseManager.IsLicenseValid(KeyCode, LicenseKey) || IsDeveloperLicense;

        public bool IsTrialActive(out int daysLeft)
        {
            daysLeft = 0;
            if (TrialStartUtc == null) return false;
            var elapsed = (DateTime.UtcNow - TrialStartUtc.Value).TotalDays;
            daysLeft = Math.Max(0, 30 - (int)Math.Floor(elapsed));
            return elapsed <= 30.0;
        }
    }
}