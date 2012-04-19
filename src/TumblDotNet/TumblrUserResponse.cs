namespace TumblDotNet
{

    public class TumblrResponse<TResponse>
    {
        public TumblrMetaData Meta { get; set; }
        public TResponse Response { get; set; }
    }

    public class TumblrUserResponse
    {
        public TumblrUserInfo User { get; set; }
    }

    public class TumblrBlogResponse
    {
        public TumblrBlogInfo Blog { get; set; }
    }

    public class TumblrMetaData
    {
        public int Status { get; set; }
        public string Msg { get; set; }
    }
}