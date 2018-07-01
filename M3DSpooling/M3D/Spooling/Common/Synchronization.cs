﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.Common.Synchronization
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

using System.Xml.Serialization;

namespace M3D.Spooling.Common
{
  public class Synchronization
  {
    [XmlAttribute("LastCompletedRPCID")]
    public uint LastCompletedRPCID;
    [XmlAttribute("Locked")]
    public bool Locked;

    public Synchronization(Synchronization rhs)
    {
      this.LastCompletedRPCID = rhs.LastCompletedRPCID;
      this.Locked = rhs.Locked;
    }

    public Synchronization()
    {
      this.LastCompletedRPCID = 0U;
      this.Locked = false;
    }
  }
}