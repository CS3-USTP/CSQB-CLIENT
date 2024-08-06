#include <iostream>
#include <string>
#include <filesystem>
#include <cstdlib>
#include <windows.h>

int main() {

    std::string zrok_url = "https://github.com/openziti/zrok/releases/download/v0.4.38/zrok_0.4.38_windows_amd64.tar.gz";
    std::string zrok_enable_token = "";

    std::filesystem::path zrok_dir = std::filesystem::path(getenv("USERPROFILE")) / "AppData" / "Local" / "Csqb";
    std::filesystem::path zrok_tar = zrok_dir / "zrok.tar.gz";
    std::filesystem::path zrok_exe = zrok_dir / "zrok.exe";
    
    std::string minecraft_host_address = "127.0.69.0:25565";
    std::string minecraft_host_domain = "mc.csqb.org";
    std::string minecraft_zrok_tunnel_token = "";
    

    // Check if Zrok Executable Exists
    if (!std::filesystem::exists(zrok_exe)) {

        std::cout << "(Step 1 / 3) Initializing Dependencies\n\n";
        Sleep(1000);

        // Create Zrok Directory
        if (std::filesystem::create_directory(zrok_dir)) {

            // Download Zrok
            std::string command = "curl -L " + zrok_url + " -o " + zrok_tar.string() + " -#";
            
            if (system(command.c_str()) != 0) {
                std::cerr << "Failed to download dependencies..\n";
                return 1;
            }

            // Extract Zrok
            command = "tar -xf " + zrok_tar.string() + " -C " + zrok_dir.string() + " > nul 2>&1";
            if (system(command.c_str()) != 0) {
                std::cerr << "Failed to extract dependencies..\n";
                return 1;
            }

            // Remove Zrok Tar
            command = "del " + zrok_tar.string();
            if (system(command.c_str()) != 0) {
                std::cerr << "Failed to clear dependency tar file..\n";
                return 1;
            }

        } 
        
        else {
            std::cerr << "Failed to initialize dependecies..\n";
            return 1;
        }
    }

    return 0;
}
