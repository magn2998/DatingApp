using System;

namespace DatingApp.API.Dtos
{
    public class PostForCreationDto
    {
        public int ProfileId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Section { get; set; }
        public DateTime Created { get; set; }

        public PostForCreationDto() 
        {
            Created = DateTime.Now;
        }
    }
}