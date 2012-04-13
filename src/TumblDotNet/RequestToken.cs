namespace TumblDotNet
{
    public class Token
    {
        public Token(string userToken, string userSecret)
        {
            UserToken = userToken;
            UserSecret = userSecret;
        }

        /// <summary>
        /// The OAuth user token
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// The OAuth user secret
        /// </summary>
        public string UserSecret { get; set; }
    }
}