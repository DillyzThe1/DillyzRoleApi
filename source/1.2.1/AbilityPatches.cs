using System;
using System.Collections.Generic;
using System.Text;
using DillyzRolesAPI.Roles;
using HarmonyLib;
using UnityEngine;
using static DillyzRolesAPI.Roles.Extensions;

namespace DillyzRolesAPI.Abilities
{
    [HarmonyPatch(typeof(Vent),nameof(Vent.CanUse))]
    class AbilityPatches
    {
        public static void Postfix(Vent __instance,[HarmonyArgument(0)] GameData.PlayerInfo playerinfo,[HarmonyArgument(1)] ref bool canUse,[HarmonyArgument(0)] ref bool couldUse,ref float __result)
        {
            float dist = float.MaxValue;
            var obj = playerinfo.Object;
            canUse = couldUse = (obj.CanMove || obj.inVent) && roleCanVent(obj);
            if (canUse)
            {
                Vector3 truepos = obj.GetTruePosition();
                Vector3 pos = __instance.transform.position;
                dist = Vector2.Distance(truepos, pos);
                canUse &= (dist <= __instance.UsableDistance);
            }
            __result = dist;
        }
        public static bool roleCanVent(PlayerControl player)
        {
            foreach (RoleGenerator role2 in allRoles)
                if (role2.containedPlayerIds.Contains(player.PlayerId))
                    if (role2.canVent)
                        return true;
            if (player.Data.IsImpostor)
                return true;
            return false;
        }
    }
}
