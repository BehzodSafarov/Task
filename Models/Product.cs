
using System.ComponentModel.DataAnnotations;

namespace Task1.Models;

public class Product 
{
    [Key]
    public int Id {get; set;}
                       
    [Required]
    public string? Title { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int Quantity { get; set; }

}