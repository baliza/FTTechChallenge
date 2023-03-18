using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Core.Services
{
	public interface IEmailFixer
	{
		string Amend(string text);
	}
}
