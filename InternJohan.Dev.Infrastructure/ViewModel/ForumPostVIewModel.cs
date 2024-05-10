using InternJohan.Dev.Infrastructure.Models;
using System;

namespace InternJohan.Dev.Infrastructure.ViewModel
{
    public class ForumPostViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public User AuthorName { get; set; }
        public string FormattedCreated { get; set; }

        public ForumPostViewModel(ForumPost forumPost)
        {
            Title = forumPost.Title;
            Description = forumPost.Description;
            AuthorName = forumPost.Author; 
            FormattedCreated = forumPost.Created.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
