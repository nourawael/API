﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities.IdentityEntities;
using Store.Service.HandleResponses;
using Store.Service.Services.UserService;
using Store.Service.Services.UserService.Dtos;

namespace Store.Web.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto input) 
        {
            var user =await _userService.Login(input);

            if (user == null)
                return BadRequest(new CustomException(400,"Email does not exist"));

            return Ok(user);
        }

        [HttpPost]

        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = await _userService.Register(input);

            if (user == null)
                return BadRequest(new CustomException(400, "Email already exists"));

            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<UserDto> GetCurrentUserDetails()
        {
            var userId = User?.FindFirst("UserId");

            var user = await _userManager.FindByIdAsync(userId.Value);

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                DisplayName = user.DipslayName,
                Email = user.Email,
            };

        }
    }
}
