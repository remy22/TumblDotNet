using System.Collections.Generic;

namespace TumblDotNet.Models
{
    public class TumblrVideoPost : TumblrPost
    {

        /// <summary>
        /// The user-supplied caption
        /// </summary>
        public string Caption { get; set; }

        public List<EmbeddableObject> Player { get; set; }

    }
}