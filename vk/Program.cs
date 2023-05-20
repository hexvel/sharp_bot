using System;
namespace VkBot
{
    public class MainClassVk
    {
        public static void Main()
        {
            string token = "vk1.a.UknqVGrk4pDGkY3wovzKAjxKekovf1gqMb302tqWu0IsSM8QtFKKxlL-imyLstU2crwe2rxvRiYnTNCDoHgqRLxoKMzSzKV8ZIu5unO5fR216Wbom4_slpVPeY3Xo5w7g9z4HvinEMblyriZUtjcI98gQJSmQj7_05G6wXwrB5xP6gFhKDpGAofT1UNX3vEpZp-P3IbQlVPW_UvRYfCXlw";

            Dictionary<string, string> Params = new Dictionary<string, string>()
            {
                ["user_id"] = "465816400",
                ["access_token"] = token,
                ["v"] = "5.131"
            };

            var answer = GetRequests("https://api.vk.com/method/users.get/", Params).Result;
            System.Console.WriteLine(answer.Content.ReadAsStringAsync().Result);
        }

        public static async Task<HttpResponseMessage> GetRequests(string url, Dictionary<string, string> Params)
        {
            HttpClient client = new HttpClient();

            try
            {
                Uri uri = new Uri(url);
                FormUrlEncodedContent content = new FormUrlEncodedContent(Params);

                return await client.PostAsync(uri, content);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Произошла ошибка:{ex}");
            }
            finally
            {
                client.Dispose();
            }
            return null;
        }

    }
}