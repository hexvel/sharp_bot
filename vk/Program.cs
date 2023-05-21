using Newtonsoft.Json.Linq;
using ApiBot;

namespace VkBot
{
    public class LongPoll
    {

        public static void Main()
        {
            while(true)
            {
                Console.WriteLine(VkApi.CheckLongPoll());
            }
        }
    }
}