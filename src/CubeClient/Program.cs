using System.Diagnostics;
using Spectre.Console;
using ProxyConfigManager;
using HostConfigManager;

namespace CubeClient
{
    class Program
    {
        static readonly string banner = 

        "           [rgb(239,157,39)]▄[/][rgb(247,161,41)]▄[/][rgb(243,159,40)]▄[/]          \n" 
        +"        [rgb(240,159,41)]▄[/][rgb(247,159,40)]▄[/][rgb(244,159,41) on rgb(247,159,40)]▄[/][rgb(249,162,42)]█[/][rgb(250,163,43)]█[/][rgb(249,162,42) on rgb(250,163,42)]▄[/][rgb(247,160,41) on rgb(247,161,42)]▄[/]         \n" 
        +"     [rgb(240,150,37)]▄[/][rgb(240,157,40)]▄[/][rgb(249,162,42) on rgb(246,160,41)]▄[/][rgb(250,163,43) on rgb(249,162,42)]▄[/][rgb(250,163,42) on rgb(250,163,43)]▄[/][rgb(251,163,43) on rgb(249,161,42)]▄[/][rgb(244,159,40) on rgb(243,155,38)]▄[/][rgb(248,161,42)]▀[/][rgb(246,157,38)]▀[/]    [rgb(242,159,38)]▄[/][rgb(240,158,37)]▄[/]    \n" 
        +"   [rgb(159,139,82)]▄[/][rgb(249,162,43) on rgb(247,160,41)]▄[/][rgb(250,163,43) on rgb(250,162,43)]▄[/][rgb(250,163,43) on rgb(250,162,43)]▄[/][rgb(250,162,42) on rgb(244,157,41)]▄[/][rgb(244,157,39) on rgb(247,160,40)]▄[/][rgb(233,144,33) on rgb(248,162,42)]▄[/][rgb(242,157,40)]▀[/]    [rgb(228,148,27)]▄[/][rgb(241,154,39)]▄[/][rgb(250,163,42) on rgb(245,159,39)]▄[/][rgb(250,163,42) on rgb(249,162,43)]▄[/][rgb(250,163,42) on rgb(249,162,42)]▄[/][rgb(250,163,42) on rgb(247,159,40)]▄[/][rgb(243,159,40)]▄[/]  \n" 
        +"   [rgb(34,110,145)]█[/][rgb(35,113,148) on rgb(92,125,119)]▄[/][rgb(50,116,141) on rgb(228,158,53)]▄[/][rgb(173,139,68) on rgb(250,163,43)]▄[/][rgb(248,161,42) on rgb(242,157,39)]▄[/][rgb(250,163,43) on rgb(248,160,42)]▄[/][rgb(250,163,43) on rgb(249,162,42)]▄[/][rgb(250,162,43) on rgb(247,160,41)]▄[/][rgb(245,159,41)]▄[/] [rgb(245,157,40)]▄[/][rgb(250,163,42) on rgb(245,158,39)]▄[/][rgb(251,163,43) on rgb(250,163,42)]▄[/][rgb(250,163,43) on rgb(248,162,41)]▄[/][rgb(250,163,42) on rgb(245,159,38)]▄[/][rgb(241,154,39) on rgb(248,163,43)]▄[/][rgb(250,163,42)]█[/][rgb(251,163,43) on rgb(250,163,42)]▄[/][rgb(246,160,42) on rgb(244,160,40)]▄[/]  \n" 
        +"   [rgb(32,108,143) on rgb(33,109,145)]▄[/][rgb(32,109,141) on rgb(34,112,148)]▄[/][rgb(35,113,147) on rgb(35,113,148)]▄[/][rgb(33,111,144) on rgb(33,111,144)]▄[/][rgb(35,113,149) on rgb(101,128,115)]▄[/][rgb(54,117,138) on rgb(233,159,51)]▄[/][rgb(183,145,71) on rgb(251,163,43)]▄[/][rgb(245,160,41) on rgb(249,161,41)]▄[/][rgb(249,162,42) on rgb(245,158,39)]▄[/][rgb(250,163,43) on rgb(246,161,40)]▄[/][rgb(250,163,42) on rgb(244,158,39)]▄[/][rgb(247,162,42) on rgb(249,161,40)]▄[/][rgb(248,159,41) on rgb(250,163,42)]▄[/][rgb(250,163,42) on rgb(250,163,42)]▄[/][rgb(250,163,43) on rgb(250,162,42)]▄[/][rgb(251,163,43) on rgb(249,161,42)]▄[/][rgb(248,161,42) on rgb(250,162,42)]▄[/][rgb(242,157,38) on rgb(250,163,42)]▄[/][rgb(240,154,40) on rgb(246,160,39)]▄[/]  \n" 
        +"   [rgb(34,110,145) on rgb(35,110,145)]▄[/][rgb(35,113,148) on rgb(34,113,148)]▄[/][rgb(35,113,148) on rgb(32,111,143)]▄[/][rgb(33,110,145)]▀[/][rgb(33,110,142) on rgb(35,112,148)]▄[/][rgb(34,112,147) on rgb(35,113,148)]▄[/][rgb(33,110,145) on rgb(33,110,145)]▄[/][rgb(35,113,148) on rgb(113,131,108)]▄[/][rgb(62,119,135) on rgb(239,161,49)]▄[/][rgb(206,152,64) on rgb(250,163,43)]▄[/][rgb(250,162,42) on rgb(250,163,42)]▄[/][rgb(250,163,43) on rgb(250,163,42)]▄[/][rgb(249,161,41) on rgb(248,160,40)]▄[/][rgb(249,162,42) on rgb(250,163,43)]▄[/][rgb(246,157,39) on rgb(250,163,43)]▄[/][rgb(248,161,42)]▀[/][rgb(250,163,43) on rgb(242,158,40)]▄[/][rgb(250,163,43) on rgb(250,163,43)]▄[/][rgb(246,161,42) on rgb(246,161,42)]▄[/]  \n" 
        +"   [rgb(33,110,144) on rgb(31,104,139)]▄[/][rgb(35,111,144) on rgb(34,112,146)]▄[/][rgb(33,109,144) on rgb(35,113,149)]▄[/]  [rgb(24,97,134)]▀[/][rgb(30,108,143)]▀[/][rgb(33,110,142) on rgb(35,113,148)]▄[/][rgb(35,112,147) on rgb(35,113,148)]▄[/][rgb(130,135,101)]█[/][rgb(249,162,42) on rgb(250,163,43)]▄[/][rgb(245,160,41) on rgb(250,163,43)]▄[/][rgb(249,160,40)]▀[/][rgb(237,149,35)]▀[/]  [rgb(246,159,41) on rgb(250,163,43)]▄[/][rgb(246,160,41) on rgb(248,162,42)]▄[/][rgb(229,155,49) on rgb(236,152,38)]▄[/]  \n" 
        +"   [rgb(33,108,144) on rgb(34,111,145)]▄[/][rgb(34,113,148) on rgb(35,113,148)]▄[/][rgb(35,112,148) on rgb(35,112,147)]▄[/][rgb(32,109,143)]▄[/][rgb(33,111,146)]▄[/]   [rgb(27,106,141)]▀[/][rgb(132,133,97)]▀[/][rgb(243,158,37)]▀[/]   [rgb(247,160,40)]▄[/][rgb(245,158,41)]▄[/][rgb(190,148,70) on rgb(239,158,45)]▄[/][rgb(189,146,71) on rgb(191,149,71)]▄[/][rgb(237,157,43) on rgb(205,149,61)]▄[/]  \n" 
        +"    [rgb(32,110,144)]▀[/][rgb(29,104,139) on rgb(34,113,148)]▄[/][rgb(34,109,146) on rgb(33,111,144)]▄[/][rgb(35,113,148) on rgb(34,113,148)]▄[/][rgb(35,113,148) on rgb(32,111,147)]▄[/][rgb(32,110,145) on rgb(32,106,138)]▄[/][rgb(33,110,145)]▄[/]   [rgb(243,159,40)]▄[/][rgb(250,160,42) on rgb(238,153,34)]▄[/][rgb(250,163,43) on rgb(247,160,42)]▄[/][rgb(250,163,43) on rgb(250,163,43)]▄[/][rgb(249,161,42) on rgb(245,160,41)]▄[/][rgb(244,159,40) on rgb(225,156,54)]▄[/][rgb(246,159,41)]▀[/]   \n" 
        +"       [rgb(33,108,144)]▀[/][rgb(32,106,143) on rgb(34,113,148)]▄[/][rgb(31,108,142) on rgb(33,110,145)]▄[/][rgb(35,113,148) on rgb(35,113,148)]▄[/][rgb(34,113,148) on rgb(33,111,147)]▄[/][rgb(130,135,101) on rgb(112,130,99)]▄[/][rgb(250,163,43) on rgb(248,161,42)]▄[/][rgb(250,163,42)]█[/][rgb(247,160,41) on rgb(249,161,40)]▄[/][rgb(242,157,38) on rgb(249,162,43)]▄[/][rgb(245,160,41)]▀[/]      \n" 
        +"          [rgb(32,111,144)]▀[/][rgb(31,109,140) on rgb(35,112,148)]▄[/][rgb(132,135,99) on rgb(130,135,101)]▄[/][rgb(243,159,39) on rgb(249,162,42)]▄[/][rgb(248,161,41)]▀[/]         \n"
        ;

        static readonly string about = 
            "[blue]CS3[/] [orange3]Student Organization[/]\n\n" +
            "Questions or issues?\n[cyan][link=https://mail.google.com]cs3.ustp@gmail.com[/][/]\n\n";

        static readonly string minecraftInfo = 
            "[grey]Join our new Minecraft server tailored for computer scientists![/] " +
            "[grey]Connect with fellow coders, build, explore, and have fun.[/]\n\n" +
            "[bold]Host:[/]       [blue]mc.csqb.org:25565[/]\n" +
            "[bold]Launcher:[/]   [blue]https://ely.by/load[/]";

        static readonly string instructions = 
            "[b]How to Get Started:[/]\n" +
            "1. [green]Launch[/] the Minecraft launcher from the link.\n" +
            "2. [green]Edit[/] your profile and skin by creating an account.\n" +
            "4. [green]Reopen[/] the app if there are errors that occur.\n\n" +
            "[bold yellow]Note: [/][red]Keep this program open to maintain access to the server.[/]";

        static readonly string KEY1 = "2b7e4ab34ccc5131e6050205483200f580c04f5b36a0a3719b851cd092985080";
        static readonly string IV1 = "0d75cfcfb4bf31bd700ec5075df553a6";
        static readonly byte[] KEY1_BYTES = Token.HexToBytes(KEY1);
        static readonly byte[]  IV1_BYTES = Token.HexToBytes(IV1);   
        static readonly string[] minecraftHost = 
            HostConfig.ServerHost["minecraft"]
            .Split(new[] { ' ' }, // split the string by space
                StringSplitOptions.RemoveEmptyEntries); // removes empty strings in the array

        static void Main(string[] args)
        {
            // Register the event handler
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            Console.TreatControlCAsInput = true;


            // Set the console configs
            Console.Title = "Cube";
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8; // for special characters
    
            // Validate program arguments
            if (args.Length != 2 || args[0] != "start" || args[1] != "cube") 
            {
                Environment.Exit(0);
            }
            
            Console.Clear();

            Setup();
        }

        // Event handler for process exit
        private static void OnProcessExit(object? sender, EventArgs e)
        {
            // Handle the event
            AnsiConsole.MarkupLine("[grey]Exiting...[/]");

            // Optionally, perform cleanup tasks here
            ClearCache();

            Thread.Sleep(3000);
        }


        static void ClearCache() {

            ProxyConfig.Execute("disable");
            ProxyConfig.Remove();
            ProxyConfig.KillProxyProcesses();
        }

        static void Setup() 
        {
            Process process;
            string? token = Prerequisites();

            if (token == null) {
                Environment.Exit(0);
                return;
            }

            try {
                
                process = ProxyConfig.Execute(
                    $"access private {token[12..24]} --bind {minecraftHost[1]}:{minecraftHost[2]} --headless"
                );


                if (process.ExitCode != 0) 
                {
                    Console.Clear();                    
                    Console.WriteLine(process.StandardOutput.ReadToEnd());
                    Console.WriteLine(process.StandardError.ReadToEnd());
                    AnsiConsole.MarkupLine("orange1]Tunnels have been refreshed, try again in a few minutes...[/]");
                    Environment.Exit(0);
                    return;
                }
            }   
            catch (Exception) 
            {   
                Console.Clear();
                AnsiConsole.MarkupLine("[red]Invalid Network.[/]");
                Environment.Exit(0);
                return;
            }

        }

        static string? Prerequisites() {

            Process process;
            string? token = null;
            bool prerequisites = true;

            AnsiConsole.Status()
                .Start("Checking for updates...", ctx => 
                {

                    process = new()
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "HostManager.exe",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,  
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };

                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        Console.Clear();
                        AnsiConsole.MarkupLine("[red]Failed to update domains![/]");
                        return;
                    }

                    ctx.Spinner(Spinner.Known.Dots);
                    ctx.SpinnerStyle(Style.Parse("blue"));
                    AnsiConsole.MarkupLine("[grey]Updating tunnels...[/]");
                    
                    // tunnel tokens from csqb-api
                    token = FetchToken();

                    if (token == null) 
                    {
                        Console.Clear();
                        AnsiConsole.MarkupLine("[red]Failed to update tunnels![/]");
                        prerequisites = false;
                        return;
                    }

                    // get the first line of the string of the token
                    token = Token.Rot13(token.Split("\n")[0]);

                    AnsiConsole.MarkupLine("[green]Tunnels updated successfully![/]");
                    AnsiConsole.MarkupLine("[grey]Requesting access...[/]");
                    
                    ctx.Status("Connecting to proxy network...");     
                    ctx.Spinner(Spinner.Known.BouncingBar);
                    ctx.SpinnerStyle(Style.Parse("deepskyblue2"));
                    
                    try 
                    { // zrok params check
                        process = ProxyConfig.Execute($"enable {token[..12]} --headless");

                        if (process.ExitCode != 0) 
                        { // zrok process check
                            AnsiConsole.MarkupLine("[orange1]Attempting restart..[/]");
                            AnsiConsole.MarkupLine("[grey]Clearing Cache...[/]");    
                            
                            ClearCache();
                            
                            AnsiConsole.MarkupLine("[grey]Cache has been cleared[/]");    
                            AnsiConsole.MarkupLine("[grey]Requesting access...[/]");

                            process = ProxyConfig.Execute($"enable {token[..12]} --headless");

                            if (process.ExitCode != 0) 
                            { // zrok process check
                                Console.Clear();
                                AnsiConsole.MarkupLine("[red]Failed to enable environment![/]");
                                prerequisites = false;
                                return;
                            };
                        }
                    }

                    catch (Exception) 
                    {
                        Console.Clear();
                        AnsiConsole.MarkupLine("[red]Invalid Network.[/]");
                        prerequisites = false;
                        return;
                    }
                }
            );

            if (token == null || !prerequisites) return null;

            Layout();   

            return token;
        }


        static string? FetchToken() {
            var tunnel = Token.Fetch(
                "https://raw.githubusercontent.com/CS3-USTP/CSQB-API/main/tunnels.txt");

            if (tunnel == null) 
            {
                AnsiConsole.MarkupLine("[red]Failed to fetch tokens![/]");
                return null;
            }

            /* Group 1 Decryption */
            // Apply ROT13 decoding again
            string token = Token.Rot13(tunnel);

            // Decrypt with KEY1 and IV1
            // contains the message
            token = Token.Decrypt(token, KEY1_BYTES, IV1_BYTES);

            return token;
        }

        
        static void Layout() 
        {
            Console.Clear();

            // Left section with banner and about information
            var leftSection = new Layout("Left").Update(
                new Panel(
                    Align.Center(
                        new Markup(banner+"\n\n"+about),
                        VerticalAlignment.Middle))
                .Header("[bold blue]About[/]", Justify.Center)
                .BorderColor(Color.SkyBlue2)
                .RoundedBorder()
                .Padding(1, 1, 1, 1)
                .Expand()
            );

            // Right section with Minecraft information and instructions
            var rightSection = new Layout("Right")
                .SplitRows(
                    new Layout("MinecraftInfo").Update(
                        new Panel(
                            Align.Left(
                                new Markup(minecraftInfo))
                        )
                        .Header("[bold green]Minecraft Server[/]", Justify.Center)
                        .BorderColor(Color.PaleGreen1)
                        .RoundedBorder()
                        .Padding(2, 2, 2, 2)
                        .Expand()),
                    new Layout("Instructions").Update(
                        new Panel(
                            Align.Left(
                                new Markup(instructions))
                        )
                        .Header("[bold mediumpurple2_1]Instructions[/]", Justify.Center)
                        .BorderColor(Color.MediumPurple1)
                        .RoundedBorder()
                        .Padding(2, 2, 2, 2)
                        .Expand()));

            // Main layout splitting the left and right sections
            var mainLayout = new Layout("Root")
                .SplitColumns(
                    new Layout("LeftPanel")
                        .SplitRows(leftSection)
                        .MinimumSize(40)
                        .Ratio(1)
                        ,
                    new Layout("RightPanel")
                        .SplitRows(rightSection)
                        .Ratio(2)
                        );

            // Render the final layout
            AnsiConsole.Write(mainLayout);
        }


        static void Error(Process process) {
            AnsiConsole.MarkupLine("[red]{0}[/]", 
                            process.StandardError.ReadToEnd()
                                .Trim()
                                .Replace("[","| ")
                                .Replace("]"," |")
                                .Replace("zrok ", "")
                                .Replace("'",""));
        }


    }
}
