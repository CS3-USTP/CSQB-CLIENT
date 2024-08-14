
using System.Diagnostics;
using System.Security.Principal;
using HostConfigManager;

namespace HostConfigWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!HostConfig.CheckAllHostsExist()) 
            {
                EnsureRunAsAdmin();
                HostConfig.IncludeServerHost();
            }
        }

        private static int EnsureRunAsAdmin()
        {
            if (!IsRunAsAdmin())
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = Environment.ProcessPath,
                    UseShellExecute = true,
                    Verb = "runas"
                };

                try
                {
                    Process? process = Process.Start(processInfo);
                    if (process != null)
                    {
                        process.WaitForExit();
                        return process.ExitCode; // Return the exit code of the restarted process
                    }
                    else
                    {
                        throw new InvalidOperationException("Failed to start the process.");
                    }
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    // Handle the case where the user cancels the UAC prompt
                    Console.WriteLine("Admin privileges are required. Exiting...");
                    Environment.Exit(-1);
                }
            }
            return 0; // Return 0 if the process is already running as admin
        }



        private static bool IsRunAsAdmin()
        {
    #pragma warning disable CA1416 // Validate platform compatibility
                using var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
    #pragma warning restore CA1416 // Validate platform compatibility
            }
        }

    
}
