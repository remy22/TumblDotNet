namespace TumblDotNet.Models
{
    public class TumblrAudioPost : TumblrPost
    {
        /// <summary>
        /// The user-supplied caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// The HTML for embedding the audio player
        /// </summary>
        public string Player { get; set; }

        /// <summary>
        /// The number of times the audio post has been played
        /// </summary>
        public long Plays { get; set; }

        /// <summary>
        /// The location of the audio file's ID3 album art image
        /// </summary>
        public string Album_Art { get; set; }

        /// <summary>
        /// The audio file's ID3 artist value
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// The audio file's ID3 Track_Name value
        /// </summary>
        public string Track_Name { get; set; }

        /// <summary>
        /// The audio file's ID3 Track_Number value
        /// </summary>
        public string Track_Number { get; set; }

        /// <summary>
        /// The audio file's ID3 Year value
        /// </summary>
        public string Year { get; set; }

    }
}