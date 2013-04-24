﻿namespace Grove.Core.Decisions.Human
{
  using System.Windows;
  using Grove.Ui.Shell;

  public class TakeMulligan : Decisions.TakeMulligan
  {
    public IShell Shell { get; set; }

    protected override void ExecuteQuery()
    {
      var result = Shell.ShowMessageBox(
        title: "Mulligan",
        message: "Do you want to improve your hand by taking a mulligan?",
        buttons: MessageBoxButton.YesNo);

      Result = result == MessageBoxResult.Yes;
    }
  }
}