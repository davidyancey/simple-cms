using System;
using AgileDevDays.Core.Enumerations;
using AgileDevDays.Core.ImageGallery;
using AgileDevDays.Core.Models;
using AgileDevDays.Infrastructure;
using AgileDevDays.Infrastructure.Entities;
using AgileDevDays.Infrastructure.Models;
using AutoMapper;

namespace AgileDevDays.Core.Membership
{
    public class MembershipManager
    {
        private readonly ApplicationSourceEntities _context = new ApplicationSourceEntities();
        private readonly Repository<ApplicationSourceEntities> _repository;
        private readonly ImageGalleryEntities _imagegallerycontext = new ImageGalleryEntities();
        private Repository<ImageGalleryEntities> _imagerepository;

        public MembershipManager()
        {
            _repository = new Repository<ApplicationSourceEntities>(_context);
            _imagerepository = new Repository<ImageGalleryEntities>(_imagegallerycontext);
        }

        public void UpdateMemberAuthenticationSource(string userID, ApplicationSource applicationSource)
        {
            Guid applicationId = GetApplicationId();

            var source = new AuthenticationSourceEntity() 
                                               {
                                                   ApplicationId = applicationId,
                                                   UserId = Guid.Parse(userID),
                                                   AuthenticationSource = applicationSource.ToString(),
                                                   AuthenticationSourceID = Guid.NewGuid()
                                               };

            _repository.Add(source);
            _repository.SaveChanges();
        }

        public Guid GetApplicationId()
        {
            return _repository.GetSingle<ApplicationEntity>(x => x.ApplicationName == "/AgileDevDays").ApplicationId;
        }

        public MemberProfile GetMemberProfile(Guid userId)
        {
            MemberProfile profile = (MemberProfile)System.Web.Security.Membership.GetUser(userId) ?? new MemberProfile();
            profile.ProfileImage =
                    Mapper.Map<ImageEntity, Image>(
                        _imagerepository.GetSingle<ImageEntity>(x => x.UserId == userId && x.IsPrimary));
            return profile;
        }
    }
}