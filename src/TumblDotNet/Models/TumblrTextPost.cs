namespace TumblDotNet.Models
{
    public class TumblrTextPost : TumblrPost
    {
        /// <summary>
        /// The optiional title of the post
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// The full post body
        /// </summary>
        public string Body { get; set; }
    }
}