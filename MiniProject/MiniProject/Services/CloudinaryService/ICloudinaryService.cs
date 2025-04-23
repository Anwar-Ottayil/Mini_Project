namespace MiniProject.Services.CloudinaryService
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile file);

    }
}
