namespace Planex.Services.Images
{
    using System.Data.Entity;

    using Planex.Data;
    using Planex.Data.Common;
    using Planex.Data.Models;

    public class ImageService : IImageService
    {
        private DbContext context;

        private IRepository<Image, int> images;

        public ImageService(DbContext context, IRepository<Image, int> images)
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