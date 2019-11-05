using System;

namespace DatingApp.API.Dtos
{
    public class ProfileToReturnDto
    {
        public string Username { get; set; }

        public string ProfileName { get; set; }
        public DateTime LastActive { get; set; }

    }
}