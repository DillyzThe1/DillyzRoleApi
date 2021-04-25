using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;
using static DillyzRolesAPI.Roles.NewRole;
using DillyzRolesAPI.RPC;
using static DillyzRolesAPI.Roles.Extensions;
using Reactor.Networking;

namespace DillyzRolesAPI.Roles
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    class SetInfectedPatch
    {
        public static void Postfix([HarmonyArgument(0)] Il2CppReferenceArray<GameData.PlayerInfo> infected)
        {
            Main.Config.SetConfigSettings();
            Main.Logic.AllModPlayerControl.Clear();
            foreach (RoleGenerator role in allRoles)
            {
                role.containedPlayerIds.Clear();
                role.containedPlayerIds = new List<byte>();
            }
                
            killedPlayers.Clear();
            //     writer.Write(writer.ToString());a
            Rpc<ResetVars>.Instance.Send((0,0));
            List<PlayerControl> crewmates = PlayerControl.AllPlayerControls.ToArray().ToList();
            crewmates.RemoveAll(x => x.Data.IsImpostor);
            foreach (RoleGenerator role in allRoles)
                if (role.isEnabled)
                {
                    var roleRandom = rng.Next(0, crewmates.Count);
                    List<PlayerControl> therealones = new List<PlayerControl>();
                    PlayerControl borgorking = null;
                    foreach (PlayerControl player in crewmates)
                        if (player != crewmates[roleRandom])
                            therealones.Add(player);
                        else
                            borgorking = player;
                    crewmates = therealones;
                    role.containedPlayerIds.Add(borgorking.PlayerId);
                    Rpc<SetRole>.Instance.Send((borgorking.PlayerId, role.NameOfRole));
                }
            localPlayers.Clear();
            localPlayer = PlayerControl.LocalPlayer;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
            {
                if (player.Data.IsImpostor)
                    continue;
                if (quickChecks.containsAnyRole(player))
                    continue;
                else
                    localPlayers.Add(player);
            }
            var localPlayerBytes = new List<byte>();
            foreach (PlayerControl player in localPlayers)
            {
                localPlayerBytes.Add(player.PlayerId);
            }
            Rpc<SetLocalVars>.Instance.Send((localPlayerBytes.ToArray(),0));
        }
    }
    public static class quickChecks
    {
        public static bool containsAnyRole(PlayerControl plr)
        {
            foreach (RoleGenerator role in allRoles)
                if (role.containedPlayerIds.Contains(plr.PlayerId))
                    return true;
            return false;
        }
    }
}
