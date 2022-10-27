namespace Task1.Services;

public static class Mappers
{
    public static Models.History ToEntityHistory(this Models.Product model)
    => new Models.History
  {
           ProductId = model.Id,
           ProductPrice = model.Price,
           ProductQuontity = model.Quantity,
           ProductTitle = model.Title
  };
  public static Models.History ToModelHistory(this Models.History entity)
    => new Models.History
  {
           Id = entity.Id,
           ProductPrice = entity.ProductPrice,
           ProductQuontity = entity.ProductQuontity,
           ProductTitle = entity.ProductTitle
  };
  
}