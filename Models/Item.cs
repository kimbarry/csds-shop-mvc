namespace CsdsShop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public string? Size { get; set; }
        public bool IsSold { get; set; }
        public decimal Price { get; set; }
        public decimal? CreditAmount { get; set; }
        public int FeePercentage { get; set; }
        public DateTime ListDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public ItemCategory Category { get; set; }
        public bool Active { get; set; } = true;
        //TODO IsActive
    }
    public enum ItemCategory
    {
        Shoes = 1,
        Dancewear = 2,
        Costume = 3,
        Accessories = 4,
        CsdsMerch = 5,
        Other = 99

    }
}
