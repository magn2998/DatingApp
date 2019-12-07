using System;
using System.Collections.Generic;

namespace DatingApp.API.Dtos
{
    public class ProfileToReturnDto
    {
        public string Username { get; set; }

        public string ProfileName { get; set; }

        public DateTime LastActive { get; set; }
        public virtual ICollection<PostCreatedToReturnDto> Posts { get; set; }
        public string ProfileDescription { get; set; }

    }
}