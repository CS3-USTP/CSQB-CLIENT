

namespace HostConfigManager
{
    public static class HostConfig
    {
        private static readonly string hostsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts");

        private static readonly Dictionary<string, string> serverHost = new()
        {
            {"minecraft", "127.0.69.0  mc.csqb.org  25565"},
            {"web",       "127.0.69.1  csqb.org     80"}
        };

        public static Dictionary<string, string> ServerHost
        {
            get { return serverHost; }
        }
        
        public static void IncludeServerHost() {
            foreach (string entry in serverHost.Values) {
                AddEntryToHosts(entry);
            }
        }

        public static void ExcludeServerHost() {
            foreach (string entry in serverHost.Values) {
                RemoveEntryFromHosts(entry);
            }
        }

        private static void AddEntryToHosts(string entry)
        {
            string hostsContent = File.ReadAllText(hostsPath);

            if (!hostsContent.Contains(entry))
            {
                File.AppendAllText(hostsPath, Environment.NewLine + entry);
                // Console.WriteLine($"Entry \"{entry.Split()[1]}\" added to hosts file.");
            }
            else
            {
                // Console.WriteLine($"Entry \"{entry.Split()[1]}\" already exists in hosts file.");
            }
        }

        private static void RemoveEntryFromHosts(string entry)
        {
            string[] lines = File.ReadAllLines(hostsPath);
            bool found = false;

            using (StreamWriter writer = new(hostsPath))
            {
                foreach (string line in lines)
                {
                    if (line.Trim() != entry)
                    {
                        writer.WriteLine(line);
                    }
                    else
                    {
                        found = true;
                    }
                }
            }

            if (found)
            {
                // Console.WriteLine($"Entry \"{entry.Split()[1]}\" removed from hosts file.");
            }
            else
            {
                // Console.WriteLine($"Entry \"{entry}\" not found in hosts file.");
            }
        }

    }
}
