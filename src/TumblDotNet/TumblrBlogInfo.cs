namespace TumblDotNet
{
    /// <summary>
    /// Tumblr blog info returned from a generic blog info request
    /// </summary>
    public class TumblrBlogInfo
    {

        /// <summary>
        /// The display title of the blog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The total number of posts to this blog
        /// </summary>
        public int Posts { get; set; }

        /// <summary>
        /// The short blog name that appears before tumblr.com in a standard blog
        /// hostname and before the domain in a custom blog hostname
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Url of the blog
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The time of the most recent post, in seconds since epoch
        /// </summary>
        public long Updated { get; set; }

        /// <summary>
        /// The description of the blog
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indicates whether the blog allows questions
        /// </summary>
        public bool Ask { get; set; }

        /// <summary>
        /// Indicates whether the blog allows anon questions
        /// </summary>
        public bool Ask_anon { get; set; }

        /// <summary>
        /// The number of likes for this user
        /// only returned if this is that users primary blog
        /// </summary>
        public int Likes { get; set; }
    }

}