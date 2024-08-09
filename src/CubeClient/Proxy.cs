
namespace CubeClient {
    class Proxy {
        public string enableToken { get; set; }
        public string execPath { get; set; }

        public Proxy(string execPath, string enableToken) {
            this.enableToken = enableToken;
            this.execPath = execPath;   
        }

        public void runSync(string execArgs) {
            System.Diagnostics.Process process = new();
            process.StartInfo.FileName = this.execPath;
            process.StartInfo.Arguments = execArgs;
            process.Start();
            process.WaitForExit();
        }

        public void runAsync(string execArgs) {
            System.Diagnostics.Process process = new();
            process.StartInfo.FileName = this.execPath;
            process.StartInfo.Arguments = execArgs;
            process.Start();
        }
    }
}