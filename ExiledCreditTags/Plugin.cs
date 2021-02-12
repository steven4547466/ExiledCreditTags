using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExiledCreditTags
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using MEC;
    using Player = Exiled.Events.Handlers.Player;

    public class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public override string Name { get; } = "Exiled tags";
        public override string Author { get; } = "Babyboucher20";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 29);
        public override string Prefix { get; } = "Exiled tags";

        public EventHandlers.EventHandlers PlayerHandlers;

        public Dictionary<String, string> CreditTags { get; set; } = new Dictionary<String, string>
        {
            {
                "76561198294763508@steam", "Exiled Dev" //iRebbok
            },
            {
                "76561198119888517@steam", "Exiled Plugin Dev" //Babyboucher20
            },
            {
                "76561198194226753@steam", "Exiled Contributor" //初音早猫
            },
            {
                "76561198224529388@steam", "Exiled Contributor" //DGvagabond
            },
            {
                "76561198037519280@steam", "Exiled Contributor" //KadeDev
            },
            {
                "76561198023272004@steam", "Exiled Dev" //iopietro
            },
            {
                "76561198199188486@steam", "Exiled Contributor" //Thunder
            },
            {
                "76561198164512098@steam", "Exiled Contributor" // Virtual
            },
            {
                "76561198142449742@steam", "Exiled Contributor" // JasKill
            },
            {
                "76561198832820936@steam", "Exiled Contributor" //Killers0992
            },
            {
                "76561198347253445@steam", "Exiled Plugin Dev" // Terminator_97
            },
            {
                "76561198230639680@steam", "Exiled Plugin Dev" // Thomasjosif
            }
        };

        public override void OnEnabled()
        {
            PlayerHandlers = new EventHandlers.EventHandlers(this);
            Player.Verified += PlayerHandlers.OnPlayerVerify;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Verified -= PlayerHandlers.OnPlayerVerify;
            base.OnDisabled();
        }
    }
}
