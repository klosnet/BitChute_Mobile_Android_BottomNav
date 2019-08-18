using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using System.Threading.Tasks;
using static Android.Views.View;

namespace BottomNavigationViewPager.Fragments
{
    [Android.Runtime.Register("onWindowVisibilityChanged", "(I)V", "GetOnWindowVisibilityChanged_IHandler")]
    public class TheFragment1 : Fragment
    {
        string _title;
        string _icon;

        protected static WebView _wv;
        protected static View _view;

        readonly ExtWebViewClient _wvc = new ExtWebViewClient();

        bool tabLoaded = false;

        //static MainActivity _main = new MainActivity();

        public static TheFragment1 NewInstance(string title, string icon)
        {
            var fragment = new TheFragment1();
            fragment.Arguments = new Bundle();
            fragment.Arguments.PutString("title", title);
            fragment.Arguments.PutString("icon", icon);
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null)
            {
                if (Arguments.ContainsKey("title"))
                    _title = (string)Arguments.Get("title");

                if (Arguments.ContainsKey("icon"))
                    _icon = (string)Arguments.Get("icon");
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _view = inflater.Inflate(Resource.Layout.TheFragmentLayout1, container, false);

            _wv = _view.FindViewById<WebView>(Resource.Id.webView1);


            if (!tabLoaded)
            {
                _wv.SetWebViewClient(_wvc);

                _wv.Settings.JavaScriptEnabled = true;

                //_wv.Settings.AllowFileAccess = true;

                //_wv.Settings.AllowContentAccess = true;

                //this didn't work when I put it here.  strange.. it would disable the setting on 
                //every other tab
                // _wv.Settings.MediaPlaybackRequiresUserGesture = false;

                _wv.LoadUrl(@"https://www.bitchute.com/");

                tabLoaded = true;
            }

            _wv.SetOnScrollChangeListener(new ExtScrollListener());

            return _view;
        }

        public static MainActivity _main = new MainActivity();

        public class ExtScrollListener : Java.Lang.Object, View.IOnScrollChangeListener
        {
            public void OnScrollChange(View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
            {
                _main.CustomOnScroll();
            }
        }

        /// <summary>
        /// tells the webview to GoBack, if it can
        /// </summary>
        public void WebViewGoBack()
        {
            if (_wv.CanGoBack())
                _wv.GoBack();
        }

        static bool _wvRl = true;

        /// <summary>
        /// one press refreshes the page; two presses pops back to the root
        /// </summary>
        public void Pop2Root()
        {
            if (_wvRl)
            {
                _wv.Reload();
                _wvRl = false;
            }
            else
            {
                _wv.LoadUrl(@"https://bitchute.com/");
            }
        }

        public static bool _wvRling = false;

        /// <summary>
        /// this is to allow faster phones and connections the ability to Pop2Root
        /// used to be set without delay inside OnPageFinished but I don't think 
        /// that would work on faster phones
        /// </summary>
        public static async void SetReload()
        {
            if (!_wvRling)
            {
                _wvRling = true;

                await Task.Delay(500);

                _wvRl = true;

                _wvRling = false;
            }
        }

        /// <summary>
        /// this fixes the issue where links overflow,
        /// interfering with the viewpager
        /// </summary>
        public static async void FixLinkOverflow()
        {
            string _jsLinkFixer = "javascript:(function() { " +
                "document.getElementById('video-description').style.overflow='hidden'; " + "})()";

            await Task.Delay(5000);

            _wv.LoadUrl(_jsLinkFixer);
        }

        //this does nothing as of now, but will eventually prevent autoplay
        //when the user is typing a comment
        public async void CancelAutoplay()
        {
            string _jsPlayCancel = "javascript:(function() { " +
                "dismiss.click()" + "})()";

            _wv.LoadUrl(_jsPlayCancel);
        }

        //I'll explain this later
        static int _autoInt = 0;

        private class ExtWebViewClient : WebViewClient
        {
            public static string _jsHideBanner = "javascript:(function() { " +
                "document.getElementById('nav-top-menu').style.display='none'; " + "})()";

            public static string _jsHideBuff = "javascript:(function() { " +
           "document.getElementById('nav-menu-buffer').style.display='none'; " + "})()";

            public static string _jsLinkFixer = "javascript:(function() { " +
            "document.getElementById('video-description').style.overflow='hidden'; " + "})()";

            public override void OnPageFinished(WebView _view, string url)
            {
                base.OnPageFinished(_view, url);

                _wv.LoadUrl(_jsHideBanner);

                _wv.LoadUrl(_jsHideBuff);

                _wv.LoadUrl(_jsLinkFixer);

                //add one to the autoint... for some reason if Tab1 has 
                //_wv.Settings.MediaPlaybackRequiresUserGesture = false; set then it won't work on the other tabs
                //this is a workaround for that glitch
                _autoInt++;

                // if autoInt is 1 then we will set the MediaPlaybackRequiresUserGesture
                //strange.. i know.. but it works
                if (_autoInt == 1)
                {
                    _wv.Settings.MediaPlaybackRequiresUserGesture = false;
                }

                SetReload();

                FixLinkOverflow();
            }
        }
    }
}
