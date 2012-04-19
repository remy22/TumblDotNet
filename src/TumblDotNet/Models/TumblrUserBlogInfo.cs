namespace TumblDotNet.Models
{
    /// <summary>
    /// Tumblr blog info returned for a users blog listing
    /// </summary>
    public class TumblrUserBlogInfo
    {

        /// <summary>
        /// The short name of the blog
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL of the blog
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The title of the blog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Is this the users primary blog?
        /// </summary>
        public bool Primary { get; set; }

        /// <summary>
        /// Total count of followers for this blog
        /// </summary>
        public int Followers { get; set; }

        /// <summary>
        /// Indicates if posts are auto-tweeted
        /// auto
        /// yes
        /// no
        /// </summary>
        public string Tweet { get; set; }


        /// <summary>
        /// Is the user an admin of this blog?
        /// </summary>
        public bool Admin { get; set; }

        /// <summary>
        /// Number of posts in the queue
        /// </summary>
        public int Queue { get; set; }

        /// <summary>
        /// Number of posts on this blog
        /// </summary>
        public int Posts { get; set; }

        /// <summary>
        /// Blog type ... public or ?
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Is this blog facebook enabled?
        /// </summary>
        public string Facebook_opengraph_enabled { get; set; }

    }
}