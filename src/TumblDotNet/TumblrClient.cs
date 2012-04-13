using System;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;

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

        #endregion

        #region OAuth Setup

        public Token GetRequestToken()
        {
            var client = new RestClient();
            client.Authenticator = OAuth1Authenticator.ForRequestToken(ConsumerKey, ConsumerSecret);
            var request = new RestRequest(REQUEST_TOKEN_URL,Method.GET);
            var response = client.Execute(request);

            return GetTokenFromParams(response.Content);
        }

        public String BuildAuthorizeUrl(Token requestToken)
        {
            return "http://www.tumblr.com/oauth/authorize?oauth_token=" + requestToken.UserToken;
        }

        //TODO: Un-steal this
        private Token GetTokenFromParams(string urlParams)
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

        #endregion

        #region Guard property for tokens

        private bool ReadyForUse
        {
            get
            {
                return (!string.IsNullOrEmpty(ConsumerKey) && !string.IsNullOrEmpty(ConsumerSecret) &&
                        !string.IsNullOrEmpty(UserToken) && !string.IsNullOrEmpty(UserSecret));
            }
        }

        #endregion

        #region user methods

        public TumblrUserInfo GetUserInfo()
        {

            var client = GetRestClient();
            
            var request = new RestRequest("/user/info/", Method.POST);
            request.RootElement = "response";

            var userInfoResponse = client.Execute<TumblrUserResponse>(request);

            return userInfoResponse.Data.User;

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
