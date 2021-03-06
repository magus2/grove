﻿namespace Grove.UserInterface.Battlefield
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class Row : IDisposable
  {
    private readonly List<Slot> _slots = new List<Slot>();

    public Row(params Slot[] slots)
    {
      _slots.AddRange(slots);
    }

    public IEnumerable<Slot> Slots { get { return _slots; } }

    public void Add(UserInterface.Permanent.ViewModel viewModel)
    {
      Slot candidate;

      if (viewModel.Card.IsAttached && viewModel.Card.Is().Attachment)
      {
        candidate = _slots.First(slot => slot.ContainsAttachmentTarget(viewModel.Card));
      }
      else
      {
        candidate = _slots
          .Where(slot => slot.CanAdd(viewModel))
          .OrderBy(slot => slot.Count)
          .First();
      }

      candidate.Add(viewModel);
    }

    public bool CanAdd(UserInterface.Permanent.ViewModel viewModel)
    {
      return _slots.Any(slot => slot.CanAdd(viewModel));
    }

    public UserInterface.Permanent.ViewModel GetPermanent(Card card)
    {
      foreach (var slot in Slots)
      {
        var viewModel = slot.GetPermanentViewModel(card);

        if (viewModel != null)
          return viewModel;
      }

      return null;
    }

    public bool Remove(UserInterface.Permanent.ViewModel viewModel)
    {
      foreach (var slot in Slots)
      {
        var removed = slot.Remove(viewModel);
        if (removed)
          return true;
      }

      return false;
    }

    public void Clear()
    {
      foreach (var slot in Slots)
      {
        slot.Clear();
      }
    }

    public bool ContainsAttachmentTarget(Card attachment)
    {
      return _slots.Any(slot => slot.ContainsAttachmentTarget(attachment));
    }

    public void Dispose()
    {
      foreach (var slot in Slots)
      {
        slot.Dispose();
      }
    }
  }
}