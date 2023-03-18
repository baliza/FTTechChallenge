using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Core.Services
{
	public class EmailFixer : IEmailFixer
	{
		public string Amend(string text)
		{
			var aux = text.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

			var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

			aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

			return string.Join("@", new string[] { aux[0], aux[1] });
		}
	}
}
