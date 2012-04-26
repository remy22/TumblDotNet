using System;
using System.Collections.Generic;
using System.Linq;
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

        public string GetAvatarUrl(string blogHostName, int size=64)
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


        //TODO: Work out why this returns unauthenticated!
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

        /*
        public void GetBlogQueue(string blogHostName)
        {
            if (String.IsNullOrEmpty(blogHostName))
                throw new ArgumentException("invalid blog host name");

            var client = GetRestClient();

            var resource = string.Format("/blog/{0}/posts/queue", blogHostName);

            var request = new RestRequest(resource, Method.GET);
            
            var response = client.Execute(request);

            Console.WriteLine(response.StatusCode);
        }*/

        //TODO: Write some overloads for this
        public List<TumblrPost> GetBlogPosts(string blogHostName, PostFormat format = PostFormat.Html, string filteredTag = "", bool includeReblogs = false, bool includeNotes = false, int offset = 0, int limit = 20, PostType postType = PostType.All )
        {
            if (String.IsNullOrEmpty(blogHostName))
                throw new ArgumentException("invalid blog host name");

            var client = GetUnAuthenticatedRestClient();

            string typeUrl = "";

            switch (postType)
            {
                    case PostType.Text:
                        typeUrl = "/text";
                        break;
                    case PostType.Quote:
                        typeUrl = "/quote";
                        break;
                    case PostType.Link:
                        typeUrl = "/link";
                        break;
                    case PostType.Audio:
                        typeUrl = "/audio";
                        break;
                    case PostType.Video:
                        typeUrl = "/video";
                        break;
                    case PostType.Answer:
                        typeUrl = "/answer";
                        break;
                    case PostType.Photo:
                        typeUrl = "/photo";
                        break;
                    default:
                        typeUrl = "";
                        break;

            }
                
            //post type on end of url for this one
            var resource = string.Format("/blog/{0}/posts{1}", blogHostName, typeUrl);

            var request = new RestRequest(resource);
            
            request.AddParameter(new Parameter() { Name = "api_key", Type = ParameterType.GetOrPost, Value = ConsumerKey });
            request.AddParameter("limit", limit);
            request.AddParameter("offset", offset);

            if(filteredTag != "")
                request.AddParameter("tag", filteredTag);

            request.AddParameter("reblog_info", includeReblogs);
            request.AddParameter("notes_info", includeNotes);

            var response = client.Execute<TumblrResponse<TumblrPostsResponse>>(request);

            return ConvertPosts(response.Data.Response.Posts);
            
        }


        #region post converter

        public static List<TumblrPost> ConvertPosts(List<TumblrPostResponse> posts)
        {
            List<TumblrPost> convertedPosts = new List<TumblrPost>();

            foreach (var post in posts)
            {
                switch (post.Type)
                {
                    case PostType.Text:
                        convertedPosts.Add(post.ToTextPost());
                        break;
                    case PostType.Photo:
                        convertedPosts.Add(post.ToPhotoPost());
                        break;
                    case PostType.Quote:
                        convertedPosts.Add(post.ToQuotePost());
                        break;
                    case PostType.Link:
                        convertedPosts.Add(post.ToLinkPost());
                        break;
                    case PostType.Chat:
                        convertedPosts.Add(post.ToChatPost());
                        break;
                    case PostType.Audio:
                        convertedPosts.Add(post.ToAudioPost());
                        break;
                    case PostType.Video:
                        convertedPosts.Add(post.ToVideoPost());
                        break;
                    case PostType.Answer:
                        convertedPosts.Add(post.ToAnswerPost());
                        break;
                }

            }

            return convertedPosts;
        }

        #endregion

        private List<TumblrPost> GetPrivatePosts(string blogHostName, string postType)
        {
            if (String.IsNullOrEmpty(blogHostName))
                throw new ArgumentException("invalid blog host name");

            var client = GetRestClient();

            //post type on end of url for this one
            var resource = string.Format("/blog/{0}/posts/{1}", blogHostName, postType);

            var request = new RestRequest(resource);

            var response = client.Execute<TumblrResponse<TumblrPostsResponse>>(request);

            return ConvertPosts(response.Data.Response.Posts);
        }

        public List<TumblrPost> GetQueue(string blogHostName)
        {
            return GetPrivatePosts(blogHostName, "queue");
        }

        public List<TumblrPost> GetDrafts(string blogHostName)
        {
            return GetPrivatePosts(blogHostName, "draft");
        }

        public List<TumblrPost> GetSubmissions(string blogHostName)
        {
            return GetPrivatePosts(blogHostName, "submission");
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

    

}
