namespace Task1.Services;

public static class Mappers
{
  public static Models.History ToModelHistory(this Models.Product product)
    => new Models.History
  {
           Id = product.Id,
           ProductPrice = product.Price,
           ProductQuontity = product.Quantity,
           ProductTitle = product.Title
  };
  
}