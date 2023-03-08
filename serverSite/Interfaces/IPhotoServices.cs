using CloudinaryDotNet.Actions;

namespace serverSite.Interfaces
{
    public interface IPhotoServices
    {
        public Task<ImageUploadResult> AddPhoto(IFormFile file);
        public Task<DeletionResult> DeletePhoto(string publicId);
    }
}