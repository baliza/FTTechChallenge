using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.ExternalServices;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public partial class UsersController : ControllerBase
	{
		private readonly IRequestValidator _validator;
		private readonly IUserFactory _userFactory;
		private readonly IUsersRepository _repo;
		private readonly ILogger<UsersController> _logger;

		public UsersController(IRequestValidator validator, IUserFactory userFactory, IUsersRepository repo, ILogger<UsersController> logger)
		{
			_validator = validator;
			_userFactory = userFactory;
			_repo = repo;
			_logger = logger;
		}

		[HttpGet]
		[Route("/get")]
		public async Task<List<User>> GetUsers()
		{
			_logger.LogInformation("GetUsers");
			try
			{
				var result = await _repo.GetAll();
				return result;
			}
			catch (Exception ex)
			{
				_logger.LogError("ERROR getting users", ex);

				return new List<User>();
			}
		}

		[HttpPost]
		[Route("/create-user")]
		public async Task<OperationResult> CreateUser(string name, string email, string address, string phone, string userType, string money)
		{
			var operationResult = _validator.IsValid(name, email, address, phone, userType, money);
			if (!operationResult.IsSuccess)
				return operationResult;

			var newUser = _userFactory.CreateUser(name, email, address, phone, userType, money);

			var users = await _repo.GetAll();

			var checkinResult = CheckIfUserExists(newUser, users);
			if (!checkinResult.IsSuccess)
				return checkinResult;

			await _repo.Save(newUser);

			return new OperationResult
			{
				IsSuccess = true,
				Message = "User Created"
			};
		}

		private OperationResult CheckIfUserExists(User newUser, List<User> users)
		{
			try
			{
				var isDuplicated = users.Any(u => new UserComparer().Compare(newUser, u));

				if (isDuplicated)
				{
					_logger.LogInformation(" The user is duplicated", newUser);

					return new OperationResult
					{
						IsSuccess = false,
						Message = " The user is duplicated"
					};
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("ERROR checking user.", ex);
				return new OperationResult
				{
					IsSuccess = false,
					Message = " The user is duplicated"
				};
			}
			return new OperationResult();
		}
	}
}