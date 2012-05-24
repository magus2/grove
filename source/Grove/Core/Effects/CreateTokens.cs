﻿namespace Grove.Core.Effects
{
  using System.Collections.Generic;

  public class CreateTokens : Effect
  {
    private readonly List<ICardFactory> _tokenFactories = new List<ICardFactory>();

    public int Count = 1;

    public override void Resolve()
    {
      for (int i = 0; i < Count; i++)
      {
        foreach (var tokenFactory in _tokenFactories)
        {
          var token = tokenFactory.CreateCard(Controller);
          Controller.PutCardIntoPlay(token);
        }
      }
    }

    public void Tokens(params ICardFactory[] tokenFactories)
    {
      _tokenFactories.AddRange(tokenFactories);
    }
  }
}