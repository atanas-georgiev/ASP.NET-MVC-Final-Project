namespace Planex.Services.Images
{
    using Planex.Data.Models;

    public interface IImageService
    {
        Image GetById(int id);
    }
}