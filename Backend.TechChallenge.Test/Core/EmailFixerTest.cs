using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Core.Services;
using Xunit;

namespace Backend.TechChallenge.Test.Core
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
	public class EmailFixerTest
	{
		[Fact]
		public void When_OK_Email_It_Does_Not_Change()
		{
			var sut = new  EmailFixer();
			var result = sut.Amend("mike@gmail.com");

			Assert.Equal("mike@gmail.com", result);
		}
		[Fact]
		public void When_a_plus_is_found_on_user_it_removes_rear_part()
		{
			var sut = new EmailFixer();
			var result = sut.Amend("mike+garcia@gmail.com");

			Assert.Equal("mike@gmail.com", result);
		}

		[Fact]
		public void When_dots_are_send_On_user_these_are_removed()
		{
			var sut = new EmailFixer();
			var result = sut.Amend("mike.garcia@gmail.com");

			Assert.Equal("mikegarcia@gmail.com", result);
		}
	}
}