
using System;
using System.IO;

namespace HostsFileManager
{
    public static class HostsFileOperations
    {
        private static string hostsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts");

        public static void IncludeServerDomains() {
            AddEntryToHostsFile("127.0.69.0 mc.csqb.org");
            AddEntryToHostsFile("127.0.69.1 csqb.org");
        }

        public static void ExcludeServerDomains() {
            AddEntryToHostsFile("127.0.69.0 mc.csqb.org");
            AddEntryToHostsFile("127.0.69.1 csqb.org");
        }

        private static void AddEntryToHostsFile(string entry)
        {
            string hostsContent = File.ReadAllText(hostsFilePath);

            if (!hostsContent.Contains(entry))
            {
                File.AppendAllText(hostsFilePath, Environment.NewLine + entry);
                Console.WriteLine($"Entry \"{entry}\" added to hosts file.");
            }
            else
            {
                Console.WriteLine($"Entry \"{entry}\" already exists in hosts file.");
            }
        }

        private static void RemoveEntryFromHostsFile(string entry)
        {
            string[] lines = File.ReadAllLines(hostsFilePath);
            bool found = false;

            using (StreamWriter writer = new StreamWriter(hostsFilePath))
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
                Console.WriteLine($"Entry \"{entry}\" removed from hosts file.");
            }
            else
            {
                Console.WriteLine($"Entry \"{entry}\" not found in hosts file.");
            }
        }
    }
}
