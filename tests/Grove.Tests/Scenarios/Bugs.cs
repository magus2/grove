﻿namespace Grove.Tests.Scenarios
{
  using System.Linq;
  using Core;
  using Core.Zones;
  using Infrastructure;
  using Xunit;

  public class Bugs
  {
    public class Ai : AiScenario
    {
      [Fact]
      public void BugDoAttackWithTrollsAndWildwood()
      {
        Hand(P1, "Birds of Paradise");
        Hand(P2, "Plains");

        Battlefield(P1, "Forest", "Sunpetal Grove", C("Troll Ascetic").IsEquipedWith("Sword of Feast and Famine"),
          "Stirring Wildwood", "Stirring Wildwood", "Troll Ascetic", "Forest", "Birds of Paradise");
        Battlefield(P2, "Plains", "Plains", "Plains", "Plains", "Plains", "Plains", "Plains", "Wall of Reverence");

        P2.Life = 25;

        RunGame(1);

        Equal(19, P2.Life);
      }

      [Fact]
      public void BugDoNotTapLandsUselessly()
      {
        var mountain = C("Mountain");
        Battlefield(P1, C("Forest"), C("Forest"), mountain);
        Hand(P1, C("Llanowar Elves"));

        RunGame(maxTurnCount: 2);

        Equal(1, P1.Battlefield.Count(x => x.Name == "Forest" && x.IsTapped));
        False(mountain.IsTapped);
      }

      [Fact]
      public void BeastSacWithOpponentAbility()
      {
        Battlefield(P1, "Ravenous Baloth");
        Battlefield(P2, "Leatherback Baloth");

        RunGame(maxTurnCount: 2);
        Equal(1, P2.Battlefield.Count());
      }

      [Fact]
      public void BugGameHangsWhenEndOfGameIsReached1()
      {
        Battlefield(P1, C("Grizzly Bears"));
        P2.Life = 2;

        RunGame(maxTurnCount: 2);

        True(Game.IsFinished);
      }

      [Fact]
      public void BugGameHangsWhenEndOfGameIsReached2()
      {
        Battlefield(P2, C("Liliana's Specter"), C("Swamp"), C("Swamp"), C("Swamp"), C("Nantuko Shade"));
        Battlefield(P1, C("Mountain"), C("Forest"), C("Forest"));

        P1.Life = 1;

        RunGame(maxTurnCount: 2);

        True(Game.IsFinished);
      }

      [Fact]
      public void BugIncorrectStateCopy()
      {
        Battlefield(P1, C("Copperline Gorge"), C("Forest"),
          C("Leatherback Baloth").IsEquipedWith(C("Sword of Feast and Famine")), C("Ravenous Baloth"));

        RunGame(maxTurnCount: 1);
      }

      [Fact]
      public void BugLeakedCopyBasiliskCollar()
      {
        Hand(P2, "Martial Coup", "Deathless Angel", "Plains", "Deathless Angel");
        Hand(P1, "Stirring Wildwood", "Sunpetal Grove", "Plains", "Basilisk Collar");
        Battlefield(P2, "Plains", "Plains", "Plains", "Plains", "Hero of Bladehold");
        Battlefield(P1, "Razorverge Thicket", C("Student of Warfare").IsEnchantedWith("Rancor"), "Razorverge Thicket",
          "Plains", "Sword of Feast and Famine");
        
        RunGame(2);
      }
    }

    public class PredifinedAi : PredefinedAiScenario
    {
      [Fact]
      public void BugRegenerateCombatDamage()
      {
        var thrun = C("Thrun, the Last Troll");
        var baloth = C("Ravenous Baloth");

        Battlefield(P1, baloth);
        Battlefield(P2, "Forest", "Forest", thrun);

        Exec(
          At(Step.DeclareAttackers)
            .DeclareAttackers(baloth),
          At(Step.SecondMain)
            .Verify(() =>
              {
                Equal(Zone.Graveyard, C(baloth).Zone);
                Equal(Zone.Battlefield, C(thrun).Zone);
              })
          );
      }

      [Fact]
      public void BugSearchWithoutResults()
      {
        var dragon = C("Shivan Dragon");
        var forest = C("Forest");

        Battlefield(P1, C("Forest").Tap(), forest, C("Mountain").Tap(), C("Mountain").Tap(),
          C("Mountain").Tap(), C("Llanowar Elves").Tap(), C("Llanowar Elves").Tap());
        Hand(P1, dragon);

        Battlefield(P2, C("Forest").Tap(), C("Mountain").Tap(), C("Grizzly Bears"), C("Forest"));
        Hand(P2, C("Elvish Warrior"), C("Grizzly Bears"), C("Order of the Sacred Bell"));

        Exec(
          At(Step.FirstMain)
            .Activate(forest) /* 1 mana must be left in pool after casting to trigger the bug*/
            .Cast(dragon)
            .Verify(() => Equal(Zone.Battlefield, C(dragon).Zone))
          );
      }

      [Fact]
      public void BugAttackingCausesEndOfGame()
      {
        bool gameEnded = true;

        var knight = C("White Knight");
        var student = C("Student of Warfare");
        var hero = C("Hero of Bladehold");

        Hand(P1, "Deathless Angel", "Baneslayer Angel", "Day of Judgment");
        Hand(P2, "Troll Ascetic");
        Battlefield(P1, "Plains", "Plains", "Plains", "Glorious Anthem", "Plains", knight, "Plains", student, hero);
        Battlefield(P2, "Razorverge Thicket", "Llanowar Elves", "Forest", "Troll Ascetic", "Plains", "Troll Ascetic",
          "Stirring Wildwood", "Plains", "Plains", "Wurmcoil Engine", "Birds of Paradise");

        Exec(
          At(Step.FirstMain)
            .Activate(student)
            .Activate(student),
          At(Step.DeclareAttackers)
            .DeclareAttackers(knight, student, hero),
          At(Step.SecondMain)
            .Verify(() => { gameEnded = false; })
          );

        False(gameEnded);
      }
    }
  }
}