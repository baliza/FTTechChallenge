using Backend.TechChallenge.Api.CustomMiddleware;
using Microsoft.AspNetCore.Builder;

namespace Backend.TechChallenge.Api.Extensions
{
	public static class ErrorsHandlerExtensions
	{
		public static void UseErrorHandlerMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionHandlerMiddleware>();
		}
	}
}