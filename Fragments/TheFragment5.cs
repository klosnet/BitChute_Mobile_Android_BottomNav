﻿using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using System;
using System.Threading.Tasks;
using static BottomNavigationViewPager.Classes.Globals;

namespace BottomNavigationViewPager.Fragments
{
    public class TheFragment5 : Fragment
    {
        string _title;
        string _icon;

        public static WebView _wv;
        public static View _view;
        public static ViewGroup _vg;
        public static LayoutInflater _inflater;

        bool tabLoaded = false;

        public static TheFragment5 NewInstance(string title, string icon)
        {
            var fragment = new TheFragment5();
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
            _view = inflater.Inflate(Resource.Layout.TheFragmentLayout5, container, false);

            _wv = _view.FindViewById<WebView>(Resource.Id.webView5);

            if (!tabLoaded)
            {
                _wv.SetWebViewClient(new ExtWebViewClient());

                _wv.Settings.JavaScriptEnabled = true;

                _wv.Settings.MediaPlaybackRequiresUserGesture = false;

                _wv.LoadUrl(@"https://www.bitchute.com/settings/");

                //_wv.Settings.AllowFileAccess = true;

                //_wv.Settings.AllowContentAccess = true;

                tabLoaded = true;
            }
            _wv.SetOnScrollChangeListener(new ExtScrollListener());

            return _view;
        }

        public void ShowAppSettingsView()
        {
            _view = _inflater.Inflate(Resource.Layout.AppSettingsFragmentLayout, _vg, false);
            
        }
        
        public static MainActivity _main = new MainActivity();

        public class ExtScrollListener : Java.Lang.Object, View.IOnScrollChangeListener
        {
            public void OnScrollChange(View v, int scrollX, int scrollY, int oldScrollX, int oldScrollY)
            {
                _main.CustomOnScroll();
            }
        }

        public void WebViewGoBack()
        {
            if (_wv.CanGoBack())
                _wv.GoBack();
        }

        public static int mysteryInt = 0;

        static bool _wvRl = true;

        public void Pop2Root()
        {
            mysteryInt++;
            
            if (mysteryInt == 6)
            {
                _wv.LoadUrl(@"https://www.soundcloud.com/vybemasterz/");
            }
            else
            {
                if (_wvRl)
                {
                    try
                    {
                        _wv.Reload();
                        _wvRl = false;
                    }
                    catch (Exception ex)
                    {
                         
                    }
                }   
                else
                {
                    _wv.LoadUrl(@"https://www.bitchute.com/settings/");
                }
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

                await Task.Delay(2666);

                mysteryInt = 0;

                _wvRling = false;
            }
        }

        /// <summary>
        /// this fixes the issue where links overflow,
        /// interfering with the viewpager
        /// </summary>
        public static async void FixLinkOverflow()
        {
            await Task.Delay(5000);

            _wv.LoadUrl(JavascriptCommands._jsLinkFixer);
        }

        private class ExtWebViewClient : WebViewClient
        {
            public override void OnPageFinished(WebView view, string url)
            {

                _wv.LoadUrl(JavascriptCommands._jsLinkFixer);

                SetReload();

                FixLinkOverflow();
            }
        }
    }
}
