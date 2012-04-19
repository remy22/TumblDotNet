namespace TumblDotNet.Models
{
    public class TumblrFollower
    {
        /// <summary>
        /// The user's name on tumblr
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL of the user's primary blog
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The time of the user's most recent post
        /// Seconds since the epoch
        /// </summary>
        public long Updated { get; set; }
    }
}