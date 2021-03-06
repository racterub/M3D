﻿using M3D.GUI.Interfaces;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace M3D
{
  public class WinFileAssociations : IFileAssociations
  {
    [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

    public string ExtensionOpenWith(string Extension)
    {
      var registryKey1 = (RegistryKey) null;
      var registryKey2 = (RegistryKey) null;
      var str1 = (string) null;
      try
      {
        registryKey1 = Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + Extension);
        var obj = registryKey1.GetValue("");
        if (obj != null)
        {
          var str2 = obj.ToString();
          registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + str2);
          if (registryKey2 != null)
          {
            str1 = registryKey2.OpenSubKey("Shell").OpenSubKey("open").OpenSubKey("command").GetValue("").ToString();
          }
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        registryKey1?.Close();
        registryKey2?.Close();
      }
      return str1;
    }

    public void Set3DFileAssociation(string Extension, string KeyName, string OpenWith, string FileDescription, string fileIcon)
    {
      var registryKey1 = (RegistryKey) null;
      var registryKey2 = (RegistryKey) null;
      var registryKey3 = (RegistryKey) null;
      var registryKey4 = (RegistryKey) null;
      bool flag;
      try
      {
        registryKey1 = Registry.CurrentUser.CreateSubKey("Software\\Classes\\" + Extension);
        registryKey1.SetValue("", KeyName);
        registryKey1.SetValue("DefaultIcon", fileIcon, RegistryValueKind.String);
        registryKey2 = Registry.CurrentUser.CreateSubKey("Software\\Classes\\" + KeyName);
        registryKey2.SetValue("", FileDescription);
        registryKey2.CreateSubKey("DefaultIcon").SetValue("", "\"" + fileIcon + "\",0");
        registryKey3 = registryKey2.CreateSubKey("Shell");
        registryKey3.CreateSubKey("open").CreateSubKey("command").SetValue("", "\"" + OpenWith + "\" \"%1\"");
        registryKey4 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\" + Extension, true);
        registryKey4?.DeleteSubKey("UserChoice", false);
        WinFileAssociations.SHChangeNotify(134217728U, 0U, IntPtr.Zero, IntPtr.Zero);
        flag = true;
      }
      catch (Exception ex)
      {
        flag = false;
      }
      finally
      {
        registryKey1?.Close();
        registryKey2?.Close();
        registryKey3?.Close();
        registryKey4?.Close();
      }
      if (flag)
      {
        return;
      }

      Delete3DFileAssociation(Extension, KeyName);
    }

    public void Delete3DFileAssociation(string Extension, string KeyName)
    {
      Delete3DFileAssociation("Software\\Classes\\" + Extension);
      Delete3DFileAssociation("Software\\Classes\\" + KeyName + "\\Shell\\edit\\command");
      Delete3DFileAssociation("Software\\Classes\\" + KeyName + "\\Shell\\open\\command");
      Delete3DFileAssociation("Software\\Classes\\" + KeyName + "\\Shell\\edit");
      Delete3DFileAssociation("Software\\Classes\\" + KeyName + "\\Shell\\open");
      Delete3DFileAssociation("Software\\Classes\\" + KeyName + "\\Shell");
      Delete3DFileAssociation("Software\\Classes\\" + KeyName + "\\DefaultIcon");
      Delete3DFileAssociation("Software\\Classes\\" + KeyName);
    }

    private void Delete3DFileAssociation(string subKey)
    {
      try
      {
        Registry.CurrentUser.DeleteSubKey(subKey);
      }
      catch (Exception ex)
      {
      }
    }
  }
}
