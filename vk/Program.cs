using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ApiBot;

namespace UserBot
{
    internal class Program
    {
        private static string token = "vk1.a.EdyMltp4JXSc34PbTdtyuhp3341mVpNc6oT_MKQRWTGR9VtqHszrHrNBV9vKrpgG5iuUH32YDVqaDnIrhb0KjoZ7wZZrhwKIEku76caosc_G7AoPd-wloFTZ3geNyDkcPQc6qgGQUi4lY6jYirz2DAf7J8dQg4TkH_m1sAgf9GrFTopUgSPzwm9bqz8VthYMBJhUnLgU6MQZJo_Qay9K2A";
        static void RunCommands(string command, string peerId, string messageid)
        {
            VkApi _api = new VkApi(token);
            switch (command)
            {
                case "пинг":
                    Ping ping = new Ping();
                    string host = new Uri("https://api.vk.com/").Host;
                    PingReply result = ping.Send(host);

                    string pingTime = $"🌐PingTime: 0.{result.RoundtripTime} сек.";
                    _api.MessageEdit(peerId, messageid, pingTime);

                    break;

            }
        }

        static void Main()
        {
            VkApi api = new VkApi(token);
            Console.WriteLine("Запуск модуля.");

            while (true)
            {
                var @event = api.CheckLongPoll()["updates"];
                foreach (var events in @event)
                {
                    byte EventType = Convert.ToByte(events[0]);
                    switch (EventType)
                    {
                        case 4:
                            string text = Convert.ToString(events.Last());
                            string prefix = text.Split(' ')[0];
                            var messageLength = text.Split(' ');
                            if (messageLength.Length < 2)
                                continue;
                            string command = text.Split(' ')[1];
                            string messaegId = Convert.ToString(events[1]);
                            string peerId = Convert.ToString(events[3]);

                            if (prefix == ".м")
                            {
                                RunCommands(command, peerId, messaegId);
                            }

                            break;
                    }
                }
            }
        }
    }
}
