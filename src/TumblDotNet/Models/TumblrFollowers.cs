using System.Collections.Generic;

namespace TumblDotNet.Models
{
    public class TumblrFollowers
    {
        /// <summary>
        /// The number of users currently following the blog
        /// </summary>
        public int Total_Users { get; set; }

        /// <summary>
        /// A list of follower details
        /// </summary>
        public List<TumblrFollower> Users { get; set; }
            
    }
}