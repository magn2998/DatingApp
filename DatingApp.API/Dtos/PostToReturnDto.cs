using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class PostToReturnDto
    {
        public int Id { get; set; }
        public string ProfileProfileName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Section { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Comments> PostComments { get; set; }
        public ICollection<PostlikeToReturnListDto> PostLikers { get; set; }
        

    }
}