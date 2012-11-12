using System;
using System.Collections.Generic;
using System.Data;
using AutoMapper;
using SimpleCms.Infrastructure.Entities;
using SimpleCms.Infrastructure;
using SimpleCms.Infrastructure.Models;
using SimpleCms.Core.Models;
using System.Linq;

namespace SimpleCms.Core.Managers.Content
{
    public class ContentManager : BaseManager
    {
        private readonly PageContentEntities _context = new PageContentEntities();
        private readonly Repository<PageContentEntities> _repository;

        public ContentManager(string connectionName) : base(connectionName)
        {
            _repository = new Repository<PageContentEntities>(_context, connectionName);
        }

        public int GetPageid(string sectionname, string areaname)
        {
            var applicationId = base.ApplicationId;
            return _repository.GetSingle<PageEntity>(x =>
                x.ApplicationId == applicationId 
                && x.SectionName == sectionname 
                && x.AreaName == areaname).PageId;
        }

        public List<SectionContent> GetPageContentList(int pageid)
        {
            List<PageContentEntity> contentlist = _repository.Get<PageContentEntity>(x => x.PageId == pageid).ToList();
            return Mapper.Map<List<SectionContent>>(contentlist);
        }

        public SectionContent GetPageContent(int contentid)
        {
            return Mapper.Map<SectionContent>(_repository.GetSingle<PageContentEntity>(x => x.ContentId == contentid));
        }

        public SectionContent CreateNewContent(SectionContent content)
        {
            var results = _repository.Add(Mapper.Map<PageContentEntity>(content));
            _repository.SaveChanges();
            return Mapper.Map<SectionContent>(results);
        }

        public void UpdatePageContent(SectionContent content)
        {
            _repository.Update(Mapper.Map<PageContentEntity>(content));
            _repository.SaveChanges();
        }
    }
}