namespace LMSAPI.DTO
{
    public class RentalDetailsDto
    {
        public int BorrowId { get; set; }
        public int BookId { get; set; }
        public string? Title { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public decimal Payment { get; set; }
    }
}
