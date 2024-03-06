
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class BasketItemsDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(10,double.MaxValue, ErrorMessage ="the price must be at least 10 ")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(1,double.MaxValue, ErrorMessage ="Quantity must be at least 1")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }

    }
}