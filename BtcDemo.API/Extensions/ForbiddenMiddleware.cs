using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.Utilities.Results;
using System.Net;
using System.Text.Json;

namespace BtcDemo.API.Extensions;

public class ForbiddenMiddleware
{   // bu sınıfta da vaktim kalırsa iyileştirme yapabilirim.
	private readonly RequestDelegate _next;

	public ForbiddenMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext httpContext)
	{
		await _next(httpContext);
		if (httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
		{
			var response = new Result(ResultStatus.Forbidden, "Yetkisiz kullanıcı");
			await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
		else if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
		{
			var response = new Result(ResultStatus.UnAuthorized, "Yanlış token ya da kullanıcı");
			await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
	}

}

public static class CustomMiddlewareExtensions
{
	public static IApplicationBuilder UseForbiddenMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ForbiddenMiddleware>();
	}
}

