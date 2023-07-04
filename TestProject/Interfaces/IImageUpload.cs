namespace TestProject.Interfaces
{
    public interface IImageService
    {
        Task<string> ImageUpload(IFormFile Image);

    }
}