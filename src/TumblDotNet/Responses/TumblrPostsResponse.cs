using System.Collections.Generic;
using TumblDotNet.Models;

namespace TumblDotNet.Responses
{
    public class TumblrPostsResponse
    {
        public TumblrBlogInfo Blog { get; set; }

        public List<TumblrPostResponse> Posts { get; set; }

        public long TotalPosts { get; set; }
    }
}