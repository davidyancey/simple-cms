using System;
using System.Collections.Generic;
using System.Data;
using AgileDevDays.Infrastructure;
using AgileDevDays.Infrastructure.Entities;
using AgileDevDays.Infrastructure.Models;
using AutoMapper;

namespace AgileDevDays.Core.Content
{
    public class ContentManager
    {
        private readonly PageContentEntities _context = new PageContentEntities();
        private readonly Repository<PageContentEntities> _repository;

        public ContentManager()
        {
            _repository = new Repository<PageContentEntities>(_context);
        }

        public int GetPageid(string sectionname, string areaname)
        {
            return _repository.GetSingle<PageEntity>(x => x.SectionName == sectionname && x.AreaName == areaname).PageId;
        }

        public List<SectionContent> GetPageContentList(int pageid)
        {
            var contentlist = _repository.Get<PageContentEntity>(x => x.PageId == pageid);
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