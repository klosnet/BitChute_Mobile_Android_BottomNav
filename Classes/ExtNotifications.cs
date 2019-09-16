using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Web;
using BottomNavigationViewPager.Fragments;

namespace BottomNavigationViewPager.Classes
{
    public class ExtNotifications
    {
        public static TheFragment5 _fm5 = new TheFragment5();

        public static int _count = 0;
        public static int _zero = 0;

        /*
        public void ExtNotificatonEvents()
        {

            var _ctx = Android.App.Application.Context;


            // Pass the current button press count value to the next activity:
            var valuesForActivity = new Bundle();
            valuesForActivity.PutInt(MainActivity.COUNT_KEY, _count);

            // When the user clicks the notification, SecondActivity will start up.
            var resultIntent = new Intent(_ctx, typeof(TheFragment1));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);

            // Construct a back stack for cross-task navigation:
            var stackBuilder = TaskStackBuilder.Create(_ctx);
            //stackBuilder.AddParentStack(Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            _fm5.GetPendingIntent();

            // Create the PendingIntent with the back stack:
            var resultPendingIntent = stackBuilder.GetPendingIntent(_zero, TheFragment5._flags);

            // Build the notification:
            var builder = new Android.Support.V4.App.NotificationCompat.Builder(_ctx, MainActivity.CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle("Button Clicked") // Set the title
                          .SetNumber(_count) // Display the count in the Content Info
                          .SetSmallIcon(Resource.Drawable.tab_playlists) // This is the icon to display
                          .SetContentText($"The button has been clicked {_count} times."); // the message to display.

            // Finally, publish the notification:
            var notificationManager = Android.Support.V4.App.NotificationManagerCompat.From(_ctx);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, builder.Build());
        
            // Increment the button press count:
            _count++;
        }*/

        public void ExtNotificatonEvents(object sender, EventArgs eventArgs)
        {

            var _ctx = Android.App.Application.Context;


            // Pass the current button press count value to the next activity:
            var valuesForActivity = new Bundle();
            valuesForActivity.PutInt(MainActivity.COUNT_KEY, _count);

            // When the user clicks the notification, SecondActivity will start up.
            var resultIntent = new Intent(_ctx, typeof(TheFragment1));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);

            // Construct a back stack for cross-task navigation:
            var stackBuilder = TaskStackBuilder.Create(_ctx);
            //stackBuilder.AddParentStack(Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            _fm5.GetPendingIntent();

            // Create the PendingIntent with the back stack:
            var resultPendingIntent = stackBuilder.GetPendingIntent(_zero, TheFragment5._flags);

            // Build the notification:
            var builder = new Android.Support.V4.App.NotificationCompat.Builder(_ctx, MainActivity.CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle("Button Clicked") // Set the title
                          .SetNumber(_count) // Display the count in the Content Info
                          .SetSmallIcon(Resource.Drawable.tab_playlists) // This is the icon to display
                          .SetContentText($"The button has been clicked {_count} times."); // the message to display.

            // Finally, publish the notification:
            var notificationManager = Android.Support.V4.App.NotificationManagerCompat.From(_ctx);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, builder.Build());


            // Increment the button press count:
            //_count++;
        }
        public class WebInterface
        {
            HttpClient _client = new HttpClient();

            public WebInterface(HttpClient httpClient)
            {
                _client = httpClient;
            }
        }
    }
}