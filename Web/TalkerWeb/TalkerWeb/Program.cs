using TalkerWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
builder.Services.Configure<PasswordValidationParameters>(builder.Configuration.GetSection("PasswordValidationParameters"));
builder.Services.Configure<API>(builder.Configuration.GetSection("API"));

builder.Services.AddAuthentication("Cookie").AddCookie("TestScheme", options =>
{
    options.Cookie.Name = "TalkerCoockies";
    options.LoginPath = "/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "login",
    pattern: "Login");
app.MapControllerRoute(
    name: "mainPage",
    pattern: "Main/Index");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
