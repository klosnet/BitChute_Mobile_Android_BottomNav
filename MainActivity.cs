/* App by:
 

.__                                             .___
|  |__   ____ ___  ________     ____   ____   __| _/
|  |  \_/ __ \\  \/  /\__  \   / ___\ /  _ \ / __ | 
|   Y  \  ___/ >    <  / __ \_/ /_/  >  <_> ) /_/ | 
|___|  /\___  >__/\_ \(____  /\___  / \____/\____ | 
     \/     \/      \/     \//_____/             \/ 

bitchute.com/channel/hexagod
soundcloud.com/vybemasterz

twitter @vybeypantelonez
minds @hexagod
steemit @vybemasterz
gab.ai @hexagod

based off the template by hnabbasi
https://github.com/hnabbasi/BottomNavigationViewPager
 
 */

using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.Design.Internal;
using Android.Support.V4.View;
using BottomNavigationViewPager.Adapters;
using System.Collections.Generic;
using Android.Support.V4.App;
using BottomNavigationViewPager.Fragments;
using Android.InputMethodServices;
using Android.Views;
using Android.Content;
using Android.Webkit;
using Android.Support.V4.Content;
using static Android.Support.Design.Widget.BottomNavigationView;
using Android.Graphics;
using System.Threading.Tasks;
using BottomNavigationViewPager.Classes;
using static Android.Views.View;

//app:layout_behavior="@string/hide_bottom_view_on_scroll_behavior"

namespace BottomNavigationViewPager
{
    [Android.App.Activity(Label = "BitChute", Theme = "@style/AppTheme", MainLauncher = true,
        ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize,
        ParentActivity = typeof(MainActivity))]

    public class MainActivity : FragmentActivity
    {
        int _tabSelected;

        ViewPager _viewPager;
        public static BottomNavigationView _navigationView;
        public static List<BottomNavigationItemView> _navViewItemList;
        IMenuItem _menu;
        Fragment[] _fragments;

        public static MainActivity _main;

        public static AppSettings _appSettings = new AppSettings();

        public static Globals _globals = new Globals();

        public static bool _navBarHideTimeout = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            _main = this;
            _appSettings.OnAppCreation();

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            InitializeTabs();

            _viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            _viewPager.PageSelected += ViewPager_PageSelected;
            _viewPager.Adapter = new ViewPagerAdapter(SupportFragmentManager, _fragments);

            _navigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            RemoveShiftMode(_navigationView);
            _navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            _navigationView.LongClick += NavigationViewLongClickListener;

            _viewPager.OffscreenPageLimit = 4;
        }

        public static TheFragment1 _fm1 = TheFragment1.NewInstance("Home", "tab_home");
        public static TheFragment2 _fm2 = TheFragment2.NewInstance("Subs", "tab_subs");
        public static TheFragment3 _fm3 = TheFragment3.NewInstance("Feed", "tab_playlist");
        public static TheFragment4 _fm4 = TheFragment4.NewInstance("MyChannel", "tab_mychannel");
        public static TheFragment5 _fm5 = TheFragment5.NewInstance("Settings", "tab_home");

        void InitializeTabs()
        {
            _fragments = new Fragment[] {
                _fm1,
                _fm2,
                _fm3,
                _fm4,
                _fm5
            };
        }

        public static bool _navHidden = false;

        public static bool _navTimeout = false;

        public static int _navTimer = 0;

        /// <summary>
        /// listens for scroll events and hides the navbar after x seconds
        /// .. timer resets every time it's called
        /// . works with a custom scroll listener
        /// </summary>
        public void CustomOnScroll()
        {
            if (_navTimer != 0)
                _navTimer = 0;

            if (!_navTimeout)
            {
                _navigationView.Visibility = ViewStates.Visible;
                _navHidden = false;
                NavBarRemove();
                _navTimeout = true;
            }
        }

        public async void NavBarRemove()
        {
            while (!_navHidden)
            {
                //canceling this for now
                //await Task.Run(() => System.Threading.Thread.Sleep(1000));

                //lets see if this is faster
                await Task.Delay(1000);

                _navTimer++;
                if (_navTimer == 8)
                {
                    _navigationView.Visibility = ViewStates.Gone;
                    _navTimeout = false;
                    _navHidden = true;
                }
            }
        }

        public async void HideNavBarAfterDelay()
        {
            if (!_navBarHideTimeout)
            {
                await Task.Delay(500);
            }
        }

        public override bool OnKeyDown(Android.Views.Keycode keyCode, KeyEvent e)
        {
            if (e.KeyCode == Android.Views.Keycode.Back)
            {
                switch (_viewPager.CurrentItem)
                {
                    case 0:
                        _fm1.WebViewGoBack();
                        break;
                    case 1:
                        _fm2.WebViewGoBack();
                        break;
                    case 2:
                        _fm3.WebViewGoBack();
                        break;
                    case 3:
                        _fm4.WebViewGoBack();
                        break;
                    case 4:
                        _fm5.WebViewGoBack();
                        break;
                }
            }
            return false;
        }


        void NavigationView_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            _navTimer = 0;

            if (_tabSelected == e.Item.Order)
            {
                switch (_viewPager.CurrentItem)
                {
                    case 0:
                        _fm1.Pop2Root();
                        break;
                    case 1:
                        _fm2.Pop2Root();
                        break;
                    case 2:
                        _fm3.Pop2Root();
                        break;
                    case 3:
                        _fm4.Pop2Root();
                        break;
                    case 4:
                        _fm5.Pop2Root();
                        break;
                }
            }
            else
            {
                //_navigationView.SelectedItemId = e.Item.Order;
                //_menu = _navigationView.Menu.GetItem(e2.Position);
                //_navigationView.SelectedItemId = _menu.ItemId;
                _viewPager.SetCurrentItem(e.Item.Order, true);
            }
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            _navTimer = 0;

            _menu = _navigationView.Menu.GetItem(e.Position);
            _navigationView.SelectedItemId = _menu.ItemId;

            _tabSelected = _viewPager.CurrentItem;

            CustomOnScroll();
        }

        //BottomNavigationView.NavigationItemReselectedEventArgs

        void RemoveShiftMode(BottomNavigationView view)
        {
            var menuView = (BottomNavigationMenuView)view.GetChildAt(0);

            try
            {
                var shiftingMode = menuView.Class.GetDeclaredField("mShiftingMode");
                shiftingMode.Accessible = true;
                shiftingMode.SetBoolean(menuView, false);
                shiftingMode.Accessible = false;

                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    var item = (BottomNavigationItemView)menuView.GetChildAt(i);
                    item.SetShiftingMode(false);
                    // set once again checked value, so view will be updated
                    item.SetChecked(item.ItemData.IsChecked);
                    item.LongClick += NavigationViewLongClickListener;

                    if (_navViewItemList != null)
                    {
                        _navViewItemList.Add(item);
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine((ex.InnerException ?? ex).Message);
            }
        }

        /// <summary>
        /// forwards the setting object array to all fragments
        /// </summary>
        /// <param name="oa"></param>
        public void OnSettingsChanged(object[] oa)
        {
            _fm1.OnSettingsChanged(oa);
        //    _fm2.OnSettingsChanged(oa);
        //    _fm3.OnSettingsChanged(oa);
        //    _fm4.OnSettingsChanged(oa);
        //    _fm5.OnSettingsChanged(oa);
        }

        /// <summary>
        /// Listen for long click events on the navbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NavigationViewLongClickListener(object sender, LongClickEventArgs e)
        {
            _fm5.ShowAppSettingsMenu();
        }

        /// <summary>
        /// method to change any tab icon, title
        /// it takes the tab number integer and string change details
        /// string can be null for now
        /// </summary>
        /// <param name="changeDetails">can be null or blank, use "home" "feed" "subs" or "explore" with int s 3 & 4</param>
        /// <param name="tab">int representing tab 0 is farthest left going up to the right</param>
        public void TabDetailChanger(int tab, string changeDetails)
        {
            switch (tab)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    if (AppSettings._fanMode)
                    {
                        if (changeDetails == "" || changeDetails == null)
                        {
                            _navViewItemList[3].SetTitle("Subs");
                        }
                        if (changeDetails == "subs")
                        {
                            _navViewItemList[3].SetTitle("Subs");
                        }
                        if (changeDetails == "feed")
                        {
                            _navViewItemList[3].SetTitle("Feed");
                        }
                        if (changeDetails == "explore")
                        {
                            _navViewItemList[3].SetTitle("Explore");
                        }
                    }
                    break;
                case 4:
                    break;
            }
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            Globals._bkgrd = true;

            while (_globals.IsInBkGrd())
            {
                Task.Delay(1200);

                _globals.IsInBkGrd();
            }
        }

        protected override void OnDestroy()
        {
            _viewPager.PageSelected -= ViewPager_PageSelected;
            _navigationView.NavigationItemSelected -= NavigationView_NavigationItemSelected;
            base.OnDestroy();
        }
    }
}
