using Backend.TechChallenge.Api.Extensions;
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
		public UsersController()
		{
		}

		[HttpPost]
		[Route("/create-user")]
		public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
		{
			var validator = new RequestValidator();
			var operationResult = validator.IsValid(name, email, address, phone, userType, money);

			if (!operationResult.IsSuccess)
				return operationResult.ToResult();

			var newUser = UserFactory.CreateUser(name, email, address, phone, userType, money);

			var users = ReadUsers();

			var checkinResult = CheckIfUserExists(newUser, users);
			if (!checkinResult.IsSuccess)
				return checkinResult.ToResult();

			WriteUserToFile(newUser);

			return new OperationResult
			{
				IsSuccess = true,
				Error = "User Created"
			}.ToResult();
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
						Error = " The user is duplicated"
					};
				}
			}
			catch
			{
				Debug.WriteLine(" The user is duplicated");
				return new OperationResult
				{
					IsSuccess = false,
					Error = " The user is duplicated"
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
					UserType = line.Split(',')[4].ToString(),
					Money = decimal.Parse(line.Split(',')[5].ToString()),
				};
				u.Add(user);
			}
			reader.Close();
			return u;
		}
	}
}