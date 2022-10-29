using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Task1.Validations;

namespace Task1.Models;

public class User : IdentityUser
{
    public string[]? Roles { get; set; }

}