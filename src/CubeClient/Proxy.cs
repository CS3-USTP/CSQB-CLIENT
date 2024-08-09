
namespace CubeClient {
    class Proxy {
        public string EnableToken { get; set; }
        public string ExecutablePath { get; set; }

        public Proxy(string executablePath, string enableToken) {
            this.EnableToken = enableToken;
            this.ExecutablePath = executablePath;   
        }

        public void runSync(string execArgs) {
            System.Diagnostics.Process process = new();
            process.StartInfo.FileName = this.ExecutablePath;
            process.StartInfo.Arguments = execArgs;
            process.Start();
            process.WaitForExit();
        }

        public void runAsync(string execArgs) {
            System.Diagnostics.Process process = new();
            process.StartInfo.FileName = this.ExecutablePath;
            process.StartInfo.Arguments = execArgs;
            process.Start();
        }
    }
}