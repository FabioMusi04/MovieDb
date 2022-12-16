using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesDb.Database;
using MoviesDb.Models;

namespace MoviesDb.Pages.Movies
{
    public class IndexModel : PageModel { 

        private readonly AppDbContext _context;
    
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get; set; }

        public void OnGet()
        {
            Movies = _context.Movies.ToList();
        }

        public IActionResult OnGetFilter(string genre)
        {
            Movies = _context.Movies.Where(m => m.Genre == genre).ToList();
            return Page();
        }

        public IActionResult OnGetDelete(int id)
        {
            var movieToDelete = _context.Movies.Find(id);

            //I delete also the image when deleting a movie
            if (System.IO.File.Exists(movieToDelete.LocalPath))
            {
                System.IO.File.Delete(movieToDelete.LocalPath);
            }

            _context.Movies.Remove(movieToDelete);
            _context.SaveChanges();

            return RedirectToPage("/Movies/Index");
        }
    }
}


