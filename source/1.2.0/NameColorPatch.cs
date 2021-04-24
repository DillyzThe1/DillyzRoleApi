using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;
using static DillyzRolesAPI.Roles.Extensions;

namespace DillyzRolesAPI.Roles
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    class NameColorPatch
    {
        public static void Postfix()
        {
            if (MeetingHud.Instance != null)
            {
                upMetPatch.Postfix(MeetingHud.Instance);
            }
            else
            {
                if (PlayerControl.LocalPlayer.Data.IsImpostor)
                    PlayerControl.LocalPlayer.nameText.color = Palette.ImpostorRed;
                else
                    PlayerControl.LocalPlayer.nameText.color = Palette.White;
                if (PlayerControl.AllPlayerControls.Count > 1 && NameColorPatch.hasAnyRole(PlayerControl.LocalPlayer))
                    foreach (RoleGenerator role in allRoles)
                        if (role.containedPlayerIds.Contains(PlayerControl.LocalPlayer.PlayerId))
                            PlayerControl.LocalPlayer.nameText.color = role.RoleColor;
            }
        }
        public static bool hasAnyRole(PlayerControl plr)
        {
            foreach (RoleGenerator role in allRoles)
                if (role.containedPlayerIds.Contains(plr.PlayerId))
                    return true;
            return false;
        }
    }
    //[HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
    class upMetPatch
    {
        public static void Postfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Data.IsImpostor)
                PlayerControl.LocalPlayer.nameText.color = Palette.ImpostorRed;
            else
                PlayerControl.LocalPlayer.nameText.color = Palette.White;
            foreach (PlayerVoteArea player in __instance.playerStates)
                if (player.TargetPlayerId == PlayerControl.LocalPlayer.PlayerId)
                    foreach (RoleGenerator role in allRoles)
                        if (role.containedPlayerIds.Contains(PlayerControl.LocalPlayer.PlayerId))
                            player.NameText.color = role.RoleColor;
        }
    }
}
