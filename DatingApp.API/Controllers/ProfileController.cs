using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ProfileController(IProfileRepository repo, IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetProfile")]
        public async Task<IActionResult> GetUser(int id)
        {
            var profile = await _repo.getProfile(id);

            var profileToReturn = _mapper.Map<ProfileToReturnDto>(profile);

            return Ok(profileToReturn);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ProfileForRegisterDto profileForRegisterDto)
        {
            profileForRegisterDto.Username = profileForRegisterDto.Username.ToLower();

            if (await _repo.userExists(profileForRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }

            var profileToCreate = _mapper.Map<Profiles>(profileForRegisterDto);

            var createdProfile = await _repo.Register(profileToCreate, profileForRegisterDto.Password);

            return Ok(createdProfile);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ProfileForLoginDto profileForLogin)
        {
            var profileFromRepo = await _repo.Login(profileForLogin.Username.ToLower(), profileForLogin.Password);

            if (profileFromRepo == null)
            {
                return BadRequest("User And Password combination is invalid");
            }

            var profileLikedPosts = _mapper.Map<IEnumerable<ProfileLikedPostToReturnDto>>(profileFromRepo.PostLikes);

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, profileFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, profileFromRepo.Username),
                    new Claim(ClaimTypes.Surname, profileFromRepo.ProfileName)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
            });
        }
    }
}