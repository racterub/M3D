﻿using M3D.Graphics;
using M3D.Graphics.Widgets2D;
using M3D.GUI.Controller;
using M3D.GUI.Controller.Settings;
using M3D.Spooling.Client;

namespace M3D.GUI.ManageFilament.Child_Frames
{
  internal class FilamentHasRetractedFilament : Manage3DInkChildWindow
  {
    private SettingsManager settingsManager;

    public FilamentHasRetractedFilament(int ID, GUIHost host, Manage3DInkMainWindow mainWindow, SettingsManager settingsManager)
      : base(ID, host, mainWindow)
    {
      this.settingsManager = settingsManager;
    }

    public override void MyButtonCallback(ButtonWidget button)
    {
      PrinterObject selectedPrinter = MainWindow.GetSelectedPrinter();
      if (selectedPrinter == null)
      {
        return;
      }

      switch (button.ID)
      {
        case 5:
          CurrentDetails.pageAfterWait = Manage3DInkMainWindow.PageID.Page0_StartupPage;
          CurrentDetails.mode = Manage3DInkMainWindow.Mode.SetDetails;
          FilamentWaitingPage.CurrentWaitingText = "Please wait. The printer is busy perfoming the requested actions.";
          if (settingsManager != null)
          {
            settingsManager.DisassociateFilamentFromPrinter(selectedPrinter.Info.serial_number);
            settingsManager.SaveSettings();
          }
          var none = (int) selectedPrinter.SetFilamentToNone(new AsyncCallback(MainWindow.GotoPageAfterOperation), new Manage3DInkMainWindow.PageAfterLockDetails(selectedPrinter, Manage3DInkMainWindow.PageID.Page8_WaitingPage, CurrentDetails));
          break;
        case 6:
          MainWindow.ActivateFrame(Manage3DInkMainWindow.PageID.Page2_RetractingFilament, CurrentDetails);
          break;
        case 9:
          MainWindow.ResetToStartup();
          break;
      }
    }

    public override void Init()
    {
      CreateManageFilamentFrame("Retracting Current 3D Ink", "Has the current 3D Ink been released?", true, true, false, false, false, false);
    }

    public override void OnActivate(Mangage3DInkStageDetails details)
    {
      base.OnActivate(details);
    }
  }
}
