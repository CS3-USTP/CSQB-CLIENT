
using System.Diagnostics;

namespace CubeExecutable
{
    class Program
    {

        // TODO: On deployment, change path of executables
        static readonly string wtExecPath = "C:/Users/LENOVO/Desktop/CSQB-CLIENT/bin/wt.exe";   
        static readonly string cubeExecPath = "C:/Users/LENOVO/Desktop/CSQB-CLIENT/src/CubeClient/bin/Debug/net8.0/Client.exe";

        static void Main(string[] args)
        {            
            ProcessStartInfo startInfo = new()
            {
                FileName = wtExecPath,
                Arguments = $"{cubeExecPath} start cube"
            };
            
            Process.Start(startInfo);


        }
    }
}