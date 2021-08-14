using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;  

namespace WebParser
{
    class Program
    {
        private const ConsoleColor gray = ConsoleColor.Gray;

        public static bool CheckTVProgram(string programmURL, string sender) //in Check Class
        {
            if(programmURL.Contains(sender))
            {
                return true;
            }
            else
                return false;
        }

        private static void SenderAusgabe(string sender) // class UI Dots or Progressbar
        {
            Console.WriteLine(sender);
            Dots();
            Console.WriteLine();
        }

        private static string ActualDate() // in Constructor of TVProgramm (Class)
        {
            string neuesDatum = DateTime.Now.ToString("yyyy-MM-dd");
            return neuesDatum;
        }

        public static string BuildURL(string tvStationName)
        {
            string url_1 = "https://www.tvspielfilm.de/tv-programm/sendungen/?page=1&order=time&date=";
            string url_2 = "&cat%5B%5D=SP&time=day&channel=";
            return url_1 + ActualDate()+ url_2 + tvStationName;
        }

        // Create classes
        // Just a fake Progressbar
        public static void Dots() // Class Progressbar
        {   
            for(int k = 0; k <= 100; k++)
            {
                Console.Write("\r");
                Console.Write(k + " %");
                System.Threading.Thread.Sleep(10);
            }
            Console.Write("[");        
            for (int i = 0; i < 30; i++)
            {
                Console.Write("#");
                System.Threading.Thread.Sleep(30);                    
                /***
                if (i == 8)
                {
                    Console.Write("\r   \r");
                    i = -1;
                    System.Threading.Thread.Sleep(1000);
                }
                ***/
            }
            Console.Write("]");
        }

        static void Welcome() //Userinterface
        {
            DateTime heute = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Check daily TV-Programm {0}", heute);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static AutoResetEvent waiter = new AutoResetEvent(false);
        static int counter = 0;

        static void Main(string[] args)
        {
            HashSet<string> myFilms = new HashSet<string>();
            // Add your favorite movies to the hast table
            // Action
            myFilms.Add("Your movie");
            myFilms.Add("another movie");
          

            string[] tvStationNamen = new string[13] // Property -> TV programms (class)
            {
                "ARD", "ZDF","SAT1","RTL","RTL2","PRO7","VOX","TELE5","K1","3SAT","ARTE","RTL-N","2NEO"
            };

            Uri[] urls = new Uri[] // Class Parse / Crawl
            {
                new Uri(BuildURL("ARD")), 
                new Uri(BuildURL("ZDF")), 
                new Uri(BuildURL("SAT1")), 
                new Uri(BuildURL("RTL")), 
                new Uri(BuildURL("RTL2")), 
                new Uri(BuildURL("PRO7")), 
                new Uri(BuildURL("VOX")), 
                new Uri(BuildURL("TELE5")), 
                new Uri(BuildURL("K1")), 
                new Uri(BuildURL("3SAT")), 
                new Uri(BuildURL("ARTE")), 
                new Uri(BuildURL("RTL-N")), 
                new Uri(BuildURL("2NEO")), 
            };

            Welcome();


            foreach(Uri url in urls)
            {
                // Regex hier
                string programName = url.Query;
                programName = programName.ToLower();

                if(CheckTVProgram(programName, "ard"))
                {
                    SenderAusgabe("Check TV Station: ARD");
                }
                else if(CheckTVProgram(programName, "zdf"))
                {
                     SenderAusgabe("Check TV Station: ZDF");
                } 
                else if(CheckTVProgram(programName, "sat1"))
                {
                     SenderAusgabe("Check TV Station: Sat1");
                } 
                else if(CheckTVProgram(programName, "rtl2"))
                {
                     SenderAusgabe("Check TV Station: RTL2");
                } 
                else if(CheckTVProgram(programName, "rtl"))
                {
                     SenderAusgabe("Check TV Station: RTL");
                }                
                else if(CheckTVProgram(programName, "vox"))
                {
                     SenderAusgabe("Check TV Station: Vox");
                } 
                else if(CheckTVProgram(programName, "tele5"))
                {
                     SenderAusgabe("Check TV Station: Tele5");
                }              
                else if(CheckTVProgram(programName, "pro7"))
                {
                     SenderAusgabe("Check TV Station: Pro7");
                }              
                else if(CheckTVProgram(programName, "k1"))
                {
                    SenderAusgabe("Check TV Station: Kabel1");
                }
                else if(CheckTVProgram(programName, "3sat"))
                {
                    SenderAusgabe("Check TV Station: 3Sat");
                }
                 else if(CheckTVProgram(programName, "arte"))
                {
                    SenderAusgabe("Check TV Station: Arte");
                }
                //else if(CheckTVProgram(programName, "rtl-n"))
                //{
                  //  SenderAusgabe("Check  TV Station:  Nitro");
                //}
                else if(CheckTVProgram(programName, "2neo"))
                {
                    SenderAusgabe("Check  TV Station:  ZDF Neo");
                }
                else
                {
                    Console.ForegroundColor =ConsoleColor.DarkRed;
                    Console.WriteLine("Das Programm wurde nicht gefunden");
                    Console.ForegroundColor =ConsoleColor.White;
                    break;
                }
 
                try
                {
                    using(System.Net.WebClient client = new System.Net.WebClient())  // Connection
                    {
                        foreach(string myfilm in myFilms) // Class Films / TV station as property
                        {
                            string htmlTVStation = client.DownloadString(url);
                            
                            if(htmlTVStation.Contains(myfilm))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Der Film: {0} kommt", myfilm );
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }
                        
                    }
                }
                catch(System.Net.WebException) // Class Connection
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The Host {0} \nisn't available. Check your internet connection", url);
                    Console.ForegroundColor = gray;
                    Environment.Exit(1);
                }
                catch(Exception Ex) //Class Connection
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Another problem occured.");
                    Console.WriteLine("Fehlerbeschreibung: {0} ", Ex.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;

                }

                /***
                using(System.Net.WebClient htastring = new System.Net.WebClient())
                {
     
                    string webseite = htastring.DownloadString("https://www.tvspielfilm.de/tv-programm/sendungen/?page=1&order=time&date=2021-07-03&cat%5B%5D=SP&time=day&channel=2NEO");
                    string dirctory = @"C:\WebParser";
                    string filename = dirctory + "\\inhaltwebseite.html";
                    string destination = @"Ccopy_inhaltwebseite.html";
                    File.AppendAllText(filename, webseite);
                    System.IO.File.Copy(filename, destination);
                }
                ***/
            }

        }
    }
}

