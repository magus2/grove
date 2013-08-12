﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Artifical.TimingRules;
  using Gameplay;
  using Gameplay.Characteristics;
  using Gameplay.Costs;
  using Gameplay.Effects;
  using Gameplay.ManaHandling;
  using Gameplay.Misc;

  public class ForbiddingWatchtower : CardTemplateSource
  {
    public override IEnumerable<CardTemplate> GetCards()
    {
      yield return Card
        .Named("Forbidding Watchtower")
        .Type("Land")
        .Text(
          "Forbidding Watchtower enters the battlefield tapped.{EOL}{T}: Add {W} to your mana pool.{EOL}{1}{W}: Forbidding Watchtower becomes a 1/5 white Soldier creature until end of turn. It's still a land.")
        .Cast(p => p.Effect = () => new PutIntoPlay(tap: true))
        .ManaAbility(p =>
          {
            p.Text = "{T}: Add {W} to your mana pool.";
            p.ManaAmount(Mana.White);
            p.Priority = ManaSourcePriorities.OnlyIfNecessary;
          })
        .ActivatedAbility(p =>
          {
            p.Text =
              "{1}{W}: Forbidding Watchtower becomes a 1/5 white Soldier creature until end of turn. It's still a land.";

            p.Cost = new PayMana("{1}{W}".Parse(), ManaUsage.Abilities);

            p.Effect = () => new ApplyModifiersToSelf(
              () => new Gameplay.Modifiers.ChangeToCreature(
                power: 1,
                toughness: 5,
                colors: L(CardColor.White),
                type: "Land Creature Soldier") { UntilEot = true });

            p.TimingRule(new StackIsEmpty());
            p.TimingRule(new OwningCardHas(c => !c.Is().Creature));
            p.TimingRule(new ChangeToCreature(minAvailableMana: 3));
          });
    }
  }
}