using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;
using static DillyzRolesAPI.Roles.NewRole;
using DillyzRolesAPI.RPC;
using static DillyzRolesAPI.Roles.Extensions;

namespace DillyzRolesAPI.Roles
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    class SetInfectedPatch
    {
        public static void Postfix(Il2CppReferenceArray<GameData.PlayerInfo> BHNEINNHPIJ)
        {
            Main.Config.SetConfigSettings();
            Main.Logic.AllModPlayerControl.Clear();
            killedPlayers.Clear();
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ResetVariiables, Hazel.SendOption.None, -1);
            //     writer.Write(writer.ToString());a
            AmongUsClient.Instance.FinishRpcImmediately(writer);
            List<PlayerControl> crewmates = PlayerControl.AllPlayerControls.ToArray().ToList();
            foreach (PlayerControl plr in crewmates)
                Main.Logic.AllModPlayerControl.Add(new ModPlayerControl { PlayerControl = plr, Role = "Impostor", UsedAbility = false, LastAbilityTime = null, Immortal = false });
            crewmates.RemoveAll(x => x.Data.IsImpostor);
            foreach (PlayerControl plr in crewmates)
                plr.getModdedControl().Role = "Crewmate";
            foreach (PlayerControl plr in PlayerControl.AllPlayerControls)
            {
                if (!plr.Data.IsImpostor)
                    plr.getModdedControl().Role = "Crewmate";
                else
                    plr.getModdedControl().Role = "Impostor";
            }
            foreach (RoleGenerator role in allRoles)
                if (role.isEnabled)
                {
                    MessageWriter writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetRole, Hazel.SendOption.None, -1);
                    var roleRandom = rng.Next(0, crewmates.Count);
                    List<PlayerControl> therealones = new List<PlayerControl>();
                    PlayerControl borgorking = null;
                    foreach (PlayerControl player in crewmates)
                        if (player != crewmates[roleRandom])
                            therealones.Add(player);
                        else
                            borgorking = player;
                    crewmates = therealones;
                    borgorking.getModdedControl().Role = role.NameOfRole;
                    writer2.Write(borgorking.PlayerId);
                    writer2.Write(role.NameOfRole);
                    AmongUsClient.Instance.FinishRpcImmediately(writer2);
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
            writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetLocalPlayers, Hazel.SendOption.None, -1);
            writer.WriteBytesAndSize(localPlayerBytes.ToArray());
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
    public static class quickChecks
    {
        public static bool containsAnyRole(PlayerControl plr)
        {
            foreach (RoleGenerator role in allRoles)
                if (plr.getModdedControl().Role == role.NameOfRole)
                    return true;
            return false;
        }
    }
}
