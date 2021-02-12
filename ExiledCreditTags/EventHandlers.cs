namespace ExiledCreditTags.EventHandlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Exiled.API.Extensions;
    using System;
    using Hints;
    using System.Collections.Generic;

    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        Random random = new Random();
        public void OnPlayerVerify(VerifiedEventArgs ev)
        {
            if (plugin.CreditTags.ContainsKey(ev.Player.UserId))
            {
                ev.Player.RankName = plugin.CreditTags[ev.Player.UserId];
                switch (plugin.CreditTags[ev.Player.UserId])
                {
                    case "Exiled Plugin Dev":
                        ev.Player.RankColor = "crimson";
                        break;
                    case "Exiled Contributor":
                        ev.Player.RankColor = "cyan";
                        break;
                    case "Exiled Dev":
                        ev.Player.RankColor = "magenta";
                        break;
                };
            }
        }
       
    }
}
