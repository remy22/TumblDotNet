using System.Collections.Generic;

namespace TumblDotNet.Models
{
    public class Photo
    {
        /// <summary>
        /// User supplied caption for the photo (only for photosets)
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// The list of avaliable photo sizes
        /// </summary>
        public List<PhotoSize> Alt_Sizes { get; set; }
    }
}