using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ApiBot
{
    public partial class VkApi
    {
        private static string access_token;

        public VkApi(string access_token)
        {
            VkApi.access_token = access_token;
        }
        public async void MessageSend(string peer_id, string message)
        {
            Random rand = new Random();
            var rnd = Convert.ToString(rand.Next(-2147000000, 2147000000));

            Dictionary<string, string> @params = new Dictionary<string, string>()
            {
                ["message"] = message,
                ["peer_id"] = peer_id,
                ["random_id"] = rnd
            };

            HttpClient client = new HttpClient();

            FormUrlEncodedContent content = new FormUrlEncodedContent(@params);
            var response = await client.PostAsync($"https://api.vk.com/method/messages.send?access_token={access_token}&v=5.131", content);
            JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        public async void MessageEdit(string peer_id, string message_id, string message)
        {
            Random rand = new Random();
            var rnd = Convert.ToString(rand.Next(-2147000000, 2147000000));

            Dictionary<string, string> @params = new Dictionary<string, string>()
            {
                ["message_id"] = message_id,
                ["message"] = message,
                ["peer_id"] = peer_id
            };

            HttpClient client = new HttpClient();

            FormUrlEncodedContent content = new FormUrlEncodedContent(@params);
            var response = await client.PostAsync($"https://api.vk.com/method/messages.edit?access_token={access_token}&v=5.131", content);
            JObject.Parse(await response.Content.ReadAsStringAsync());
        }
        public static object GetLongPollServer()
        {
            Dictionary<string, string> Params = new Dictionary<string, string>()
            {
                ["access_token"] = access_token,
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

        public JObject CheckLongPoll()
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
            var response = await client.PostAsync($"https://api.vk.com/method/{method}?access_token={access_token}&v=5.131", content);
            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
    }
}
