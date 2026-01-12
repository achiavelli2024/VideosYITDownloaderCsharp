using System;
using System.IO;

namespace VideosYITDownloaderCsharp.Utils
{
    internal class FileLogger : ILogger
    {
        private readonly string _logDir;

        public FileLogger()
        {
            _logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(_logDir);
        }

        private void Write(string level, string message)
        {
            string file = Path.Combine(_logDir, $"log-{DateTime.UtcNow:yyyy-MM-dd}.txt");
            string line = $"{DateTime.UtcNow:O} [{level}] {message}";
            File.AppendAllLines(file, new[] { line });
        }

        public void Info(string message) => Write("INFO", message);
        public void Warn(string message) => Write("WARN", message);
        public void Error(string message) => Write("ERROR", message);
    }
}