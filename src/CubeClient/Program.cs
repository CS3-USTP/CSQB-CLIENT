
using System;
using System.Diagnostics;

namespace CubeClient {

class Program
{
    static void Main(string[] args)
    {
        // Path to the executable file
        // Todo: Update the path to the actual executable file
        string exePath = @"C:\Users\LENOVO\Desktop\CSQB-CLIENT\out\proxy.exe";

        // Optional arguments for the executable
        string arguments = "--help";

        // Create a new process to start the executable
        Process process = new Process();
        process.StartInfo.FileName = exePath;
        process.StartInfo.Arguments = arguments;

        try
        {
            // Start the process
            process.Start();

            // Optionally wait for the process to exit
            process.WaitForExit();

            // press any key to exit
            Console.ReadKey();

            // Get the exit code if needed
            int exitCode = process.ExitCode;
            Console.WriteLine($"Process exited with code: {exitCode}");
        }
        catch (Exception ex)
        {
            // Handle any errors
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

}