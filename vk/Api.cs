using System;
using VkBot;
using Newtonsoft.Json.Linq;

namespace ApiBot
{
    public partial class VkApi
    {
        private string access_token, v;
        public static string token = "vk1.a.AsJCIhYaIF3WTES_WtwDo8EPe9D4_ffZ0U9rI81DrG7fY52jzce_yek5JjL3iKgzfv944u70Xg4Z3rxDK9JJyPIcpQq6PvcrOcWa6MVcyZLNPsBuIyhdO8PMkQ09WI-Dm9QwcJMT9PRQjOGIag8XzeQa8D9dYvaID5EwPgLO-O8wrVgkZwm8yJJVNS1aTy_T";
        public static object GetLongPollServer()
        {
            Dictionary<string, string> Params = new Dictionary<string, string>()
            {
                ["access_token"] = token,
                ["v"] = "5.131"
            };

            JObject LongPollServer = Requests("https://api.vk.com/method/messages.getLongPollServer/", Params).Result;
            return LongPollServer["response"];
        }

        public static JObject MakeLongRequest()
        {
            JObject longPollServer = (JObject)GetLongPollServer();
            string url = $"https://{longPollServer["server"]}?act=a_check&key={longPollServer["key"]}&ts={longPollServer["ts"]}&wait=25&rps_delay=0";

            Dictionary<string, string> values = new Dictionary<string, string>();
            
            return Requests(url, values).Result;
        }

        public static object CheckLongPoll()
        {
            GetLongPollServer();
            JObject Event = MakeLongRequest();
            return Event;
        }

        public static async Task<JObject> Requests(string url, Dictionary<string, string> Params)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(url);

            FormUrlEncodedContent content = new FormUrlEncodedContent(Params);
            var response = await client.PostAsync(uri, content);
            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> Method(string method, Dictionary<string, string> Params)
        {
            HttpClient client = new HttpClient();

            FormUrlEncodedContent content = new FormUrlEncodedContent(Params);
            var response = await client.PostAsync($"https:://api.vk.com/method/{method}?access_token={this.access_token}&v=5.131", content);
            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
    }
}