using System;
using AutoMapper;
using SimpleCms.Infrastructure.Entities;
using SimpleCms.Infrastructure.Models;
using SimpleCms.Infrastructure;
using SimpleCms.Core.Membership;
using SimpleCms.Core.Enumerations;
using SimpleCms.Core.Models;
using SimpleCms.Core.Managers;

namespace SimpleCms.Core.Managers.Membership
{
    public class MembershipManager : BaseManager
    {
        private readonly ImageGalleryEntities _imagegallerycontext = new ImageGalleryEntities();
        private Repository<ImageGalleryEntities> _imagerepository;
        

        public MembershipManager(string connectionName): base(connectionName)
        {
            _imagerepository = new Repository<ImageGalleryEntities>(_imagegallerycontext, connectionName);            
        }

        public void UpdateMemberAuthenticationSource(string userID, ApplicationSource applicationSource)
        {
            Guid applicationId = base.ApplicationId;

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