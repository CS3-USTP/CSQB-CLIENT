using System.Diagnostics;


namespace ProxyConfigManager
{
    public class ProxyConfig
    {
        static readonly string execPath = "CubeProxy.exe";

        public static void Remove()
        {
            try
            {
                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string zrokFolder = Path.Combine(userFolder, ".zrok");

                if (Directory.Exists(zrokFolder))
                {
                    Directory.Delete(zrokFolder, true);
                    // Console.WriteLine($"Folder '{zrokFolder}' deleted successfully.");
                }
                else
                {
                    // Console.WriteLine($"Folder '{zrokFolder}' does not exist.");
                }
            }
            catch (Exception)
            {
                // Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        public static Process Execute(string execArgs) 
        {            
            Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = execPath,
                    Arguments = execArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,  
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };
            process.Start();
            process.WaitForExit();
            return process;
        }


        public static void KillProxyProcesses()
        {
            // Get all processes with the name "CubeProxy" (without ".exe")
            Process[] processes = Process.GetProcessesByName("CubeProxy");
            
            foreach (Process process in processes)
            {
                try
                {
                    // Kill the process
                    process.Kill();
                    process.WaitForExit(); // Optional: Wait for the process to exit
                    // Console.WriteLine($"Killed process {process.Id} - {process.ProcessName}");
                }
                catch (Exception)
                {
                    // Handle any exceptions that might occur
                    // Console.WriteLine($"Failed to kill process {process.Id} - {process.ProcessName}: {ex.Message}");
                }
            }
        }
    }   

}

