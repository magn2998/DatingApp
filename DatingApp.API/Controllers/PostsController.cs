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

            return postsToReturn;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<PostToReturnDto> GetSinglePost(int id) {
            var post = await _repo.GetPost(id);

            if(post == null) {return null; }

            var postToReturn = _mapper.Map<PostToReturnDto>(post);

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

        [HttpPost("{profileId}/{id}")]
        public async Task<IActionResult> DeletePost(int profileId, int id) 
        {
            if (profileId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }

            var PostFromRepo = await _repo.GetPost(id);

            // IMPLEMENT DELETE POST COMMENTS

            if(PostFromRepo == null) { return NotFound(); }

            _repo.Delete(PostFromRepo);

            if(await _repo.SaveAll()) { return Ok("Succesfully Deleted The Post"); }

            return BadRequest("Failed to delete Post");

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

    }
}