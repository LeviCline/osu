// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Audio;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Beatmaps.Timing;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.UI;
using osu.Game.Skinning;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModFunBeats<TObject> : Mod, IApplicableToDrawableRuleset<TObject>
        where TObject : HitObject
    {
        public override string Name => "FunBeats";
        public override string Acronym => "FB";
        public override IconUsage? Icon => OsuIcon.Heart;
        public override ModType Type => ModType.Fun;
        public override string Description => "Beats is dope";
        public override bool RequiresConfiguration => false;
        public override double ScoreMultiplier => 1.0;


        public void ApplyToDrawableRuleset(DrawableRuleset<TObject> drawableRuleset)
        {
            drawableRuleset.Overlays.Add(new FunBeatsContainer());
        }

        public class FunBeatsContainer : BeatSyncedContainer
        {
            private PausableSkinnableSound hatSample;
            private PausableSkinnableSound clapSample;
            private PausableSkinnableSound kickSample;
            private PausableSkinnableSound finishSample;

            private int? firstBeat;

            public FunBeatsContainer()
            {
                Divisor = 2;
            }

            [BackgroundDependencyLoader]
            private void load()
            {
                InternalChildren = new Drawable[]
                {
                    hatSample = new PausableSkinnableSound(new SampleInfo("Gameplay/nightcore-hat")),
                    clapSample = new PausableSkinnableSound(new SampleInfo("Gameplay/nightcore-clap")),
                    kickSample = new PausableSkinnableSound(new SampleInfo("Gameplay/nightcore-kick")),
                    finishSample = new PausableSkinnableSound(new SampleInfo("Gameplay/nightcore-finish")),
                };
            }

            private const int bars_per_segment = 4;

            protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, ChannelAmplitudes amplitudes)
            {
                base.OnNewBeat(beatIndex, timingPoint, effectPoint, amplitudes);

                int beatsPerBar = (int)timingPoint.TimeSignature;
                int segmentLength = beatsPerBar * Divisor * bars_per_segment;

                if (!IsBeatSyncedWithTrack)
                {
                    firstBeat = null;
                    return;
                }

                if (!firstBeat.HasValue || beatIndex < firstBeat)
                    // decide on a good starting beat index if once has not yet been decided.
                    firstBeat = beatIndex < 0 ? 0 : (beatIndex / segmentLength + 1) * segmentLength;

                if (beatIndex >= firstBeat)
                    playBeatFor(beatIndex % segmentLength, timingPoint.TimeSignature);
            }

            private void playBeatFor(int beatIndex, TimeSignatures signature)
            {
                if (beatIndex == 0)
                    finishSample?.Play();

                switch (signature)
                {
                    case TimeSignatures.SimpleTriple:
                        switch (beatIndex % 6)
                        {
                            case 0:
                                kickSample?.Play();
                                break;

                            case 3:
                                clapSample?.Play();
                                break;

                            default:
                                hatSample?.Play();
                                break;
                        }

                        break;

                    case TimeSignatures.SimpleQuadruple:
                        switch (beatIndex % 4)
                        {
                            case 0:
                                kickSample?.Play();
                                break;

                            case 2:
                                clapSample?.Play();
                                break;

                            default:
                                hatSample?.Play();
                                break;
                        }

                        break;
                }
            }
        }
    }
}
