using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace SEGES.Backend.Helpers
{
    public class FileStorage : IFileStorage
    {
        private readonly Cloudinary _cloudinary;

        public FileStorage(IConfiguration configuration)
        {

            Account account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

            _cloudinary = new Cloudinary(account);
        }

        public async Task RemoveFileAsync(string path, string containerName)
        {

        }

        public async Task<string> SaveFileAsync(byte[] content, string extention, string containerName)
        {
            using (var stream = new MemoryStream(content))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), stream),
                    PublicId = null,
                    Folder = containerName,
                    Format = extention.Replace(".", ""),
                    Overwrite = false,
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString();
            }
        }
    }
}




