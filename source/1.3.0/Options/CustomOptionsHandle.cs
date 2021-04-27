using System;
using System.Collections.Generic;
using HarmonyLib;
using Reactor.Networking;
using UnhollowerBaseLib;
using UnityEngine;
using Reactor.Extensions;
using DillyzRolesAPI.RPC;

namespace DillyzRolesAPI.Options
{
    //https://github.com/Galster-dev/CrowdedSheriff/blob/master/src/OptionsPatches.cs
    static class CustomOptions
    {
        public static int stringId = 313; // goes up with each option
        public static List<CustomNumberOption> numOpts = new List<CustomNumberOption>();
        public static List<CustomBoolOption> boolOpts = new List<CustomBoolOption>();

        //[HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetString))]
        // i had trouble with this when trying to convert it over, but i figured using it as just a custom thing works anyway
        static class customTranslationController
        {
            public static bool Prefix(StringNames __stringNames, ref string __result)
            {
                bool wasCustom = false;
                foreach (CustomNumberOption custom in numOpts)
                    if (__stringNames == custom.numOptionTitle)
                    {
                        wasCustom = true;
                        __result = custom.hostOptionsName;
                    }
                foreach (CustomBoolOption custom in boolOpts)
                    if (__stringNames == custom.boolOptionTitle)
                    {
                        wasCustom = true;
                        __result = custom.hostOptionsName;
                    }
                if (wasCustom)
                    return true;
                else
                    return false;
            }
        }
        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Start))]
        static class optionsMenuStartPatch
        {
            public static void OnValueChanged(OptionBehaviour option)
            {
                if (!AmongUsClient.Instance || !AmongUsClient.Instance.AmHost) return;
                /*foreach (CustomNumberOption custom in numOpts)
                    if (option.Title == custom.numOptionTitle)
                    {
                        //custom.value = option.GetInt();
                        var checkValue = option.GetInt();
                        if (checkValue > custom.maxValue)
                            custom.value = custom.maxValue;
                        if (checkValue < custom.minValue)
                            custom.value = custom.minValue;
                        // patched increase & decrease now for infinite range
                    }*/
                foreach (CustomBoolOption custom in boolOpts)
                    if (option.Title == custom.boolOptionTitle)
                        custom.value = option.GetBool();
                if (PlayerControl.GameOptions.isDefaults)
                {
                    PlayerControl.GameOptions.isDefaults = false;
                    UnityEngine.Object.FindObjectOfType<GameOptionsMenu>().Update();
                }
                var local = PlayerControl.LocalPlayer;
                if (local != null)
                    local.RpcSyncSettings(PlayerControl.GameOptions);
            }
            static void Postfix(ref GameOptionsMenu __instance)
            {
                var thisPos = -8.35f;
                var killCool = __instance.GetComponentsInChildren<NumberOption>()[1];
                foreach (NumberOption num in __instance.GetComponentsInChildren<NumberOption>())
                    if (num.name == "KillCooldown")
                        killCool = num;

                foreach (CustomNumberOption custom in numOpts)
                {

                    var countOption = UnityEngine.Object.Instantiate(killCool, __instance.transform);
                    countOption.transform.localPosition = new Vector3(countOption.transform.localPosition.x, thisPos, countOption.transform.localPosition.z);
                    countOption.Title = custom.numOptionTitle;
                    countOption.Value = custom.value;
                    var str = "";
                    customTranslationController.Prefix(countOption.Title, ref str);
                    countOption.TitleText.text = str;
                    countOption.OnValueChanged = new Action<OptionBehaviour>(OnValueChanged);
                    countOption.gameObject.AddComponent<OptionBehaviour>();
                    __instance.GetComponentInParent<Scroller>().YBounds.max += 0.3f;
                    thisPos -= 0.50f;
                }

                foreach (CustomBoolOption custom in boolOpts)
                {
                    var toggleOption = UnityEngine.Object.Instantiate(__instance.GetComponentsInChildren<ToggleOption>()[1], __instance.transform);
                    toggleOption.transform.localPosition = new Vector3(toggleOption.transform.localPosition.x, thisPos, toggleOption.transform.localPosition.z);
                    toggleOption.Title = custom.boolOptionTitle;
                    toggleOption.CheckMark.enabled = custom.value;
                    var str2 = "";
                    customTranslationController.Prefix(toggleOption.Title, ref str2);
                    toggleOption.TitleText.text = str2;
                    toggleOption.OnValueChanged = new Action<OptionBehaviour>(OnValueChanged);
                    toggleOption.gameObject.AddComponent<OptionBehaviour>();
                    __instance.GetComponentInParent<Scroller>().YBounds.max += 0.3f;
                    thisPos -= 0.50f;
                }
            }
        }
        [HarmonyPatch(typeof(GameSettingMenu), nameof(GameSettingMenu.OnEnable))]
        static class gameSettingEnablePatch
        {
            static void Prefix(ref GameSettingMenu __instance)
            {
                __instance.HideForOnline = new Il2CppReferenceArray<Transform>(0);
            }
        }
        [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.OnEnable))]
        static class numboptEnablePatch
        {
            static bool Prefix(ref NumberOption __instance)
            {
                foreach (CustomNumberOption custom in numOpts)
                    if (__instance.Title == custom.numOptionTitle)
                    {
                        string title = "";
                        customTranslationController.Prefix(__instance.Title, ref title);
                        __instance.TitleText.text = title;
                        __instance.OnValueChanged = new Action<OptionBehaviour>(optionsMenuStartPatch.OnValueChanged);
                        __instance.Value = custom.value;
                        __instance.enabled = true;

                        return false;
                    }
                return true;
            }
        }
        [HarmonyPatch(typeof(ToggleOption), nameof(ToggleOption.OnEnable))]
        static class toggleoptEnablePatch
        {
            static bool Prefix(ref ToggleOption __instance)
            {
                foreach (CustomBoolOption custom in boolOpts)
                    if (__instance.Title == custom.boolOptionTitle)
                    {
                        string str = "";
                        customTranslationController.Prefix(__instance.Title, ref str);
                        __instance.TitleText.text = str;
                        __instance.CheckMark.enabled = custom.defValue;
                        __instance.OnValueChanged = new Action<OptionBehaviour>(optionsMenuStartPatch.OnValueChanged);
                        __instance.enabled = true;

                        return false;
                    }
                return true;
            }
        }
        [HarmonyPatch(typeof(GameOptionsData), nameof(GameOptionsData.ToHudString))]
        static class optionsDataToHudPatch
        {
            static void Postfix(ref string __result)
            {
                var builder = new System.Text.StringBuilder(__result);
                foreach (CustomNumberOption custom in numOpts)
                    builder.AppendLine(custom.hostOptionsName + $": {(custom.suffix.ToString() != " " ? custom.value.ToString() + custom.suffix.ToString() : custom.value.ToString()/*.ToString().Contains(".") ? $"{custom.value}f" : $"{custom.value}"*/)}");
                foreach (CustomBoolOption custom in boolOpts)
                    builder.AppendLine(custom.hostOptionsName + $": {(custom.value ? "On" : "Off")}");
                __result = builder.ToString();
            }
        }

        [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.Increase))]
        class numberRaisePatch
        {
            public static void Postfix(ref NumberOption __instance)
            {
                foreach (CustomNumberOption custom in numOpts)
                    if (__instance.Title == custom.numOptionTitle)
                    {
                        custom.value += custom.incrementValue;
                        if (custom.value > custom.maxValue)
                            custom.value = custom.maxValue;
                        if (custom.value < custom.minValue)
                            custom.value = custom.minValue;
                        __instance.Value = custom.value;
                        if (AmongUsClient.Instance.AmHost)
                            Rpc<SendOpt>.Instance.Send((true, custom.value, false, custom.optTitleInt));
                    }
            }
        }
        [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.Decrease))]
        class numberDropPatch
        {
            public static void Postfix(ref NumberOption __instance)
            {
                foreach (CustomNumberOption custom in numOpts)
                    if (__instance.Title == custom.numOptionTitle)
                    {
                        custom.value -= custom.incrementValue;
                        if (custom.value > custom.maxValue)
                            custom.value = custom.maxValue;
                        if (custom.value < custom.minValue)
                            custom.value = custom.minValue;
                        __instance.Value = custom.value;
                        if (AmongUsClient.Instance.AmHost)
                            Rpc<SendOpt>.Instance.Send((true, custom.value, false, custom.optTitleInt));
                    }
            }
        }
        [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.FixedUpdate))]
        class numberFixUpdPatch
        {
            public static void Postfix(ref NumberOption __instance)
            {
                foreach (CustomNumberOption custom in numOpts)
                    if (__instance.Title == custom.numOptionTitle)
                    {
                        __instance.ValueText.text = custom.value.ToString() + $"{(custom.suffix.ToString() != " " ? custom.suffix.ToString() : "")}";
                        //Rpc<SendOpt>.Instance.Send((false, custom.value, false, custom.optTitleInt));
                    }
            }
        }
        [HarmonyPatch(typeof(ToggleOption), nameof(ToggleOption.Toggle))]
        class toggleOptPatch
        {
            public static void Postfix(ref ToggleOption __instance)
            {
                foreach (CustomBoolOption custom in boolOpts)
                    if (__instance.Title == custom.boolOptionTitle)
                    {
                        if (AmongUsClient.Instance.AmHost)
                            Rpc<SendOpt>.Instance.Send((false, 0f, custom.value, custom.optTitleInt));
                    }
            }
        }
    }
}
