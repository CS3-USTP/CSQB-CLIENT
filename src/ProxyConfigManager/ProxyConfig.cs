using System;
using System.IO;
using System.Diagnostics;


namespace ProxyConfigManager
{
    public class ProxyConfig
    {
    
        public static void Remove()
        {
            try
            {
                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string zrokFolder = Path.Combine(userFolder, ".zrok");

                if (Directory.Exists(zrokFolder))
                {
                    Directory.Delete(zrokFolder, true);
                    Console.WriteLine($"Folder '{zrokFolder}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Folder '{zrokFolder}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        
        public static Process Execute(string execArgs) 
        {
            // TODO: On deployment, update path of executables
            const string execPath = "C:/Users/LENOVO/Desktop/CSQB-CLIENT/bin/Proxy.exe";
            
            Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = execPath,
                    Arguments = execArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,  
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();
            return process;
        }
    }   




}

