﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Mania.Objects;
using osu.Game.Rulesets.Mania.UI;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Mania.Mods
{
    /// <summary>
    /// Mod makes the targets visible on the top part of the screen, fading out before the keys.
    /// </summary>
    public class ManiaModHidden : ModHidden, IApplicableToDrawableRuleset<ManiaHitObject>
    {
        public override string Description => @"Keys fade out before you hit them!";
        public override double ScoreMultiplier => 1;
        public override Type[] IncompatibleMods => new[] { typeof(ModFlashlight<ManiaHitObject>) };

        /// <summary>
        /// The direction in which the cover should expand.
        /// </summary>
        protected virtual CoverExpandDirection ExpandDirection => CoverExpandDirection.AgainstScroll;

        public virtual void ApplyToDrawableRuleset(DrawableRuleset<ManiaHitObject> drawableRuleset)
        {
            ManiaPlayfield maniaPlayfield = (ManiaPlayfield)drawableRuleset.Playfield;

            foreach (Column column in maniaPlayfield.Stages.SelectMany(stage => stage.Columns))
            {
                HitObjectContainer hoc = column.HitObjectArea.HitObjectContainer;
                Container hocParent = (Container)hoc.Parent;

                hocParent.Remove(hoc);
                hocParent.Add(new PlayfieldCoveringWrapper(hoc).With(c =>
                {
                    c.RelativeSizeAxes = Axes.Both;
                    c.Direction = ExpandDirection;
                    c.Coverage = 0.5f;
                }));
            }
        }
    }
}
