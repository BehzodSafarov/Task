using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Task1.Models;

public class User : IdentityUser
{
    public string[]? Roles { get; set; }

}