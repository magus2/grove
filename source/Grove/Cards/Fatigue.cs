﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical.TargetingRules;
  using Artifical.TimingRules;
  using Gameplay.Effects;
  using Gameplay.Misc;
  using Gameplay.Modifiers;
  using Gameplay.States;
  using Gameplay.Targeting;

  public class Fatigue : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Fatigue")
        .ManaCost("{1}{U}")
        .Type("Sorcery")
        .Text("Target player skips his or her next draw step.")
        .FlavorText("Mind if I just lie down right here for a few weeks.")
        .Cast(p =>
          {
            p.Effect = () => new ApplyModifiersToTargets(() =>
              {
                var modifier = new SkipStep(Step.Draw);

                modifier.AddLifetime(new EndOfStep(Step.Draw, self =>
                  self.Modifier.SourceEffect.Target.Player().IsActive));

                return modifier;
              });

            p.TargetSelector.AddEffect(trg => trg.Is.Player());

            p.TimingRule(new OnSecondMain());
            p.TargetingRule(new EffectOpponent());
          });
    }
  }
}