namespace TumblDotNet.Models
{
    public class TumblrAnswerPost : TumblrPost
    {
        /// <summary>
        /// The blog name of the user asking the question
        /// </summary>
        public string Asking_Name { get; set; }

        /// <summary>
        /// The blog url of the user asking the question
        /// </summary>
        public string Asking_Url { get; set; }

        /// <summary>
        /// The question being asked
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// The answer given
        /// </summary>
        public string Answer { get; set; }
    }
}