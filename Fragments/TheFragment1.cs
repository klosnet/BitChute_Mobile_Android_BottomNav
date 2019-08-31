using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using BottomNavigationViewPager.Classes;
using System.Threading.Tasks;
using static Android.Views.View;
using static BottomNavigationViewPager.Classes.Globals;

namespace BottomNavigationViewPager.Fragments
{
    [Android.Runtime.Register("onWindowVisibilityChanged", "(I)V", "GetOnWindowVisibilityChanged_IHandler")]
    public class TheFragment1 : Fragment
    {
        string _title;
        string _icon;

        protected static WebView _wv;
        protected static View _view;
        protected static LayoutInflater _li;

        readonly ExtWebViewClient _wvc = new ExtWebViewClient();

        bool tabLoaded = false;
        //static MainActivity _main = new MainActivity();

        public static Globals _globals = new Globals();
        public static TheFragment2 _fm2 = new TheFragment2();
        public static MainActivity _main = new MainActivity();
        
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

            _li = inflater;

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
                
                //_wv.SetOnLongClickListener()

                tabLoaded = true;
            }

            _wv.SetOnScrollChangeListener(new ExtScrollListener());

            _wv.SetOnLongClickListener(new ExtClickListener());

            return _view;
        }

        //public static MainActivity _main = new MainActivity();


        public class ExtClickListener : Java.Lang.Object, View.IOnLongClickListener
        {
            public bool OnLongClick(View v)
            {
                Globals._lcp = true;
                return Globals._lcp;
            }
        }

        public class ExtScrollListener : Java.Lang.Object, View.IOnScrollChangeListener
        {
            public void OnScrollChange(View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
            {
                if (_globals.NavTimer() > 0)
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
        public static string _jsLinkFixer = "javascript:(function() { " +
            "document.getElementById('video-description').style.overflow='hidden'; " + "})()";
        
        /// <summary>
        /// this fixes the issue where links overflow,
        /// interfering with the viewpager
        /// </summary>
        public static async void FixLinkOverflow()
        {
            await Task.Delay(5000);

            _wv.LoadUrl(_jsLinkFixer);
        }

        public static string _jsPlayCancel = "javascript:(function() { " +
            "dismiss.click()" + "})()";

        //this does nothing as of now, but will eventually prevent autoplay
        //when the user is typing a comment
        public async void CancelAutoplay()
        {
            await Task.Run(() =>_wv.LoadUrl(_jsPlayCancel));
        }

        //I'll explain this later
        static int _autoInt = 0;

        /// <summary>
        /// long click listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool WebviewLongClickListener(object sender, LongClickEventArgs e)
        {
            Globals._tabUrlSender = 0;
            Globals._lcp = true;
            return Globals._lcp;
        }

        public void ManualWebViewLoad(string url)
        {
            _wv.LoadUrl(url);
        }
        
        private class ExtWebViewClient : WebViewClient
        {

            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                return base.ShouldOverrideUrlLoading(view, request);
            }

            public override WebResourceResponse ShouldInterceptRequest(WebView view, string url)
            {
                if (Globals._lcp)
                {
                    if (Globals._tabUrlSender == 0)
                    {
                            _fm2.ExtendedLoadUrl(url);
                            _main.SelectedTabOverride(Globals._currentTab);
                            url = "javascript:(function() { " +
                                "document.getElementById('video-description').style.overflow='hidden'; " + "})()";
                    }
                    Globals._lcp = false;
                }
                return base.ShouldInterceptRequest(view, url);
            }
            
            public override bool ShouldOverrideKeyEvent(WebView view, KeyEvent e)
            {
                if (e.KeyCode == Android.Views.Keycode.A || e.KeyCode == Android.Views.Keycode.B 
                    || e.KeyCode == Android.Views.Keycode.C || e.KeyCode == Android.Views.Keycode.D 
                    || e.KeyCode == Android.Views.Keycode.E || e.KeyCode == Android.Views.Keycode.F ||
                    e.KeyCode == Android.Views.Keycode.G || e.KeyCode == Android.Views.Keycode.H
                    || e.KeyCode == Android.Views.Keycode.I || e.KeyCode == Android.Views.Keycode.J 
                    || e.KeyCode == Android.Views.Keycode.K || e.KeyCode == Android.Views.Keycode.L ||
                    e.KeyCode == Android.Views.Keycode.M || e.KeyCode == Android.Views.Keycode.N
                    || e.KeyCode == Android.Views.Keycode.O || e.KeyCode == Android.Views.Keycode.P ||
                    e.KeyCode == Android.Views.Keycode.Q || e.KeyCode == Android.Views.Keycode.R ||
                    e.KeyCode == Android.Views.Keycode.S ||  e.KeyCode == Android.Views.Keycode.T ||
                    e.KeyCode == Android.Views.Keycode.U || e.KeyCode == Android.Views.Keycode.V ||
                    e.KeyCode == Android.Views.Keycode.X || e.KeyCode == Android.Views.Keycode.Y ||
                    e.KeyCode == Android.Views.Keycode.Z)
                {

                }

                return base.ShouldOverrideKeyEvent(view, e);
            }



            public override void OnPageFinished(WebView _view, string url)
            {
                base.OnPageFinished(_view, url);

                _wv.LoadUrl(JavascriptCommands._jsHideBanner);

                _wv.LoadUrl(JavascriptCommands._jsHideBuff);

                _wv.LoadUrl(JavascriptCommands._jsLinkFixer);

                if (!AppSettings._tab1FeaturedOn)
                {
                    _wv.LoadUrl(JavascriptCommands._jsHideCarousel);
                }

                //add one to the autoint... for some reason if Tab1 has 
                //_wv.Settings.MediaPlaybackRequiresUserGesture = false set, then it won't work on the other tabs.
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
