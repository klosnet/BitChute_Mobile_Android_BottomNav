using System.Collections.Generic;
using Android.Content;

namespace BottomNavigationViewPager.Classes
{
    public class Globals
    {
        //   Android.App.ActivityManager _am = (Android.App.ActivityManager)Android.App.Application
        //         .Context.GetSystemService(Context.ActivityService);
        
        public static bool _setWebView { get; set; }
        public static bool _navIsTimingOut { get; set; }
        public static int _wvHeight { get; set; }
        
        public static Android.App.ActivityManager _am = (Android.App.ActivityManager)Android.App.Application
             .Context.GetSystemService(Context.ActivityService);
        
        /// <summary>
        /// global bool setting: 
        /// returns/should be set to false if this app is in the foreground
        /// returns/should be set to true when the app goes background
        /// 
        /// it doesn't override the OS setting; it keeps the status for you
        /// </summary>
        public static bool _bkgrd = true;

        /// <summary>
        /// returns false when the ActivityManager contains
        /// an entry for this app running in foreground: 
        /// importance is present in package.name with OS focus
        /// 
        /// requires a modified android manifest for get_task ALLOWED
        /// </summary>
        /// <returns>bool</returns>
        public bool IsInBkGrd()
        {
            var _ctx = Android.App.Application.Context;

            var runningAppProcesses = _am.RunningAppProcesses;

            List<Android.App.ActivityManager.RunningAppProcessInfo> list
                = new List<Android.App.ActivityManager.RunningAppProcessInfo>();

            list.AddRange(_am.RunningAppProcesses);

            foreach (var _process in list)
            {
                if (_process.Importance == Android.App.ActivityManager.RunningAppProcessInfo.ImportanceForeground)
                {
                    foreach (var _pkg in _process.PkgList)
                    {
                        if (_pkg == _ctx.PackageName)
                        {
                            _bkgrd = false;
                        }

                        else
                        {
                            _bkgrd = true;
                        }
                    }
                }
            }
            return _bkgrd;
        }


        /// <summary>
        /// this class contains javascript commands in the form of strings that
        /// can be run via LoadUrl
        /// </summary>
        public class JavascriptCommands
        {
            /// <summary>
            /// fixes the link overflow issue
            /// </summary>
            public static string _jsLinkFixer = "javascript:(function() { " +
                 "document.getElementById('video-description').style.overflow='hidden'; " + "})()";

            /// <summary>
            /// hides the static banner
            /// </summary>
            public static string _jsHideBanner = "javascript:(function() { " +
                 "document.getElementById('nav-top-menu').style.display='none'; " + "})()";

            /// <summary>
            /// hides the banner buffer
            /// </summary>
            public static string _jsHideBuff = "javascript:(function() { " +
                "document.getElementById('nav-menu-buffer').style.display='none'; " + "})()";

            /// <summary>
            /// hides the carousel aka featured creators
            /// </summary>
            public static string _jsHideCarousel = "javascript:(function() { " +
                "document.getElementById('carousel').style.display='none'; " + "})()";

            /// <summary>
            /// shows the carousel aka featured creators
            /// </summary>
            public static string _jsShowCarousel = "javascript:(function() { " +
                "document.getElementById('carousel').style.display='inherit'; " + "})()";
            
            /// <summary>
            /// hides the listing all element
            /// </summary>
            public static string _jsHideTab1 = "javascript:(function() { " +
                            "document.getElementById('listing-all').style.display='none'; " + "})()";
            
            /// <summary>
            /// shows the listing all element
            /// </summary>
            public static string _jsShowTab1 = "javascript:(function() { " +
                            "document.getElementById('listing-all').style.display='inherit'; " + "})()";
            
            /// <summary>
            /// hides the popular listings
            /// </summary>
            public static string _jsHideTab2 = "javascript:(function() { " +
                            "document.getElementById('listing-popular').style.display='none'; " + "})()";

            /// <summary>
            /// shows the popular listings
            /// </summary>
            public static string _jsShowTab2 = "javascript:(function() { " +
                            "document.getElementById('listing-popular').style.display='inherit'; " + "})()";
            
            /// <summary>
            /// shows the subscribed feed
            /// </summary>
            public static string _jsSelectTab3 = "javascript:(function() { " +
                            "document.getElementById('listing-subscribed').style.display='block'; " + "})()";
            
            /// <summary>
            /// hides the tab scroll inner element
            /// </summary>
            public static string _jsHideLabel = "javascript:(function() { " +
                            "document.getElementsByClassName('tab-scroll-inner')[0].style.display='none'; " + "})()";
            
            /// <summary>
            /// shows the tab scroll inner element
            /// </summary>
            public static string _jsShowLabel = "javascript:(function() { " +
                            "document.getElementsByClassName('tab-scroll-inner')[0].style.display='inherit'; " + "})()";

            /// <summary>
            /// hides the trending tab
            /// </summary>
            public static string _jsHideTrending = "javascript:(function() { " +
                            "document.getElementById('listing-trending').style.display='none'; " + "})()";
            
            /// <summary>
            /// shows the trending tab
            /// </summary>
            public static string _jsShowTrending = "javascript:(function() { " +
                            "document.getElementById('listing-trending').style.display='block'; " + "})()";
            //$('.show-more').click(function(){listingExtend(40);});

            public static string _jqShowMore = "javascript:(function() { " +
                            "document.listingExtend(40);" + "})()";

        }

        /// <summary>
        /// contains url strings
        /// </summary>
        public class URLs
        {
            public static string _homepage = "https://www.bitchute.com/";

            public static string _subspage = "https://www.bitchute.com/subscriptions/";

            public static string _explore =  "https://www.bitchute.com/channels/";

            public static string _settings = "https://www.bitchute.com/settings/";
        }
    }
}