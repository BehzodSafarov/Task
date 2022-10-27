using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task1;
using Task1.Repositories;
using Task1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseInMemoryDatabase("Data"));


builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    
}).AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>(); 

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IHistoryRepository, HistoryRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IHistoryService, HistoryService>();


var app = builder.Build();

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
    name: "default",
    pattern: "{controller=Account}/{action=LogIn}");
await Seed.InitializeRoleAsync(app);
await Seed.InitializeUserAsync(app);

app.Run();
