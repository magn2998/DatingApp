using System;
using System.Collections.Generic;

namespace DatingApp.API.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int ProfileId { get; set; }
        public virtual Profiles Profile { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Section { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Comments> PostComments { get; set; }
        public virtual ICollection<Postlike> PostLikers { get; set; }
    }
}