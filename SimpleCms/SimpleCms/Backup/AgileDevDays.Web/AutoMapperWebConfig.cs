using System.Collections.Generic;
using AgileDevDays.Core.Challenge;
using AgileDevDays.Core.Content;
using AgileDevDays.Core.ImageGallery;
using AgileDevDays.Core.Membership;
using AgileDevDays.Web.Models;
using AutoMapper;

namespace AgileDevDays.Web
{
    public static class AutoMapperWebConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<Gallery, GalleryModel>();
            Mapper.CreateMap<Image, ImageModel>();
            Mapper.CreateMap<MemberProfile, ProfileModel>();
            Mapper.CreateMap<SectionContent, ContentModel>();
            Mapper.CreateMap<ChallengeEntry, ChallengeModel>();
            Mapper.CreateMap<ChallengeModel, ChallengeEntry>();
        }
    }
}