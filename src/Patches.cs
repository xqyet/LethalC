using HarmonyLib;
using UnityEngine;

namespace LethalESP
{
    internal class Patches
    {
        [HarmonyPatch(typeof(GrabbableObject), "Start")]
        internal static class GrabbableObject_Start
        {
            private static void Postfix(GrabbableObject __instance)
            {
                if (!LethalESP.Instance.GrabbableObjects.Contains(__instance))
                {
                    LethalESP.Instance.GrabbableObjects.Add(__instance);
                }
            }
        }

        [HarmonyPatch(typeof(Turret), "Start")]
        internal static class Turret_Start
        {
            private static void Postfix(Turret __instance)
            {
                if (!LethalESP.Instance.Turrets.Contains(__instance))
                {
                    LethalESP.Instance.Turrets.Add(__instance);
                }
            }
        }

        [HarmonyPatch(typeof(Landmine), "Start")]
        internal static class Landmine_Start
        {
            private static void Postfix(Landmine __instance)
            {
                if (!LethalESP.Instance.Landmines.Contains(__instance))
                {
                    LethalESP.Instance.Landmines.Add(__instance);
                }
            }
        }

        // 🔧 New patch to attach the ESP runner so rendering works!
        [HarmonyPatch(typeof(HUDManager), "Start")]
        internal static class HUDManager_Start_Patch
        {
            private static void Postfix(HUDManager __instance)
            {
                if (__instance.GetComponent<LethalESPRunner>() == null)
                {
                    var runner = __instance.gameObject.AddComponent<LethalESPRunner>();
                    Object.DontDestroyOnLoad(runner);
                    Debug.Log("[LethalESP] ESPRunner successfully attached to HUDManager.");
                }
            }
        }
    }
}
