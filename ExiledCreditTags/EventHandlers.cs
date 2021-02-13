namespace ExiledCreditTags.EventHandlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Exiled.API.Extensions;
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using Hints;
    using System.Collections.Generic;
	using MEC;

	public class EventHandlers
    {

        public static Dictionary<Player, Tuple<string, string>> startingBadgesAndColors = new Dictionary<Player, Tuple<string, string>>();

        public void OnPlayerVerify(VerifiedEventArgs ev)
        {
            Timing.CallDelayed(0.5f, () =>
            {
                startingBadgesAndColors.Add(ev.Player, new Tuple<string, string>(ev.Player.RankName, ev.Player.RankColor));
                string PlayerID = HashSh1(ev.Player.UserId);
                if (Plugin.Instance.CreditTags.ContainsKey(PlayerID))
                {
                    Plugin.Role role = (Plugin.Role)Plugin.Instance.CreditTags[PlayerID];
                    string[] nameAndColors = RoleToNameAndColors(role);
                    EnableBadgeOrCPT(ev.Player, nameAndColors);
                }
            });
        }

        public static void EnableBadgeOrCPT(Player player, string[] nameAndColors)
		{
            if (Plugin.Instance.Config.UseBadge && (Plugin.Instance.Config.BadgeOverride || player.RankName == string.Empty))
            {
                player.RankName = nameAndColors[0];
                player.RankColor = nameAndColors[1];
            }
            else if (!Plugin.Instance.Config.UseBadge && (Plugin.Instance.Config.CPTOverride || player.ReferenceHub.nicknameSync.Network_customPlayerInfoString == string.Empty))
            {
                player.ReferenceHub.nicknameSync.Network_customPlayerInfoString = $"<color=#{nameAndColors[2]}>{nameAndColors[0]}</color>";
            }
        }

        public static string[] RoleToNameAndColors (Plugin.Role role)
		{
            if ((role & Plugin.Role.ExiledDeveloper) == Plugin.Role.ExiledDeveloper)
			{
                return new string[3] { "Exiled Dev", "magenta", "00FFFF" };
            }
            else if ((role & Plugin.Role.ExiledContributor) == Plugin.Role.ExiledContributor)
			{
                return new string[3] { "Exiled Contributor", "cyan", "800080" };
            }
            else if ((role & Plugin.Role.PluginDeveloper) == Plugin.Role.PluginDeveloper)
			{
                return new string[3] { "Exiled Plugin Dev", "crimson", "DC143C" };
            }
            else 
                return new string[3];
		}

        public static string HashSh1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hashSh1 = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

                // declare stringbuilder
                var sb = new StringBuilder(hashSh1.Length * 2);

                // computing hashSh1
                foreach (byte b in hashSh1)
                {
                    // "x2"
                    sb.Append(b.ToString("X2").ToLower());
                }

                // final output
                Log.Debug(string.Format("The SHA1 hash of {0} is: {1}", input, sb.ToString()));

                return sb.ToString();
            }
        }
    }
}
