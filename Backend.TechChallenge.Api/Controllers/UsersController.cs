using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public partial class UsersController : ControllerBase
	{
		private readonly IRequestValidator _validator;
		private readonly IUserService _userService;
		private readonly ILogger<UsersController> _logger;

		public UsersController(IRequestValidator validator, IUserService userService, ILogger<UsersController> logger)
		{
			_validator = validator;
			_userService = userService;
			_logger = logger;
		}

		[HttpGet]
		[Route("/get")]
		public async Task<List<User>> GetUsers()
		{
			try
			{
				var result = await _userService.GetAllUsers();
				return result.Data;
			}
			catch (Exception ex)
			{
				_logger.LogError("ERROR getting users", ex);

				throw;
			}
		}

		[HttpPost]
		[Route("/create-user")]
		public async Task<OperationResult> CreateUser(string name, string email, string address, string phone, string userType, string money)
		{
			try
			{
				var validationResult = _validator.IsValid(name, email, address, phone, userType, money);

				if (!validationResult.IsSuccess)
					return validationResult;

				var result = await _userService.AddUser(name, email, address, phone, userType, money);

				if (result.IsSuccess)
					return new OperationResult { Message = "User Created" };
				_logger.LogError("Could not register user");
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError("ERROR adding user", ex);

				throw;
			}
		}
	}
}