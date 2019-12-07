using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IProfileRepository _repo;
        private readonly IMapper _mapper;

        public PostsController(IProfileRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("section/{section}")]
        public IEnumerable<PostToReturnDto> getPostsInSection(string section)
        {
            var posts = _repo.GetPostsBySection(section);

            var postsToReturn = _mapper.Map<IEnumerable<PostToReturnDto>>(posts);

            int likesCounter_;
            int dislikeCounter_;

            foreach (var post in postsToReturn)
            {
                likesCounter_ = 0;
                dislikeCounter_ = 0;
                foreach (var postLike in post.PostLikers)
                {
                    if(postLike.Reaction == "like") likesCounter_++;
                    if(postLike.Reaction == "dislike") dislikeCounter_++;
                }
                post.dislikeCounter = dislikeCounter_;
                post.likesCounter = likesCounter_;
            }

            return postsToReturn;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<PostToReturnDto> GetSinglePost(int id) {
            var post = await _repo.GetPost(id);

            if(post == null) {return null; }

            var postToReturn = _mapper.Map<PostToReturnDto>(post);

            int likesCounter_ = 0;
            int dislikeCounter_ = 0;

            foreach (var action in postToReturn.PostLikers)
            {
                if(action.Reaction == "like") likesCounter_++;
                if(action.Reaction == "dislike") dislikeCounter_++;
            }
            postToReturn.likesCounter = likesCounter_;
            postToReturn.dislikeCounter = dislikeCounter_;

            return postToReturn;
        }

        [HttpGet("{profileId}/likedposts")]
        public async Task<IActionResult> GetLikedPosts(int profileId)
        {
            if (profileId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }
            var profileFromRepo = await _repo.getProfile(profileId);

            var profileLikedPosts = _mapper.Map<IEnumerable<ProfileLikedPostToReturnDto>>(profileFromRepo.PostLikes);

            return Ok(profileLikedPosts);
        }
        

        [HttpPost("{profileId}")]
        public async Task<IActionResult> CreatePost(int profileId, PostForCreationDto PostForCreation) 
        {
            if (profileId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }
            
            PostForCreation.ProfileId = profileId;
            
            var profile = await _repo.getProfile(profileId);

            if (profile == null) { return BadRequest("Could Not Find Profile"); }

            var post = _mapper.Map<Posts>(PostForCreation);

            _repo.Add(post);

            if(await _repo.SaveAll()) {
                var postToReturn = _mapper.Map<PostToReturnDto>(post);
                return Ok(postToReturn);
            }

            return BadRequest("Failed to upload Post");

        }

        [HttpPost("action/{profileId}/{actionId}/{id}")]
        public async Task<IActionResult> LikePost(int profileId, int id, int actionId)
        {
            if(actionId != 0 && actionId != 1) { return BadRequest(); }

            string action = actionId == 0 ? "dislike" : "like"; 

            if (profileId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }


            var PostFromRepo = await _repo.GetPost(id);
            var ProfileFromRepo = await _repo.getProfile(profileId);

            if (PostFromRepo == null || ProfileFromRepo == null) { return NotFound(); }

            var likeFromRepo = await _repo.GetLike(profileId, id);
            
            if (likeFromRepo == null) {
                var like = new Postlike {
                    PostId = id,
                    LikerId = profileId,
                    Reaction = action
                };
                _repo.Add(like);
            } else if (likeFromRepo.Reaction == action) {
                _repo.Delete(likeFromRepo);
            } else { 
                likeFromRepo.Reaction = action;
            }

            if(await _repo.SaveAll()) {
                return Ok();
            }

            return BadRequest("Failed to " + action + " Post");
            
        }

        [HttpDelete("{profileId}/delete/{postId}")]
        public async Task<IActionResult> DeletePost(int profileId, int postId) 
        {
            if (profileId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }

            var post = await _repo.GetPost(postId);

            if(post == null) {
                return NotFound();
            }

            if (post.ProfileId != profileId) { return Unauthorized(); }

            foreach (var like in post.PostLikers)
            {
                _repo.Delete(like);
            }

            _repo.Delete(post);

            if (await _repo.SaveAll()) {
                return Ok();
            }

            return BadRequest();

        }

    }
}