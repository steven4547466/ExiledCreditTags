using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ExiledCreditTags
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Exiled.API.Features;
    using MEC;
    using Player = Exiled.Events.Handlers.Player;

    public class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public static Plugin Instance;

        public override string Name { get; } = "Exiled tags";
        public override string Author { get; } = "Babyboucher20";
        public override Version Version { get; } = new Version(1, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 29);
        public override string Prefix { get; } = "Exiled tags";

        public EventHandlers.EventHandlers PlayerHandlers;

        public Dictionary<string, int> CreditTags { get; set; }  = GetURL();

        public override void OnEnabled()
        {
            Instance = this;
            PlayerHandlers = new EventHandlers.EventHandlers();
            Player.Verified += PlayerHandlers.OnPlayerVerify;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Verified -= PlayerHandlers.OnPlayerVerify;
            base.OnDisabled();
        }

        public static Dictionary<string, int> GetURL()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://gist.githubusercontent.com/steven4547466/a65f05599a04f069f322ea07f12cf232/raw/roles.json");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Dictionary<string, int> dict = Utf8Json.JsonSerializer.Deserialize<Dictionary<string, int>>(reader.ReadToEnd());
                return dict;
            }
        }

        public enum Role
		{
            None = 0,
            ExiledDeveloper = 1,
            ExiledContributor = 2,
            PluginDeveloper = 4
		}
    }
}
