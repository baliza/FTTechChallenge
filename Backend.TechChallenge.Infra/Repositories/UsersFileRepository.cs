using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.ExternalServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Infra.Repository
{
	public class UsersFileRepository : IUsersRepository
	{
		private readonly string _connectionString = Directory.GetCurrentDirectory() + "/Files/Users.txt";

		public UsersFileRepository()
		{
			_connectionString = Directory.GetCurrentDirectory() + "/Files/Users.txt";
		}

		public async Task<List<User>> GetAll()
		{
			var u = new List<User>();
			var reader = ReadUsersFromFile();
			while (reader.Peek() >= 0)
			{
				var line = await reader.ReadLineAsync();
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

		public async Task Save(User user)
		{
			using StreamWriter outputFile = new StreamWriter(_connectionString, append: true);
			await outputFile.WriteLineAsync(user.Name + "," + user.Email + "," + user.Phone + "," + user.Address + "," + user.UserType + "," + user.Money);
		}

		private StreamReader ReadUsersFromFile()
		{
			FileStream fileStream = new FileStream(_connectionString, FileMode.Open);
			StreamReader reader = new StreamReader(fileStream);
			return reader;
		}
	}
}