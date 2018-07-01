﻿// Decompiled with JetBrains decompiler
// Type: M3D.Graphics.Ext3D.ModelRendering.GraphicsModelData
// Assembly: M3DGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1F16290A-C81C-448C-AD40-1D1E8ABC54ED
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSoftware.exe

using M3D.Model;
using M3D.Model.Utils;
using System;
using System.Collections.Generic;

namespace M3D.Graphics.Ext3D.ModelRendering
{
  public class GraphicsModelData
  {
    public VertexTNV[] dataTNV;
    public TriangleFace[] faces;
    private VertexNormalKey vertex;

    public GraphicsModelData(List<VertexTNV> vertex_list)
    {
      this.faces = new TriangleFace[vertex_list.Count / 3];
      this.dataTNV = new VertexTNV[vertex_list.Count];
      int num = 0;
      while (num < vertex_list.Count)
      {
        for (int index = num; index < num + 3; ++index)
          this.dataTNV[index] = vertex_list[index];
        this.faces[num / 3] = new TriangleFace((uint) num, (uint) (num + 1), (uint) (num + 2));
        num += 3;
      }
    }

    public GraphicsModelData(ModelData modelData, bool smoothing)
    {
      smoothing = true;
      int faceCount = modelData.getFaceCount();
      this.faces = new TriangleFace[faceCount];
      this.dataTNV = new VertexTNV[faceCount * 3];
      for (int index = 0; index < faceCount; ++index)
      {
        ModelData.Face face = modelData.getFace(index);
        Vector3 position1 = modelData[face.index1];
        Vector3 position2 = modelData[face.index2];
        Vector3 position3 = modelData[face.index3];
        if (smoothing)
        {
          this.dataTNV[3 * index] = new VertexTNV(this.cheatSmoothing(modelData.getVertex(face.index1), face.Normal), position1);
          this.dataTNV[3 * index + 1] = new VertexTNV(this.cheatSmoothing(modelData.getVertex(face.index2), face.Normal), position2);
          this.dataTNV[3 * index + 2] = new VertexTNV(this.cheatSmoothing(modelData.getVertex(face.index3), face.Normal), position3);
        }
        else
        {
          this.dataTNV[3 * index] = new VertexTNV(face.Normal, position1);
          this.dataTNV[3 * index + 1] = new VertexTNV(face.Normal, position2);
          this.dataTNV[3 * index + 2] = new VertexTNV(face.Normal, position3);
        }
        this.faces[index] = new TriangleFace((uint) (3 * index), (uint) (3 * index + 1), (uint) (3 * index + 2));
      }
    }

    private Vector3 cheatSmoothing(ModelData.Vertex vertex, Vector3 faceNormal)
    {
      Vector3 vector3 = new Vector3(0.0f, 0.0f, 0.0f);
      uint num1 = 0;
      for (int index = 0; index < vertex.Faces.Count; ++index)
      {
        Vector3 normal = vertex.Faces[index].Normal;
        float num2 = Math.Abs(faceNormal.Dot(normal) / (faceNormal.Length() * normal.Length()));
        if ((double) num2 >= 0.698131680488586)
        {
          vector3 += normal * num2;
          ++num1;
        }
      }
      return (vector3 * (1f / (float) num1)).Normalize();
    }
  }
}