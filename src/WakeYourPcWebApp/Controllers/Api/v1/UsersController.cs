using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WakeYourPcWebApp.Models;
using WakeYourPcWebApp.ViewModels;


namespace WakeYourPcWebApp.Controllers.Api.v1
{
    [Route("api/v1/users/")]
    public class UsersController : Controller
    {
        private readonly IConfigurationRoot _config;
        private readonly IWakeupRepository _repository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IConfigurationRoot config, IWakeupRepository repository, ILogger<UsersController> logger)
        {
            _config = config;
            _repository = repository;
            _logger = logger;
        }


        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            try
            {
                var user = _repository.GetUser(username);
                if(user == null)
                {
                    var json = Json(new { errorMessage = $"username [{username}] not found" });
                    return BadRequest(json.Value);
                }
                var userViewModel = Mapper.Map<UserViewModel>(user);
                return Ok(userViewModel);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured while getting a User {exception}");
                return BadRequest("Error occured while getting a User");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddUser([FromBody] UserViewModel userViewModel)
        {
            try
            {
                var user = Mapper.Map<User>(userViewModel);
                _repository.AddUser(user);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(Mapper.Map<UserViewModel>(user));
                }
                return BadRequest("Error occured while adding a User and saving changes");
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error occured while adding a User {exception}");
                return BadRequest("Error occured while adding a User");
            }
        }

    }
}
