﻿using System;
using System.Xml.Serialization;
using M3D.Slicer.General;

namespace M3D.SlicerConnectionCura.SlicerSettingsItems
{
  [Serializable]
  public class SettingsItemBoolType : SlicerSettingsItem
  {
    [XmlAttribute]
    public bool value;

    public SettingsItemBoolType()
    {
      value = false;
    }

    public SettingsItemBoolType(bool _value)
    {
      value = _value;
    }

    protected override bool SetFromSlicerValue(string val)
    {
      value = !(val == "0");
      return true;
    }

    public override SettingItemType GetItemType()
    {
      return SettingItemType.BoolType;
    }

    public override string TranslateToSlicerValue()
    {
      return !value ? "0" : "1";
    }

    public override string TranslateToUserValue()
    {
      return !value ? "false" : "true";
    }

    public override void ParseUserValue(string value)
    {
      if (value.ToLowerInvariant() == "true")
      {
        this.value = true;
      }
      else
      {
        this.value = false;
      }
    }

    public override bool HasWarning
    {
      get
      {
        return false;
      }
    }

    public override bool HasError
    {
      get
      {
        return false;
      }
    }

    public override string GetErrorMsg()
    {
      return "";
    }

    public override SlicerSettingsItem Clone()
    {
      return new SettingsItemBoolType(value);
    }
  }
}
