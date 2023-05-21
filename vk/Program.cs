using Newtonsoft.Json.Linq;
using ApiBot;

namespace VkBot
{
    public class LongPoll
    {

        public static object Messages(JArray Events)
        {
            var Text = Events.Last();
            Dictionary<string, string> message = new Dictionary<string, string>()
            {
                ["text"] = (string)Text,
            };

            return message;
        }

        public static void Main()
        {
            string token = "vk1.a.UKL8FGlcvg6sQbhhnb_79iB7lPH0JfUf5s-eEkTGBAauncZMoAYynMJr-W5fdO34Hm6Z8k5yLsbPVFZNJXYRxmE1HeK_BEjxxi5-c-SEo0gQTIdR7ZqndAkdiWYI_KWXTwZ0wyPKZSboXAVEW_WXgiEWtRKG5V8WNebCJ9SfmW7E0XWNXfuYZIsHD1PUPZNJ88whkVsio8_2wECyHv23EQ";
            VkApi api = new VkApi(token);

            while (true)
            {
                var @event = api.CheckLongPoll()["updates"];
                foreach (var events in @event)
                {
                    if ((int)events[0] == 4)
                    {
                        var text = events.Last();
                        var messaegId = events[1];
                        var peerId = events[3];
                        var timeStamp = events[4];

                        if ((string)text == "пинг")
                        {
                            DateTime now = DateTime.Now;
                            TimeSpan span = now - new DateTime();
                            double milliseconds = span.TotalMilliseconds;
                            System.Console.WriteLine($"1: {milliseconds}\n2: {timeStamp}");

                            Dictionary<string, string> @params = new Dictionary<string, string>()
                            {
                                ["message_id"] = (string)messaegId,
                                ["message"] = "ПОНГ",
                                ["peer_id"] = (string)peerId
                            };

                            var a = api.Method("messages.edit", @params).Result;
                            System.Console.WriteLine(a);
                        }
                    }
                }
            }
        }
    }
}