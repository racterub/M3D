﻿// Decompiled with JetBrains decompiler
// Type: M3D.Graphics.Ext3D.ModelRendering.OpenGLRendererVBOs
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace M3D.Graphics.Ext3D.ModelRendering
{
  public class OpenGLRendererVBOs : OpenGLRender
  {
    public uint VboId;
    public uint IboId;
    public int Numelements;
    public int Numfaces;

    public OpenGLRendererVBOs(GraphicsModelData graphicsModelData)
      : base(graphicsModelData)
    {
    }

    public override OpenGLRendererObject.OpenGLRenderMode RenderMode
    {
      get
      {
        return OpenGLRendererObject.OpenGLRenderMode.VBOs;
      }
    }

    public override void Create()
    {
      int length1 = this.graphicsModelData.dataTNV.Length;
      int length2 = this.graphicsModelData.faces.Length;
      GL.GenBuffers(1, out this.VboId);
      switch (GL.GetError())
      {
        case ErrorCode.NoError:
          GL.BindBuffer(BufferTarget.ArrayBuffer, this.VboId);
          GL.BufferData<VertexTNV>(BufferTarget.ArrayBuffer, (IntPtr) (length1 * (Vector2.SizeInBytes + Vector3.SizeInBytes + Vector3.SizeInBytes)), this.graphicsModelData.dataTNV, BufferUsageHint.StaticDraw);
          int @params;
          GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out @params);
          ErrorCode error = GL.GetError();
          switch (error)
          {
            case ErrorCode.NoError:
              if (length1 * (Vector2.SizeInBytes + Vector3.SizeInBytes + Vector3.SizeInBytes) != @params)
                throw new ApplicationException("Error while uploading VERTICES data.");
              if (length2 > 0)
              {
                GL.GenBuffers(1, out this.IboId);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.IboId);
                GL.BufferData<TriangleFace>(BufferTarget.ElementArrayBuffer, (IntPtr) (length2 * 3 * 4), this.graphicsModelData.faces, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
              }
              this.Numelements = length1;
              this.Numfaces = length2;
              return;
            case ErrorCode.OutOfMemory:
              throw new OutOfMemoryException("Out of GPU memory");
            default:
              throw new ApplicationException("Error while creating VERTICES Buffer Object.\n\nERROR: " + Enum.GetName(typeof (ErrorCode), (object) error));
          }
        case ErrorCode.OutOfMemory:
          throw new OutOfMemoryException("Out of GPU memory");
        default:
          throw new ApplicationException("Error while creating VERTICES Buffer Object.\n\nERROR: " + Enum.GetName(typeof (ErrorCode), (object) GL.GetError()));
      }
    }

    public override void Distroy()
    {
      if (!this.isInitalized())
        return;
      GL.DeleteBuffer(this.IboId);
      GL.DeleteBuffer(this.VboId);
      this.IboId = 0U;
      this.VboId = 0U;
      this.Numelements = 0;
      this.Numfaces = 0;
    }

    public override unsafe void DrawCallback()
    {
      GL.BindBuffer(BufferTarget.ArrayBuffer, this.VboId);
      GL.InterleavedArrays(InterleavedArrayFormat.T2fN3fV3f, 0, (IntPtr) ((void*) null));
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, 32, 20);
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, 32, 8);
      if (this.Numfaces > 0)
      {
        GL.EnableClientState(ArrayCap.IndexArray);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.IboId);
        GL.DrawElements(PrimitiveType.Triangles, 3 * this.Numfaces, DrawElementsType.UnsignedInt, 0);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        GL.DisableClientState(ArrayCap.IndexArray);
      }
      else
        GL.DrawArrays(PrimitiveType.Triangles, 0, this.Numelements);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public override bool isInitalized()
    {
      return this.VboId > 0U;
    }
  }
}