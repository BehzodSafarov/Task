using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Task1.Validations;

namespace Task1.Models;

public class User
{
    public string? UserName { get; set; }
    public string? Password {get; set;}
    public string[]? Roles { get; set; }

}