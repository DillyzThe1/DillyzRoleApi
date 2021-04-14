using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace DillyzRolesAPI
{
    // https://github.com/CrowdedMods/CrowdedMod/blob/master/src/CrowdedMod/Patches/GenericPatches.cs
    class ColorPatches
    {
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CheckColor), typeof(byte))]
        public static class CheckColorPatch
        {
            public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] byte colorId)
            {
                __instance.RpcSetColor(colorId);
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayerTab), nameof(PlayerTab.UpdateAvailableColors))]
        public static class AvailableColorPatch
        {
            public static bool Prefix(PlayerTab __instance)
            {
                PlayerControl.SetPlayerMaterialColors(PlayerControl.LocalPlayer._cachedData.ColorId, __instance.DemoImage);
                for (int i = 0; i < Palette.PlayerColors.Length; i++)
                    __instance.AvailableColors.Add(i);
                return false;
            }
        }
    }
}
