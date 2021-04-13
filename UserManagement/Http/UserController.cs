using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using UserManagement.Dto;
using UserManagement.Repository;

namespace UserManagement.Http
{
    [AllowAnonymous]
    [Route("Users")]
    public class UserController : Controller
    {   
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserController(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                await _userRepository.CreateUser(user);
            }
            catch(Exception ex)
            {
                _logger.Error($"{ex}");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                await _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
                return Conflict();
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                await _userRepository.GetUser(userId);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                await _userRepository.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex}");
            }

            return Ok();
        }
    }
}
