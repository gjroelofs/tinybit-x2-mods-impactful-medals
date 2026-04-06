using System;
using System.Collections.Generic;
using Common.Components;
using Common.RPG;
using HarmonyLib;
using Xenonauts.Common.Components.Combatant;
using Xenonauts.Common.Stats;

namespace TinyBit.ImpactfulMedals.Patches
{
    /// <summary>
    /// Harmony patches that replace each medal's Modifiers property with unique, thematically
    /// appropriate stat bonuses instead of the generic +1 to all attributes.
    ///
    /// Medal bonuses summary:
    ///   Distinguished Service:  +5 Bravery, +5 TUs, +3 Reflexes
    ///   Golden Star:            +8 Accuracy, +5 Reflexes
    ///   Crimson Heart:          +10 HP, +5 Bravery, +3 Strength
    ///   Crux Solaris:           +5 Accuracy, +5 Reflexes, +5 TUs
    ///   Gallantry Citation:     +8 Bravery, +5 HP, +5 Strength
    ///   Award for Bravery:      +5 Reflexes, +5 Accuracy, +3 TUs
    ///   Award for Courage:      +8 Bravery, +5 TUs, +3 Strength
    ///   Award for Valor:        +5 Strength, +5 HP, +5 Accuracy
    /// </summary>
    public static class MedalModifierPatches
    {
        // --- Helper to build modifier lists ---

        /// <summary>
        /// Creates RangeComponentModifiers for a stat, handling HP and TU special cases:
        /// - UnmodifiedHitPoints: creates both HitPoints (value+max) and UnmodifiedHitPoints (value+max)
        /// - TimeUnits: creates modifier with matching deltaMaximum
        /// - All others: deltaValue only (0-100 range stats don't need max adjustment)
        /// </summary>
        private static void AddModifier(List<IModifier> list, Type statType, float value)
        {
            if (statType == typeof(UnmodifiedHitPoints)) {
                list.Add(new RangeComponentModifier(typeof(HitPoints), value, 0, value));
                list.Add(new RangeComponentModifier(statType, value, 0, value));
            } else if (statType == typeof(TimeUnits)) {
                list.Add(new RangeComponentModifier(statType, value, 0, value));
            } else {
                list.Add(new RangeComponentModifier(statType, value, 0, 0));
            }
        }

        // --- Distinguished Service Medal: +5 Bravery, +5 TUs, +3 Reflexes ---
        // Veterans are unshakeable and move with practiced efficiency.

        [HarmonyPatch(typeof(DistinguishedServiceMedal), "get_Modifiers")]
        public static class DistinguishedServicePatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<IModifier> __result)
            {
                var mods = new List<IModifier>();
                AddModifier(mods, typeof(Bravery), 5);
                AddModifier(mods, typeof(TimeUnits), 5);
                AddModifier(mods, typeof(Reflexes), 3);
                __result = mods;
            }
        }

        // --- Golden Star: +8 Accuracy, +5 Reflexes ---
        // Proven killer who doesn't miss.

        [HarmonyPatch(typeof(GoldenStar), "get_Modifiers")]
        public static class GoldenStarPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<IModifier> __result)
            {
                var mods = new List<IModifier>();
                AddModifier(mods, typeof(Accuracy), 8);
                AddModifier(mods, typeof(Reflexes), 5);
                __result = mods;
            }
        }

        // --- Crimson Heart: +10 HP, +5 Bravery, +3 Strength ---
        // What doesn't kill you makes you stronger.

        [HarmonyPatch(typeof(CrimsonHeart), "get_Modifiers")]
        public static class CrimsonHeartPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<IModifier> __result)
            {
                var mods = new List<IModifier>();
                AddModifier(mods, typeof(UnmodifiedHitPoints), 10);
                AddModifier(mods, typeof(Bravery), 5);
                AddModifier(mods, typeof(Strength), 3);
                __result = mods;
            }
        }

        // --- Crux Solaris: +5 Accuracy, +5 Reflexes, +5 TUs ---
        // One-soldier army — faster, deadlier, more actions.

        [HarmonyPatch(typeof(CruxSolaris), "get_Modifiers")]
        public static class CruxSolarisPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<IModifier> __result)
            {
                var mods = new List<IModifier>();
                AddModifier(mods, typeof(Accuracy), 5);
                AddModifier(mods, typeof(Reflexes), 5);
                AddModifier(mods, typeof(TimeUnits), 5);
                __result = mods;
            }
        }

        // --- Gallantry Citation: +8 Bravery, +5 HP, +5 Strength ---
        // Carried the team through hell — iron will and iron body.

        [HarmonyPatch(typeof(GallantryCitation), "get_Modifiers")]
        public static class GallantryCitationPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<IModifier> __result)
            {
                var mods = new List<IModifier>();
                AddModifier(mods, typeof(Bravery), 8);
                AddModifier(mods, typeof(UnmodifiedHitPoints), 5);
                AddModifier(mods, typeof(Strength), 5);
                __result = mods;
            }
        }

        // --- Award for Bravery: +5 Reflexes, +5 Accuracy, +3 TUs ---
        // Close-quarters crash site fighting hones reactions.

        [HarmonyPatch(typeof(AwardForBravery), "get_Modifiers")]
        public static class AwardForBraveryPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<IModifier> __result)
            {
                var mods = new List<IModifier>();
                AddModifier(mods, typeof(Reflexes), 5);
                AddModifier(mods, typeof(Accuracy), 5);
                AddModifier(mods, typeof(TimeUnits), 3);
                __result = mods;
            }
        }

        // --- Award for Courage: +8 Bravery, +5 TUs, +3 Strength ---
        // Facing civilian terror hardens resolve and urgency.

        [HarmonyPatch(typeof(AwardForCourage), "get_Modifiers")]
        public static class AwardForCouragePatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<IModifier> __result)
            {
                var mods = new List<IModifier>();
                AddModifier(mods, typeof(Bravery), 8);
                AddModifier(mods, typeof(TimeUnits), 5);
                AddModifier(mods, typeof(Strength), 3);
                __result = mods;
            }
        }

        // --- Award for Valor: +5 Strength, +5 HP, +5 Accuracy ---
        // Grueling base assault builds raw combat power.

        [HarmonyPatch(typeof(AwardForValor), "get_Modifiers")]
        public static class AwardForValorPatch
        {
            [HarmonyPostfix]
            public static void Postfix(ref IEnumerable<IModifier> __result)
            {
                var mods = new List<IModifier>();
                AddModifier(mods, typeof(Strength), 5);
                AddModifier(mods, typeof(UnmodifiedHitPoints), 5);
                AddModifier(mods, typeof(Accuracy), 5);
                __result = mods;
            }
        }
    }
}
