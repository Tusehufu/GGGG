using InternJohan.Dev.Infrastructure.Models;
using System;

namespace InternJohan.Dev.Infrastructure.ViewModel
{
    public class ForumPostReplyViewModel
    {
        public string Content { get; set; }
        public User AuthorName { get; set; }
        public string FormattedCreated { get; set; }

        // Konstruktor för att mappa ForumPostReply till ForumPostReplyViewModel
        public ForumPostReplyViewModel(ForumPostReply reply)
        {
            Content = reply.Content;
            AuthorName = reply.Author; 
            FormattedCreated = reply.Created.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
