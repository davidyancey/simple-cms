using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimpleCms.Infrastructure.Entities;
using SimpleCms.Infrastructure;
using SimpleCms.Infrastructure.Models;
using SimpleCms.Core.Models;

namespace SimpleCms.Core.Managers.ImageGallery
{
    public class ImageGalleryManager : BaseManager
    {
        private readonly ImageGalleryEntities _context = new ImageGalleryEntities();
        private readonly Repository<ImageGalleryEntities> _repository;

        public ImageGalleryManager(string connectionName)
            : base(connectionName)
        {
            _repository = new Repository<ImageGalleryEntities>(_context, connectionName);
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
            var applicationId = base.ApplicationId;
            List<Gallery> galleries = Mapper.Map<List<GalleryEntity>, 
                    List<Gallery>>(_repository.Get<GalleryEntity>(x =>
                                    x.ApplicationId == applicationId).ToList());

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