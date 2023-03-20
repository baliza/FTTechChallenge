using Backend.TechChallenge.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Api.CustomMiddleware
{
	/// <summary>
	/// Controlador de errores
	/// </summary>
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		/// <param name="next"></param>
		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		/// <summary>
		/// Pasa la solicitud al siguiente controlador
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context).ConfigureAwait(false);
			}
			catch (Exception error)
			{
				HttpResponse response = context.Response;
				response.ContentType = "application/json";
				var operationResult = new OperationResult<string>(error?.Message);

				switch (error)
				{
					case Exception e:
						{
							response.StatusCode = (int)HttpStatusCode.BadRequest;
							break;
						}
					default:
						{
							response.StatusCode = (int)HttpStatusCode.InternalServerError;
							break;
						}
				}

				string result = JsonSerializer.Serialize(operationResult);

				await response.WriteAsync(result).ConfigureAwait(false);
			}
		}
	}
}