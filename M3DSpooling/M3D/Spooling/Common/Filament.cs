﻿namespace M3D.Spooling.Common
{
  public class Filament
  {
    private FilamentSpool.TypeEnum type;
    private string codeStr;
    private FilamentConstants.ColorsEnum color;
    private FilamentConstants.Branding brand;

    public Filament(FilamentSpool.TypeEnum type, FilamentConstants.ColorsEnum color, string codeStr, FilamentConstants.Branding brand)
    {
      this.type = type;
      this.color = color;
      this.codeStr = codeStr;
      this.brand = brand;
    }

    public Filament(Filament other)
    {
      type = other.type;
      codeStr = other.codeStr;
      color = other.color;
    }

    public FilamentSpool ToSpool()
    {
      return new FilamentSpool() { filament_color_code = (uint)color, filament_type = type, filament_temperature = FilamentConstants.Temperature.Default(type), filament_location = FilamentSpool.Location.External };
    }

    public FilamentSpool.TypeEnum Type
    {
      get
      {
        return type;
      }
    }

    public string TypeStr
    {
      get
      {
        return FilamentConstants.TypesToString(type);
      }
    }

    public string CodeStr
    {
      get
      {
        return codeStr;
      }
    }

    public FilamentConstants.ColorsEnum Color
    {
      get
      {
        return color;
      }
    }

    public string ColorStr
    {
      get
      {
        return FilamentConstants.ColorsToString(color);
      }
    }

    public FilamentConstants.Branding Brand
    {
      get
      {
        return brand;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      return Equals(obj as Filament);
    }

    public bool Equals(Filament b)
    {
      if (b == null || type != b.type || color != b.color)
      {
        return false;
      }

      return brand == b.brand;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
