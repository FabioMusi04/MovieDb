using System.ComponentModel.DataAnnotations;

namespace MoviesDb.Models
{
    public class Movie:IValidatableObject
    {
        [Key]
        public int PKMovie { get; set; }

        [Required, MaxLength(40, ErrorMessage = "The title cannot be longer than 30 characters")]
        public string Title { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Invalid movie length")]
        public int Length { get; set; }

        [Required, MaxLength(40, ErrorMessage = "The director name cannot be longer than 30 characters")]
        public string Director { get; set; }

        [Required]
        public string Genre { get; set; }

        public string? LocalPath { get; set; }

        public string? WebPath { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string[] validGenres = {
                "Comedy", "History", "Sci-fi", "Romance", "Thriller", "Action",
                "Horror", "Fantasy", "Drama"
            };

            if(!validGenres.Contains(this.Genre, StringComparer.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("Invalid movie genre", new List<string> { "Genre" });
            }
        }
    }
}
