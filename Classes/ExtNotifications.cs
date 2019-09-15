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
namespace BottomNavigationViewPager.Classes
{
    class ExtNotifications
    {
        public class WebInterface
        {
            private HttpClient _client;

            public WebInterface(HttpClient httpClient)
            {
                _client = httpClient;
            }
            /*
            public async Task<T> MakeGetRequest<T>(string resource)
            {
                try
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(_client.BaseAddress, resource),
                        Method = HttpMethod.Get,
                    };
                    var response = await _client.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var yo = response.ToString();
                        var check = yo;
                        var responseString = await response.Content.ReadAsStringAsync();
                        Task t = Task.Run(() =>
                        {
                            int zero = 0;
                        });
                        return t;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        // you need to maybe re-authenticate here
                        return default(T);
                    }
                    else
                    {
                        return default(T);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }*/
        }
    }
}