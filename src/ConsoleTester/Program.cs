﻿using System;
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
        public const string UserToken = "sYM9rfA8IbyJdkYyap8DQmhRlq46pupJSNJT0nciC9mFQ3nMjQ";
        public const string UserSecret = "iAwgAi7okUtaX24i1H1eiVtWKkKqCU4kdAjDAaFcKcBSBNh3sb";

        static void Main(string[] args)
        {

#region OAuth Authentication
            /*var reqToken = TumblrClient.GetRequestToken(TumblrConsumerKey,TumblrConsumerSecret);

            var redirectUrl = TumblrClient.BuildAuthorizeUrl(reqToken);

            Console.WriteLine(redirectUrl);
            
            Console.Write("Enter verifier please:");
            var verifier = Console.ReadLine();

            var accessToken = TumblrClient.GetAccessToken(TumblrConsumerKey,TumblrConsumerSecret,reqToken, verifier);
            Console.WriteLine("User secret: " + accessToken.UserSecret);
            Console.WriteLine("User token: " + accessToken.UserToken);*/
#endregion

            var client = new TumblrClient(TumblrConsumerKey,TumblrConsumerSecret,UserToken,UserSecret);

            var followers = client.GetBlogFollowers("rcknight.tumblr.com", 0, 12);

            Console.WriteLine(followers.Total_Users);
            Console.WriteLine(followers.Users[0].Name);


            //try a request 
            var userInfo = client.GetUserInfo();
            Console.WriteLine(userInfo.Blogs[0].Name);

            var blogInfo = client.GetBlogInfo("danleech.com");

            Console.WriteLine(blogInfo.Title);

            var avatarUrl = client.GetAvatarUrl("danleech.com", 512);

            var posts = client.GetPosts("danleech.com");


            Console.WriteLine(avatarUrl);

            Console.ReadLine();
        }
    }
}
