using System;
using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO
{
    public class ReturnBookDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }

        [Required]
        public int BorrowId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
    }
}