using HarmonyLib;
using static DillyzRolesAPI.Roles.Extensions;


namespace DillyzRolesAPI.Roles
{
	[HarmonyPatch(typeof(ExileController), "Begin")]
	public static class ExilePatch
	{
		public static void Postfix([HarmonyArgument(0)] GameData.PlayerInfo exiled, ExileController __instance)
		{
			foreach (PlayerControl player in PlayerControl.AllPlayerControls)
			{
				if (!PlayerControl.GameOptions.ConfirmImpostor)
					continue;
				if (player.PlayerId != exiled.PlayerId)
					continue;
				if (!player.isAnyRole())
					continue;
				foreach (RoleGenerator role in allRoles)
					if (role.containedPlayerIds.Contains(player.PlayerId))
						__instance.completeString = exiled.PlayerName + " " + role.EjectionText;
			}
		}
		public static bool isAnyRole(this PlayerControl player)
		{
			foreach (RoleGenerator role in allRoles)
				if (role.containedPlayerIds.Contains(player.PlayerId))
					return true;
			return false;
		}
	}
}