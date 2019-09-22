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
using BottomNavigationViewPager.Fragments;
using HtmlAgilityPack;

namespace BottomNavigationViewPager.Classes
{
    class Notifications
    {
        public static TheFragment5 _fm5 = MainActivity._fm5;

        public static List<string> _notificationList = new List<string>();

        public static List<string> _previousNotificationList = new List<string>();

        public List<string> DecodeHtmlNotifications(string html)
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