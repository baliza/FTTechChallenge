using Backend.TechChallenge.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Core.Services
{
	public interface IUserService
	{
		Task<OperationResult> AddUser(string name, string email, string address, string phone, string userType, string money);

		Task<OperationResult<List<User>>> GetAllUsers();
	}
}