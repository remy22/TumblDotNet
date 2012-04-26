using System.Collections.Generic;

namespace TumblDotNet.Models
{
    public class TumblrPhotoPost : TumblrPost
    {

        /// <summary>
        /// The list of photo objects, may be multiple for photosets
        /// </summary>
        public List<Photo> Photos { get; set; }

        /// <summary>
        /// The user-supplied caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// The width of the photo or photoset
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the photo or photoset
        /// </summary>
        public int Height { get; set; }

    }
}