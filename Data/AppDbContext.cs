using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task1.Entities;

namespace Task1.Data;

public class AppDbContext : IdentityDbContext
{
  public DbSet<User>? Users {get; set;} 
  public DbSet<Product>? Products{get; set;}
  public DbSet<Arxive>? Arxives{get; set;}

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

}