using System.ComponentModel.DataAnnotations;
using Task1.Validations;

namespace Task1.Models;

public class User
{
    [Required]
    public string? UserName { get; set; }

    [Password(6)]
    public string? Password  { get; set; }

    public string? ReturnUrl {get; set;}
    
}