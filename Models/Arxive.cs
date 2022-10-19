namespace Task1.Models;

public class Arxive
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt {get; set;}
  public DateTime RemovedAt {get;set;}
  public  int ProductId {get; set;}
  public string? ProductTitle {get; set;}
  public int ProductQuontity {get; set;}
  public double ProductPrice { get; set; }
}