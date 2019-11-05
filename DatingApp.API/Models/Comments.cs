using System;

namespace DatingApp.API.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int ProfileId { get; set; }
        public virtual Profiles Profile { get; set; }
        public int PostId { get; set; }
        public virtual Posts Post { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}