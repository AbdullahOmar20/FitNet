

namespace Core.Entities.OrderAggregate
{
    /// <summary>
    /// A snapshot for the product at the time the order is placed 
    /// </summary>
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int productItemId, string productname, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productname;
            PictureUrl = pictureUrl;
        }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }         


    }
}