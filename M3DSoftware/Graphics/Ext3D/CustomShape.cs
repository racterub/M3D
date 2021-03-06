﻿using M3D.Graphics.Ext3D.ModelRendering;
using M3D.Model.Utils;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace M3D.Graphics.Ext3D
{
  public class CustomShape : Element3D
  {
    public bool CullBackFaces = true;
    private Color4 ambient;
    private Color4 diffuse;
    private Color4 emission;
    private Color4 specular;
    private float shininess;
    private Vector3 ext;
    private Vector3 max;
    private Vector3 min;
    protected int texture_handle;
    protected OpenGLRendererObject texturedGeometry;

    public CustomShape()
      : this(0, null)
    {
    }

    public CustomShape(int ID)
      : this(ID, null)
    {
    }

    public CustomShape(int ID, Element3D parent)
      : base(ID, parent)
    {
      ambient = new Color4(0.05f, 0.05f, 0.05f, 1f);
      diffuse = new Color4(1f, 1f, 1f, 1f);
      specular = new Color4(1f, 1f, 1f, 1f);
      shininess = 100f;
    }

    public override void Render3D()
    {
      GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, ambient);
      GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, diffuse);
      GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, specular);
      GL.Material(MaterialFace.Front, MaterialParameter.Shininess, shininess);
      GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, emission);
      GL.DepthMask(true);
      GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
      if (!CullBackFaces)
      {
        GL.DepthMask(false);
        GL.Disable(EnableCap.CullFace);
      }
      GL.BindTexture(TextureTarget.Texture2D, texture_handle);
      if (texturedGeometry != null)
      {
        texturedGeometry.Draw();
      }

      GL.DepthMask(true);
      GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, new Color4(0.0f, 0.0f, 0.0f, 1f));
      GL.BindTexture(TextureTarget.Texture2D, 0);
      if (!CullBackFaces)
      {
        GL.Enable(EnableCap.CullFace);
      }

      base.Render3D();
    }

    public void Rescale(float x, float y, float z)
    {
      texturedGeometry.Scale(x, y, z);
    }

    public void Create(List<VertexTNV> vertex_list, int opengl_texture_handle)
    {
      if (vertex_list == null)
      {
        throw new ArgumentNullException("vertex_list cannot be null");
      }

      if (vertex_list.Count < 3)
      {
        throw new ArgumentException("vertex_list must have at least 3 vertices.");
      }

      if (vertex_list.Count % 3 != 0)
      {
        throw new ArgumentException("vertex_list.Count must be a multiple of 3.");
      }

      max = new Vector3(vertex_list[0].Position.X, vertex_list[0].Position.Y, vertex_list[0].Position.Z);
      min = new Vector3(vertex_list[0].Position.X, vertex_list[0].Position.Y, vertex_list[0].Position.Z);
      foreach (VertexTNV vertex in vertex_list)
      {
        if (vertex.Position.X < (double)min.X)
        {
          min.X = vertex.Position.X;
        }

        if (vertex.Position.Y < (double)min.Y)
        {
          min.Y = vertex.Position.Y;
        }

        if (vertex.Position.Z < (double)min.Z)
        {
          min.Z = vertex.Position.Z;
        }

        if (vertex.Position.X > (double)max.X)
        {
          max.X = vertex.Position.X;
        }

        if (vertex.Position.Y > (double)max.Y)
        {
          max.Y = vertex.Position.Y;
        }

        if (vertex.Position.Z > (double)max.Z)
        {
          max.Z = vertex.Position.Z;
        }
      }
      ext = new Vector3(max.X - min.X, max.Y - min.Y, max.Z - min.Z);
      texturedGeometry = new OpenGLRendererObject(new GraphicsModelData(vertex_list), true);
      texture_handle = opengl_texture_handle;
    }

    public int TextureHandle
    {
      get
      {
        return texture_handle;
      }
      set
      {
        texture_handle = value;
      }
    }

    public Color4 Ambient
    {
      get
      {
        return ambient;
      }
      set
      {
        ambient = value;
      }
    }

    public Color4 Diffuse
    {
      get
      {
        return diffuse;
      }
      set
      {
        diffuse = value;
      }
    }

    public Color4 Specular
    {
      get
      {
        return specular;
      }
      set
      {
        specular = value;
      }
    }

    public float Shininess
    {
      get
      {
        return shininess;
      }
      set
      {
        shininess = value;
      }
    }

    public Color4 Emission
    {
      get
      {
        return emission;
      }
      set
      {
        emission = value;
      }
    }

    public Vector3 Ext
    {
      get
      {
        return ext;
      }
    }

    public Vector3 Max
    {
      get
      {
        return max;
      }
    }

    public Vector3 Min
    {
      get
      {
        return min;
      }
    }
  }
}
