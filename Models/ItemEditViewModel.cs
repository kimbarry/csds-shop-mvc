namespace CsdsShop.Models
{
    public class ItemEditViewModel
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public bool IsSold { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public int FeePercentage { get; set; }
        public ItemCategory Category { get; set; }
        public bool Active { get; set; } = true;
        public IFormFile? Image { get; set; }
    }
}
