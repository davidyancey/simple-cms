using System;
using AutoMapper;
using SimpleCms.Infrastructure.Entities;
using SimpleCms.Infrastructure.Models;
using SimpleCms.Infrastructure;
using SimpleCms.Core.Membership;
using SimpleCms.Core.Enumerations;
using SimpleCms.Core.Models;

namespace SimpleCms.Core.Managers.Membership
{
    public class MembershipManager
    {
        private readonly ApplicationSourceEntities _context = new ApplicationSourceEntities();
        private readonly Repository<ApplicationSourceEntities> _repository;
        private readonly ImageGalleryEntities _imagegallerycontext = new ImageGalleryEntities();
        private Repository<ImageGalleryEntities> _imagerepository;
        private string _applicationName;

        public MembershipManager(string applicationName, string connectionName)
        {
            _repository = new Repository<ApplicationSourceEntities>(_context,connectionName);
            _imagerepository = new Repository<ImageGalleryEntities>(_imagegallerycontext, connectionName);
            _applicationName = string.Format("/{0}",applicationName);
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
            return _repository.GetSingle<ApplicationEntity>(x => x.ApplicationName == _applicationName).ApplicationId;
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