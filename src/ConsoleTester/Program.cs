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
        public const string UserToken = "oeJhgY9ynQ8tnSfsPPKOzfXPms2vwlHyVEVCy93m1gx0DUjDoP";
        public const string UserSecret = "5tHDrmQ8wrspI4G0vptalehMS8oZQy5y8kcvwXzqWtQ7LE8tyC";

        static void Main(string[] args)
        {

#region OAuth Authentication
            /*var reqToken = TumblrClient.GetRequestToken(TumblrConsumerKey,TumblrConsumerSecret);

            var redirectUrl = TumblrClient.BuildAuthorizeUrl(reqToken);

            Console.WriteLine(redirectUrl);
            
            Console.Write("Enter verifier please:");
            var verifier = Console.ReadLine();

            var accessToken = TumblrClient.GetAccessToken(TumblrConsumerKey,TumblrConsumerSecret,reqToken, verifier);
            */
#endregion

            var client = new TumblrClient(TumblrConsumerKey,TumblrConsumerSecret,UserToken,UserSecret);

            //try a request 
            var userInfo = client.GetUserInfo();
            Console.WriteLine(userInfo.Blogs[0].Name);

            var blogInfo = client.GetBlogInfo("rcknight.tumblr.com");
            Console.ReadLine();
        }
    }
}
