using System.Collections.Generic;

namespace TumblDotNet.Models
{
    public class TumblrChatPost : TumblrPost
    {

        /// <summary>
        /// The optional title of this post
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The full chat body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// An array of line objects for the entire dialogue
        /// </summary>
        public List<DialogueLine> Dialogue { get; set; } 

    }
}