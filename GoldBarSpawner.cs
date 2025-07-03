using GameNetcodeStuff;
using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LethalESP
{
    [HarmonyPatch(typeof(PlayerControllerB), "Update")]
    internal static class GoldBarSpawner
    {
        private static void Postfix(PlayerControllerB __instance)
        {
            if (__instance != null && __instance.isPlayerControlled && Keyboard.current.lKey.wasPressedThisFrame)
            {
                // Find Gold Bar item definition
                Item goldBarItem = StartOfRound.Instance.allItemsList.Find(item => item.itemName.ToLower().Contains("gold bar"));

                if (goldBarItem == null)
                {
                    LethalESP.Instance.Logger.LogWarning("Gold Bar item not found!");
                    return;
                }

                // Spawn position in front of player
                Vector3 spawnPosition = __instance.transform.position + __instance.transform.forward * 2f + Vector3.up * 1f;

                // Instantiate the grabbable object
                GameObject goldBarObject = Object.Instantiate(goldBarItem.spawnPrefab, spawnPosition, Quaternion.identity);

                NetworkObject netObj = goldBarObject.GetComponent<NetworkObject>();
                if (netObj != null)
                {
                    netObj.Spawn(true);
                }

                LethalESP.Instance.Logger.LogInfo("Spawned Gold Bar.");
            }
        }
    }
}
