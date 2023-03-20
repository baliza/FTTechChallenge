using Backend.TechChallenge.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Core.ExternalServices
{
	public interface IUsersRepository
	{
		Task<List<User>> GetAll();
		Task Save(User user);
	}
}