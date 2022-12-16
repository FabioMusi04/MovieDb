using System.ComponentModel.DataAnnotations;

namespace MoviesDb.Models
{
    public class AccountUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
