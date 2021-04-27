using Hazel;
using System.Collections.Generic;
using System.Linq;
using static DillyzRolesAPI.Roles.NewRole;
using DillyzRolesAPI.Roles;
using HarmonyLib;
using Reactor;
using Reactor.Networking;
using UnhollowerBaseLib;
using static DillyzRolesAPI.Roles.Extensions;
using DillyzRolesAPI.Options;

namespace DillyzRolesAPI.RPC
{
    /*enum CustomRPC
    {
        SetRole = 53,
        SetLocalPlayers = 54,
        ResetVariiables = 55,
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class HandleRpcPatch
    {
        static void Postfix(byte ONIABIILFGF, MessageReader JIGFBHFFNFI)
        {
            byte callId = ONIABIILFGF;
            MessageReader reader = JIGFBHFFNFI;
            switch (callId)
            {
                case (byte)CustomRPC.SetRole:
                    byte bobux = reader.ReadByte();
                    string roleName = reader.ReadString();
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                        if (player.PlayerId == bobux)
                            player.getModdedControl().Role = roleName;
                    break;
                case (byte)CustomRPC.SetLocalPlayers:
                    localPlayers.Clear();
                    localPlayer = PlayerControl.LocalPlayer;
                    var localPlayerBytes = reader.ReadBytesAndSize();
                    foreach (byte id in localPlayerBytes)
                        foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                            if (player.PlayerId == id)
                                localPlayers.Add(player);
                    break;
                case (byte)CustomRPC.ResetVariiables:
                    Main.Config.SetConfigSettings();
                    Main.Logic.AllModPlayerControl.Clear();
                    killedPlayers.Clear();
                    List<PlayerControl> crewmates = PlayerControl.AllPlayerControls.ToArray().ToList();
                    foreach (PlayerControl plr in crewmates)
                        Main.Logic.AllModPlayerControl.Add(new ModPlayerControl { PlayerControl = plr, Role = "Impostor", UsedAbility = false, LastAbilityTime = null, Immortal = false });
                    crewmates.RemoveAll(x => x.Data.IsImpostor);
                    foreach (PlayerControl plr in crewmates)
                        plr.getModdedControl().Role = "Crewmate";
                    break;
            }
        }
    }*/ // Please use the reactor rpc api, i'm a dummy for not doing so before.

    public enum CustomRpcCalls : uint
    {
        setRole,
        setLocalVars,
        resetVars,
        sendOption
    }
    [RegisterCustomRpc((uint)CustomRpcCalls.setRole)]
    public class SetRole : PlayerCustomRpc<RoleAPI, (byte, string)> //killer id, player id
    {
        public SetRole(RoleAPI plugin, uint id) : base(plugin, id)
        { }
        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;
        public override void Write(MessageWriter writer, (byte, string) data)
        {
            writer.Write(data.Item1);
            writer.Write(data.Item2);
        }
        public override (byte, string) Read(MessageReader reader)
        {
            byte item1 = reader.ReadByte();
            string item2 = reader.ReadString();
            return (item1, item2);
        }
        public override void Handle(PlayerControl innerNetObject, (byte, string) data)
        {
            foreach (RoleGenerator role in allRoles)
                if (role.NameOfRole == data.Item2)
                    role.containedPlayerIds.Add(data.Item1);
        }
    }
    [RegisterCustomRpc((uint)CustomRpcCalls.setLocalVars)]
    public class SetLocalVars : PlayerCustomRpc<RoleAPI, (Il2CppStructArray<byte>, int)> //killer id, player id
    {
        public SetLocalVars(RoleAPI plugin, uint id) : base(plugin, id)
        { }
        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;
        public override void Write(MessageWriter writer, (Il2CppStructArray<byte>, int) data)
        {
            writer.Write(data.Item1);
        }
        public override (Il2CppStructArray<byte>, int) Read(MessageReader reader)
        {
            Il2CppStructArray<byte> item1 = reader.ReadBytesAndSize();
            return (item1, 0);
        }
        public override void Handle(PlayerControl innerNetObject, (Il2CppStructArray<byte>, int) data)
        {
            foreach (byte id in data.Item1)
                foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                    if (player.PlayerId == id)
                        localPlayers.Add(player);
        }
    }
    [RegisterCustomRpc((uint)CustomRpcCalls.resetVars)]
    public class ResetVars : PlayerCustomRpc<RoleAPI, (int, int)> //killer id, player id
    {
        public ResetVars(RoleAPI plugin, uint id) : base(plugin, id)
        { }
        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;
        public override void Write(MessageWriter writer, (int, int) data)
        { }
        public override (int, int) Read(MessageReader reader)
        { return (0, 0); }
        public override void Handle(PlayerControl innerNetObject, (int, int) data)
        {
            Main.Config.SetConfigSettings();
            Main.Logic.AllModPlayerControl.Clear();
            killedPlayers.Clear();
            List<PlayerControl> crewmates = PlayerControl.AllPlayerControls.ToArray().ToList();
            crewmates.RemoveAll(x => x.Data.IsImpostor);
            foreach (RoleGenerator role in allRoles)
            {
                role.containedPlayerIds.Clear();
                role.containedPlayerIds = new List<byte>();
            }
        }
    }
    [RegisterCustomRpc((uint)CustomRpcCalls.sendOption)]
    public class SendOpt : PlayerCustomRpc<RoleAPI, (bool, float, bool, int)> // isNumber, numbValue, boolValue, optTitle
    {
        public SendOpt(RoleAPI plugin, uint id) : base(plugin, id)
        { }
        public override RpcLocalHandling LocalHandling => RpcLocalHandling.None;
        public override void Write(MessageWriter writer, (bool, float, bool, int) data)
        {
            writer.Write(data.Item1);
            writer.Write(data.Item2);
            writer.Write(data.Item3);
            writer.Write(data.Item4);
        }
        public override (bool, float, bool, int) Read(MessageReader reader)
        {
            bool item1 = reader.ReadBoolean();
            float item2 = reader.ReadSingle();
            bool item3 = reader.ReadBoolean();
            int item4 = reader.ReadInt32();
            return (item1, item2, item3, item4);
        }
        public override void Handle(PlayerControl innerNetObject, (bool, float, bool, int) data)
        {
            if (data.Item1)
            {
                foreach (CustomNumberOption custom in CustomOptions.numOpts)
                    if (custom.optTitleInt == data.Item4)
                        custom.value = data.Item2;
            }
            else
            {
                foreach (CustomBoolOption custom in CustomOptions.boolOpts)
                    if (custom.optTitleInt == data.Item4)
                        custom.value = data.Item3;
            }
        }
    }
}
