using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Backend.TechChallenge.Core.ExternalServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		public UsersController(IRequestValidator validator, IUserFactory userFactory, IUsersRepository repo)
		{
			_validator = validator;
			_userFactory = userFactory;
			_repo = repo;
		}

		[HttpGet]
		[Route("/get")]
		public async Task<List<User>> GetUser()
		{
			var result = await _repo.GetAll();
			return result;
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
				var isDuplicated = false;
				foreach (var user in users)
				{
					if (user.Email == newUser.Email
						||
						user.Phone == newUser.Phone)
					{
						isDuplicated = true;
					}
					else if (user.Name == newUser.Name)
					{
						if (user.Address == newUser.Address)
						{
							isDuplicated = true;
							throw new Exception("User is duplicated");
						}
					}
				}

				if (isDuplicated)
				{
					Debug.WriteLine(" The user is duplicated");

					return new OperationResult
					{
						IsSuccess = false,
						Message = " The user is duplicated"
					};
				}
			}
			catch
			{
				Debug.WriteLine(" The user is duplicated");
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