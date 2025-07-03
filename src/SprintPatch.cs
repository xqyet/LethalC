using GameNetcodeStuff;
using HarmonyLib;

namespace LethalESP
{
    [HarmonyPatch(typeof(PlayerControllerB), "Update")]
    internal static class SprintPatch
    {
        private static void Postfix(PlayerControllerB __instance)
        {
            if (__instance != null && __instance.isPlayerControlled)
            {
                __instance.sprintMeter = 1f;
                // not needed
                // __instance.sprintCooldown = 0f;
            }
        }
    }
}
