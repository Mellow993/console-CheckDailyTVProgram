using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser
{
    struct Check
    {
        public void CheckHost()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Check connection to host");
            Console.ResetColor();
        }

    }

    struct Errors
    {
        public void NoInternetConnection()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No Internet Connection");
            Console.ResetColor();
        }

        public void HostIsOutOfReach()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No Internet Connection");
            Console.ResetColor();
        }
    }


    struct Info
    {
        public void MovieFound(string movie)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The following movies will be broadcasted tonight {0}", movie);
            Console.ResetColor();
        }

        public void NoMoviesHaveBeenFound(string movie)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("The following movies will be broadcasted tonight {0}", movie);
            Console.ResetColor();
        }

    }
}
