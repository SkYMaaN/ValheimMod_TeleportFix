using BepInEx;
using HarmonyLib;
using UnityEngine;
using BepInEx.Configuration;

namespace ValheimMod
{
    [BepInPlugin("SkYMaN.ValheimMod_TeleportActivationDistanceFix", "TeleportActivationDistanceFix", "1.0.2")]
    [BepInProcess("valheim.exe")]
    public class Valheim_TeleportActivationDistanceClass : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("SkYMaN.ValheimMod_TeleportActivationDistanceFix");
        private static ConfigEntry<float> configEntry_TeleportActivatingDistance;
        void Awake()
        {
            Debug.Log("Starting loading mod - TeleportActivationDistanceFix");
            harmony.PatchAll();
            configEntry_TeleportActivatingDistance = Config.Bind<float>("General", "Portal Activation Distance", 3f, "Portal Activation Distance");
            Debug.Log("Finish loading mod - TeleportActivationDistanceFix");
        }
        [HarmonyPatch(typeof(TeleportWorld), "Awake")]
        class TeleportFix
        {
            static void Prefix(ref float ___m_activationRange)
            {
                ___m_activationRange = configEntry_TeleportActivatingDistance.Value;
                Debug.Log("New portal activation Distance:  " + ___m_activationRange);
            }
        }
    }
}