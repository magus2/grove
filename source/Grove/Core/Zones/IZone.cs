﻿namespace Grove.Core.Zones
{
  public interface IZone
  {
    Zone Zone { get; }    
    
    void Remove(Card card);    
    
    void AfterRemove(Card card);
    void AfterAdd(Card card);
  }
}