using System;
using System.Collections.Generic;

namespace TumblDotNet.Models
{
    public class TumblrMetaData
    {
        public int Status { get; set; }
        public string Msg { get; set; }
    }

    /// <summary>
    /// Generic post return 
    /// </summary>
    public class TumblrPost
    {
        public string Blog_Name { get; set; }
        public long Id { get; set; }
        public string Post_Url { get; set; }
        public PostType Type { get; set; }
        public long TimeStamp { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// The post format - HTML or Markdown
        /// </summary>
        public String Format { get; set; }

        public string Reblog_Key { get; set; }

        public List<string> Tags { get; set; }

        public bool Bookmarklet { get; set; }

        public bool Mobile { get; set; }

        public string Source_Url { get; set; }

        public string Source_Title { get; set; }
    }
}