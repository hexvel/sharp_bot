using Newtonsoft.Json.Linq;

namespace VkBot
{
    public class LongPoll
    {

        public static string token = "vk1.a.zAayRSi-WrItwn8NGEakRlAxGugoXNL6c88yKOH6CJhuGw9TsWue-tpnpNEj2XBh4vAxu6O9VIDUFxMylcFqN8AsMuHau5FBLvjlUknFhIRgWwJJxd_NlXbbS0DG4_n-lyLQmksLiWsHuH0ZUYduQNGgMbTYIjrqCS4z_k5msoUdF6TFzfkz5EhC5lTPi-EQ2UMolhAm2uZiUYEw-nodcw";
        public static object GetLongPollServer()
        {
            Dictionary<string, string> Params = new Dictionary<string, string>()
            {
                ["access_token"] = token,
                ["method"] = "messages.getLongPollServer",
                ["v"] = "5.131"
            };

            JObject LongPollServer = Requests("https://api.vk.com/method/", Params).Result;
            return LongPollServer["response"];
        }

        public static JObject MakeLongRequest()
        {
            JObject longPollServer = (JObject)GetLongPollServer();
            string url = $"https://{longPollServer["server"]}/";

            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                ["act"] = "a_check",
                ["key"] = (string)longPollServer["key"],
                ["ts"] = (string)longPollServer["ts"],
                ["wait"] = "25",
                ["rps_delay"] = "0"
            };
            
            return Requests(url, values).Result;
        }

        public static object CheckLongPoll()
        {
            GetLongPollServer();
            JObject Event = MakeLongRequest();
            return Event;
        }

        public static void Main()
        {
            while(true)
            {
                Console.WriteLine(CheckLongPoll());
            }
        }

        public static async Task<JObject> Requests(string url, Dictionary<string, string> Params)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri(url);

            FormUrlEncodedContent content = new FormUrlEncodedContent(Params);
            var response = await client.PostAsync(uri, content);
            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
    }
}