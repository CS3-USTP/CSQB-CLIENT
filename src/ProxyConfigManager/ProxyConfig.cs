using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace ProxyConfigManager
{
    public class ProxyConfig
    {
        public static void CreateConfigFile(string content = "")
        {
            // Get the path to the user's folder
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string zrokFolder = Path.Combine(userFolder, ".zrok");
            string configFilePath = Path.Combine(zrokFolder, "config.json");

            try
            {
                // Create the .zrok directory if it does not exist
                if (!Directory.Exists(zrokFolder))
                {
                    Directory.CreateDirectory(zrokFolder);
                }

                // Write or overwrite the config.json file
                File.WriteAllText(configFilePath, content);

#if WINDOWS
                // Apply Windows-specific file access control
                FileInfo fileInfo = new FileInfo(configFilePath);
                FileSecurity fileSecurity = fileInfo.GetAccessControl();

                // Get the SID for all authenticated users
                var allUsersSid = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);

                // Deny write access to all authenticated users
                fileSecurity.AddAccessRule(new FileSystemAccessRule(allUsersSid, FileSystemRights.Write, AccessControlType.Deny));

                // Allow full control for administrators
                fileSecurity.AddAccessRule(new FileSystemAccessRule("BUILTIN\\Administrators", FileSystemRights.FullControl, AccessControlType.Allow));

                // Allow full control for the system
                fileSecurity.AddAccessRule(new FileSystemAccessRule("SYSTEM", FileSystemRights.FullControl, AccessControlType.Allow));

                // Apply the updated access control settings
                fileInfo.SetAccessControl(fileSecurity);

                Console.WriteLine($"Config file created and write access restricted to system and administrators: {configFilePath}");
#elif LINUX
                // Apply Linux-specific file permissions
                // Make the file read-only for the owner
                Process.Start(new ProcessStartInfo
                {
                    FileName = "chmod",
                    Arguments = "400 " + configFilePath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }).WaitForExit();

                Console.WriteLine($"Config file created and write access restricted: {configFilePath}");
#else
                Console.WriteLine($"Config file created: {configFilePath}");
                Console.WriteLine("Access control settings for this platform are not implemented.");
                // Optionally, handle other platforms or provide more specific information here.
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
