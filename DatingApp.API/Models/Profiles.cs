using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.API.Models
{
    public class Profiles
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ProfileName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string ProfileDescription { get; set; }
        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesRecieved { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Postlike> PostLikes { get; set; }
    }
}