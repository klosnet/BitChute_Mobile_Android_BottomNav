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

        public static List<object> _settingsList = new List<object>();
        public static Spinner _tab4OverrideSpinner;
        public static Spinner _tab5OverrideSpinner;


        public static bool _zoomControl { get; set; }
        public static bool _tab1FeaturedOn  { get; set; }
        public static bool _fanMode  { get; set; }
        public static bool _tab3Hide { get; set; }
        public static bool _settingsTabOverride { get; set; }

        public static string _tab4OverridePreference { get; set; }
        public static string _tab5OverridePreference { get; set; }

        bool tabLoaded = false;

        public static string _tab5Title = "";
        public static string _url = "https://www.bitchute.com/settings/";

        public static RadioButton _fmoffrb;
        public static RadioButton _fmonrb;

        public static RadioButton _zcoffrb;
        public static RadioButton _zconrb;

        public static RadioButton _t3honrb;
        public static RadioButton _t3hoffrb;

        public static RadioButton _t1fonrb;
        public static RadioButton _t1foffrb;

        public static RadioButton _stoverrideoffrb;
        public static RadioButton _stoverrideonrb;

        public static List<string> _tabOverrideStringList = new List<string>();
        ArrayAdapter<string> _tab4SpinOverrideAdapter;
        ArrayAdapter<string> _tab5SpinOverrideAdapter;

        public static TheFragment5 _fm5;

        public static TheFragment5 NewInstance(string title, string icon)
        {
            var fragment = new TheFragment5();
            fragment.Arguments = new Bundle();
            fragment.Arguments.PutString("title", title);
            fragment.Arguments.PutString("icon", icon);
            _fm5 = fragment;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            _tabOverrideStringList.Add("Home");
            _tabOverrideStringList.Add("Subs");
            _tabOverrideStringList.Add("Feed");
            _tabOverrideStringList.Add("Explore");
            _tabOverrideStringList.Add("Settings");
            _tabOverrideStringList.Add("MyChannel");

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
                
                _zcoffrb = _view.FindViewById<RadioButton>(Resource.Id._zoomControlOffBtn);
                _zconrb = _view.FindViewById<RadioButton>(Resource.Id._zoomControlOnBtn);
                _fmoffrb = _view.FindViewById<RadioButton>(Resource.Id._zoomControlOffBtn);
                _fmonrb = _view.FindViewById<RadioButton>(Resource.Id._fanModeOnBtn);
                _t3hoffrb = _view.FindViewById<RadioButton>(Resource.Id._tab3HideOverrideOff);
                _t3honrb = _view.FindViewById<RadioButton>(Resource.Id._tab3HideOverrideOn);
                _t1foffrb = _view.FindViewById<RadioButton>(Resource.Id._tab1FeaturedCreatorsOff);
                _t1fonrb = _view.FindViewById<RadioButton>(Resource.Id._tab1FeaturedCreatorsOn);
                _stoverrideoffrb = _view.FindViewById<RadioButton>(Resource.Id._stOverrideOffRb);
                _stoverrideonrb = _view.FindViewById<RadioButton>(Resource.Id._stOverrideOnRb);
                _tab4OverrideSpinner = _view.FindViewById<Spinner>(Resource.Id.tab4OverrideSpinner);
                _tab5OverrideSpinner = _view.FindViewById<Spinner>(Resource.Id.tab5OverrideSpinner);

                _zcoffrb.CheckedChange += ExtSettingChanged;
                _fmonrb.CheckedChange += ExtSettingChanged;
                _t3hoffrb.CheckedChange += ExtSettingChanged;
                _t1foffrb.CheckedChange += ExtSettingChanged;
                _stoverrideonrb.CheckedChange += OnTab5OverrideChanged;
                _tab4OverrideSpinner.ItemSelected += OnTab4OverrideSelectionChanged;
                _tab5OverrideSpinner.ItemSelected += OnTab5OverrideSelectionChanged;
                
                _tab4SpinOverrideAdapter = new ArrayAdapter<string>(_ctx,
                        Android.Resource.Layout.SimpleListItem1, _tabOverrideStringList);

                _tab4OverrideSpinner.Adapter = _tab4SpinOverrideAdapter;

                _tab5SpinOverrideAdapter = new ArrayAdapter<string>(_ctx,
                        Android.Resource.Layout.SimpleListItem1, _tabOverrideStringList);

                _tab5OverrideSpinner.Adapter = _tab5SpinOverrideAdapter;

                tabLoaded = true;
            }
            _wv.SetOnScrollChangeListener(new ExtScrollListener());

            _appSettingsLayout.Visibility = ViewStates.Gone;

            SetCheckedState();

            return _view;
        }

        public void OnSettingsChanged(List<object> settings)
        {
            _wv.Settings.SetSupportZoom(Convert.ToBoolean(settings[0]));

            if (Convert.ToBoolean(settings[2]))
            {
                _wv.LoadUrl(Globals.JavascriptCommands._jsHideTab1);
                _wv.LoadUrl(Globals.JavascriptCommands._jsHideTab2);
                _wv.LoadUrl(Globals.JavascriptCommands._jsSelectTab3);
                //_wv.LoadUrl(Globals.JavascriptCommands._jsHideLabel);
            }
        }

        public void SetCheckedState()
        {
            TheFragment5._zoomControl = _prefs.GetBoolean("zoomcontrol", false);
            TheFragment5._fanMode = _prefs.GetBoolean("fanmode", false);
            TheFragment5._tab3Hide = _prefs.GetBoolean("tab3hide", true);
            TheFragment5._tab1FeaturedOn = _prefs.GetBoolean("t1featured", true);
            TheFragment5._settingsTabOverride = _prefs.GetBoolean("settingstaboverride", false);

                if (_zoomControl)
                {

                    _zconrb.Checked = true;
                    var _test = _zconrb.Checked;
                }
                else
                {
                    _zconrb.Checked = false;
                    _zcoffrb.Checked = true;
                }
                if (_fanMode)
                {
                    _fmoffrb.Checked = false;
                    _fmoffrb.Checked = true;
                }
                else
                {
                    _fmoffrb.Checked = true;
                    _fmonrb.Checked = false;
                }
                if (_tab1FeaturedOn)
                {
                    _t1foffrb.Checked = false;
                    _t1fonrb.Checked = true;
                }
                else
                {
                    _t1foffrb.Checked = true;
                    _t1fonrb.Checked = true;
                }
                if (_tab3Hide)
                {
                    _t3hoffrb.Checked = false;
                    _t3honrb.Checked = true;
                }
                else
                {
                    _t3hoffrb.Checked = true;
                    _t3honrb.Checked = false;
                }
                if (_settingsTabOverride)
                {
                    _stoverrideoffrb.Checked = false;
                    _stoverrideonrb.Checked = true;
                }
                else
                {
                    _stoverrideoffrb.Checked = true;
                    _stoverrideonrb.Checked = false;
                }

        }

        /// <summary>
        /// shows the app specific settings menu
        /// when user long presses "settings" tab
        /// </summary>
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
                    _wv.LoadUrl(_url);
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

        public void OnTab5OverrideChanged(object sender, EventArgs e)
        {
            if (_stoverrideonrb.Checked)
            {
                _settingsTabOverride = true;
            }
            else
            {
                _settingsTabOverride = false;
            }
            var prefEditor = _prefs.Edit();

            var ch = _settingsTabOverride;
            prefEditor.PutBoolean("settingstaboverride", _settingsTabOverride);
        }

        public void OnTab4OverrideSelectionChanged(object sender, EventArgs e)
        {
            _zconrb.Checked = true;

            _tab4OverrideSpinner = _view.FindViewById<Spinner>(Resource.Id.tab4OverrideSpinner);

            if (_tab4OverrideSpinner != null)
            {
                _tab4OverridePreference = _tab4OverrideSpinner.SelectedItem.ToString();
                _main.TabDetailChanger(3, _tab4OverrideSpinner.SelectedItem.ToString());
                Globals._t4Is = _tab4OverrideSpinner.SelectedItemId.ToString();
            }
        }

        public void OnTab5OverrideSelectionChanged(object sender, EventArgs e)
        {
            _tab5OverrideSpinner = _view.FindViewById<Spinner>(Resource.Id.tab5OverrideSpinner);

            if (_tab5OverrideSpinner != null)
            {
                _tab5OverridePreference = _tab5OverrideSpinner.SelectedItem.ToString();
                _main.TabDetailChanger(4, _tab5OverrideSpinner.SelectedItem.ToString());
                Globals._t5Is = _tab5OverrideSpinner.SelectedItemId.ToString();
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

            if (_tab4OverrideSpinner.SelectedItem == null)
            {
                return;
            }
            _tab4OverridePreference = _tab4OverrideSpinner.SelectedItem.ToString();
            _tab5OverridePreference = _tab5OverrideSpinner.SelectedItem.ToString();

            var prefEditor = _prefs.Edit();
            if (tabLoaded)
            {
                if (_zconrb.Checked)
                {
                    _zoomControl = true;
                }
                else
                {
                    _zoomControl = false;
                }

                if (_fmonrb.Checked)
                {
                    _fanMode = true;

                    _main.TabDetailChanger(3, _tab4OverridePreference);
                }
                else
                {
                    _fanMode = false;
                }
                if (_t3honrb.Checked)
                {
                    _tab3Hide = true;
                }
                else
                {
                    _tab3Hide = false;
                }
                if (_t1fonrb.Checked)
                {
                    _tab1FeaturedOn = true;
                }
                else
                {
                    _tab1FeaturedOn = false;
                }
                if (_stoverrideonrb.Checked)
                {
                    _settingsTabOverride = true;

                    _main.TabDetailChanger(4, _tab5OverridePreference);
                }
                else
                {
                    _settingsTabOverride = false;

                }

                prefEditor.PutBoolean("zoomcontrol", _zoomControl);
                prefEditor.PutBoolean("fanmode", _fanMode);
                prefEditor.PutBoolean("tab3hide", _tab3Hide);
                prefEditor.PutBoolean("t1featured", _tab1FeaturedOn);
                var ch = _settingsTabOverride;
                prefEditor.PutBoolean("settingstaboverride", _settingsTabOverride);
                prefEditor.Commit();

                _settingsList.Clear();
                _settingsList.Add(_zoomControl);
                _settingsList.Add(_fanMode);
                _settingsList.Add(_tab3Hide);
                _settingsList.Add(_tab1FeaturedOn);
                _settingsList.Add(_settingsTabOverride);

                _main.OnSettingsChanged(_settingsList);
            }
        }
    }
}
