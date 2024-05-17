using System.Reflection;
using Aki.Reflection.Patching;
using EFT;
using HarmonyLib;

namespace InGameMap.Patches
{
    // internal class GameWorldOnGameStartedPatch : ModulePatch
    // {
    //     protected override MethodBase GetTargetMethod()
    //     {
    //         return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.OnGameStarted));
    //     }

    //     [PatchPostfix]
    //     public static void PatchPostfix(GameWorld __instance)
    //     {
    //         Plugin.Instance.Map?.OnRaidStart(__instance.MainPlayer.Location);
    //     }
    // }

    internal class GameWorldOnDestroyPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.OnDestroy));
        }

        [PatchPostfix]
        public static void PatchPostfix()
        {
            Plugin.Instance.Map?.OnRaidEnd();
        }
    }
}

