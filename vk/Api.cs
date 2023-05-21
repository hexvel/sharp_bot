using System;
using Newtonsoft.Json.Linq;

namespace VkBot
{
    struct VkApi
    {
        private string access_token, v;

        public async Task<JObject> Method(string method, Dictionary<string, string> Params)
        {
            HttpClient client = new HttpClient();

            FormUrlEncodedContent content = new FormUrlEncodedContent(Params);
            var response = await client.PostAsync($"https:://api.vk.com/method/{method}?access_token={this.access_token}&v=5.131", content);
            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
    }
}