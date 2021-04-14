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
            if (MeetingHud.Instance == null)
                try
                {
                    if (PlayerControl.AllPlayerControls.Count > 1 && NameColorPatch.hasAnyRole(PlayerControl.LocalPlayer))
                        foreach (RoleGenerator role in allRoles)
                            if (role.NameOfRole == PlayerControl.LocalPlayer.getModdedControl().Role)
                                PlayerControl.LocalPlayer.nameText.color = role.RoleColor;
                }
                catch
                { }
            else
            {
                upMetPatch.updateMeetingHUD(MeetingHud.Instance);
            }
        }
        public static bool hasAnyRole(PlayerControl plr)
        {
            if (plr != null && allRoles != null && plr.getModdedControl().Role != null)
                foreach (RoleGenerator role in allRoles)
                    if (role.NameOfRole == plr.getModdedControl().Role)
                        return true;
            return false;
        }
    }
    //[HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
    class upMetPatch
    {
        public static void updateMeetingHUD(MeetingHud __instance)
        {
            foreach (PlayerVoteArea player in __instance.playerStates)
            {
                if (player.NameText.text == PlayerControl.LocalPlayer.nameText.text && NameColorPatch.hasAnyRole(PlayerControl.LocalPlayer))
                {
                    foreach (RoleGenerator role in allRoles)
                        if (role.NameOfRole == PlayerControl.LocalPlayer.getModdedControl().Role)
                            player.NameText.color = role.RoleColor;
                }
            }
        }
    }
}
