﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai.TimingRules;
  using Core.Costs;
  using Core.Dsl;
  using Core.Effects;

  public class FireAnts : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Fire Ants")
        .ManaCost("{2}{R}")
        .Type("Creature Insect")
        .Text("{T}: Fire Ants deals 1 damage to each other creature without flying.")
        .FlavorText("Visitors to Shiv fear the dragons, the goblins, or the viashino. Natives fear the ants.")
        .Power(2)
        .Toughness(1)
        .ActivatedAbility(p =>
          {
            p.Text = "{T}: Fire Ants deals 1 damage to each other creature without flying.";
            p.Cost = new Tap();
            p.Effect = () => new DealDamageToCreaturesAndPlayers(
              amountCreature: 1,
              filterCreature: (e, card) => !card.Has().Flying && e.Source.OwningCard != card);

            p.TimingRule(new MassRemoval());
          });
    }
  }
}