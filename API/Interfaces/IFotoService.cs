using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IFotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file); //FAZ UPLOAD DA IMAGEM
        Task<DeletionResult> DeletePhotoAsync(string idPublico);
    }
}