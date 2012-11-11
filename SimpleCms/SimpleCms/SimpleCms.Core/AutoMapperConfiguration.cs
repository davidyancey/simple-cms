using SimpleCms.Core;
using SimpleCms.Infrastructure.Models;
using AutoMapper;
using SimpleCms.Core.Models;
using System.Collections.Generic;

namespace SimpleCms.Core
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Gallery, GalleryEntity>();
            Mapper.CreateMap<GalleryEntity, Gallery>();
            Mapper.CreateMap<Image, ImageEntity>();
            Mapper.CreateMap<ImageEntity, Image>();
            Mapper.CreateMap<SectionContent, PageContentEntity>();
            Mapper.CreateMap<PageContentEntity, SectionContent>();
        }
    }
}