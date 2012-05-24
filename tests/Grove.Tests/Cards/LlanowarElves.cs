﻿namespace Grove.Tests.Cards
{
  using System.Linq;
  using Grove.Core;
  using Infrastructure;
  using Xunit;

  public class LlanowarElves
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void PlayForestAndElves()
      {
        var forest = C("Forest");
        var elves = C("Llanowar Elves");

        Hand(P1, forest, elves);

        RunGame(maxTurnCount: 2);

        True(P1.Battlefield.Contains(forest));
        True(P1.Battlefield.Contains(elves));
      }
    }

    public class Predefined : PredifinedScenario
    {
      [Fact]
      public void TapLlanowarElvesToAddOneGreenManaToPool()
      {
        var elves = C("Llanowar elves");
        Hand(P1, elves);

        Exec(
          At(Step.FirstMain)
            .Cast(elves),
          At(Step.FirstMain, turn: 3)
            .Activate(elves)
            .Verify(() => {
              True(P1.Battlefield.Contains(elves));
              Equal(Mana.Green, P1.ManaPool);
            }));
      }
    }
  }
}