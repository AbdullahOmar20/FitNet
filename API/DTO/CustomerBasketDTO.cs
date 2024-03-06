

using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        
        public List<BasketItemsDTO> Items { get; set; }
    }
}