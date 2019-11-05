using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IProfileRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<Profiles> Register (Profiles profile, string password);
        Task<Profiles> Login (string username, string password);
        Task<bool> userExists (string username);
        Task<Profiles> getProfile (int profileId);
        Task<Posts> GetPost(int Id);
        IEnumerable<Posts> GetPostsBySection(string section);
        Task<Postlike> GetLike(int userId, int postId);
        ICollection<Postlike> GetLikedPosts(int profileId);

    }
}