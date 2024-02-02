using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class FotoService : IFotoService
    {
        //interface de servico de Ifoto
        private readonly Cloudinary _cloudinary;

        public FotoService(IOptions<CloudinarySettings> config)
        {
            var conta = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(conta);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var resultadoUpload = new ImageUploadResult();

            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();//arquivo ser descartado
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"), //tranforma tam da imagem, crop deixa quadrada, se concetrna no rosto
                    Folder = "da-net/"
                };

                resultadoUpload = await _cloudinary.UploadAsync(uploadParams);
            }

            return resultadoUpload;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string idPublico)
        {
            var deleteParams = new DeletionParams(idPublico);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}