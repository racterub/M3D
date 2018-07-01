﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.Preprocessors.BondingPreprocessor
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

using M3D.Spooling.Common;
using M3D.Spooling.Common.Utils;
using M3D.Spooling.Core;
using M3D.Spooling.Preprocessors.Foundation;
using M3D.Spooling.Printer_Profiles;
using RepetierHost.model;
using System;

namespace M3D.Spooling.Preprocessors
{
  internal class BondingPreprocessor : IPreprocessor
  {
    private const float WAVE_PERIOD_LENGTH = 5f;
    private const float WAVE_PERIOD_LENGTH_QUARTER = 1.25f;
    private const float WAVE_SIZE = 0.15f;

    public override string Name
    {
      get
      {
        return "bonding";
      }
    }

    internal override bool ProcessGCode(GCodeFileReader input_reader, GCodeFileWriter output_writer, Calibration calibration, JobDetails jobdetails, InternalPrinterProfile printerProfile)
    {
      int wave_step = 0;
      int num1 = 0;
      bool flag1 = true;
      bool flag2 = true;
      bool flag3 = false;
      int num2 = 0;
      double num3 = 0.0;
      Position position = new Position();
      float num4 = (float) num3;
      float num5 = 0.0f;
      int firstLayerTemp = jobdetails.jobParams.preprocessor.bonding.FirstLayerTemp;
      int secondLayerTemp = jobdetails.jobParams.preprocessor.bonding.SecondLayerTemp;
      while (true)
      {
        GCode nextLine = input_reader.GetNextLine(false);
        if (nextLine != null)
        {
          if (!jobdetails.jobParams.options.use_wave_bonding)
          {
            output_writer.Write(nextLine);
          }
          else
          {
            if (nextLine.ToString().Contains(";LAYER:"))
            {
              int num6;
              try
              {
                num6 = int.Parse(nextLine.ToString().Substring(7));
              }
              catch (Exception ex)
              {
                num6 = 0;
                ErrorLogger.LogException("Exception in BondingPreProcessor.ProcessGcode " + ex.Message, ex);
              }
              if (num6 < num1)
                num1 = num6;
              flag2 = num6 == num1;
            }
            if (nextLine.hasG && (nextLine.G == (ushort) 0 || nextLine.G == (ushort) 1) && !flag1)
            {
              if (nextLine.hasX || nextLine.hasY)
                flag3 = true;
              float num6 = !nextLine.hasX ? 0.0f : nextLine.X - position.relativeX;
              float num7 = !nextLine.hasY ? 0.0f : nextLine.Y - position.relativeY;
              float num8 = !nextLine.hasZ ? 0.0f : nextLine.Z - position.relativeZ;
              float num9 = !nextLine.hasE ? 0.0f : nextLine.E - position.relativeE;
              position.absoluteX += num6;
              position.absoluteY += num7;
              position.absoluteZ += num8;
              position.absoluteE += num9;
              position.relativeX += num6;
              position.relativeY += num7;
              position.relativeZ += num8;
              position.relativeE += num9;
              if (nextLine.hasF)
                position.F = nextLine.F;
              float num10 = (float) Math.Sqrt((double) num6 * (double) num6 + (double) num7 * (double) num7);
              int num11 = 1;
              if ((double) num10 > 1.25)
                num11 = (int) ((double) num10 / 1.25);
              float num12 = position.absoluteX - num6;
              float num13 = position.absoluteY - num7;
              float num14 = position.relativeX - num6;
              float num15 = position.relativeY - num7;
              float num16 = position.relativeZ - num8;
              float num17 = position.relativeE - num9;
              float num18 = num6 / num10;
              float num19 = num7 / num10;
              float num20 = num8 / num10;
              float num21 = num9 / num10;
              if (flag2 && (double) num9 > 0.0)
              {
                for (int index = 1; index < num11 + 1; ++index)
                {
                  float num22;
                  float num23;
                  float num24;
                  float num25;
                  if (index == num11)
                  {
                    double absoluteX = (double) position.absoluteX;
                    double absoluteY = (double) position.absoluteY;
                    num22 = position.relativeX;
                    num23 = position.relativeY;
                    num24 = position.relativeZ;
                    num25 = position.relativeE;
                  }
                  else
                  {
                    num22 = num14 + (float) index * 1.25f * num18;
                    num23 = num15 + (float) index * 1.25f * num19;
                    num24 = num16 + (float) index * 1.25f * num20;
                    num25 = num17 + (float) index * 1.25f * num21;
                  }
                  double num26 = (double) num25 - (double) num5;
                  if (index != num11)
                  {
                    GCode code = new GCode();
                    code.G = nextLine.G;
                    if (nextLine.hasX)
                      code.X = (float) ((double) position.relativeX - (double) num6 + ((double) num22 - (double) num14));
                    if (nextLine.hasY)
                      code.Y = (float) ((double) position.relativeY - (double) num7 + ((double) num23 - (double) num15));
                    if (nextLine.hasF && index == 1)
                      code.F = nextLine.F;
                    if (flag3)
                      code.Z = (float) ((double) position.relativeZ - (double) num8 + ((double) num24 - (double) num16)) + this.CurrentAdjustmentsZ(ref wave_step);
                    else if (nextLine.hasZ && ((double) num8 > 1.40129846432482E-45 || (double) num8 < -1.40129846432482E-45))
                      code.Z = (float) ((double) position.relativeZ - (double) num8 + ((double) num24 - (double) num16));
                    code.E = (float) ((double) position.relativeE - (double) num9 + ((double) num25 - (double) num17)) + num4;
                    output_writer.Write(code);
                  }
                  else
                  {
                    if (flag3)
                    {
                      if (nextLine.hasZ)
                        nextLine.Z += this.CurrentAdjustmentsZ(ref wave_step);
                      else
                        nextLine.Z = num16 + num8 + this.CurrentAdjustmentsZ(ref wave_step);
                    }
                    nextLine.E += num4;
                  }
                  num5 = num25;
                }
              }
            }
            else if (nextLine.hasG && nextLine.G == (ushort) 92)
            {
              if (nextLine.hasE)
                position.relativeE = nextLine.E;
              if (printerProfile.OptionsConstants.G92WorksOnAllAxes)
              {
                if (nextLine.hasX)
                  position.relativeX = nextLine.X;
                if (nextLine.hasY)
                  position.relativeY = nextLine.Y;
                if (nextLine.hasZ)
                  position.relativeZ = nextLine.Z;
              }
              if (!nextLine.hasE && !nextLine.hasX && (!nextLine.hasY && !nextLine.hasZ))
              {
                position.relativeE = 0.0f;
                if (printerProfile.OptionsConstants.G92WorksOnAllAxes)
                {
                  position.relativeX = 0.0f;
                  position.relativeY = 0.0f;
                  position.relativeZ = 0.0f;
                }
              }
            }
            else if (nextLine.hasG && nextLine.G == (ushort) 90)
              flag1 = false;
            else if (nextLine.hasG && nextLine.G == (ushort) 91)
              flag1 = true;
            output_writer.Write(nextLine);
            ++num2;
          }
        }
        else
          break;
      }
      return true;
    }

    private float CurrentAdjustmentsZ(ref int wave_step)
    {
      double num1 = wave_step != 0 ? (wave_step != 2 ? 0.0 : -1.5) : 1.0;
      wave_step = (wave_step + 1) % 4;
      double num2 = 0.150000005960464;
      return (float) (num1 * num2);
    }

    private void ProcessForTackPoints(GCodeFileWriter output_writer, GCode currLine, GCode prevLine, ref GCode Last_Tack_Point, ref int cornercount)
    {
      if (cornercount <= 1)
      {
        if (!this.isSharpCorner(currLine, prevLine))
          return;
        if (Last_Tack_Point == null)
          this.doTackPoint(currLine, prevLine, output_writer);
        Last_Tack_Point = currLine;
        ++cornercount;
      }
      else
      {
        if (cornercount < 1 || !this.isSharpCorner(currLine, Last_Tack_Point))
          return;
        this.doTackPoint(currLine, Last_Tack_Point, output_writer);
        Last_Tack_Point = currLine;
      }
    }

    public double distance(BondingPreprocessor.Vector2 A, BondingPreprocessor.Vector2 B)
    {
      return Math.Sqrt(Math.Pow((double) A.x - (double) B.x, 2.0) + Math.Pow((double) A.y - (double) B.y, 2.0));
    }

    public bool isSharpCorner(GCode currLine, GCode prevLine)
    {
      bool flag = false;
      BondingPreprocessor.Vector2 dot1 = new BondingPreprocessor.Vector2(currLine);
      BondingPreprocessor.Vector2 dot2 = new BondingPreprocessor.Vector2(prevLine);
      double num1 = Math.Pow((double) dot1.Dot(dot1), 2.0);
      double num2 = Math.Pow((double) dot2.Dot(dot2), 2.0);
      double num3 = Math.Acos((double) dot1.Dot(dot2) / (num1 * num2));
      if (num3 > 0.0 && num3 < 1.57079633)
        flag = true;
      return flag;
    }

    public void doTackPoint(GCode currLine, GCode Last_Tack_Point, GCodeFileWriter output_writer)
    {
      int num = (int) Math.Ceiling(this.distance(new BondingPreprocessor.Vector2(currLine), new BondingPreprocessor.Vector2(Last_Tack_Point)));
      if (num <= 5)
        return;
      output_writer.Write(new GCode("G4 P" + num.ToString()));
    }

    public class Vector2
    {
      public float x;
      public float y;

      public Vector2(GCode G)
      {
        this.x = G.X;
        this.y = G.Y;
      }

      public float Dot(BondingPreprocessor.Vector2 dot)
      {
        return this.x * dot.x + this.y + dot.y;
      }
    }
  }
}