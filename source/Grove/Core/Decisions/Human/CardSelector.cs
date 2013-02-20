﻿namespace Grove.Core.Decisions.Human
{
  using Results;
  using Targeting;
  using Ui;
  using Ui.SelectTarget;
  using Ui.Shell;

  public class CardSelector
  {
    public ViewModel.IFactory TargetDialog { get; set; }
    public IShell Shell { get; set; }
    public Game Game { get; set; }

    public void ExecuteQuery(SelectCards selectCards)
    {
      var chosenCards = new ChosenCards();

      var validatorParameters = new TargetValidatorParameters
        {
          TargetSpec = p => selectCards.Validator(p.Target.Card()),
          ZoneSpec = p => p.Zone == selectCards.Zone && p.ZoneOwner == selectCards.Controller,
          MinCount = selectCards.MinCount,
          MaxCount = selectCards.MaxCount,
          Text = selectCards.Text
        };

      var validator = new TargetValidator(validatorParameters);
      validator.Initialize(Game);

      var dialog = TargetDialog.Create(validator, canCancel: false);
      Shell.ShowModalDialog(dialog, DialogType.Small, InteractionState.SelectTarget);

      foreach (var target in dialog.Selection)
      {
        chosenCards.Add(target.Card());
      }

      selectCards.Result = chosenCards;
    }
  }
}