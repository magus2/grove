﻿namespace Grove.Effects
{
  public class UntapTargetPermanents : Effect
  {
    protected override void ResolveEffect()
    {
      foreach (var validTarget in ValidEffectTargets)
      {
        validTarget.Card().Untap();
      }
    }
  }
}