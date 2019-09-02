
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace BottomNavigationViewPager.Fragments
{
    /// <summary>
    /// this fragment contains both the UI and settings for app specific items.
    /// it is not to be confused with Globals, which is used for instance app states. 
    /// </summary>
    public class AppSettings : Fragment
    {
        public static Android.Content.ISharedPreferences _prefs;
        public static Android.Content.ISharedPreferencesEditor _prefEditor;

        public static MainActivity _main = new MainActivity();
        public static AppSettings _appSettings;

        public static View _view = TheFragment5._view;

        public static List<string> _tab4OverrideStringList;
        public static List<string> _tab5OverrideStringList;

        public static List<object> _totalSettingList;

        public static List<string> _tabOverrideStringList
        {
            get
            {
                string s = "Subs";
                string f = "Feed";
                string e = "Explore";
                string h = "Home";
                _tabOverrideStringList.Add(s);
                _tabOverrideStringList.Add(f);
                _tabOverrideStringList.Add(e);
                _tabOverrideStringList.Add(h);
                return _tabOverrideStringList;
            }
            set
            {
                //nothing atm
            }
        }

        public AppSettings GetAppSettings()
        {
            return this;
        }

        public static bool _zoomControl = false;
        public static bool _fanMode = false;
        public static bool _tab3Hide = false;
        public static bool _tab1FeaturedOn = true;
        public static bool _settingsTabOverride = false;
        

        //public static bool _zoomControl
        //{
        //    get
        //    {
        //        return _zoomControl;
        //    }
        //    set
        //    {
        //        if (_zcoffrb != null)
        //        {
        //            if (!_zoomControl)
        //            {
        //                _zcoffrb.Checked = true;
        //                _zconrb.Checked = false;
        //            }
        //            else
        //            {
        //                _zcoffrb.Checked = false;
        //                _zconrb.Checked = true;
        //            }
        //        }
        //    }
        //}

        //public static bool _fanMode
        //{
        //    get
        //    {
        //        return _fanMode;
        //    }
        //    set
        //    {
        //        if (_fmoffrb != null)
        //        {
        //            if (_fanMode)
        //            {
        //                _fmoffrb.Checked = false;
        //                _fmonrb.Checked = true;
        //            }
        //            else
        //            {
        //                _fmoffrb.Checked = true;
        //                _fmonrb.Checked = false;
        //            }
        //        }
        //    }
        //}

        //public static bool _tab3Hide
        //{
        //    get
        //    {
        //        return _tab3Hide;
        //    }
        //    set
        //    {
        //        if (_t3honrb != null)
        //        {
        //            if (_tab3Hide)
        //            {
        //                _t3honrb.Checked = true;
        //                _t3hoffrb.Checked = false;
        //            }
        //            else
        //            {

        //                _t3honrb.Checked = false;
        //                _t3hoffrb.Checked = true;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// gets, sets whether or not tab1 has featured creators.
        ///// set true when user desires featured creators on homepage.
        ///// </summary>
        //public static bool _tab1FeaturedOn
        //{
        //    get
        //    {
        //        return _tab1FeaturedOn;
        //    }
        //    set
        //    {
        //        if (_t1foffrb != null)
        //        {
        //            if (_tab1FeaturedOn)
        //            {
        //                _t1fonrb.Checked = true;
        //                _t1foffrb.Checked = false;
        //            }
        //            else
        //            {

        //                _t1fonrb.Checked = false;
        //                _t1foffrb.Checked = true;
        //            }
        //        }
        //    }
        //}

        //public static bool _settingsTabOverride
        //{
        //    get
        //    {
        //        return _settingsTabOverride;
        //    }
        //    set
        //    {
        //        if (_stoverrideoffrb != null)
        //        {
        //            if (!_settingsTabOverride)
        //            {
        //                _stoverrideoffrb.Checked = true;
        //                _stoverrideonrb.Checked = false;
        //            }
        //            else
        //            {
        //                _stoverrideoffrb.Checked = false;
        //                _stoverrideonrb.Checked = true;
        //            }
        //        }
        //    }
        //}

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
        public static string _tab4OverridePreference;
        public static string _tab5OverridePreference;
        
        /// <summary>
        /// should be called when the app loads
        /// 
        /// sets all of the CheckChanged listeners for radiobuttons on the prefs 
        /// and gets all the booleans
        /// from app pref api
        /// </summary>
        /// <returns></returns>
        public void OnViewCreation()
        {
            //_tab4OverrideStringList = _tabOverrideStringList;
            //_tab5OverrideStringList = _tabOverrideStringList;

            _prefs = Android.App.Application.Context.GetSharedPreferences("BitChute", FileCreationMode.Private);
            _prefEditor = _prefs.Edit();
            
            _zoomControl = _prefs.GetBoolean("zoomcontrol", false);
            _fanMode = _prefs.GetBoolean("fanmode", false);
            _tab3Hide = _prefs.GetBoolean("tab3hide", true);
            _tab1FeaturedOn = _prefs.GetBoolean("t1featured", true);
            _settingsTabOverride = _prefs.GetBoolean("settingstaboverride", false);

            if (_view != null)
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
                //AssignSettingsFromFile();
            }
        }

        public void OnAppCreation()
        {
            _prefs = Android.App.Application.Context.GetSharedPreferences("BitChute", FileCreationMode.Private);
            _prefEditor = _prefs.Edit();

            _zoomControl = _prefs.GetBoolean("zoomcontrol", false);
            _fanMode = _prefs.GetBoolean("fanmode", false);
            _tab3Hide = _prefs.GetBoolean("tab3hide", true);
            _tab1FeaturedOn = _prefs.GetBoolean("t1featured", true);
            _settingsTabOverride = _prefs.GetBoolean("settingstaboverride", false);
        }

        

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

            if (_zcoffrb.Checked)
            {
                _zoomControl = false;
            }
            else
            {
                _zoomControl = true;
            }
            if (_fmoffrb.Checked)
            {
                _fanMode = false;
            }
            else
            {
                _fanMode = true;
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
            if (_stoverrideoffrb.Checked)
            {
                _settingsTabOverride = false;
            }
            else
            {
                _settingsTabOverride = true;
            }

            prefEditor.PutBoolean("zoomcontrol", _zoomControl);
            prefEditor.PutBoolean("fanmode", _fanMode);
            prefEditor.PutBoolean("tab3hide", _tab3Hide);
            prefEditor.PutBoolean("t1featured", _tab1FeaturedOn);
            prefEditor.PutBoolean("settingstaboverride", _settingsTabOverride);
            prefEditor.Commit();
            
            object[] _settingsArray = { _zoomControl, _fanMode, _tab3Hide, _tab1FeaturedOn, _settingsTabOverride };
            _main.OnSettingsChanged(_settingsArray);
        }
    }
}
