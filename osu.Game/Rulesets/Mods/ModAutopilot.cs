// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class ModAutopilot : Mod, IApplicableFailOverride, IUpdatableByPlayfield, IApplicableToDrawableRuleset<HitObject>
    {
        public override string Name => "Autopilot";
        public override string Acronym => "AP";
        public override IconUsage? Icon => OsuIcon.ModAutopilot;
        public override ModType Type => ModType.Automation;
        public override string Description => @"Automatic cursor movement - just follow the rhythm.";
        public override double ScoreMultiplier => 1;
        public override Type[] IncompatibleMods => new[] { typeof(ModRelax), typeof(ModSuddenDeath), typeof(ModNoFail), typeof(ModAutoplay) };

        public bool PerformFail() => false;

        public bool RestartOnFail => false;

        public void Update(Playfield playfield)
        {
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<HitObject> drawableRuleset)
        {
        }
    }
}
