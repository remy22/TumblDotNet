namespace TumblDotNet.Models
{
    public class TumblrLinkPost : TumblrPost
    {

        /// <summary>
        /// The title of the page the link points to
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The link
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// User supplied description for the link
        /// </summary>
        public string Description { get; set; }

    }
}