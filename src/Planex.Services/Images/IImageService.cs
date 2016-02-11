using Planex.Data.Models;

namespace Planex.Services.Images
{
    public interface IImageService
    {
        Image GetById(int id);
    }
}
