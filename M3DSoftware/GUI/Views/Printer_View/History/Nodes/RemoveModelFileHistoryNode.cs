﻿// Decompiled with JetBrains decompiler
// Type: M3D.GUI.Views.Printer_View.History.Nodes.RemoveModelFileHistoryNode
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Graphics.Ext3D;

namespace M3D.GUI.Views.Printer_View.History.Nodes
{
  internal class RemoveModelFileHistoryNode : BaseModelFileHistoryNode
  {
    public RemoveModelFileHistoryNode(uint objectID, string filename, string zipfilename, TransformationNode.Transform transform)
      : base(objectID, filename, zipfilename, transform)
    {
    }

    public override void Undo(PrinterView printerView)
    {
      this.AddModel(printerView);
    }

    public override void Redo(PrinterView printerView)
    {
      this.RemoveModel(printerView);
    }
  }
}