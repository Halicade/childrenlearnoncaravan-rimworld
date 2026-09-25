using System.Reflection;
using RimWorld;
using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace ChildrenLearnOnCaravan
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            //Harmony.DEBUG = true;
            var harmony = new Harmony("heatherathebyne.childrenlearnoncaravan");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch]
    public static class Patch_Caravan_NeedsTracker
    {
        public static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(Caravan_NeedsTracker), "TrySatisfyPawnNeeds");
        }

        static void Postfix(Pawn pawn, int delta) 
        {
            if (pawn.DevelopmentalStage.Child())
                pawn.needs?.learning?.Learn(1.2E-05f * pawn.GetStatValue(StatDefOf.LearningRateFactor) * 0.5f * delta);

            return;
        }
    }
    }