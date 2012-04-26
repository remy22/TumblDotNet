using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using TumblDotNet.Authentication;

namespace TumblDotNet
{
    public static class TumblrAuthenticator
    {
        private const string REQUEST_TOKEN_URL = "http://www.tumblr.com/oauth/request_token";
        private const string AUTHORIZE_URL = "http://www.tumblr.com/oauth/authorize";
        private const string ACCESS_TOKEN_URL = "http://www.tumblr.com/oauth/access_token";

        #region OAuth Setup

        public static Token GetRequestToken(string consumerKey, string consumerSecret)
        {
            var client = new RestClient();
            client.Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret);
            var request = new RestRequest(REQUEST_TOKEN_URL, Method.GET);
            var response = client.Execute(request);
            return GetTokenFromParams(response.Content);
        }

        public static String BuildAuthorizeUrl(Token requestToken)
        {
            return AUTHORIZE_URL + "?oauth_token=" + requestToken.UserToken;
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

            return new Token(token, secret);
        }

        public static Token GetAccessToken(string consumerKey, string consumerSecret, Token requestToken, string verifier)
        {
            var client = new RestClient();

            if (String.IsNullOrEmpty(verifier))
                throw new ArgumentException("Invalid verifier");

            client.Authenticator = OAuth1Authenticator.ForAccessToken(consumerKey, consumerSecret, requestToken.UserToken, requestToken.UserSecret, verifier);


            var request = new RestRequest(ACCESS_TOKEN_URL, Method.GET);
            var response = client.Execute(request);

            return GetTokenFromParams(response.Content);

        }

        #endregion
    }
}
