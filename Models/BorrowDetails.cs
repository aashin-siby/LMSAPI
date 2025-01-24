using System;
using System.ComponentModel.DataAnnotations;

namespace LMSAPI.Models
{
    public class BorrowDetails
    {
        [Key]
        public int BorrowId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public decimal Penalty { get; set; } = 0;
    }
}