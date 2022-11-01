using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task1.Models;

namespace Task1.Repositories;

public class AppDbContext : IdentityDbContext
{
  public DbSet<Product>? Products{get; set;}
  public DbSet<History>? Arxives{get; set;}

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
  
}