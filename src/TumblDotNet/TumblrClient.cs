using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using TumblDotNet.Authentication;
using TumblDotNet.Models;
using TumblDotNet.Responses;

namespace TumblDotNet
{
    public class TumblrClient
    {
        private const string API_BASE = "http://api.tumblr.com/v2";
        private const string REQUEST_TOKEN_URL = "http://www.tumblr.com/oauth/request_token";
        private const string AUTHORIZE_URL = "http://www.tumblr.com/oauth/authorize";
        private const string ACCESS_TOKEN_URL = "http://www.tumblr.com/oauth/access_token";

        #region Tokens

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }

        public string UserToken { get; set; }
        public string UserSecret { get; set; }

        #endregion

        #region construction

        public TumblrClient(string consumerKey, string consumerSecret)
        {
            if (string.IsNullOrEmpty(consumerKey))
                throw new ArgumentException("consumerKey");

            if (string.IsNullOrEmpty(consumerSecret))
                throw new ArgumentException("consumerSecret");

            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }

        public TumblrClient(string consumerKey, string consumerSecret, string userToken, string userSecret)
        {
            if(string.IsNullOrEmpty(consumerKey))
                throw new ArgumentException("consumerKey");

            if (string.IsNullOrEmpty(consumerSecret))
                throw new ArgumentException("consumerSecret");

            if (string.IsNullOrEmpty(userToken))
                throw new ArgumentException("userToken");

            if (string.IsNullOrEmpty(userSecret))
                throw new ArgumentException("userSecret");

            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            UserToken = userToken;
            UserSecret = userSecret;
        }

        public TumblrClient(string consumerKey, string consumerSecret, Token accessToken) : this(consumerKey, consumerSecret, accessToken.UserToken, accessToken.UserSecret)
        {}
        
        #endregion

        #region OAuth Setup

        public static Token GetRequestToken(string consumerKey, string consumerSecret)
        {
            var client = new RestClient();
            client.Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret);
            var request = new RestRequest(REQUEST_TOKEN_URL,Method.GET);
            var response = client.Execute(request);

            return GetTokenFromParams(response.Content);
        }

        public static String BuildAuthorizeUrl(Token requestToken)
        {
            return "http://www.tumblr.com/oauth/authorize?oauth_token=" + requestToken.UserToken;
        }

        //TODO: Un-steal this
        private static Token GetTokenFromParams(string urlParams)
        {

            string secret = "";
            string token = "";
            //TODO - Make this not suck
            var parameters = urlParams.Split('&');

            foreach (var parameter in parameters)
            {
                if (parameter.Split('=')[0] == "oauth_token_secret")
                {
                    secret = parameter.Split('=')[1];
                }
                else if (parameter.Split('=')[0] == "oauth_token")
                {
                    token = parameter.Split('=')[1];
                }
            }

            return new Token(token,secret);
        }

        public static Token GetAccessToken(string consumerKey, string consumerSecret, Token requestToken, string verifier)
        {
            var client = new RestClient();

            if(string.IsNullOrEmpty(verifier))
                throw new ArgumentException("Invalid verifier");

            client.Authenticator = OAuth1Authenticator.ForAccessToken(consumerKey, consumerSecret, requestToken.UserToken, requestToken.UserSecret,verifier);
            

            var request = new RestRequest(ACCESS_TOKEN_URL, Method.GET);
            request.AddParameter("oauth_verifier", verifier);
            var response = client.Execute(request);

            return GetTokenFromParams(response.Content);

        }

        #endregion

        #region user methods

        public TumblrUserInfo GetUserInfo()
        {

            var client = GetRestClient();
            
            var request = new RestRequest("/user/info/", Method.POST);
            
            var userInfoResponse = client.Execute<TumblrResponse<TumblrUserResponse>>(request);

            return userInfoResponse.Data.Response.User;

        }

        #endregion

        #region blog methods

        public TumblrBlogInfo GetBlogInfo(string blogHostName)
        {

            if(String.IsNullOrEmpty(blogHostName))
                throw new ArgumentException("invalid blog host name");

            var client = GetUnAuthenticatedRestClient();

            var resource = string.Format("/blog/{0}/info",blogHostName);

            var request = new RestRequest(resource);
            request.AddParameter(new Parameter(){Name = "api_key",Type = ParameterType.GetOrPost, Value=ConsumerKey});
            
            var response = client.Execute<TumblrResponse<TumblrBlogResponse>>(request);

            return response.Data.Response.Blog;
        }


        public string GetAvatarUrl(string blogHostName, int size = 64)
        {
            List<int> supportedSizes = new List<int>() {16, 24, 30, 40, 48, 64, 96, 128, 512};
            if (!supportedSizes.Contains(size))
            {
                size = 64;
            }

            if (String.IsNullOrEmpty(blogHostName))
                throw new ArgumentException("invalid blog host name");

            var resource = string.Format("{0}/blog/{1}/avatar/{2}", API_BASE, blogHostName, size);

            return resource;
        }

        public TumblrFollowers GetBlogFollowers(string blogHostName, int limit=20, int offset = 0)
        {

            if (String.IsNullOrEmpty(blogHostName))
                throw new ArgumentException("invalid blog host name");

            var client = GetRestClient();

            var resource = string.Format("/blog/{0}/followers", blogHostName);

            var request = new RestRequest(resource,Method.GET);
            request.AddParameter("limit", limit);
            request.AddParameter("offset", offset);

            var response = client.Execute<TumblrResponse<TumblrFollowersResponse>>(request);

            var ret = new TumblrFollowers();
            ret.Total_Users = response.Data.Response.Total_Users;
            ret.Users = response.Data.Response.Users;

            return ret;
        }

        public void GetBlogQueue(string blogHostName)
        {
            if (String.IsNullOrEmpty(blogHostName))
                throw new ArgumentException("invalid blog host name");

            var client = GetRestClient();

            var resource = string.Format("/blog/{0}/posts/queue", blogHostName);

            var request = new RestRequest(resource, Method.GET);
            
            var response = client.Execute(request);

            Console.WriteLine(response.StatusCode);
        }


        #endregion

        #region Rest Setup

        private RestClient GetRestClient()
        {
            RestClient client = new RestClient(API_BASE);
            client.ClearHandlers();
            client.AddHandler("*", new JsonDeserializer());
            
            client.Authenticator = GetAuthenticator();
            return client;
        }

        private RestClient GetUnAuthenticatedRestClient()
        {
            RestClient client = new RestClient(API_BASE);
            client.ClearHandlers();
            client.AddHandler("*", new JsonDeserializer());
            return client;
        }

        private OAuth1Authenticator GetAuthenticator()
        {
            return OAuth1Authenticator.ForAccessToken(ConsumerKey, ConsumerSecret, UserToken, UserSecret);
        }

        #endregion

    }

    public class OAuthRequestTokenResponse
    {
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }
        public bool callback_confirmed { get; set; }
    }
}
