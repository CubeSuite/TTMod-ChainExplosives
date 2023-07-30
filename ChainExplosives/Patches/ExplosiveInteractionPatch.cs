using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace ChainExplosives.Patches
{
    public class ExplosiveInteractionPatch 
    {
        [HarmonyPatch(typeof(ExplosiveInteration), "Interact")]
        [HarmonyPostfix]
        static void detonateAll() {
            Debug.Log("DetonateAll() called");
            //foreach(ExplosiveInstance explosive in ChainExplosivesPlugin.explosives) {
            //    Debug.Log($"Detonating explosive: {explosive.commonInfo.instanceId}");
            //    explosive.Detonate();
            //}
            //
            // ChainExplosivesPlugin.explosives.Clear();

            foreach(ExplosiveVisuals visuals in ChainExplosivesPlugin.explosiveVisuals) {
                Debug.Log($"Detonating visuals: {visuals.myInstRef.Get().commonInfo.instanceId}"); ;
                visuals.myInstRef.Get().Detonate();
            }

            ChainExplosivesPlugin.explosiveVisuals.Clear();
        }
    }

    public class ExplosiveInstancePatch 
    {
        [HarmonyPatch(typeof(ExplosiveInstance), "Detonate")]
        [HarmonyPostfix]
        static void debugDetonate(ExplosiveInstance __instance) {
            Debug.Log($"Detonated Explosive: {__instance.commonInfo.instanceId}");
        }
    }
}
