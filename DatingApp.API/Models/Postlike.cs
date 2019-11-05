namespace DatingApp.API.Models
{
    public class Postlike
    {
        public int LikerId { get; set; }
        public int PostId { get; set; }
        public string Reaction { get; set; }
        public virtual Profiles Liker { get; set; }
        public virtual Posts Post { get; set; }
    }
}