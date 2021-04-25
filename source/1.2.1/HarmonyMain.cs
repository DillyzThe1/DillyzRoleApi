using System;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Extensions;
using UnityEngine;
using static DillyzRolesAPI.Roles.NewRole;

namespace DillyzRolesAPI
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class RoleAPI : BasePlugin
    {
        public const string Id = "gg.reactor.0dillyzroleapi";

        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> Name { get; private set; }

        public override void Load()
        {
            Harmony.PatchAll();
            RegisterInIl2CppAttribute.Register();
            RegisterCustomRpcAttribute.Register(this);
        }
    }
}
