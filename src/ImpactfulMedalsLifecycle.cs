using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Artitas;
using Artitas.Utils;
using Common.Content;
using Common.Content.Descriptors;
using Common.Modding;
using HarmonyLib;
using log4net;

namespace TinyBit.ImpactfulMedals
{
    /// <summary>
    /// Entry point for the Impactful Medals mod.
    /// Harmony patches override medal Modifiers properties.
    /// GetRequiredAssets declares the tooltip template overrides so the ContentManager loads them.
    /// </summary>
    public class ImpactfulMedalsLifecycle : IModLifecycle
    {
        private static readonly ILog Log = ArtitasLogger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string[] MedalTooltips = {
            "tooltips/medals/medal_distinguishedservice",
            "tooltips/medals/medal_goldenstar",
            "tooltips/medals/medal_crimsonheart",
            "tooltips/medals/medal_cruxsolaris",
            "tooltips/medals/medal_gallantry_citation",
            "tooltips/medals/medal_citation_bravery",
            "tooltips/medals/medal_citation_courage",
            "tooltips/medals/medal_citation_valor",
        };

        public void Create(Mod mod, Harmony patcher)
        {
            Log.Info("[ImpactfulMedals] Mod loaded — medal bonuses have been patched.");
        }

        public void Destroy()
        {
            Log.Info("[ImpactfulMedals] Mod unloaded.");
        }

        public void OnWorldCreate(IModLifecycle.Section section, WeakReference<World> worldRef) { }

        public IEnumerable<Descriptor> GetRequiredAssets(IModLifecycle.Section section)
        {
            if (section == IModLifecycle.Section.Strategy) {
                return MedalTooltips.Select(path =>
                    AssetDescriptor.Construct<Template>(path));
            }
            return Enumerable.Empty<Descriptor>();
        }

        public void OnWorldDispose(IModLifecycle.Section section, WeakReference<World> worldRef) { }
    }
}
