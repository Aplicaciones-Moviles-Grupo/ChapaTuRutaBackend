using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;
using ChapaTuRuta.Platform.API.Shared.Infrastructure.Cloudinary.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using CloudinaryNet =  CloudinaryDotNet.Cloudinary;

namespace ChapaTuRuta.Platform.API.Shared.Infrastructure.Cloudinary.Services;

public class CloudinaryService:ICloudinaryService
{
    private readonly CloudinaryNet _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> settings)
    {
        var account = new Account(settings.Value.CloudName, settings.Value.ApiKey, settings.Value.ApiSecret);
        _cloudinary = new CloudinaryNet(account);
    }

    public async Task<(string url, string publicId)> UploadImageAsync(IFormFile? file, string folder)
    {
        if(file == null || file.Length == 0)
            throw new ArgumentException("The file cannot be empty", nameof(file));
        
        using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            Folder = folder,
            Transformation = new Transformation().Width(500).Height(500).Crop("fill").Quality("auto")
        };
        
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        
        if(uploadResult.Error != null)
            throw new InvalidOperationException($"Error uploading file to Cloudinary: {uploadResult.Error.Message}");
        
        var url = uploadResult.SecureUrl.ToString();
        var publicId = uploadResult.PublicId;
        
        return (url, publicId);

    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        if(String.IsNullOrEmpty(publicId))
            throw new ArgumentException("PublicId cannot be empty", nameof(publicId));
        
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result.Result == "ok" || result.Result == "not found";
    }
}