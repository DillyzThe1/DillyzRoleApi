using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HarmonyLib;
using InnerNet;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DillyzRolesAPI
{
    [HarmonyPatch(typeof(FindGameButton), nameof(FindGameButton.OnClick))]
    public class FindGameDisablePatch
    {
        public static void Prefix(FindGameButton __instance)
        {
            __instance.gameObject.SetActive(false);
            AmongUsClient.Instance.LastDisconnectReason = DisconnectReasons.Custom;
            AmongUsClient.Instance.LastCustomDisconnect = "Cheating on regular servers is <#FF6A00>STRICTLY</color> prohibited.\n<#FF0000>Don't</color> attempt it.";
            AmongUsClient.Instance.HandleDisconnect(AmongUsClient.Instance.LastDisconnectReason, AmongUsClient.Instance.LastCustomDisconnect);
        }
    }
    [HarmonyPatch(typeof(JoinGameButton), nameof(JoinGameButton.OnClick))]
    public class JoinGameCodePatchLmao
    {
        public static void Prefix(JoinGameButton __instance)
        {
            if (SceneManager.GetActiveScene().name == "MMOnline")
                if (__instance.transform.Find("GameIdText").transform.Find("Text_TMP").GetComponent<TextMeshPro>().m_text.Contains("SUS"))
                {
                    Application.OpenURL("https://youtu.be/dQw4w9WgXcQ");
                    AmongUsClient.Instance.LastDisconnectReason = DisconnectReasons.Custom;
                    AmongUsClient.Instance.LastCustomDisconnect = "When The <#FF0000>Impostor</color> is sus!\n(lmao get rickrolled)";//filepath;
                    AmongUsClient.Instance.HandleDisconnect(AmongUsClient.Instance.LastDisconnectReason, AmongUsClient.Instance.LastCustomDisconnect);
                }
        }
    }
    [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Start))]
    public class gameStartManagePatch
    {
        public static void Postfix(GameStartManager __instance)
        {
            if (SceneManager.GetActiveScene().name == "OnlineGame" && __instance.GameRoomName != null)
            {
                __instance.GameRoomName.transform.position = __instance.MakePublicButton.transform.position;
                __instance.MakePublicButton.gameObject.SetActive(false);
                __instance.StartButton.transform.position += new Vector3(0f, -0.9f, 0f);
                __instance.PlayerCounter.transform.position += new Vector3(0.1f, 0f, 0f);
                __instance.GameStartText.transform.position += new Vector3(0f, -0.9f, 0f);
                gameStartUpdatePatch.originalCode = __instance.GameRoomName.text;
            }
        }
    }
    [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Update))]
    public class gameStartUpdatePatch
    {
        public static string originalCode;
        public static void Postfix(GameStartManager __instance)
        {
            if (SceneManager.GetActiveScene().name == "OnlineGame" && originalCode.Contains("Code") && __instance.GameRoomName != null)
                if (AmongUsClient.Instance.AmHost)
                    if (Input.GetKey("c"))
                        __instance.GameRoomName.text = originalCode;
                    else
                        __instance.GameRoomName.text = "Hold C!";
                else
                    __instance.GameRoomName.text = "Requires host!";
            else
            {
                __instance.StartButton.transform.localPosition = new Vector3(0f, -0.5f, 0f);
                __instance.PlayerCounter.transform.localPosition = new Vector3(0.3f, -1.2f, 0f);
            }
        }
    } // i originally put this in the sensei mod, but decided to move it to the api.
    [HarmonyPatch(typeof(PassiveButton), nameof(PassiveButton.ReceiveClickDown))]
    class passivePatch
    {
        public static void Postfix(PassiveButton __instance)
        {
            if (__instance.name == "FreePlayButton")
            {
                Application.OpenURL("https://youtu.be/dQw4w9WgXcQ");
                AmongUsClient.Instance.LastDisconnectReason = DisconnectReasons.Custom;
                AmongUsClient.Instance.LastCustomDisconnect = "Why does <#FF0000>everyone</color> test in freeplay?\nIt's buggy with mods.";//filepath;
                AmongUsClient.Instance.HandleDisconnect(AmongUsClient.Instance.LastDisconnectReason, AmongUsClient.Instance.LastCustomDisconnect);
                //__instance.gameObject.SetActive(false);
            }
            //Reactor.Logger<RoleAPI>.Message(__instance.name);
        }
    }
}
