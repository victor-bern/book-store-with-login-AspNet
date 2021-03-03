using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }


        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Book> Books { get; set; }



        public string GetFirstName()
        {
            return Name.Split(' ')[0];
        }
    }
}