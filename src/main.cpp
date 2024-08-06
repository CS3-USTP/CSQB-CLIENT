
#include <iostream>
#include <string>
#include <filesystem>
#include <cstdlib>
#include <csignal>
#include <exception>
#include <windows.h>
#include <fstream>
#include <shellapi.h>


// ======================== Global Variables ========================

std::string command;
std::string zrok_url = "https://github.com/openziti/zrok/releases/download/v0.4.38/zrok_0.4.38_windows_amd64.tar.gz";
std::filesystem::path zrok_dir = std::filesystem::path(getenv("USERPROFILE")) / "AppData" / "Local" / "Csqb";
std::filesystem::path zrok_tar = zrok_dir / "zrok.tar.gz";
std::filesystem::path zrok_exe = zrok_dir / "zrok.exe";
std::string zrok_enable_token = "u8mseFvy2H5L";
    
std::string minecraft_host_domain = "mc.csqb.org";
std::string minecraft_host_address = "127.0.69.0";
std::string minecraft_host_port = "25565";
std::string minecraft_zrok_tunnel_token = "wg2nug3zbgz1";


// ======================== Utility Functions ========================

bool isWTCmdAvailable() {
    // Command to check if 'wt.exe' is available
    int result = system("where wt.exe >nul 2>nul");
    
    // If the result is 0, the command was successful, meaning 'wt.exe' is available
    return (result == 0);
}

// ======================== Admin Privileges ======================== 


void RequestAdminPrivileges() {

    std::wstring wtPath = L"wt.exe";

    if (isWTCmdAvailable()) {
        TCHAR szPath[MAX_PATH];
        GetModuleFileName(NULL, szPath, MAX_PATH);

        // Attempt to use Windows Terminal
        SHELLEXECUTEINFO sei = { sizeof(sei) };
        sei.lpVerb = TEXT("runas");
        sei.nShow = SW_SHOWNORMAL;

        #if defined(UNICODE)
            std::wstring command = L"\"" + std::wstring(szPath) + L"\"";
            sei.lpFile = L"wt.exe";
            sei.lpParameters = command.c_str();
        #else
            std::string command = "\"" + std::string(szPath) + "\"";
            sei.lpFile = "wt.exe";
            sei.lpParameters = command.c_str();
        #endif

        if (!ShellExecuteEx(&sei)) {
            DWORD error = GetLastError();
            std::cerr << "Failed to launch command. Error code: " << error << std::endl;
        }
    }


    else {
        WCHAR szPath[MAX_PATH];
        GetModuleFileNameW(NULL, szPath, MAX_PATH);

        SHELLEXECUTEINFOW sei = { sizeof(sei) };
        sei.lpVerb = L"runas";
        sei.nShow = SW_SHOWNORMAL;
        std::wstring wtPath = L"wt.exe";
        std::wstring command = L"\"" + std::wstring(szPath) + L"\"";
        sei.lpFile = szPath;

        if (!ShellExecuteExW(&sei)) {
            DWORD error = GetLastError();
            std::cerr << "Failed to launch command. Error code: " << error << std::endl;
        }   
    }
}

bool IsAdmin() {
    BOOL isAdmin = FALSE;
    PSID adminGroup = NULL;

    SID_IDENTIFIER_AUTHORITY NtAuthority = SECURITY_NT_AUTHORITY;
    if (AllocateAndInitializeSid(&NtAuthority, 2, SECURITY_BUILTIN_DOMAIN_RID,
                                DOMAIN_ALIAS_RID_ADMINS, 0, 0, 0, 0, 0, 0, &adminGroup)) {
        if (!CheckTokenMembership(NULL, adminGroup, &isAdmin)) {
            isAdmin = FALSE;
        }
        FreeSid(adminGroup);
    }
    return isAdmin == TRUE;
}

// ======================== Hosts File ========================

bool IsEntryInHostsFile(const std::string& entry) {
    std::ifstream hostsFile("C:\\Windows\\System32\\drivers\\etc\\hosts");
    std::string line;
    while (std::getline(hostsFile, line)) {
        if (line.find(entry) != std::string::npos) {
            return true;
        }
    }
    return false;
}

void AddEntryToHostsFile(const std::string& entry) {
    std::ofstream hostsFile("C:\\Windows\\System32\\drivers\\etc\\hosts", std::ios_base::app);
    if (hostsFile.is_open()) {
        hostsFile << std::endl << entry << std::endl;
        hostsFile.close();
    } else {
        std::cerr << "Failed to open hosts file for writing." << std::endl;
    }
}


// ======================== Error Handlers ========================

void reportError(const std::string& message) {
    std::cerr << message;
    std::cout << "\n\n Looks like an error has occured, kindly report this to the developer. cs3.ustp@gmail.com \n";

    // press any key to exit
    std::cout << "\nPress any key to exit...";
    std::cin.get();
    exit(1);
}



// ======================== Cleanup Handlers ========================

void cleanup() {
    system("cls");
    std::cout << "Performing cleanup before exiting..." << std::endl;
    // Add your cleanup code here, such as closing files, releasing resources, etc.

    // Disable Zrok Account Token
    command = zrok_exe.string() + " disable";
    system(command.c_str());
    
    exit(0);
}

void handleSignal(int signum = 0) {
    cleanup();

    if (signum != 0) {  // Called by signal handler
        std::cout << "Exiting due to signal: " << signum << std::endl;
        std::exit(signum);
    }
}

void handleTerminate() {
    std::cout << "Unhandled exception or terminate called." << std::endl;
    cleanup();
    std::abort(); // Re-throw the exception or terminate the program
}



// ======================== Setup Functions ========================

void initHosts() {
    system("cls");
    std::cout << "\n(Step 1 / 4) Setting up domain host. \n\n";

    const std::string entry = minecraft_host_address + " " + minecraft_host_domain;

    if (!IsEntryInHostsFile(entry)) {

        if (!IsAdmin()) {
            std::cout << "You need administrator priviledges to continue. \n" << std::endl;
            RequestAdminPrivileges();
        }

        if (!IsAdmin()) exit(1);

        AddEntryToHostsFile(entry);
    } 
}

void initZrok() {

    /*
    * Step 1: Download Zrok
    * Step 2: Enable Zrok Account Token
    */

    // Check if Zrok Executable Exists
    if (!std::filesystem::exists(zrok_exe)) {

        system("cls");
        std::cout << "\n(Step 2 / 4) Getting required dependencies.\n\n";

        // Create Zrok Directory
        if (!std::filesystem::exists(zrok_dir))
            if (!std::filesystem::create_directory(zrok_dir)) 
                reportError("Failed to create directory.."); 
                
         // Download Zrok
        command = "curl -L " + zrok_url + " -#  -o " + zrok_tar.string();
        if (system(command.c_str()) != 0) 
            reportError("Failed to download dependencies..");

        // Extract Zrok
        command = "tar -xf " + zrok_tar.string() + " -C " + zrok_dir.string();
        if (system(command.c_str()) != 0) 
            reportError("Failed to extract dependencies..");

        // Remove Zrok Tar
        command = "del " + zrok_tar.string();
        if (system(command.c_str()) != 0) 
            reportError("Failed to clear dependency tar file..");
        
    }

    // Enable Zrok Account/Environment Token
    system("cls");
    std::cout << "\n(Step 3 / 4) Creating environment node.\n\n";

    // when previous exit cleanup isn't finished, will cause an error
    Sleep(2000); 

    command = zrok_exe.string() + " enable " + zrok_enable_token;
    system(command.c_str());
}

void initTunnel() {

    /*
    * Step 3: Connect Tunnel
    */
    
    system("cls");
    std::cout << "(Step 4 / 4) Accessing tunnel endpoint.\n\n";

    command = zrok_exe.string() + " access private " + minecraft_zrok_tunnel_token + " --headless --bind " + minecraft_host_domain + ":" + minecraft_host_port;
    if (system(command.c_str()) != 0) 
        reportError("Failed tunnel connection..");
}



// ======================== Windows-Main Function ========================

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {

    // Exit Handlers
    std::atexit(cleanup);                // Normal program exit
    std::signal(SIGINT, handleSignal);   // Handle Ctrl+C
    std::signal(SIGTERM, handleSignal);  // Handle termination signal
    std::signal(SIGABRT, handleSignal);  // Handle abort signal
    std::set_terminate(handleTerminate); // Uncaught exceptions


    // Initialize Hosts File
    initHosts(); // requires admin privileges

    // Initialize Zrok Client
    initZrok();

    // Connect to Zrok Tunnel
    initTunnel();

    return 0;
}
