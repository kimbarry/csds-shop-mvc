using System.ComponentModel;

namespace CsdsShop.Models
{
    public class ItemDetailViewModel
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }
        [DisplayName("Fee Percentage")]
        public int FeePercentage { get; set; }
        public ItemCategory Category { get; set; }
        public bool Active { get; set; } = true;
        [DisplayName("List Date")]
        public string? ListDate { get; set; }
        [DisplayName("Sold Date")]
        public string? SoldDate { get; set; }
        [DisplayName("Sold?")]
        public bool IsSold { get; set; }
    }
}
