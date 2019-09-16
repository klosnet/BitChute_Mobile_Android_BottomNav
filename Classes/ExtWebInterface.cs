using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class ExtWebInterface
{
    private HttpClient _client;

    public ExtWebInterface()
    {
        _client = new HttpClient();
    }

    public ExtWebInterface(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public static string _notificationRawText;

    public static string GetNotificationText(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AutomaticDecompression = DecompressionMethods.GZip;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            _notificationRawText = reader.ReadToEnd();

        }
        return _notificationRawText;
    }

    //public async void GetNotificationText(string resource)
    //{
    //    try
    //    {
    //        var request = new HttpRequestMessage()
    //        {
    //            RequestUri = new Uri(_client.BaseAddress, resource),
    //            Method = HttpMethod.Get,
    //        };
    //        var response = await _client.SendAsync(request);
    //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
    //        {
    //            var responseString = await response.Content.ReadAsStringAsync();
    //            _notificationRawText = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString).ToString();
    //            var check = _notificationRawText;
    //            return;
    //        }
    //        else if (response.StatusCode == HttpStatusCode.Unauthorized)
    //        {
    //            // you need to maybe re-authenticate here
    //            return;
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.Write(e.ToString());
    //    }
    //}
        //}
        //    public async Task<T> MakeGetRequest<T>(string resource)
        //    {
        //    try
        //    {
        //        var request = new HttpRequestMessage()
        //        {
        //            RequestUri = new Uri(_client.BaseAddress, resource),
        //            Method = HttpMethod.Get,
        //        };
        //        var response = await _client.SendAsync(request);
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            var responseString = await response.Content.ReadAsStringAsync();
        //            //var model = await (Task)Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
        //            return model;
        //        }
        //        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        //        {
        //            // you need to maybe re-authenticate here
        //            return default(T);
        //        }
        //        else
        //        {
        //            return default(T);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    
}