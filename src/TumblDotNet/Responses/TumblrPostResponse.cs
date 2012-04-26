using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TumblDotNet.Models;

namespace TumblDotNet.Responses
{
    public class TumblrPostResponse
    {

        #region Conversion methods

        private void FillTumblrPostFields(TumblrPost post)
        {
            post.Blog_Name = Blog_Name;
            post.Id = Id;
            post.Post_Url = Post_Url;
            post.Type = Type;
            post.TimeStamp = TimeStamp;
            post.Date = Date;
            post.Format = Format;
            post.Reblog_Key = Reblog_Key;
            post.Tags = Tags;
            post.Bookmarklet = Bookmarklet;
            post.Mobile = Mobile;
            post.Source_Title = Source_Title;
            post.Source_Url = Source_Url;
        }

        public TumblrTextPost ToTextPost()
        {
            var ret = new TumblrTextPost();
            FillTumblrPostFields(ret);

            //fill specific fields
            ret.Title = Title;
            ret.Body = Body;

            return ret;
        }

        public TumblrPhotoPost ToPhotoPost()
        {
            var ret = new TumblrPhotoPost();
            FillTumblrPostFields(ret);

            //fill specific fields
            ret.Photos = Photos;
            ret.Caption = Caption;
            ret.Width = Width;
            ret.Height = Height;

            return ret;
        }

        public TumblrQuotePost ToQuotePost()
        {
            var ret = new TumblrQuotePost();
            FillTumblrPostFields(ret);

            //fill specific fields
            ret.Text = Text;
            ret.Source = Source;

            return ret;
        }

        public TumblrLinkPost ToLinkPost()
        {
            var ret = new TumblrLinkPost();
            FillTumblrPostFields(ret);

            //fill specific fields
            ret.Title = Title;
            ret.Url = Url;
            ret.Description = Description;

            return ret;
        }

        public TumblrChatPost ToChatPost()
        {
            var ret = new TumblrChatPost();
            FillTumblrPostFields(ret);

            //fill specific fields
            ret.Title = Title;
            ret.Body = Body;
            ret.Dialogue = Dialogue;

            return ret;
        }

        public TumblrAudioPost ToAudioPost()
        {
            var ret = new TumblrAudioPost();
            FillTumblrPostFields(ret);

            //fill specific fields
            ret.Caption = Caption;
            ret.Player = Player;
            ret.Plays = Plays;
            ret.Album_Art = Album_Art;
            ret.Artist = Artist;
            ret.Track_Name = Track_Name;
            ret.Track_Number = Track_Number;
            ret.Year = Year;

            return ret;
        }

        public TumblrVideoPost ToVideoPost()
        {
            var ret = new TumblrVideoPost();
            FillTumblrPostFields(ret);

            //fill specific fields
            ret.Caption = Caption;

            //TODO: Fix this
            ret.Player = new List<EmbeddableObject>();

            return ret;
        }

        public TumblrAnswerPost ToAnswerPost()
        {
            var ret = new TumblrAnswerPost();
            FillTumblrPostFields(ret);

            //fill specific fields
            ret.Asking_Name = Asking_Name;
            ret.Asking_Url = Asking_Url;
            ret.Question = Question;
            ret.Answer = Answer;

            return ret;
        }


        #endregion

        #region Shared stuff
        public string Blog_Name { get; set; }
        public long Id { get; set; }
        public string Post_Url { get; set; }
        public PostType Type { get; set; }
        public long TimeStamp { get; set; }

        //TODO: parse this into an actual datetime somehow?
        public string Date { get; set; }

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

        #endregion

        #region semi shared stuff

        /// <summary>
        /// The user-supplied caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// The optiional title of the post
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The full post body
        /// </summary>
        public string Body { get; set; }

        #endregion

        #region Photo post stuff

        /// <summary>
        /// The list of photo objects, may be multiple for photosets
        /// </summary>
        public List<Photo> Photos { get; set; }

        /// <summary>
        /// The width of the photo or photoset
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the photo or photoset
        /// </summary>
        public int Height { get; set; }

        #endregion

        #region Quote post stuff
        
        /// <summary>
        /// The text of the quote
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Full HTML source for the quote
        /// </summary>
        public string Source { get; set; }

        #endregion

        #region Link post stuff

        /// <summary>
        /// The link
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// User supplied description for the link
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Chat post stuff

        /// <summary>
        /// An array of line objects for the entire dialogue
        /// </summary>
        public List<DialogueLine> Dialogue { get; set; } 

        #endregion

        #region Audio post stuff

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

        #endregion

        #region Video post stuff

        //TODO: Handle video posts properly!!
        //this clashes with the player property for an audio post which is a different type
        /*public List<EmbeddableObject> Player { get; set; }*/

        #endregion

        #region Answer post stuff

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

        #endregion

    }
}
