
using System.Diagnostics;

namespace CubeExecutable
{
    class Program
    {

        // TODO: On deployment, change path of windows terminal
        

        static void Main(string[] args)
        {            
            ProcessStartInfo startInfo = new()
            {
                FileName = "C:/Users/LENOVO/Desktop/CSQB-CLIENT/bin/wt.exe",
                Arguments = "C:/Users/LENOVO/Desktop/CSQB-CLIENT/src/CubeClient/bin/Debug/net8.0/client.exe start cube"
            };
            
            Process.Start(startInfo);


        }
    }
}