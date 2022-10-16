using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Entities;

public class Arxive
{
  [Key]
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt {get; set;}
  public DateTime RemovedAt {get;set;}
  public  int ProductId {get; set;}
  public string? ProductTitle {get; set;}
  public ulong ProductQuontity {get; set;}
  public double ProductPrice { get; set; }
}