using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
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
            string token = "vk1.a.AsJCIhYaIF3WTES_WtwDo8EPe9D4_ffZ0U9rI81DrG7fY52jzce_yek5JjL3iKgzfv944u70Xg4Z3rxDK9JJyPIcpQq6PvcrOcWa6MVcyZLNPsBuIyhdO8PMkQ09WI-Dm9QwcJMT9PRQjOGIag8XzeQa8D9dYvaID5EwPgLO-O8wrVgkZwm8yJJVNS1aTy_T";
            VkApi api = new VkApi(token);

            while (true)
            {
                var @event = api.CheckLongPoll()["updates"];
                foreach (var events in @event)
                {
                    if ((int)events[0] == 4)
                    {
                        var text = Convert.ToString(events.Last());
                        var messaegId = Convert.ToString(events[1]);
                        var peerId = Convert.ToString(events[3]);
                        var timeStamp = Convert.ToString(events[4]);

                        if (text == "б пинг")
                        {
                            Ping ping = new();
                            string host = new Uri("https://api.vk.com/").Host;
                            PingReply result = ping.Send(host);

                            /*Dictionary<string, string> @params = new Dictionary<string, string>()
                            {
                                ["message_id"] = messaegId,
                                ["message"] =  $"Ping Time: 0.{result.RoundtripTime} сек",
                                ["peer_id"] = peerId
                            };*/
                            string messageed = $"Ping Time: 0.{result.RoundtripTime} сек";

                                api.message_edit(peerId, messaegId, messageed);
                        } else if (text == "б хуй") {
                            api.message_send(peerId, "залупа");
                        } else if (text == "б +др") {
                            
                        }
                    }
                }
            }
        }
    }
}