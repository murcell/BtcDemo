using AutoMapper;
using BtcDemo.Client.ProfileMap;
using Microsoft.AspNetCore.Authentication.JwtBearer;


public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ModelProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);

        //builder.Services.AddScoped<IAuthApiService, AuthApiService>();
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //builder.Services.AddHttpClient<AppUserApiService>(opt =>
        //{
        //	opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]); 
        //});

        builder.Services.AddHttpClient();

        //builder.Services.AddHttpClient<AuthApiService>(opt =>
        //{
        //	opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]);
        //});

        //builder.Services.AddHttpClient<CoinApiService>(opt =>
        //{
        //	opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]);
        //});

        //builder.Services.AddHttpClient<ICoinloreService, CoinloreService>(opt =>
        //{
        //    opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]);
        //});

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
                        {
                            opt.LoginPath = "/Account/Login";
                            opt.LogoutPath = "/Account/Logout";
                            opt.AccessDeniedPath = "/Account/AccessDenid";
                            opt.Cookie.SameSite = SameSiteMode.Strict;
                            opt.Cookie.HttpOnly = true;
                            opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                            opt.Cookie.Name = "BtcDemoJwtCookie";
                        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            //pattern: "{controller=Home}/{action=Index}/{id?}"
            pattern: "{controller}/{action}",
            defaults: new { controller = "Account", action = "Login" }
            );

        app.MapDefaultControllerRoute();
        app.Run();
    }
}