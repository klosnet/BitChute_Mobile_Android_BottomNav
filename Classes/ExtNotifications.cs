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
    class ExtNotifications
    {
        public static TheFragment5 _fm5 = new TheFragment5();

        public static List<string> _notificationTextList = new List<string>();

        public static List<string> _notificationTypes = new List<string>();

        public static List<string> _notificationLinks = new List<string>();

        public static List<string> _previousNotificationList = new List<string>();

        public static List<CustomNotification> _customNoteList = new List<CustomNotification>();
        
        public class CustomNotification
        {
            public string _noteType { get; set; }
            public string _noteText { get; set; }
            public string _noteLink { get; set; }
            public int _noteIndex { get; set; }
        }


        public List<CustomNotification> DecodeHtmlNotifications(string html)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='notification-unread']"))
                {
                    var _tagContents = node.InnerText;

                    var _viewer = _tagContents;

                    _notificationTypes.Add(_viewer);
                }
                 

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='notification-target']"))
                {
                    var _tagContents = node.InnerText;

                    var _viewer = _tagContents;



                    _notificationTextList.Add(_viewer);
                }

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a[@class='notification-view']"))
                {
                    var _tagContents = node.Attributes["href"].Value.ToString();

                    var _viewer = "https://bitchute.com" + _tagContents;

                    _notificationLinks.Add(_viewer);
                }


                _customNoteList.Clear();

                int currentListIndex = 0;

                foreach (var type in _notificationTypes)
                {
                    var note = new CustomNotification();
                    note._noteType = type.ToString();
                    note._noteLink = _notificationLinks[currentListIndex].ToString();
                    note._noteText = _notificationTextList[currentListIndex].ToString();
                    _customNoteList.Add(note);
                    currentListIndex++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //_fm5.SendNotifications();
            return _customNoteList;
        }
    }
}