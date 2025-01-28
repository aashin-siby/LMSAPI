using System.ComponentModel.DataAnnotations;

namespace LMSAPI.Models;
//Model to store the the Borrow Date, Return Date and Payment amount 
public class BorrowDetails
{
    [Key]
    public int BorrowId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    [DataType(DataType.Date)]

    public DateTime BorrowDate { get; set; }
    [DataType(DataType.Date)]

    public DateTime? ReturnDate { get; set; }

    public decimal Payment { get; set; }
    public Book? Book { get; set; }
}
