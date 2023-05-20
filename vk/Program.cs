using System;
using VkNet;
using VkNet.Model;
namespace VkBot
{
    public class MainClassVk
    {
        public static void Main()
        {   
         Dictionary<string, object> Params(params object[] args)
         {
            {''}
         }; 
        }

        static async Task<HttpResponseMessage?> GetResponse(string url, Dictionary<string, string> Params)
        {
            HttpClient client = new HttpClient();

            try
            {
                Uri uri = new Uri(url);
                FormUrlEncodedContent content = new FormUrlEncodedContent(Params);

                return await client.PostAsync(url, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка:\n", ex.ToString());
            }
            finally
            {
                client.Dispose();
            }

            return null;
        }
    }
}