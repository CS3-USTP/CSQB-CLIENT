
using System.Diagnostics;

namespace CubeExecutable
{
    class Program
    {
        static readonly string wtExecPath = "wt.exe";   
        static readonly string cubeExecPath = "Client.exe";

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