﻿namespace Grove.Gameplay.Effects
{
  public class PutTargetOnTopOfLibrary : Effect
  {
    protected override void ResolveEffect()
    {
      Target.Card().PutOnTopOfLibrary();
    }
  }
}