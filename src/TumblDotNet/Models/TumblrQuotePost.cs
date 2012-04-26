namespace TumblDotNet.Models
{
    public class TumblrQuotePost : TumblrPost
    {
        /// <summary>
        /// The text of the quote
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Full HTML source for the quote
        /// </summary>
        public string Source { get; set; }
    }
}