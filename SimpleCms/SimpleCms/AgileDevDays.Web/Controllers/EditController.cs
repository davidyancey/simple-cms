using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileDevDays.Core.Content;
using AgileDevDays.Web.Models;

namespace AgileDevDays.Web.Controllers
{
    public class EditController : BaseController
    {
        public ActionResult Index(int contentId)
        {
            SectionContent content = ContentManager.GetPageContent(contentId);
            ContentModel model = new ContentModel()
                                     {
                                         ContentId = contentId,
                                         Content = content.ContentBody,
                                         Title = content.ContentTitle,
                                         AuthorId = content.ContentAuthor,
                                         PostedOn = DateTime.Now,
                                         PageId = content.PageId
                                     };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ContentModel model)
        {
            SectionContent content = new SectionContent
                                         {
                                             ContentAuthor = model.AuthorId,
                                             ContentBody = model.Content,
                                             ContentId = model.ContentId,
                                             ContentTitle = model.Title,
                                             CreateDate = DateTime.Now,
                                             PageId = model.PageId,
                                             PublishDate = model.PostedOn
                                         };

            ContentManager.UpdatePageContent(content);
            return View(model);
        }

    }
}
