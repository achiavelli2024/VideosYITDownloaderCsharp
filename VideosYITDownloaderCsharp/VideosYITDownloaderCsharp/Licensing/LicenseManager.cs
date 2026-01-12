using System;
using System.Globalization;
using System.IO;

namespace VideosYITDownloaderCsharp.Licensing
{
    internal static class LicenseManager
    {
        public const string DeveloperLicense = "123456789@";
        private const ulong Salt = 123456789UL;
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lic.data");

        public static LicenseInfo Current { get; private set; }

        public static bool IsLicenseValid(string keyCode, string licenseKey)
        {
            if (licenseKey == DeveloperLicense) return true;
            if (string.IsNullOrWhiteSpace(keyCode) || string.IsNullOrWhiteSpace(licenseKey)) return false;
            if (keyCode.Length != 10 || licenseKey.Length != 10) return false;
            if (!ulong.TryParse(keyCode, out var keyNum)) return false;
            ulong expected = (keyNum * 7UL + Salt) % 10000000000UL;
            return licenseKey == expected.ToString("D10");
        }

        public static LicenseInfo Load()
        {
            if (!File.Exists(FilePath)) return null;
            var info = new LicenseInfo();
            foreach (var line in File.ReadAllLines(FilePath))
            {
                var parts = line.Split(new[] { '=' }, 2);
                if (parts.Length != 2) continue;
                var key = parts[0].Trim().ToUpperInvariant();
                var val = parts[1].Trim();
                switch (key)
                {
                    case "KEY":
                        info.KeyCode = val;
                        break;
                    case "LICENSE":
                        info.LicenseKey = val;
                        break;
                    case "TRIAL_START_UTC":
                        if (long.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out var ticks))
                            info.TrialStartUtc = new DateTime(ticks, DateTimeKind.Utc);
                        break;
                    case "LAST_DOWNLOAD_UTC":
                        if (long.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out var ticks2))
                            info.LastDownloadUtc = new DateTime(ticks2, DateTimeKind.Utc);
                        break;
                }
            }
            return info;
        }

        public static void Save(LicenseInfo info)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            File.WriteAllLines(FilePath, new[]
            {
                $"KEY={info.KeyCode}",
                $"LICENSE={info.LicenseKey}",
                $"TRIAL_START_UTC={(info.TrialStartUtc?.Ticks.ToString(CultureInfo.InvariantCulture) ?? "")}",
                $"LAST_DOWNLOAD_UTC={(info.LastDownloadUtc?.Ticks.ToString(CultureInfo.InvariantCulture) ?? "")}"
            });
            Current = info;
        }

        public static LicenseInfo EnsureLicense()
        {
            var hwKey = HardwareIdProvider.GetKeyCode();
            var info = Load();

            if (info == null || info.KeyCode != hwKey)
            {
                return PromptActivation(hwKey);
            }

            if (IsLicenseValid(info.KeyCode, info.LicenseKey) || info.IsDeveloperLicense)
            {
                Current = info;
                return info;
            }

            if (info.IsTrialActive(out _))
            {
                Current = info;
                return info;
            }

            return PromptActivation(hwKey);
        }

        private static LicenseInfo PromptActivation(string hwKey)
        {
            using (var form = new LicenseForm(hwKey))
            {
                var result = form.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && Current != null)
                {
                    return Current;
                }
            }
            return null;
        }

        public static bool TryActivate(string keyCode, string licenseKey, out string message)
        {
            keyCode = keyCode?.Trim();
            licenseKey = licenseKey?.Trim();

            if (string.IsNullOrWhiteSpace(keyCode) || keyCode.Length != 10)
            {
                message = "KeyCode deve ter 10 dígitos.";
                return false;
            }

            if (licenseKey == DeveloperLicense || IsLicenseValid(keyCode, licenseKey))
            {
                var info = new LicenseInfo
                {
                    KeyCode = keyCode,
                    LicenseKey = licenseKey
                };
                Save(info);
                message = "Licença ativada com sucesso.";
                return true;
            }

            message = "Licença inválida para este KeyCode.";
            return false;
        }

        public static void StartTrial(string keyCode)
        {
            var info = new LicenseInfo
            {
                KeyCode = keyCode,
                LicenseKey = "TRIAL",
                TrialStartUtc = DateTime.UtcNow
            };
            Save(info);
        }

        public static bool CanDownloadNow(out string reason)
        {
            reason = string.Empty;
            var info = Current ?? Load();
            if (info == null)
            {
                var key = HardwareIdProvider.GetKeyCode();
                reason = $"Licença ou trial não encontrados. Keycode: {key} | Contato: Cel 11 942963117 / email: alexandrechiavelli@gmail.com";
                return false;
            }

            if (info.IsFullLicense || info.IsDeveloperLicense)
                return true;

            if (!info.IsTrialActive(out _))
            {
                var key = HardwareIdProvider.GetKeyCode();
                reason = $"Trial expirado. Adquira a licença PRO. Keycode: {key} | Contato: Cel 11 942963117 / email: alexandrechiavelli@gmail.com";
                return false;
            }

            if (info.LastDownloadUtc.HasValue)
            {
                var elapsed = DateTime.UtcNow - info.LastDownloadUtc.Value;
                if (elapsed.TotalHours < 24)
                {
                    var remaining = TimeSpan.FromHours(24) - elapsed;
                    var key = HardwareIdProvider.GetKeyCode();
                    reason =
                        $"Trial: limite de 1 download a cada 24h. Aguarde {remaining.Hours}h {remaining.Minutes}m. " +
                        $"Para remover o limite, adquira a licença PRO. Keycode: {key} | Contato: Cel 11 942963117 / email: alexandrechiavelli@gmail.com";
                    return false;
                }
            }

            return true;
        }

        public static void MarkDownloadDone()
        {
            var info = Current ?? Load();
            if (info == null) return;
            info.LastDownloadUtc = DateTime.UtcNow;
            Save(info);
        }
    }
}