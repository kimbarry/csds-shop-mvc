namespace CsdsShop.Models
{
    public class ItemCreationDto
    {
        public int SellerId { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }
        //TODO
        public IFormFile Image { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public int FeePercentage { get; set; }
        public string Category { get; set; }
    }
}
