using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planex.Data;
using Planex.Data.Models;

namespace Planex.Services.Images
{
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
