using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AgileDevDays.Core.Content;
using AgileDevDays.Web.Models;

namespace AgileDevDays.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected internal readonly ContentManager ContentManager = new ContentManager();

        private int _pageContentId;
        public int PageContentId
        {
            get { return _pageContentId; }
            set { _pageContentId = value; }
        }

        public virtual int GetPageContentId(string sectionname, string areaname)
        {
            return ContentManager.GetPageid(sectionname, areaname);
        }

        public List<ContentModel> CreateDefaultConent(int pageid, bool canEdit)
        {
            SectionContent content = ContentManager.CreateNewContent(new SectionContent
            {
                ContentAuthor = Guid.Empty,
                ContentBody = "Default Content",
                ContentTitle = "Default Title",
                CreateDate = DateTime.Now,
                PageId = pageid,
                PublishDate = DateTime.Now
            });

            return new List<ContentModel>()
                       {
                           new ContentModel()
                               {
                                   ContentId = content.ContentId,
                                   Content = content.ContentBody,
                                   Title = content.ContentTitle,
                                   AuthorId = content.ContentAuthor,
                                   PostedOn = DateTime.Now,
                                   PageId = content.PageId,
                                   ShowEdit = canEdit
                               }
                       };
        }
    }
}
