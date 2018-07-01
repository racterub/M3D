﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.Printer_Profiles.ProEEPROMConstants
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

using M3D.Spooling.Core;

namespace M3D.Spooling.Printer_Profiles
{
  internal class ProEEPROMConstants : EEPROMProfile
  {
    public ProEEPROMConstants()
      : base("Pro", 78, (ushort) 382, (ushort) 319, 2)
    {
      this.AddEepromAddressInfo(new EepromAddressInfo("FirmwareVersion", (ushort) 0, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FirmwareCRC", (ushort) 2, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("LastRecordedZValue", (ushort) 4, 4, typeof (int)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashX", (ushort) 6, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashY", (ushort) 8, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BedCompensationBackRight", (ushort) 10, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BedCompensationBackLeft", (ushort) 12, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BedCompensationFrontLeft", (ushort) 14, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BedCompensationFrontRight", (ushort) 16, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FilamentColorID", (ushort) 18, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FilamentTypeID", (ushort) 20, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FilamentTemperature", (ushort) 21, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FilamentAmount", (ushort) 22, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashExpansionXPlus", (ushort) 24, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashExpansionYLPlus", (ushort) 26, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashExpansionYRPlus", (ushort) 28, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashExpansionYRMinus", (ushort) 30, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashExpansionZ", (ushort) 32, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashExpansionE", (ushort) 34, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("ZCalibrationBLO", (ushort) 36, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("ZCalibrationBRO", (ushort) 38, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("ZCalibrationFRO", (ushort) 40, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("ZCalibrationFLO", (ushort) 42, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("ZCalibrationZO", (ushort) 44, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("ReservedForSpooler", (ushort) 46, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BacklashSpeed", (ushort) 48, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("BedCompensationVersion", (ushort) 50, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("SpeedLimitX", (ushort) 51, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("SpeedLimitY", (ushort) 53, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("SpeedLimitZ", (ushort) 55, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("SpeedLimitEp", (ushort) 57, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("SpeedLimitEn", (ushort) 59, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FilamentSize", (ushort) 65, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FilamentUID", (ushort) 66, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("EnabledFeatures", (ushort) 68, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("CalibrationOffset", (ushort) 70, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("PowerFailureProgressIndicator", (ushort) 72, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("PowerFailurePrintingState", (ushort) 74, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("NozzleSizeExtrusionWidth", (ushort) 75, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("PowerFailureLastSavedX", (ushort) 76, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("PowerFailureLastSavedY", (ushort) 78, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("SerialNumber", (ushort) 320, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FANTYPE", (ushort) 341, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FANOFFSET", (ushort) 342, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("FANSCALE", (ushort) 343, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("HeaterCalibrationMode", (ushort) 345, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("HardwareStatus", (ushort) 348, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("HeaterTempMeasure_B", (ushort) 349, 4, typeof (float)));
      this.AddEepromAddressInfo(new EepromAddressInfo("HoursCounterSpooler", (ushort) 352, 4, typeof (uint)));
      this.AddEepromAddressInfo(new EepromAddressInfo("SavedZState", (ushort) 371, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("ExtruderCurrent", (ushort) 372, 2, typeof (ushort)));
      this.AddEepromAddressInfo(new EepromAddressInfo("HeaterResistance_M", (ushort) 373, 4, typeof (float)));
    }
  }
}