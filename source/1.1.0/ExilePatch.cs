using HarmonyLib;
using static DillyzRolesAPI.Roles.Extensions;


namespace DillyzRolesAPI.Roles
{
	[HarmonyPatch(typeof(ExileController), "Begin")]
	public static class ExilePatch
	{
		public static void Postfix([HarmonyArgument(0)] GameData.PlayerInfo exiled, ExileController __instance)
		{
			RoleCheck a = new RoleCheck();
			foreach (PlayerControl player in PlayerControl.AllPlayerControls)
			{
				if (!PlayerControl.GameOptions.ConfirmImpostor)
					continue;
				if (player.PlayerId != exiled.PlayerId)
					continue;
				if (!a.isAnyRole(player.PlayerId))
					continue;
				foreach (RoleGenerator role in allRoles)
					if (role.NameOfRole == player.getModdedControl().Role)
						__instance.completeString = exiled.PlayerName + " " + role.EjectionText;
			}
		}
	}
}