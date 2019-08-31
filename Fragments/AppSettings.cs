using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;

namespace BottomNavigationViewPager.Fragments
{
    /// <summary>
    /// this fragment contains both the UI and settings for app specific items.
    /// it is not to be confused with Globals, which is used for instance app states. 
    /// </summary>
    public class AppSettings : Fragment
    {
        string _title;
        string _icon;
        bool _tabLoaded = false;

        public static bool _zoomControl
        {
            get
            {
                return _zoomControl;
            }
            set
            {
                if (_zcoffrb != null)
                {
                    if (!_zoomControl)
                    {
                        _zcoffrb.Checked = true;
                        _zconrb.Checked = false;
                    }
                    else
                    {
                        _zcoffrb.Checked = false;
                        _zconrb.Checked = true;
                    }
                }
            }
        }

        public static bool _fanMode
        {
            get
            {
                return _fanMode;
            }
            set
            {
                if (_fmoffrb != null)
                {
                    if (_fanMode)
                    {
                        _fmoffrb.Checked = false;
                        _fmonrb.Checked = true;
                    }
                    else
                    {
                        _fmoffrb.Checked = true;
                        _fmonrb.Checked = false;
                    }
                }
            }
        }

        public static bool _tab3Hide
        {
            get
            {
                return _tab3Hide;
            }
            set
            {
                if (_t3honrb != null)
                {
                    if (_tab3Hide)
                    {
                        _t3honrb.Checked = true;
                        _t3hoffrb.Checked = false;
                    }
                    else
                    {

                        _t3honrb.Checked = false;
                        _t3hoffrb.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// gets, sets whether or not tab1 has featured creators.
        /// set true when user desires featured creators on homepage.
        /// </summary>
        public static bool _tab1FeaturedOn
        {
            get
            {
                return _tab1FeaturedOn;
            }
            set
            {
                if (_t1foffrb != null)
                {
                    if (_tab1FeaturedOn)
                    {
                        _t1fonrb.Checked = true;
                        _t1foffrb.Checked = false;
                    }
                    else
                    {

                        _t1fonrb.Checked = false;
                        _t1foffrb.Checked = true;
                    }
                }
            }
        }

        public static bool _settingsTabOverride
        {
            get
            {
                return _settingsTabOverride;
            }
            set
            {
                if (_stoverrideoffrb != null)
                {
                    if (!_settingsTabOverride)
                    {
                        _stoverrideoffrb.Checked = true;
                        _stoverrideonrb.Checked = false;
                    }
                    else
                    {
                        _stoverrideoffrb.Checked = false;
                        _stoverrideonrb.Checked = true;
                    }
                }
            }
        }

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

        public static int _settingsTabOverridePref { get; set; }

        public static string _zoomControlString;
        public static string _fanModeString;
        public static string _tab3HideString;
        public static string _tab1FeaturedString;
        public static string _settingsTabOverrideString;
        

        public void AssignSettingsFromFile()
        {
            string[] sa = System.IO.File.ReadAllLines("preferences.cfg");

            if (sa[0] == "zoomcontrol=off")
            {
                _zoomControl = false;
            }
            else
            {
                _zoomControl = true;
            }
            if (sa[1] == "fanmode=off")
            {
                _fanMode = false;
            }
            else
            {
                _fanMode = true;
            }
            if (sa[2] == "tab3hide=on")
            {
                _tab3Hide = true;
            }
            else
            {
                _tab3Hide = false;
            }
            if (sa[3] == "tab1featured=on")
            {
                _tab1FeaturedOn = true;
            }
            else
            {
                _tab1FeaturedOn = false;
            }
            if (sa[4] == "settingstaboverride=off")
            {
                _settingsTabOverride = false;
            }
            else
            {
                _settingsTabOverride = true;
            }
        }

        /// <summary>
        /// writes the app settings to file 'preferences.cfg'
        /// </summary>
        public void WriteSettingsToFile()
        {
            if (_zoomControl == false)
            {
                _zoomControlString = "zoomcontrol=off" + "\r\n";
            }
            else
            {
                _zoomControlString = "zoomcontrol=on" + "\r\n";
            }
            if (_fanMode == false)
            {
                _fanModeString = "fanmode=off" + "\r\n";
            }
            else
            {
                _fanModeString = "fanmode=on" + "\r\n";
            }
            if (_tab3Hide == true)
            {
                _tab3HideString = "tab3hide=on" + "\r\n";
            }
            else
            {
                _tab3HideString = "tab3hide=off" + "\r\n";
            }
            if (_tab1FeaturedOn)
            {
                _tab1FeaturedString = "tab1featured=on" + "\r\n";
            }
            else
            {
                _tab1FeaturedString = "tab1featured=off" + "\r\n";
            }
            if (!_settingsTabOverride)
            {
                _settingsTabOverrideString = "settingstaboverride=off" + "\r\n";
            }
            else
            {
                _settingsTabOverrideString = "settingstaboverride=on" + "\r\n";
            }

            string[] _settingsStringArray = { _zoomControlString, _fanModeString, _tab3HideString, _tab1FeaturedString, _settingsTabOverrideString };

            System.IO.File.Delete("preferences.cfg");

            System.IO.File.WriteAllLines("preferences.cfg", _settingsStringArray);
        }

        public static AppSettings NewInstance(string title, string icon)
        {
            var fragment = new AppSettings();
            fragment.Arguments = new Bundle();
            fragment.Arguments.PutString("title", title);
            fragment.Arguments.PutString("icon", icon);
            return fragment;
        }

        /// <summary>
        /// should be called when the app loads
        /// </summary>
        /// <returns></returns>
        public void OnAppLoaded()
        {
            AssignSettingsFromFile();
        }

        public void ExtSettingChanged(object sender, EventArgs e)
        {
            if (_zcoffrb.Checked)
            {
                _zoomControlString = "zoomcontrol=off" + "\r\n";
            }
            else
            {
                _zoomControlString = "zoomcontrol=on" + "\r\n";
            }
            if (_fmoffrb.Checked)
            {
                _fanModeString = "fanmode=off" + "\r\n";
            }
            else
            {
                _fanModeString = "fanmode=on" + "\r\n";
            }
            if (_t3honrb.Checked)
            {
                _tab3HideString = "tab3hide=on" + "\r\n";
            }
            else
            {
                _tab3HideString = "tab3hide=off" + "\r\n";
            }
            if (_t1fonrb.Checked)
            {
                _tab1FeaturedString = "tab1featured=on" + "\r\n";
            }
            else
            {
                _tab1FeaturedString = "tab1featured=off" + "\r\n";
            }
            if (_stoverrideoffrb.Checked)
            {
                _settingsTabOverrideString = "settingstaboverride=off" + "\r\n";
            }
            else
            {
                _settingsTabOverrideString = "settingstaboverride=on" + "\r\n";
            }

            WriteSettingsToFile();
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
            var _view = inflater.Inflate(Resource.Layout.AppSettingsFragmentLayout, container, false);
            
            if (!_tabLoaded)
            {
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


                _zcoffrb.CheckedChange += ExtSettingChanged;
                _fmoffrb.CheckedChange += ExtSettingChanged;
                _t3hoffrb.CheckedChange += ExtSettingChanged;
                _t1foffrb.CheckedChange += ExtSettingChanged;
                _stoverrideoffrb.CheckedChange += ExtSettingChanged;

                _tabLoaded = true;
            }
            return _view;
        }
    }
}
