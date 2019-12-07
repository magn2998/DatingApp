using System;
using System.Collections.Generic;

namespace DatingApp.API.Dtos
{
    public class ProfileDetailedToReturn
    {
        public string ProfileName { get; set; }
        public DateTime Created { get; set; }
        public string ProfileDescription { get; set; }
        public virtual ICollection<ProfileLikedPostToReturnDto> PostLikes { get; set; }
        public virtual ICollection<PostCreatedToReturnDto> Posts { get; set; }

    }
}