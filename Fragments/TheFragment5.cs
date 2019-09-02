using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using BottomNavigationViewPager.Classes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottomNavigationViewPager.Fragments
{
    public class TheFragment5 : Fragment
    {
        string _title;
        string _icon;

        public static WebView _wv;
        public static LinearLayout _wvLayout;
        public static LinearLayout _appSettingsLayout;
        public static View _view;
        public static AppSettings _appSettings = new AppSettings();

        public static List<object> _settingsList = new List<object>();
        public static Spinner _tab4OverrideSpinner;
        public static Spinner _tab5OverrideSpinner;

        bool tabLoaded = false;

        public static string _url = "https://www.bitchute.com/settings/";

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

            _wvLayout = _view.FindViewById<LinearLayout>(Resource.Id.webViewLayout);
            _appSettingsLayout = _view.FindViewById<LinearLayout>(Resource.Id.appSettingsMainLayout);
            //var _view2 = inflater.Inflate(Resource.Layout.SettingsFragmentLayout, container, false);

            if (!tabLoaded)
            {
                _wv.SetWebViewClient(new ExtWebViewClient());

                _wv.Settings.JavaScriptEnabled = true;

                _wv.Settings.DisplayZoomControls = false;

                _wv.Settings.MediaPlaybackRequiresUserGesture = false;

                _wv.LoadUrl(_url);

                //_wv.Settings.AllowFileAccess = true;

                //_wv.Settings.AllowContentAccess = true;
                
                var _ctx = Android.App.Application.Context;
          
                _prefs = Android.App.Application.Context.GetSharedPreferences("BitChute", FileCreationMode.Private);
                
                AppSettings._zcoffrb = _view.FindViewById<RadioButton>(Resource.Id._zoomControlOffBtn);
                AppSettings._zconrb = _view.FindViewById<RadioButton>(Resource.Id._zoomControlOnBtn);
                AppSettings._fmoffrb = _view.FindViewById<RadioButton>(Resource.Id._zoomControlOffBtn);
                AppSettings._fmonrb = _view.FindViewById<RadioButton>(Resource.Id._fanModeOnBtn);
                AppSettings._t3hoffrb = _view.FindViewById<RadioButton>(Resource.Id._tab3HideOverrideOff);
                AppSettings._t3honrb = _view.FindViewById<RadioButton>(Resource.Id._tab3HideOverrideOn);
                AppSettings._t1foffrb = _view.FindViewById<RadioButton>(Resource.Id._tab1FeaturedCreatorsOff);
                AppSettings._t1fonrb = _view.FindViewById<RadioButton>(Resource.Id._tab1FeaturedCreatorsOn);
                AppSettings._stoverrideoffrb = _view.FindViewById<RadioButton>(Resource.Id._stOverrideOffRb);
                AppSettings._stoverrideonrb = _view.FindViewById<RadioButton>(Resource.Id._stOverrideOnRb);
                AppSettings._tab4OverrideSpinner = _view.FindViewById<Spinner>(Resource.Id.tab4OverrideSpinner);
                AppSettings._tab5OverideSpinner = _view.FindViewById<Spinner>(Resource.Id.tab5OverrideSpinner);

                AppSettings._zcoffrb.CheckedChange += ExtSettingChanged;
                AppSettings._fmoffrb.CheckedChange += ExtSettingChanged;
                AppSettings._t3hoffrb.CheckedChange += ExtSettingChanged;
                AppSettings._t1foffrb.CheckedChange += ExtSettingChanged;
                AppSettings._stoverrideoffrb.CheckedChange += ExtSettingChanged;
                AppSettings._tab4OverrideSpinner.ItemSelected += OnTab4OverrideSelectionChanged;
                AppSettings._tab5OverideSpinner.ItemSelected += OnTab5OverrideSelectionChanged;

                _appSettings.GetTabOverrideStringList();

                var tab4SpinOverrideAdapter = new ArrayAdapter<string>(_ctx,
                        Android.Resource.Layout.SimpleListItem1, AppSettings._tabOverrideStringList);

                AppSettings._tab4OverrideSpinner.Adapter = tab4SpinOverrideAdapter;

                var tab5SpinOverrideAdapter = new ArrayAdapter<string>(_ctx,
                        Android.Resource.Layout.SimpleListItem1, AppSettings._tabOverrideStringList);

                AppSettings._tab5OverideSpinner.Adapter = tab5SpinOverrideAdapter;

                tabLoaded = true;
            }
            _wv.SetOnScrollChangeListener(new ExtScrollListener());

            _appSettingsLayout.Visibility = ViewStates.Gone;
            
            return _view;
        }

        public void OnSettingsChanged(List<object> settings)
        {
            _wv.Settings.SetSupportZoom(Convert.ToBoolean(settings[0]));

            if (Convert.ToBoolean(settings[2]))
            {
                _wv.LoadUrl(Globals.JavascriptCommands._jsSelectTab);

                _wv.LoadUrl(Globals.JavascriptCommands._jsSelectTab2);

                _wv.LoadUrl(Globals.JavascriptCommands._jsSelectTab3);

                _wv.LoadUrl(Globals.JavascriptCommands._jsHideLabel);
            }
        }
        
        public void ShowAppSettingsMenu()
        {
            if (_wvLayout.Visibility == ViewStates.Visible)
            {
                _wvLayout.Visibility = ViewStates.Gone;
                _appSettingsLayout.Visibility = ViewStates.Visible;
            }
            else
            {
                _appSettingsLayout.Visibility = ViewStates.Gone;
                _wvLayout.Visibility = ViewStates.Visible;
            }
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
        
        private class ExtWebViewClient : WebViewClient
        {
            public override void OnPageFinished(WebView view, string url)
            {
                SetReload();
            }
        }

        public void OnTab4OverrideSelectionChanged(object sender, EventArgs e)
        {
            switch (_tab4OverrideSpinner.SelectedItemPosition)
            {
                case 0:
                    _main.TabDetailChanger(3, "subs");
                    break;
                case 1:
                    _main.TabDetailChanger(3, "feed");
                    break;
                case 2:
                    _main.TabDetailChanger(3, "home");
                    break;
                case 3:
                    _main.TabDetailChanger(3, "explore");
                    break;
            }
        }

        public void OnTab5OverrideSelectionChanged(object sender, EventArgs e)
        {
            switch (_tab5OverrideSpinner.SelectedItemPosition)
            {
                case 0:
                    _main.TabDetailChanger(4, "subs");
                    break;
                case 1:
                    _main.TabDetailChanger(4, "feed");
                    break;
                case 2:
                    _main.TabDetailChanger(4, "home");
                    break;
                case 3:
                    _main.TabDetailChanger(4, "explore");
                    break;
            }
        }
        public static Android.Content.ISharedPreferences _prefs;
        public static Android.Content.ISharedPreferencesEditor _prefEditor;

        /// <summary>
        /// called when the .Checked state of radio buttons in the app settings fragment is changed
        /// sets the settings when this event occurs and calls a method to notify all fragments via mainactivity.
        ///writes the values to android preferences aswell using the api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExtSettingChanged(object sender, EventArgs e)
        {
            var prefEditor = _prefs.Edit();

            if (AppSettings._zcoffrb.Checked)
            {
                AppSettings._zoomControl = false;
            }
            else
            {
                AppSettings._zoomControl = true;
            }
            if (AppSettings._fmoffrb.Checked)
            {
                AppSettings._fanMode = false;
            }
            else
            {
                AppSettings._fanMode = true;
            }
            if (AppSettings._t3honrb.Checked)
            {
                AppSettings._tab3Hide = true;
            }
            else
            {
                AppSettings._tab3Hide = false;
            }
            if (AppSettings._t1fonrb.Checked)
            {
                AppSettings._tab1FeaturedOn = true;
            }
            else
            {
                AppSettings._tab1FeaturedOn = false;
            }
            if (AppSettings._stoverrideoffrb.Checked)
            {
                AppSettings._settingsTabOverride = false;
            }
            else
            {
                AppSettings._settingsTabOverride = true;
            }

            prefEditor.PutBoolean("zoomcontrol", AppSettings._zoomControl);
            prefEditor.PutBoolean("fanmode", AppSettings._fanMode);
            prefEditor.PutBoolean("tab3hide", AppSettings._tab3Hide);
            prefEditor.PutBoolean("t1featured", AppSettings._tab1FeaturedOn);
            prefEditor.PutBoolean("settingstaboverride", AppSettings._settingsTabOverride);
            prefEditor.Commit();
            
            _settingsList.Add(AppSettings._zoomControl);
            _settingsList.Add(AppSettings._fanMode);
            _settingsList.Add(AppSettings._tab3Hide);
            _settingsList.Add(AppSettings._tab1FeaturedOn);
            _settingsList.Add(AppSettings._settingsTabOverride);
            
            _main.OnSettingsChanged(_settingsList);
        }
    }
}
