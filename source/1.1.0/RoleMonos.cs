using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;
using static DillyzRolesAPI.Roles.Extensions;
using static DillyzRolesAPI.Roles.NewRole;
using static DillyzRolesAPI.Roles.OtherExtension;

namespace DillyzRolesAPI.Roles
{
    public class staticvars
    {
        public static bool noColorYoinking = true;
    }
    public class RoleGenerator : MonoBehaviour
    {
        public string NameOfRole { get; set; }
        public Color RoleColor { get; set; }
        public string IntroText { get; set; }
        public string EjectionText { get; set; }
        public bool isEnabled { get; set; }
        public void Awake()
        {
            allRoles.Add(this);
            //Reactor.Logger<RoleAPI>.Message(this.NameOfRole + " added.");
        }
        public bool isThisRole(int PlrID)
        {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (PlrID == player.PlayerId)
                  if (player.getModdedControl().Role == this.NameOfRole)
                    return true;
            return false;
        }
    }
    public class RoleCheck : MonoBehaviour
    {
        public bool isAnyRole(int PlrID)
        {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (PlrID == player.PlayerId)
                    if (player.getModdedControl().Role != null)
                        return true;
            return false;
        }
        public bool isThisRole(int PlrID, string requiredRole)
        {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (PlrID == player.PlayerId)
                    if (player.getModdedControl().Role == requiredRole)
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
        public static void canYoinkColors(bool hi)
        {
            staticvars.noColorYoinking = !hi;
        }
    }
}
