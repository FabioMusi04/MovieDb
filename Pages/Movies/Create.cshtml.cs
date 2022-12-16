using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MoviesDb.Database;
using MoviesDb.Models;
using MoviesDb.Services;

namespace MoviesDb.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly FileHandler _fileHandler;

        public CreateModel(AppDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _env = environment;
            _fileHandler = new FileHandler(_env);
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public IActionResult OnPost(List<IFormFile> postedFiles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //if image was uploaded
            if(postedFiles.Count > 0)
            {
                IFormFile img = postedFiles[0];

                //saves image
                //fileData[0] is filePath, fileData[1] is fileName
                string[] fileData = _fileHandler.SaveImage(img);
                               
                Movie.LocalPath = fileData[0];
                Movie.WebPath = "/Uploads/" + fileData[1];                
            }
            else
            {
                Movie.LocalPath = "";
                Movie.WebPath = "";
            }
            
            _context.Movies.Add(Movie);

            _context.SaveChanges();

            var a = _context.Movies.ToList();

            return RedirectToPage("/Movies/Index");
        }

    }
}
