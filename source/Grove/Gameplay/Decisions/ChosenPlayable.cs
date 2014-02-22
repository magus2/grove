﻿namespace Grove.Gameplay.Decisions
{
  using System;
  using Infrastructure;

  [Copyable, Serializable]
  public class ChosenPlayable
  {
    public IPlayable Playable { get; set; }
    public bool WasPriorityPassed { get { return Playable.WasPriorityPassed; } }

    public static ChosenPlayable Pass
    {
      get
      {
        return new ChosenPlayable
          {
            Playable = new Pass()
          };
      }
    }
  }
}