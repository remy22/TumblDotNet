using TumblDotNet.Models;

namespace TumblDotNet.Responses
{
    public class TumblrResponse<TResponse>
    {
        public TumblrMetaData Meta { get; set; }
        public TResponse Response { get; set; }
    }
}