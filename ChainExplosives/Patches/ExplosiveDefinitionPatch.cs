using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIMSpace.Generating.Rules.QuickSolutions;
using HarmonyLib;
using UnityEngine;

namespace ChainExplosives.Patches
{
    public class ExplosiveDefinitionPatch 
    {
        [HarmonyPatch(typeof(ExplosiveDefinition), "InitInstance")]
        [HarmonyPostfix]
        static void addToExplosive(ExplosiveDefinition __instance, ref ExplosiveInstance newInstance) {
            ChainExplosivesPlugin.ids.Add(newInstance.commonInfo.resId);
            ChainExplosivesPlugin.explosives.Add(newInstance);
            Debug.Log($"Added id {newInstance.commonInfo.resId} to list");
            Debug.Log($"Added explosive {newInstance.commonInfo.instanceId} to list");
            Debug.Log($"New explosives count: {ChainExplosivesPlugin.explosives.Count}");
        }
    }

    public class ExplosiveBuilderPatch {
        [HarmonyPatch(typeof(ExplosiveBuilder), "Build")]
        [HarmonyPostfix]
        static void addToExplosiveVisuals(ExplosiveBuildInfo buildInfo, bool removeResources, bool forceBuild, ExplosiveVisuals logObj = null) {
            if (logObj != null) {
                Debug.Log($"Added explosive visuals {logObj.myInstRef.Get().commonInfo.instanceId} to list");
                Debug.Log($"New explosive visuals count: {ChainExplosivesPlugin.explosives.Count}");
            }
            else {
                Debug.Log($"Couldn't add null explosive visuals");
            }
        }
    }
}
