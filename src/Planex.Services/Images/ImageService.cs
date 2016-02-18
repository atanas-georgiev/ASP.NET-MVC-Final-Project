namespace Planex.Services.Images
{
    using System.Data.Entity;

    using Planex.Data;
    using Planex.Data.Models;

    public class ImageService : IImageService
    {
        private DbContext context;

        private IRepository<Image> images;

        public ImageService(DbContext context, IRepository<Image> images)
        {
            this.context = context;
            this.images = images;
        }

        public Image GetById(int id)
        {
            return this.images.GetById(id);
        }
    }
}