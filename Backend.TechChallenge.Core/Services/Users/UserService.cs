using Backend.TechChallenge.Core.ExternalServices;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Api.Services
{
	public class UserService : IUserService
	{
		private readonly IUsersRepository _repo;
		private readonly IUserFactory _factory;

		private readonly ILogger<UserService> _logger;

		public UserService(IUsersRepository repo, IUserFactory factory, ILogger<UserService> logger)
		{
			_repo = repo;
			_factory = factory;
			_logger = logger;
		}

		public async Task<OperationResult> AddUser(string name, string email, string address, string phone, string userType, string money)
		{
			try
			{
				var newUser = _factory.CreateUser(name, email, address, phone, userType, money);

				var users = await _repo.GetAll();
				var userExists = CheckIfUserExists(newUser, users);
				if (userExists)
				{
					return new OperationResult
					{
						IsSuccess = false,
						Message = " The user is duplicated"
					};
				}

				await _repo.Save(newUser);
			}
			catch (Exception ex)
			{
				_logger.LogError("ERROR adding user.", ex);
				throw;
			}
			return new OperationResult();
		}

		public async Task<OperationResult<List<User>>> GetAllUsers()
		{
			var users = await _repo.GetAll();

			return new OperationResult<List<User>>(users);
		}

		private bool CheckIfUserExists(User newUser, List<User> users)
		{
			var isDuplicated = users.Any(u => new UserComparer().Compare(newUser, u));
			if (isDuplicated)
			{
				_logger.LogInformation(" The user is duplicated", newUser);
				return true;
			}

			return false;
		}
	}
}