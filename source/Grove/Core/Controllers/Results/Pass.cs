﻿namespace Grove.Core.Controllers.Results
{
  public class Pass : Playable
  {
    public override bool WasPriorityPassed
    {
      get { return true; }
    }        
  }
}