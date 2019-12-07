using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class ProfileLikedPostToReturnDto
    {
        public int PostId { get; set; }
        public string Reaction { get; set; }
        public PostCreatedToReturnDto Post { get; set; }
    }
}