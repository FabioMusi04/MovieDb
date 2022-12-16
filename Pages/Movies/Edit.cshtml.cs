using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoviesDb.Models;
using MoviesDb.Database;
using System.Linq;
using MoviesDb.Services;

namespace MoviesDb.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly FileHandler _fileHandler;

        public EditModel(AppDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _env = environment;
            _fileHandler = new FileHandler(_env);
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var p = _context.Movies.FirstOrDefault(o => o.PKMovie == id);

            if (p == null)
            {
                return NotFound();
            }

            Movie = p;
            return Page();
        }

        public IActionResult OnPost(int id, List<IFormFile> postedFiles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(postedFiles.Count > 0)
            {
                IFormFile img = postedFiles[0];

                //Before saving the new image, I want to delete the old one
                if (System.IO.File.Exists(Movie.LocalPath))
                {
                    System.IO.File.Delete(Movie.LocalPath);
                }

                //saves image
                //fileData[0] is filePath, fileData[1] is fileName
                string[] fileData = _fileHandler.SaveImage(img);

                Movie.LocalPath = fileData[0];
                Movie.WebPath = "/Uploads/" + fileData[1];
            }


            try
            {
                _context.Attach(Movie).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToPage("/Movies/Index");
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
