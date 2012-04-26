namespace TumblDotNet.Responses
{
    public class OAuthRequestTokenResponse
    {
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }
        public bool callback_confirmed { get; set; }
    }
}