using Task1.Entities;

namespace Task1.Services;


public static class Mapper
{
    public static Product ToEntity(this Models.Product model)
     => new Product
    {
       Price = model.Price,
       Quantity = model.Quantity,
       Title = model.Title
    };

     public static Models.Product ToModel(this Product entity)
     => new Models.Product
    {
        Price = entity.Price,
        Quantity = entity.Quantity,
        Title = entity.Title,
        Id = entity.Id
    };

     public static Arxive ToEntity(this Models.Arxive model)
     => new Arxive
    {
       CreatedAt = model.CreatedAt,
       UpdatedAt = model.UpdatedAt,
       RemovedAt = model.RemovedAt,
       ProductPrice = model.ProductPrice,
       ProductTitle = model.ProductTitle,
       ProductQuontity = model.ProductQuontity
    };

    public static Models.Arxive ToModel(this Arxive entity)
     => new Models.Arxive
    {
       Id = entity.Id,
       CreatedAt = entity.CreatedAt,
       UpdatedAt = entity.UpdatedAt,
       RemovedAt = entity.RemovedAt,
       ProductPrice = entity.ProductPrice,
       ProductTitle = entity.ProductTitle,
       ProductQuontity = entity.ProductQuontity
    };

  public static Arxive ToArxiveEntity(this Models.Product model)
    => new Arxive
  {
           ProductPrice = model.Price,
           ProductQuontity = model.Quantity,
           ProductTitle = model.Title
  };
  public static Models.Arxive ToArxiveModel(this Arxive entity)
    => new Models.Arxive
  {
           ProductPrice = entity.ProductPrice,
           ProductQuontity = entity.ProductQuontity,
           ProductTitle = entity.ProductTitle
  };

}