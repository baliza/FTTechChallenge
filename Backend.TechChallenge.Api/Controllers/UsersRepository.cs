using Backend.TechChallenge.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Backend.TechChallenge.Api.Controllers
{
	public class UsersRepository
	{
		private readonly string _connectionString = Directory.GetCurrentDirectory() + "/Files/Users.txt";

		public UsersRepository()
		{
			_connectionString = Directory.GetCurrentDirectory() + "/Files/Users.txt";
		}

		private StreamReader ReadUsersFromFile()
		{
			FileStream fileStream = new FileStream(_connectionString, FileMode.Open);
			StreamReader reader = new StreamReader(fileStream);
			return reader;
		}

		public List<User> GetAll()
		{
			var u = new List<User>();
			var reader = ReadUsersFromFile();
			while (reader.Peek() >= 0)
			{
				var line = reader.ReadLineAsync().Result;
				string[] lineSplited = line.Split(',');
				var user = new User
				{
					Name = lineSplited[0],
					Email = lineSplited[1],
					Phone = lineSplited[2],
					Address = lineSplited[3],
					UserType = lineSplited[4].ToUserType(),
					Money = decimal.Parse(lineSplited[5]),
				};
				u.Add(user);
			}
			reader.Close();
			return u;
		}

		public void Save(User user)
		{
			using StreamWriter outputFile = new StreamWriter(_connectionString, append: true);
			outputFile.WriteLine(user.Name + "," + user.Email + "," + user.Phone + "," + user.Address + "," + user.UserType + "," + user.Money);
		}
	}
}