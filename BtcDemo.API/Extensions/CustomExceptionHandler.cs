using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.Utilities.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace BtcDemo.API.Extensions;

public static class CustomExceptionHandler
{
	// bu sınıfta vaktim kalırsa iyileştirme yapabilirim.
	public static void UseCustomException(this IApplicationBuilder app)
	{
		app.UseExceptionHandler(config =>
		{
			config.Run(async context =>
			{
				context.Response.StatusCode = 500;
				context.Response.ContentType = "application/json";
				var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

				if (errorFeature != null)
				{
					var ex = errorFeature.Error;
					var errResponse = new Result(ResultStatus.ServerError, ex.Message);
					await context.Response.WriteAsync(JsonSerializer.Serialize(errResponse));
				}

			});

		});
	}
}
