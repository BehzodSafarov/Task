using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Entities;

public class Product
{
    [Key]
    public int Id {get; set;}
    public string? Title { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

}