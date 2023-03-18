using Backend.TechChallenge.Api.Extensions;
using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
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

		public UsersController(IRequestValidator validator, IUserFactory userFactory)
		{
			_validator = validator;
			_userFactory = userFactory;
		}

		[HttpGet]
		[Route("/get")]
		public async Task<OperationResult> GetUser()
		{
			return new OperationResult { IsSuccess = true, Message = "paco" };
		}

		[HttpPost]
		[Route("/create-user")]
		public async Task<OperationResult> CreateUser(string name, string email, string address, string phone, string userType, string money)
		{
			var operationResult = _validator.IsValid(name, email, address, phone, userType, money);

			if (!operationResult.IsSuccess)
				return operationResult;

			var newUser = _userFactory.CreateUser(name, email, address, phone, userType, money);

			var users = ReadUsers();

			var checkinResult = CheckIfUserExists(newUser, users);
			if (!checkinResult.IsSuccess)
				return checkinResult;

			WriteUserToFile(newUser);

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

		private List<User> ReadUsers()
		{
			var u = new List<User>();
			var reader = ReadUsersFromFile();
			while (reader.Peek() >= 0)
			{
				var line = reader.ReadLineAsync().Result;
				var user = new User
				{
					Name = line.Split(',')[0].ToString(),
					Email = line.Split(',')[1].ToString(),
					Phone = line.Split(',')[2].ToString(),
					Address = line.Split(',')[3].ToString(),
					UserType = line.Split(',')[4].ToUserType(),
					Money = decimal.Parse(line.Split(',')[5].ToString()),
				};
				u.Add(user);
			}
			reader.Close();
			return u;
		}
	}
}