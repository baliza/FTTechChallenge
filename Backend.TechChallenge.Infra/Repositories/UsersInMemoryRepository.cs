using Backend.TechChallenge.Core.ExternalServices;
using Backend.TechChallenge.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Infra.Repository
{
	public class UsersInMemoryRepository : IUsersRepository
	{
		private readonly List<User> _listUsers;

		public UsersInMemoryRepository()
		{
			_listUsers = new List<User>();
		}

		public async Task<List<User>> GetAll()
		{
			return await Task.FromResult(_listUsers);
		}

		public async Task Save(User user)
		{
			await Task.Run(() => _listUsers.Add(user));
		}
	}
}