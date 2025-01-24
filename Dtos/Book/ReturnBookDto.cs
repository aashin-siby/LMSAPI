using System;
using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO
{
    public class ReturnBookDto
    {
        [Required]
        public int BorrowId { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }
    }
}