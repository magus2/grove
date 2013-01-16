﻿namespace Grove.Core.Effects
{
  public class ControllerGainsLife : Effect
  {
    public int Amount { get; set; }

    protected override void ResolveEffect()
    {
      Controller.Life += Amount;
    }
  }
}