namespace ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

public interface ICloudinaryService
{
    Task<(string url, string publicId)> UploadImageAsync(IFormFile? file, string folder);
    Task<bool> DeleteImageAsync(string publicId);
}