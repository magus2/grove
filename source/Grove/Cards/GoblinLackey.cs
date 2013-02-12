﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Dsl;
  using Core.Effects;
  using Core.Triggers;
  using Core.Zones;

  public class GoblinLackey : CardsSource
  {
    public override IEnumerable<CardFactory> GetCards()
    {
      yield return Card
        .Named("Goblin Lackey")
        .ManaCost("{R}")
        .Type("Creature Goblin")
        .Text(
          "Whenever Goblin Lackey successfully deals damage to a player, you may choose a Goblin card in your hand and put that Goblin into play.")
        .FlavorText("All bark, someone else's bite.")
        .Power(1)
        .Toughness(1)
        .TriggeredAbility(p =>
          {
            p.Text =
              "Whenever Goblin Lackey successfully deals damage to a player, you may choose a Goblin card in your hand and put that Goblin into play.";
            p.Trigger(new OnDamageDealt(playerFilter: delegate { return true; }));
            p.Effect = () => new PutSelectedCardToBattlefield(
              text: "Select a goblin in your hand.",
              validator: card => card.Is("goblin"),
              zone: Zone.Hand
              );
          });
    }
  }
}