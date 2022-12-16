namespace MoviesDb.Services
{
    public class FileHandler
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public FileHandler(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _env = environment;
        }

        public string[] SaveImage(IFormFile img)
        {
            string wwwPath = this._env.WebRootPath;
            string path = Path.Combine(this._env.WebRootPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string extension = Path.GetExtension(img.FileName);
            var guid = Guid.NewGuid();
            string fileName = $"img" + guid + extension;
            string filePath = Path.Combine(path, fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                img.CopyTo(stream);
            }

            //return two strings: [0]:filePath, [1]:fileName
            return new string[] { filePath, fileName };
        }
    }
}
