﻿// Decompiled with JetBrains decompiler
// Type: M3D.Spooling.Client.SpoolerMessage
// Assembly: M3DSpooling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D19DB185-E399-4809-A97E-0B15EB645090
// Assembly location: C:\Program Files (x86)\M3D - Software\2017.12.18.1.8.3.0\M3DSpooling.dll

using M3D.Spooling.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace M3D.Spooling.Client
{
  public class SpoolerMessage
  {
    [XmlIgnore]
    public MessageType Type;
    [XmlIgnore]
    public PrinterSerialNumber SerialNumber;
    [XmlAttribute("Message")]
    public string Message;
    [XmlAttribute("PlugInID")]
    public string PlugInID;
    [XmlAttribute("State")]
    public string State;
    private static XmlSerializer __class_serializer;

    public SpoolerMessage(MessageType type, PrinterSerialNumber serialnumber, string message, string pluginID, string state)
    {
      this.Type = type;
      this.SerialNumber = serialnumber;
      this.Message = message;
      this.PlugInID = pluginID;
      this.State = state;
    }

    public SpoolerMessage()
      : this(MessageType.UserDefined, PrinterSerialNumber.Undefined, "", "", "")
    {
    }

    public SpoolerMessage(MessageType type, PrinterSerialNumber serialnumber, string message)
      : this(type, serialnumber, message, "", "")
    {
    }

    public SpoolerMessage(MessageType type, string message)
      : this(type, PrinterSerialNumber.Undefined, message, "", "")
    {
    }

    public SpoolerMessage(string serialization)
    {
      using (TextReader textReader = (TextReader) new StringReader(serialization))
      {
        try
        {
          SpoolerMessage spoolerMessage = (SpoolerMessage) SpoolerMessage.ClassSerializer.Deserialize(textReader);
          this.Type = spoolerMessage.Type;
          this.SerialNumber = spoolerMessage.SerialNumber;
          this.Message = spoolerMessage.Message;
          this.State = spoolerMessage.State;
          this.PlugInID = spoolerMessage.PlugInID;
        }
        catch (Exception ex)
        {
          this.Type = MessageType.ErrorUndefinedMessage;
          this.SerialNumber = new PrinterSerialNumber("0");
          this.Message = (string) null;
        }
      }
    }

    public string Serialize()
    {
      XmlWriterSettings settings = new XmlWriterSettings();
      settings.OmitXmlDeclaration = true;
      StringWriter stringWriter = new StringWriter();
      XmlWriter xmlWriter = XmlWriter.Create((TextWriter) stringWriter, settings);
      XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
      namespaces.Add("", "");
      try
      {
        SpoolerMessage.ClassSerializer.Serialize(xmlWriter, (object) this, namespaces);
      }
      catch (Exception ex)
      {
        if (Debugger.IsAttached)
          Debugger.Break();
        throw;
      }
      return stringWriter.ToString();
    }

    public byte[] GetRawData()
    {
      if (this.Type == MessageType.RawData)
        return Convert.FromBase64String(this.Message);
      return (byte[]) null;
    }

    public override string ToString()
    {
      switch (this.Type)
      {
        case MessageType.PrinterConnected:
          return "The new printer is ready.";
        case MessageType.JobComplete:
          return "Finished printing " + this.Message;
        case MessageType.JobCanceled:
          return "Cancelled job " + this.Message;
        case MessageType.FirmwareUpdateComplete:
          return "Your printer has been updated. You're ready to go.";
        case MessageType.FirmwareUpdateFailed:
          return "Failed to update firmware.";
        case MessageType.ResetPrinterConnection:
        case MessageType.FirmwareErrorCyclePower:
          return "Unable to connect to the printer. Try resetting or reconnecting your printer.";
        case MessageType.UserDefined:
          return this.Message;
        case MessageType.RawData:
          return "Raw Serial Data";
        case MessageType.PrinterNotConnected:
          return "Sorry. Please make sure that a M3D printer has been connected.";
        case MessageType.BedLocationMustBeCalibrated:
          if (string.IsNullOrEmpty(this.Message))
            return "The Z location of your printer has been lost. Please clear the print bed. \n\nCalibrate bed location now?";
          return this.Message;
        case MessageType.BedOrientationMustBeCalibrated:
          if (string.IsNullOrEmpty(this.Message))
            return "Sorry. There is a problem with your factory gantry values. You will not be able to print until you calibrate your printer. \n\nCalibrate gantry now?";
          return this.Message;
        case MessageType.MicroMotionControllerFailed:
          if (string.IsNullOrEmpty(this.Message))
            return "Sorry. There seems to be a problem with your Micro-motion chip.";
          return this.Message;
        case MessageType.InvalidZ:
          return "Sorry. Your printer has lost its Z location. This can happen if the printer loses power either during a print or soon after a print has completed. You can not safely print with the printer in this state." + this.Message;
        case MessageType.ModelOutOfPrintableBounds:
          return "Sorry. Your model cannot be printed because it is out of the printable bounds of the printer.";
        case MessageType.PrinterTimeout:
          return "System has been inactive for too long, heater and motors have been turned off";
        case MessageType.WarningABSPrintLarge:
          return "Warning. Large ABS prints may fail. Do you want to continue printing?";
        case MessageType.IncompatibleSpooler:
          return "The software you are running is using a different version of the M3D Print Spooler interface. You may need to reinstall your software.";
        case MessageType.UnexpectedDisconnect:
          return "Your 3D printer disconnected unexpectedly.";
        case MessageType.CantStartJobPrinterBusy:
          return "Sorry. Can't start new print job because the printer is busy printing something else.";
        case MessageType.BacklashOutOfRange:
          return "Your printer has invalid backlash values.";
        case MessageType.CheckGantryClips:
          return "Please make sure that your gantry clips have been removed.";
        case MessageType.SinglePointCalibrationNotSupported:
          return "Sorry, but single-point calibration is not currently supported on this printer";
        case MessageType.MultiPointCalibrationNotSupported:
          return "Sorry, but five-point calibration is not currently supported on this printer";
        case MessageType.RPCError:
          return "A message sent to print spooler was invalid." + this.Message;
        default:
          return this.Type.ToString() + " - " + this.Message;
      }
    }

    [XmlAttribute("Type")]
    public string __type
    {
      get
      {
        return this.Type.ToString();
      }
      set
      {
        this.Type = (MessageType) Enum.Parse(typeof (MessageType), value, false);
      }
    }

    [XmlAttribute("Serialnumber")]
    public string __serialNumber
    {
      get
      {
        return this.SerialNumber.ToString();
      }
      set
      {
        this.SerialNumber = new PrinterSerialNumber(value);
      }
    }

    private static XmlSerializer ClassSerializer
    {
      get
      {
        if (SpoolerMessage.__class_serializer == null)
          SpoolerMessage.__class_serializer = new XmlSerializer(typeof (SpoolerMessage));
        return SpoolerMessage.__class_serializer;
      }
    }
  }
}