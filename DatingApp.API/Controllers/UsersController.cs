using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Gender)) {
                userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }

            var users = await _repo.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}/likees")]
        public async Task<IActionResult> GetLikees(int id)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }
            var likees = await _repo.GetuserLikers(id);

            var likeesToReturn = _mapper.Map<IEnumerable<UserLikersDto>>(likees);

            List<int> likeeList = new List<int>();

            foreach (var item in likeesToReturn)
            {
                likeeList.Add(item.LikeeId);
            }

            return Ok(likeeList);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto) 
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }
            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if(await _repo.SaveAll()) {
                return NoContent();
            }

            throw new System.Exception($"Updating user {id} failed on safe");
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId) 
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }

            if (id == recipientId) { return BadRequest("You Cannot Like Your Own Profile"); }

            var like = await _repo.GetLike(id, recipientId);

            if (await _repo.GetUser(recipientId) == null) {
                return NotFound();
            }

            bool dislike = false;

            if (like != null) {
                _repo.Delete<Like>(like);
                dislike = true;
            }
            else {
                like = new Like 
                {
                    LikerId = id,
                    LikeeId = recipientId
                };
                _repo.Add<Like>(like);
            }

            if (await _repo.SaveAll()) {
                return Ok(dislike);
            }

            return BadRequest("Failed to Like User");

        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteProfile(int id) 
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) 
            {
                return Unauthorized();
            }

            var user = await _repo.GetUser(id);

            if (user == null) { return BadRequest("User Already Deleted"); }
            
            
            _repo.DeleteUser(id);
            _repo.Delete<User>(user);

            if(await _repo.SaveAll()) 
            {
              return Ok(true);
            }
            return BadRequest("Failed to delete account");  

        }
    }
}