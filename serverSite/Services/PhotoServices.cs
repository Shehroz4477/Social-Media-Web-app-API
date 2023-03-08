using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using serverSite.Helpers;
using serverSite.Interfaces;

namespace serverSite.Services
{
    public class PhotoServices : IPhotoServices
    {
        private readonly Cloudinary _cloudinary; 
        public PhotoServices(IOptions<CloudinarySettings> config)
        {
            var account = new Account
            (
              config.Value.CloudName,
              config.Value.ApiKey,
              config.Value.ApiSecret  
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> AddPhoto(IFormFile file)
        {
            var imageUploadResult = new ImageUploadResult();
            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var imageUploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height("500").Width("500").Crop("fill").Gravity("face"),
                    Folder = "da-dotnet7"
                };
                imageUploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            }
            return imageUploadResult;
        }

        public async Task<DeletionResult> DeletePhoto(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deletionParams);
        }
    }
}