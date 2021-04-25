using System;
using System.Runtime.InteropServices;
using DillyzRolesAPI.Roles;
using HarmonyLib;
using static DillyzRolesAPI.Roles.Extensions;
using System.Drawing.Text;
using System.Drawing;

namespace DillyzRolesAPI
{
    [HarmonyPatch(typeof(IntroCutscene.Nested_0), nameof(IntroCutscene.Nested_0.MoveNext))]
    class IntroCutscenePatch
    {
        static void Postfix(IntroCutscene.Nested_0 __instance)
        {
            foreach (RoleGenerator role in allRoles)
                if (role.containedPlayerIds.Contains(PlayerControl.LocalPlayer.PlayerId))
                {
                    __instance.__this.Title.text = role.NameOfRole;
                    __instance.__this.Title.color = role.RoleColor;
                    __instance.__this.ImpostorText.text = role.IntroText;
                    __instance.__this.BackgroundBar.material.color = role.RoleColor;
                    
                }
        }
    }
}
