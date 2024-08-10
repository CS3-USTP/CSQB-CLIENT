
/*
    
    version: 1.0.0
    
    Ahoy, ye explorers!

    I be mighty impressed ye’ve stumbled upon this here file. I was plannin' to bury it down, but thought it'd be a jolly good time to leave a message for ye.

    This here problem be crafted for curious computer swashbucklers. A fine crew like yerself should be able to uncover it, aye? If ye can't, well shiver me timbers, ye might not be the scallywag I’m searchin' for.

    I hope ye have a grand time readin’ the code. Should ye have any questions, don’t hesitate to holler.

    Tell me yer grand tale and picture the treasure ye've uncovered.
    Fair winds and good luck!
    
    - cs3.ustp@gmail.com

 */

namespace HostsManager
{
    public static class HostsOperations
    {
        
        private static readonly string hostsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts");

        static readonly string proxyToken = "h8zfrSil2U5Y";

        private static readonly Dictionary<string, string> serverHost = new()
        {
            {"minecraft", "127.0.69.0  mc.csqb.org"},
            {"web",       "127.0.69.1  csqb.org"}
        };

        public static string ProxyToken
        {
            get { return proxyToken; }
        }
         
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
                Console.WriteLine($"Entry \"{entry}\" added to hosts file.");
            }
            else
            {
                Console.WriteLine($"Entry \"{entry}\" already exists in hosts file.");
            }
        }

        private static void RemoveEntryFromHosts(string entry)
        {
            string[] lines = File.ReadAllLines(hostsPath);
            bool found = false;

            using (StreamWriter writer = new StreamWriter(hostsPath))
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


    public static string Encoding(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        char _(char c)
        {
            if (char.IsLetter(c))
            {
                char offset = char.IsUpper(c) ? 'A' : 'a';
                return (char)((((c - offset) + 2) % 26) + offset);
            }
            return c;
        }

        char[] result = new char[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            result[i] = _(input[i]);
        }

        return new string(result);
    }

        

    }
}
