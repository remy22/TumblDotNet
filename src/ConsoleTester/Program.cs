using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TumblDotNet;

namespace ConsoleTester
{
    class Program
    {
        public const string TumblrConsumerKey = "t1ecRlHNgK9tfwRojPUwHrZwxyYUvp8LngbmZipinr3Yd7gbok";
        public const string TumblrConsumerSecret = "bVKDKK38CobZXKlJhZF2AObh8cvkFeuOp0sAMW8LJtvNq99mBW";

        static void Main(string[] args)
        {

            var client = new TumblrClient(TumblrConsumerKey, TumblrConsumerSecret);

            var token = client.GetRequestToken();
            
            Console.ReadLine();
        }
    }
}
