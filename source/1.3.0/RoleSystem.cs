using HarmonyLib;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static DillyzRolesAPI.Roles.NewRole;
using Reactor.Extensions;
using static UnityEngine.UI.Button;
using static Rewired.Controller;
using UnityEngine.UI;

namespace DillyzRolesAPI.Roles
{
    public class DeadPlayer
    {
        public byte KillerId { get; set; }
        public byte PlayerId { get; set; }
        public DateTime KillTime { get; set; }
        public DeathReason DeathReason { get; set; }
    }
    public class Extensions
    {
        public static List<RoleGenerator> allRoles = new List<RoleGenerator>();
        //there used to be a "this" before PlayerControl, but vs didnt like that...
        //public bool isPlayerRole(PlayerControl plr, RoleGenerator role)
        //{
        //    return role.containedPlayerIds.Contains(PlayerControl.LocalPlayer.PlayerId);
        //}
        public bool isPlayerImmortal(PlayerControl plr)
        {
            if (plr.getModdedControl() != null)
            {
                return plr.getModdedControl().Immortal;
            }
            else
            {
                return false;
            }
        }
    }
    public static class OtherExtension
    {
        public static ModPlayerControl getModdedControl(this PlayerControl plr)
        {
            return Main.Logic.AllModPlayerControl.Find(x => x.PlayerControl == plr);
        }
    }
    [HarmonyPatch]
    public class NewRole
    {
        public static class Main
        {
            public static Assets Assets = new Assets();
            public static ModdedPalette Palette = new ModdedPalette();
            public static ModdedConfig Config = new ModdedConfig();
            public static ModdedLogic Logic = new ModdedLogic();
        }
        public class ModdedLogic
        {
            //public ModPlayerControl getRolePlayer(string roleName)
            //{
            //    return Main.Logic.AllModPlayerControl.Find(x => x.Role == roleName);
            //}
            public ModPlayerControl getImmortalPlayer()
            {
                return Main.Logic.AllModPlayerControl.Find(x => x.Immortal);
            }
            public bool anyPlayerImmortal()
            {
                return Main.Logic.AllModPlayerControl.FindAll(x => x.Immortal).Count > 0;
            }
            public List<ModPlayerControl> AllModPlayerControl = new List<ModPlayerControl>();
            public bool sabotageActive { get; set; }
        }

        public class Assets
        { }
        public class ModdedPalette
        {
            public Color exampleColor = new Color(0.55f, 0.55f, 0.55f, 1f);
        }
        public class ModPlayerControl
        {
            public PlayerControl PlayerControl { get; set; }
            //public string Role { get; set; }
            public DateTime? LastAbilityTime { get; set; }
            public bool UsedAbility { get; set; }
            public bool Immortal { get; set; }
        }
        public static GameObject rend;
        public static List<DeadPlayer> killedPlayers = new List<DeadPlayer>();
        public static PlayerControl currentTarget = null;
        public static PlayerControl localPlayer = null;
        public static List<PlayerControl> localPlayers = new List<PlayerControl>();
        public static System.Random rng = new System.Random();
        public static KillButtonManager KillButton;
        public static int KBTarget;
        public static double DistLocalClosest;
        public static string versionString = "1.3.0";
        public class ModdedConfig
        {
            public bool undeadSpeaks { get; set; }
            public float flashTimer { get; set; }
            public float extCool { get; set; }
            public void SetConfigSettings()
            { }
        }
        [HarmonyPatch(typeof(VersionShower), "Start")]
        public static class VersionStartPatch
        {
            static void Postfix(VersionShower __instance)
            {
                var OGTEXT = __instance.text.text;
                var obj = new GameObject();
                foreach (GameObject gameObj in UnityEngine.Object.FindObjectsOfType<GameObject>())
                    if (gameObj.name.StartsWith("ReactorVersion"))
                        obj = gameObj;
                if (obj != null) GameObject.Destroy(obj);
                __instance.text.text = "\n\n";
                if (modsText != null)
                    foreach (string str in modsText)
                        __instance.text.text += "\n";
                __instance.text.text += OGTEXT;
                if (modsText != null)
                    foreach (string str in modsText)
                        __instance.text.text += "\n" + str;
                __instance.text.text += "\nRole API <#F6FF00>" + versionString + "</color> by <#3AA3D9>DillyzThe1</color>.";
                __instance.text.text += "\nReactor API <#F6FF00>2021.4.12s</color> by <#3AA3D9>js6pak</color>.";
                __instance.transform.position = new Vector3(-5.24f,2.85f,-5f);
            }
        }
        public static List<string> modsText = new List<string>();
        public static List<string> pingText = new List<string>();
        [HarmonyPatch(typeof(PingTracker), "Update")]
        public static class PingPatch
        {
            public static void Postfix(PingTracker __instance)
            {
                string ping = __instance.text.text;
                __instance.text.text = "";
                foreach (string str in pingText)
                    __instance.text.text += "\n";
                __instance.text.text += ping;
                foreach (string str in pingText)
                    __instance.text.text += "\n" + str;
                __instance.text.text += "\nDillyzRoleApi " + versionString + "\n<#3AA3D9>github.com/DillyzThe1</color>";
            }
        }
        [HarmonyPatch(typeof(StatsManager), nameof(StatsManager.AmBanned), MethodType.Getter)]
        public static class AmBannedPatch
        {
            public static void Postfix(out bool __result)
            {
                __result = false;
            }
        }

        /*[HarmonyPatch(typeof(CreditsScreenPopUp), nameof(CreditsScreenPopUp.OnEnable))]
        class creditPatch
        {
            public static void Postfix(CreditsScreenPopUp __instance)
            {
                if (staticvars.shamelessDiscordPromo)
                {
                    Application.OpenURL("https://discord.gg/49NFTwcYgZ");
                    if (__instance.gameObject != null && __instance.gameObject.active)
                        __instance.gameObject.SetActive(false);
                }
            }
        }*/
        //[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
        public class passiveCreditsPatch
        {
            public static AssetBundle bundle; // this is set aswell
            public static void Postfix()
            {
                if (bundle == null)
                {
                    byte[] array = Properties.Resources.sus;
                    passiveCreditsPatch.bundle = AssetBundle.LoadFromMemory(array);
                }
                /*if (staticvars.shamelessDiscordPromo)
                {
                    GameObject thatDiscButton = GameObject.Instantiate(passiveCreditsPatch.bundle.LoadAsset<GameObject>("SelfPromo").DontUnload());
                    thatDiscButton.transform.parent = GameObject.Find("BottomButtons").transform;
                    thatDiscButton.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                    thatDiscButton.GetComponent<ButtonBehavior>().OnClick.AddListener((UnityEngine.Events.UnityAction)listener);
                    thatDiscButton.GetComponent<Image>().sprite = passiveCreditsPatch.bundle.LoadAsset<Sprite>("discord").DontUnload();
                    thatDiscButton.name = "ShamelessSelfPromo"; // please tell me what i did wrong
                }*/
            }
            public static void listener()
            {
                Application.OpenURL("https://discord.gg/49NFTwcYgZ");
            }
        }
        // hold on wait wait i moved something to patch in anticheat // update: its above u
        /*public static class AssetLoader
        {
            public static AssetBundle bundle;

            public static Sprite dsicord;
            public static void BundleLoad()
            {
                if (bundle == null)
                {
                    byte[] array = Properties.Resources.sus;
                    bundle = AssetBundle.LoadFromMemory(array);
                }
                dsicord = bundle.LoadAsset<Sprite>("discord").DontUnload();
            }
        }*/
    }
}
