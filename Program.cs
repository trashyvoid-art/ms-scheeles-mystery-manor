using System;
using static System.Console;

/* NASR BIN SAFWAN
 * ITP SUMMER 2021 - JANELL BAXTER
 * WITH HELP FROM KAREN SPRIGGS, JANELL BAXTER AND K LUEPTOW
 */

namespace MurderMystereee
{
    class MainClass
    {
        public static void Main()
        {
            // THE CAVEAT:
            // i've decided to make this the start of a bigger projects where
            // i can reuse the same characters, locations, assets, and other stuff
            // in different routes, which would be randomized until certain conditions
            // are met. this, however. 

            ForegroundColor = ConsoleColor.Green;

            WriteLine(@"
███╗░░░███╗░██████╗  ░██████╗░█████╗░██╗░░██╗███████╗███████╗██╗░░░░░███████╗██╗░██████╗
████╗░████║██╔════╝  ██╔════╝██╔══██╗██║░░██║██╔════╝██╔════╝██║░░░░░██╔════╝╚█║██╔════╝
██╔████╔██║╚█████╗░  ╚█████╗░██║░░╚═╝███████║█████╗░░█████╗░░██║░░░░░█████╗░░░╚╝╚█████╗░
██║╚██╔╝██║░╚═══██╗  ░╚═══██╗██║░░██╗██╔══██║██╔══╝░░██╔══╝░░██║░░░░░██╔══╝░░░░░░╚═══██╗
██║░╚═╝░██║██████╔╝  ██████╔╝╚█████╔╝██║░░██║███████╗███████╗███████╗███████╗░░░██████╔╝
╚═╝░░░░░╚═╝╚═════╝░  ╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚══════╝╚══════╝░░░╚═════╝░

███╗░░░███╗██╗░░░██╗░██████╗████████╗███████╗██████╗░██╗░░░██╗  ███╗░░░███╗░█████╗░███╗░░██╗░█████╗░██████╗░
████╗░████║╚██╗░██╔╝██╔════╝╚══██╔══╝██╔════╝██╔══██╗╚██╗░██╔╝  ████╗░████║██╔══██╗████╗░██║██╔══██╗██╔══██╗
██╔████╔██║░╚████╔╝░╚█████╗░░░░██║░░░█████╗░░██████╔╝░╚████╔╝░  ██╔████╔██║███████║██╔██╗██║██║░░██║██████╔╝
██║╚██╔╝██║░░╚██╔╝░░░╚═══██╗░░░██║░░░██╔══╝░░██╔══██╗░░╚██╔╝░░  ██║╚██╔╝██║██╔══██║██║╚████║██║░░██║██╔══██╗
██║░╚═╝░██║░░░██║░░░██████╔╝░░░██║░░░███████╗██║░░██║░░░██║░░░  ██║░╚═╝░██║██║░░██║██║░╚███║╚█████╔╝██║░░██║
╚═╝░░░░░╚═╝░░░╚═╝░░░╚═════╝░░░░╚═╝░░░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░  ╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝░╚════╝░╚═╝░░╚═╝");

            ForegroundColor = ConsoleColor.White;
            WriteLine("\n\n\n                PRESS ENTER TO CONTINUE");

            ReadKey();
            Game game = new Game();
        }
    }
}
