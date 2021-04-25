using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;
using static DillyzRolesAPI.Roles.Extensions;
using static DillyzRolesAPI.Roles.NewRole;
using static DillyzRolesAPI.Roles.OtherExtension;
using Reactor;

namespace DillyzRolesAPI.Roles
{
    public class staticvars
    {
        public static bool noColorYoinking = true;
        public static bool shamelessDiscordPromo = false; // DON'T ENABLE! It's experimental.
    }
    [RegisterInIl2Cpp]
    public class RoleGenerator : MonoBehaviour
    {
        public string NameOfRole { get; set; }
        public Color RoleColor { get; set; }
        public string IntroText { get; set; }
        public string EjectionText { get; set; }
        public bool isEnabled { get; set; }
        public bool canVent { get; set; }
        public List<byte> containedPlayerIds { get; set; }
        public void Awake()
        {
            this.containedPlayerIds = new List<byte>();
            allRoles.Add(this);
            //Reactor.Logger<RoleAPI>.Message(this.NameOfRole + " added.");
        }
        //[]
        /*public bool isThisRole(int PlrID)
        {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (PlrID == player.PlayerId && player.getModdedControl().Role != null)
                    if (player.getModdedControl().Role == this.NameOfRole)
                        return true;
            return false;
        }*/ // use role.players.contains(PlayerId);
    }
    /*[RegisterInIl2Cpp]
    public class RoleCheck : MonoBehaviour
    {
        public bool isAnyRole(int PlrID)
        {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (PlrID == player.PlayerId)
                    foreach (RoleGenerator role in allRoles)
                        if (role.containedPlayerIds.Contains(player.PlayerId))
                            return true;
            return false;
        }
        public bool isThisRole(int PlrID, RoleGenerator requiredRole)
        {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (PlrID == player.PlayerId)
                    if (requiredRole.containedPlayerIds.Contains(PlayerControl.LocalPlayer.PlayerId))
                        return true;
            return false;
        }
        public ModPlayerControl getModControl(int PlrID)
        {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (PlrID == player.PlayerId)
                    return Main.Logic.AllModPlayerControl.Find(x => x.PlayerControl == player);
            return null;
        }
        //public static void canYoinkColors(bool hi)
        //{
        //    staticvars.noColorYoinking = !hi;
        //}
    }*/
}
