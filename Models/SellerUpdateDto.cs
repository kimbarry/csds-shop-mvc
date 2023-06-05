namespace CsdsShop.Models
{
    public class SellerUpdateDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string AccountToCredit { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public string? CellNum { get; set; }
        public string? Notes { get; set; }
    }
}
