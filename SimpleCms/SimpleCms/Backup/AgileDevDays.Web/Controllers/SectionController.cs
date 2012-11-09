using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AgileDevDays.Core.Content;
using AgileDevDays.Web.Models;
using AutoMapper;

namespace AgileDevDays.Web.Controllers
{
    public class SectionController : BaseController
    {
        public ActionResult Index(string area, string subarea = null)
        {
            var canEdit = true;// User.IsInRole("Admin");
            if (subarea == null)
                subarea = area;
            int pageid = ContentManager.GetPageid(area, subarea);
            List<SectionContent> contentList = ContentManager.GetPageContentList(pageid);
            List<ContentModel> pageContent = (from item in contentList
                                              let user = Membership.GetUser(item.ContentAuthor)
                                              let authorName = user == null ? string.Empty : user.UserName
                                              select new ContentModel
                                                         {
                                                             AuthorId = item.ContentAuthor,
                                                             Content = item.ContentBody,
                                                             ContentId = item.ContentId,
                                                             Title = item.ContentTitle,
                                                             PostedOn = item.PublishDate,
                                                             PageId = item.PageId,
                                                             AuthorName = authorName,
                                                             ShowEdit = canEdit
                                                         }).ToList();
            ViewBag.Title = area;
            if (pageContent.Count == 0)
                pageContent = CreateDefaultConent(pageid, canEdit);
            return View(pageContent);
        }

        

    }
}
