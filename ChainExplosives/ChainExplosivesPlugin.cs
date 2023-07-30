using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using ChainExplosives.Patches;
using HarmonyLib;
using UnityEngine;

namespace ChainExplosives
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class ChainExplosivesPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.equinox.ChainExplosives";
        private const string PluginName = "ChainExplosives";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);
        public static ManualLogSource Log = new ManualLogSource(PluginName);

        public static List<int> ids = new List<int>();
        public static List<ExplosiveInstance> explosives = new List<ExplosiveInstance>();
        public static List<ExplosiveVisuals> explosiveVisuals = new List<ExplosiveVisuals>();

        private void Awake() {
            // Apply all of our patches
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loading...");
            Harmony.PatchAll();
            Logger.LogInfo($"PluginName: {PluginName}, VersionString: {VersionString} is loaded.");
            Log = Logger;

            Harmony.CreateAndPatchAll(typeof(ExplosiveDefinitionPatch));
            Harmony.CreateAndPatchAll(typeof(ExplosiveInteractionPatch));
            Harmony.CreateAndPatchAll(typeof(ExplosiveInstancePatch));
            Harmony.CreateAndPatchAll(typeof(ExplosiveBuilderPatch));
        }
    }
}
