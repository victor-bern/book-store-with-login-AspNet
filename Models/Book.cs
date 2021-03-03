using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsRead { get; set; }

        [Required]
        public int UserId { get; set; }


    }
}