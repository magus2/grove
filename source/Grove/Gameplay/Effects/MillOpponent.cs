﻿namespace Grove.Core.Effects
{
  public class MillOpponent : Effect
  {
    private readonly int _count;

    private MillOpponent() {}

    public MillOpponent(int count)
    {
      _count = count;
    }

    protected override void ResolveEffect()
    {
      Controller.Opponent.Mill(_count);
    }
  }
}