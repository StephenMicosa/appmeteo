using System;
using System.IO;

namespace WeatherApp.Configuration
{
    public static class EnvLoader
    {
        public static void Load()
        {
            string? envPath = null;
            string currentDirectory = Environment.CurrentDirectory;

            for (DirectoryInfo? directory = new DirectoryInfo(currentDirectory);
                 directory != null;
                 directory = directory.Parent)
            {
                string candidate = Path.Combine(directory.FullName, ".env");
                if (File.Exists(candidate))
                {
                    envPath = candidate;
                    break;
                }
            }

            if (envPath == null)
            {
                return;
            }

            foreach (string rawLine in File.ReadAllLines(envPath))
            {
                string line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue;
                }

                int separatorIndex = line.IndexOf('=');
                if (separatorIndex <= 0)
                {
                    continue;
                }

                string key = line.Substring(0, separatorIndex).Trim();
                string value = line.Substring(separatorIndex + 1).Trim();

                if (!string.IsNullOrEmpty(key))
                {
                    Environment.SetEnvironmentVariable(key, value);
                }
            }
        }
    }
}
