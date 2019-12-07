using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext _context;

        public ProfileRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Profiles> Login(string username, string password)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Username == username);

            if(profile == null){
                return null;
            }
            if(!VerifyPasswordHash(password, profile.PasswordHash, profile.PasswordSalt)){
                return null;
            }

            return profile;

        }

        public async Task<Profiles> Register(Profiles profile, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            profile.PasswordHash = passwordHash;
            profile.PasswordSalt = passwordSalt;

            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            return profile;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++){
                    if (computedHash[i] != passwordHash[i]) {return false;}
                }
            }
            return true;
        }

        public async Task<bool> userExists(string username)
        {
            if (await _context.Profiles.AnyAsync(x => x.Username == username)){
                return true;
            }
            return false;
        }

        public async Task<Profiles> getProfile(int profileId) 
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == profileId);
            profile.Posts = profile.Posts.OrderByDescending(d => d.Created).ToList();
             
            return profile;
        }

        public async Task<Posts> GetPost(int Id)
        {
            return await _context.Posts.FirstOrDefaultAsync(m => m.Id == Id);
        }

        public IEnumerable<Posts> GetPostsBySection(string section) 
        {
            var posts = _context.Posts.AsQueryable();

            posts = posts.Where(p => p.Section == section);

            posts = posts.OrderByDescending(d => d.Created);

            return posts;
        }

        public async Task<Postlike> GetLike(int profileId, int postId)
        {
            return await _context.Postlike.FirstOrDefaultAsync(u => u.LikerId == profileId && u.PostId == postId);
        }

        public ICollection<Postlike> GetLikedPosts(int profileId)
        {
            var profile =  _context.Profiles.FirstOrDefault(x => x.Id == profileId);

            return profile.PostLikes;

        }

    }
}