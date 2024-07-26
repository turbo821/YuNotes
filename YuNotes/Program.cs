using Microsoft.EntityFrameworkCore;
using YuNotes.Data;
using YuNotes.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NotesContext>(options => options.UseSqlite(connection));
builder.Services.AddScoped<IRepository, SQLiteRepository>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.Run();
