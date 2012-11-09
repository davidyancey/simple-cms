using AgileDevDays.Core.Challenge;
using AgileDevDays.Core.Content;
using AgileDevDays.Core.ImageGallery;
using AgileDevDays.Core.Models;
using AgileDevDays.Infrastructure.Models;
using AutoMapper;

namespace AgileDevDays.Core
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
            Mapper.CreateMap<ChallengeEntity, ChallengeEntry>();
            Mapper.CreateMap<ChallengeEntry, ChallengeEntity>();
        }
    }
}