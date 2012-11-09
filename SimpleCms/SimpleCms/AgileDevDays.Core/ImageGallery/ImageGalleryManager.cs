using System.Collections.Generic;
using System.Linq;
using AgileDevDays.Infrastructure;
using AgileDevDays.Infrastructure.Entities;
using AutoMapper;
using GalleryEntity = AgileDevDays.Core.Models.GalleryEntity;
using ImageEntity = AgileDevDays.Core.Models.ImageEntity;

namespace AgileDevDays.Core.ImageGallery
{
    public class ImageGalleryManager
    {
        private readonly ImageGalleryEntities _context = new ImageGalleryEntities();
        private readonly Repository<ImageGalleryEntities> _repository;

        public ImageGalleryManager()
        {
            _repository = new Repository<ImageGalleryEntities>(_context);
        }

        public void CreateGallery(Gallery gallery)
        {
            _repository.Add(gallery);
        }

        public void AddImage(Image image)
        {
            _repository.Add(image);
        }

        public List<Gallery> GetGalleries()
        {
            List<Gallery> galleries = Mapper.Map<List<GalleryEntity>, List<Gallery>>(_repository.Get<GalleryEntity>().ToList());
            foreach (Gallery gallery in galleries)
            {
                Gallery gallery1 = gallery;
                gallery.Images = Mapper.Map<List<ImageEntity>, List<Image>>(
                    _repository.Get<ImageEntity>(x => x.GalleryID == gallery1.GalleryId).ToList());
            }

            return galleries;
        }
    }
}