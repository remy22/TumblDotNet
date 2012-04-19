using System;
using System.Collections.Generic;

namespace TumblDotNet.Models
{
    public class TumblrUserInfo
    {

        /// <summary>
        /// The users tumblr short name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The number of blogs the user is following
        /// </summary>
        public int Following { get; set; }

        /// <summary>
        /// The default posting format
        /// html
        /// markdown
        /// raw
        /// </summary>
        public String Default_post_format { get; set; }

        /// <summary>
        /// The total count of the user's likes
        /// </summary>
        public string Likes { get; set; }

        /// <summary>
        /// Blogs which the user has permission to post to
        /// </summary>
        public List<TumblrUserBlogInfo> Blogs { get; set; }

        
    }
}