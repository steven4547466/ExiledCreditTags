using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExiledCreditTags.Commands
{
	[CommandHandler(typeof(ClientCommandHandler))]
	class CycleBadge : ICommand
	{

		private System.Random rnd = new System.Random();

		public string Command { get; } = "ecyclebadge";

		public string[] Aliases { get; } = { "ecycle", "ecycletag" };

		public string Description { get; } = "Cycle to the next tag you have, if you have one.";

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
				response = $"Your exiled badge is currently hidden.";
				return true;
			}
			else
			{
				Plugin.Role role;
				Plugin.Role tempRole;
				if (Plugin.Instance.Config.UseBadge) 
				{
					tempRole = (Plugin.Role) ((int)EventHandlers.EventHandlers.TagToRole(player.RankName) << 1);
				} 
				else
				{
					tempRole = (Plugin.Role)((int)EventHandlers.EventHandlers.TagToRole(Regex.Replace(player.CustomInfo, "(<color=#[A-Fa-f0-9]{0,6}>|</color>)", "")) << 1);
				}

				if (tempRole > Plugin.Role.PluginDeveloper) tempRole = EventHandlers.EventHandlers.HighestRole((Plugin.Role)Plugin.Instance.CreditTags[PlayerID]);
				while ((Plugin.Instance.CreditTags[PlayerID] & (int)tempRole) == 0)
				{
					tempRole = (Plugin.Role)((int)tempRole << 1);
					if (tempRole > Plugin.Role.PluginDeveloper) tempRole = EventHandlers.EventHandlers.HighestRole((Plugin.Role)Plugin.Instance.CreditTags[PlayerID]);
				}
				role = tempRole;

				string[] nameAndColors = EventHandlers.EventHandlers.RoleToNameAndColors(role);
				if ((Plugin.Instance.Config.UseBadge && player.RankName == nameAndColors[0]) 
					|| (!Plugin.Instance.Config.UseBadge && player.CustomInfo.Contains(nameAndColors[0])))
				{
					response = $"No other badges to cycle to.";
					return true;
				}

				EventHandlers.EventHandlers.EnableBadgeOrCPT(player, nameAndColors);
				response = $"Cycled to <color=#{nameAndColors[2]}>{nameAndColors[0]}</color>.";
				return true;
			}
		}
	}
}
