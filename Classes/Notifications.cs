using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HtmlAgilityPack;

namespace BottomNavigationViewPager.Classes
{
    class Notifications
    {
        public static List<object> _notificationList = new List<object>();

        public static List<object> _previousNotificationList = new List<object>();

        public List<object> DecodeHtmlNotifications(string html)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='notification-target']"))
                {
                    var _tagContents = node.InnerText;

                    var _viewer = _tagContents;

                    _notificationList.Add(_viewer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _notificationList;
        }
    }
}