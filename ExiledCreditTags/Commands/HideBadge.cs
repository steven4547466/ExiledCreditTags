using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExiledCreditTags.Commands
{
	[CommandHandler(typeof(ClientCommandHandler))]
	class HideBadge : ICommand
	{

		private System.Random rnd = new System.Random();

		public string Command { get; } = "etogglebadge";

		public string[] Aliases { get; } = { "etoggle", "etoggletag" };

		public string Description { get; } = "Show your highest/hide your Exiled related badge or custom player info.";

		private string[] rankNames = new[] { "Exiled Dev", "Exiled Contributor", "Exiled Plugin Dev" };

		public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
		{
			Player player = Player.Get(((PlayerCommandSender)sender).SenderId);
			string PlayerID = EventHandlers.EventHandlers.HashSh1(player.UserId);
			if (!Plugin.Instance.CreditTags.ContainsKey(PlayerID))
			{
				response = "You don't have an exiled related badge.";
				return false;
			}

			if (!rankNames.Contains(player.RankName))
			{
				string[] nameAndColors = EventHandlers.EventHandlers.RoleToNameAndColors((Plugin.Role)Plugin.Instance.CreditTags[PlayerID]);
				EventHandlers.EventHandlers.EnableBadgeOrCPT(player, nameAndColors);
				response = $"Enabled <color=#{nameAndColors[2]}>{nameAndColors[0]}</color>.";
				return true;
			}
			else
			{
				if (Plugin.Instance.Config.UseBadge)
				{
					Tuple<string, string> badgeAndColor = EventHandlers.EventHandlers.startingBadgesAndColors[player];
					player.RankName = badgeAndColor.Item1;
					player.RankColor = badgeAndColor.Item2;
				} 
				else
				{
					player.CustomInfo = string.Empty;
				}
				response = "Disabled Exiled related badge.";
				return true;
			}
		}
	}
}
