using System;
using System.Collections.Generic;

namespace DatingApp.API.Dtos
{
    public class PostCreatedToReturnDto
    {
        public string Id { get; set; }

        public string ProfileProfileName { get; set; }
        public string ProfileId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Section { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<PostlikeToReturnListDto> PostLikers { get; set; }

    }
}