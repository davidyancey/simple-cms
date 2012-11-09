using System;
using System.IO;
using System.Web;
using System.Web.WebPages;

namespace AgileDevDays.Core.Helper
{
    public class Social : HelperPage
    {
        public static HelperResult TweetFollowButton(string userName = "", string imageUrl = "http://twitter-badges.s3.amazonaws.com/t_logo-a.png", string dataCount = "vertical", string shareText = "Tweet", 
            string tweetText = "", string url = "", string language = "", 
            string relatedUsername = "", string relatedUserDescription = "")
        {
            return new HelperResult((Action<TextWriter>) (x =>
            {
                HelperPage.WriteLiteralTo(x,(object)string.Format("<a href=\"http://www.twitter.com/{0}\">",userName));
                HelperPage.WriteLiteralTo(x,
                    (object)string.Format("<img src=\"{0}\" alt=\"Follow me @{1}\"/>", imageUrl, userName));
                HelperPage.WriteLiteralTo(x, (object)"</a>");
            }));
        }

        public static HelperResult FacebookFollowButton(string pageUrl = "", string imageUrl = "")
        {
            return new HelperResult((Action<TextWriter>)(x =>
            {
                HelperPage.WriteLiteralTo(x, (object)string.Format("<a href=\"{0}\">", pageUrl));
                HelperPage.WriteLiteralTo(x,
                    (object)string.Format("<img src=\"{0}\"/>", imageUrl));
                HelperPage.WriteLiteralTo(x, (object)"</a>");
            }));

        }

        public static HelperResult TweetButton(string userName = "", string imageUrl = "http://twitter-badges.s3.amazonaws.com/t_logo-a.png", string dataCount = "vertical", string shareText = "Tweet",
            string tweetText = "", string url = "", string language = "",
            string relatedUsername = "", string relatedUserDescription = "")
        {
            return new HelperResult((Action<TextWriter>)(x =>
            {
                HtmlString local_0 = new HtmlString(!tweetText.IsEmpty()
                    ? string.Format(" data-text=\"{0}\"", (object)HttpUtility.HtmlAttributeEncode(tweetText)) : "");
                HtmlString local_1 = new HtmlString(!url.IsEmpty()
                    ? string.Format(" data-url=\"{0}\"", (object)HttpUtility.HtmlAttributeEncode(url)) : "");
                HtmlString local_2 = new HtmlString(language.IsEmpty() ||
                        language.Equals("en", StringComparison.OrdinalIgnoreCase) ? ""
                        : string.Format(" data-lang=\"{0}\"", (object)HttpUtility.HtmlAttributeEncode(language)));
                HtmlString local_3 = new HtmlString(!userName.IsEmpty()
                    ? string.Format(" data-via=\"{0}\"", (object)HttpUtility.HtmlAttributeEncode(userName)) : "");
                HtmlString local_4 = new HtmlString(!relatedUsername.IsEmpty()
                    ? string.Format(" data-related=\"{0}{1}\"",
                    (object)HttpUtility.HtmlAttributeEncode(relatedUsername),
                    !StringExtensions.IsEmpty(relatedUserDescription)
                    ? (object)(":" + HttpUtility.HtmlAttributeEncode(relatedUserDescription)) : (object)"") : "");
                HelperPage.WriteLiteralTo(x, (object)"<a href=\"http://twitter.com/share\"");
                HelperPage.WriteTo(x, (object)local_0);
                HelperPage.WriteTo(x, (object)local_1);
                HelperPage.WriteTo(x, (object)local_2);
                HelperPage.WriteTo(x, (object)local_3);
                HelperPage.WriteTo(x, (object)local_4);
                HelperPage.WriteLiteralTo(x, (object)" data-count=\"");
                HelperPage.WriteTo(x, (object)((object)dataCount).ToString().ToLower());
                HelperPage.WriteLiteralTo(x, (object)"\">");
                HelperPage.WriteTo(x, (object)shareText);
                HelperPage.WriteLiteralTo(x, (object)"</a>");
                HelperPage.WriteLiteralTo(x,
                    (object)string.Format("<img src=\"{0}\" alt=\"Follow me @{1}\"/>", imageUrl, userName));
            }));
        }


    }
}