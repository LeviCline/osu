﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osuTK;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics.Containers;

namespace osu.Game.Overlays.SearchableList
{
    public class DisplayStyleControl : CompositeDrawable
    {
        public readonly Bindable<PanelDisplayStyle> DisplayStyle = new Bindable<PanelDisplayStyle>();

        public DisplayStyleControl()
        {
            AutoSizeAxes = Axes.Both;

            InternalChild = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Spacing = new Vector2(5f, 0f),
                Direction = FillDirection.Horizontal,
                Children = new[]
                {
                    new DisplayStyleToggleButton(FontAwesome.Solid.ThLarge, PanelDisplayStyle.Grid, DisplayStyle),
                    new DisplayStyleToggleButton(FontAwesome.Solid.ListUl, PanelDisplayStyle.List, DisplayStyle),
                },
            };

            DisplayStyle.Value = PanelDisplayStyle.Grid;
        }

        private class DisplayStyleToggleButton : OsuClickableContainer
        {
            private readonly SpriteIcon icon;
            private readonly PanelDisplayStyle style;
            private readonly Bindable<PanelDisplayStyle> bindable;

            public DisplayStyleToggleButton(IconUsage icon, PanelDisplayStyle style, Bindable<PanelDisplayStyle> bindable)
            {
                this.bindable = bindable;
                this.style = style;
                Size = new Vector2(25f);

                Children = new Drawable[]
                {
                    this.icon = new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Icon = icon,
                        Size = new Vector2(18),
                        Alpha = 0.5f,
                    },
                };

                bindable.ValueChanged += bindable_ValueChanged;
                bindable_ValueChanged(new ValueChangedEvent<PanelDisplayStyle>(bindable.Value, bindable.Value));
                Action = () => bindable.Value = this.style;
            }

            private void bindable_ValueChanged(ValueChangedEvent<PanelDisplayStyle> e)
            {
                icon.FadeTo(e.NewValue == style ? 1.0f : 0.5f, 100);
            }

            protected override void Dispose(bool isDisposing)
            {
                base.Dispose(isDisposing);

                bindable.ValueChanged -= bindable_ValueChanged;
            }
        }
    }

    public enum PanelDisplayStyle
    {
        Grid,
        List,
    }
}
