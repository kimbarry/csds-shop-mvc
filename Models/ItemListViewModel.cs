namespace CsdsShop.Models
{
    public class ItemListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; internal set; }
        public string ImgUrl { get; internal set; }
    }
}
