using Microsoft.EntityFrameworkCore;
using YuNotes.Data;
using YuNotes.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using YuNotes.Repositories.Interfaces;
using YuNotes.Services.Interfaces;
using YuNotes.Services;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
string email = builder.Configuration.GetSection("Credentials:Email").Value!;
string code = builder.Configuration.GetSection("Credentials:CodeApp").Value!;

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NotesContext>(options => options.UseSqlite(connection));

builder.Services.AddScoped<INotesRepository, SQLiteNotesRepository>();
builder.Services.AddScoped<IUsersReposiroty, SQLiteUsersRepository>();
builder.Services.AddScoped<INotesService, NotesService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IPasswordRecoveryService>(new SimplePasswordRecoveryService(new System.Net.NetworkCredential(email, code)));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
