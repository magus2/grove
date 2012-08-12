﻿namespace Grove.Cards
{
  using System.Collections.Generic;
  using Core;
  using Core.Ai;
  using Core.Details.Cards.Effects;
  using Core.Details.Cards.Triggers;
  using Core.Details.Mana;
  using Core.Dsl;
  using Core.Zones;

  public class SteamVents : CardsSource
  {
    public override IEnumerable<ICardFactory> GetCards()
    {
      yield return C.Card
        .Named("Steam Vents")
        .Type("Land - Island Mountain")
        .Text(
          "{T}: Add {U} or {R} to your mana pool.{EOL}As Steam Vents enters the battlefield, you may pay 2 life. If you don't, Steam Vents enters the battlefield tapped.")
        .Timing(Timings.Lands())
        .Abilities(
          C.StaticAbility(
            C.Trigger<OnZoneChange>((t, _) => t.To = Zone.Battlefield),
            C.Effect<PayLifeOrTap>(e => e.Life = 2)),
          C.ManaAbility(new ManaUnit(ManaColors.Blue | ManaColors.Red), "{T}: Add {U} or {R} to your mana pool."));
    }
  }
}