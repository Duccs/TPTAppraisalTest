using Microsoft.AspNetCore.Hosting;
using TestProject.Interfaces;

namespace TestProject.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> ImageUpload(IFormFile Image)
        {
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
