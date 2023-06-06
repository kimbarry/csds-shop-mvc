using System.ComponentModel;

namespace CsdsShop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        [Description("True/False item sold")]
        [DisplayName("Sold?")]
        public bool IsSold { get; set; }
        public decimal Price { get; set; }
        [Description("Amount to credited to seller.")]
        [DisplayName("Credit Amount")]
        public decimal? CreditAmount { get; set; }
        [Description("Amount to be paid to the studio.")]
        [DisplayName("Fee Percentage")]
        public int FeePercentage { get; set; }
        [Description("Date item was listed.")]
        [DisplayName("List Date")]
        public DateTime ListDate { get; set; }
        [Description("Date item was sold.")]
        [DisplayName("Sale Date")]
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
