﻿namespace Grove.Cards
{
  using System;
  using System.Collections.Generic;
  using Gameplay.Misc;

  public class WindingWurm : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Winding Wurm")
        .ManaCost("{4}{G}")
        .Type("Creature Wurm")
        .Text(
          "{Echo} {4}{G} (At the beginning of your upkeep, if this came under your control since the beginning of your last upkeep, sacrifice it unless you pay its echo cost.)")
        .FlavorText("Entire trees were stripped of their bark and branches by the wurm's writhing path.")
        .Power(6)
        .Toughness(6)
        .Echo("{4}{G}");
    }
  }
}