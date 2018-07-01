﻿// Decompiled with JetBrains decompiler
// Type: M3D.SlicerConnectionCura.SlicerSettingsItems.SettingsItemIntType
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Graphics.Widgets2D;
using M3D.Slicer.General;
using System;
using System.Xml.Serialization;

namespace M3D.SlicerConnectionCura.SlicerSettingsItems
{
  [Serializable]
  public class SettingsItemIntType : SettingsGenericBoundedNumber<int>, IReportFormat
  {
    public SettingsItemIntType()
      : base(int.MinValue, new Range<int>(), new Range<int>())
    {
    }

    public SettingsItemIntType(int value, Range<int> warning_range, Range<int> error_range)
      : base(value, warning_range, error_range)
    {
    }

    protected override bool SetFromSlicerValue(string val)
    {
      try
      {
        this.value = int.Parse(val);
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    public override SettingItemType GetItemType()
    {
      return SettingItemType.IntType;
    }

    public override string TranslateToSlicerValue()
    {
      return this.value.ToString();
    }

    public override bool HasError
    {
      get
      {
        return base.HasError;
      }
    }

    public override bool HasWarning
    {
      get
      {
        return base.HasWarning;
      }
    }

    public override string GetErrorMsg()
    {
      return base.GetErrorMsg();
    }

    public override SlicerSettingsItem Clone()
    {
      return (SlicerSettingsItem) new SettingsItemIntType(this.value, this.warning_range, this.error_range);
    }

    [XmlIgnore]
    public NumFormat Format
    {
      get
      {
        return NumFormat.Whole;
      }
    }
  }
}