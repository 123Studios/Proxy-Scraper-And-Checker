using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ProxyGetterAndChecker
{
    internal class Program
    {
        static string option;
        static int delay;
        static string path;
        static void Main(string[] args)
        {
            if (args.Length == 0) { Menu(); }
            else { path = args[0]; Check(); }


        }
        public static void Menu()
        {
            Console.Title = $"ProxyGetterAndChecker By 123Studios - Made for privacy ONLY";
            Console.WriteLine("1.)HTTPS\n2.)Socks-4\n3.)Socks-5"); Console.Write("Please enter option [>] "); string option = Console.ReadLine();
            if (option == "1") { option = "http"; }
            if (option == "2") { option = "socks4"; }
            if (option == "3") { option = "socks5"; }
            Console.WriteLine("Delay (Ex: 1000)"); Console.Write("Please enter option [>] "); delay = Int32.Parse(Console.ReadLine());
            Start();
        }
        public static void Start()
        {
            try
            {
                WebClient wc = new WebClient();
                string data = wc.DownloadString($"https://api.proxyscrape.com/v2/?request=displayproxies&protocol={option}&timeout=10000&country=all&ssl=all&anonymity=all");
                File.WriteAllText(Environment.CurrentDirectory + "/out.txt", data);
            }
            catch 
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; 
                Console.WriteLine("[!] Cannot download proxy from proxyspace.com\nPress a key to exit...");
                Console.ReadKey();
                Environment.Exit(0); 
            }
            var lines = File.ReadAllLines(Environment.CurrentDirectory + "/out.txt").Count();
            string[] p1 = File.ReadAllLines(Environment.CurrentDirectory + "/out.txt");
            File.WriteAllText(Environment.CurrentDirectory + "/hits.txt", "");
            int hits = 0;
            int bad = 0;
            for (int i = 0; i < lines; i++)
            {

                Console.Title = $"ProxyGetterAndChecker By 123Studios - Made for privacy ONLY - Good: {hits} Bad: {bad} ({i}/{lines})";
                string current = p1[i];
                string[] lineData = current.Split(':');
                var target = lineData[0];
                var port = lineData[1];
                var client = new TcpClient();
                if (client.ConnectAsync(target, Convert.ToInt32(port)).Wait(delay))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"[+] Valid Proxy - {target}:{port}");
                    hits++;
                    using (StreamWriter w = File.AppendText(Environment.CurrentDirectory + "/hits.txt"))
                    {
                        w.WriteLine(current);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"[-] Invalid Proxy - {target}:{port}");
                    bad++;
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Finished task.");
            Console.ReadKey();
        }
        public static void Check()
        {
            var lines = File.ReadAllLines(path).Count();
            string[] p1 = File.ReadAllLines(path);
            File.WriteAllText(Environment.CurrentDirectory + "/hits.txt", "");
            Console.WriteLine("Delay (Ex: 1000)"); Console.Write("Please enter option [>] "); delay = Int32.Parse(Console.ReadLine());


            int hits = 0;
            int bad = 0;
            for (int i = 0; i < lines; i++)
            {

                Console.Title = $"ProxyGetterAndChecker By 123Studios - Made for privacy ONLY - Good: {hits} Bad: {bad} ({i}/{lines})";
                string current = p1[i];
                string[] lineData = current.Split(':');
                var target = lineData[0];
                var port = lineData[1];
                var client = new TcpClient();
                if (client.ConnectAsync(target, Convert.ToInt32(port)).Wait(delay))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"[+] Valid Proxy - {target}:{port}");
                    hits++;
                    using (StreamWriter w = File.AppendText(Environment.CurrentDirectory + "/hits.txt"))
                    {
                        w.WriteLine(current);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"[-] Invalid Proxy - {target}:{port}");
                    bad++;
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Finished task.");
            Console.ReadKey();
        }

    }
        
}
