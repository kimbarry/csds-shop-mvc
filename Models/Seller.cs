namespace CsdsShop.Models
{
    public class Seller
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string AccountToCredit { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public string? CellNum { get; set; }
        public IEnumerable<Item>? ItemsList { get; set; }
        public string? Notes { get; set; }

    }
}
