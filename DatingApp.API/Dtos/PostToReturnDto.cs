using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class PostToReturnDto
    {
        public int Id { get; set; }
        public string ProfileProfileName { get; set; }
        public int ProfileId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Section { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Comments> PostComments { get; set; }
        public virtual ICollection<PostlikeToReturnListDto> PostLikers { get; set; }
        public int likesCounter { get; set; }
        public int dislikeCounter { get; set; }
        

    }
}