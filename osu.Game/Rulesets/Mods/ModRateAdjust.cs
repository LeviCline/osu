// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Audio;

namespace osu.Game.Rulesets.Mods
{
    /// <summary>
    /// Mod lets user change the speed of the track, extended into halftime and doubletime
    /// </summary>
    public abstract class ModRateAdjust : Mod, IApplicableToAudio
    {
        public abstract BindableNumber<double> SpeedChange { get; }

        public virtual void ApplyToTrack(ITrack track)
        {
            track.AddAdjustment(AdjustableProperty.Tempo, SpeedChange);
        }

        public virtual void ApplyToSample(DrawableSample sample)
        {
            sample.AddAdjustment(AdjustableProperty.Frequency, SpeedChange);
        }

        public override string SettingDescription => SpeedChange.IsDefault ? string.Empty : $"{SpeedChange.Value:N2}x";
    }
}
