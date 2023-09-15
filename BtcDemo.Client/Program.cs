using BtcDemo.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<AppUserApiService>(opt =>
{
	opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]); 
});

builder.Services.AddHttpClient<AuthApiService>(opt =>
{
	opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]);
});

builder.Services.AddHttpClient<CoinApiService>(opt =>
{
	opt.BaseAddress = new Uri(uriString: builder.Configuration["BaseUrl"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
