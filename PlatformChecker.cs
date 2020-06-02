using UnityEngine;
using System.Collections;
using System;

namespace CatAstropheGames
{
    /// <summary>
    /// Uthe Unity classes one have to check three Editors (windows, osx, linux) to check for the platform.
    /// Also naming is not excellent: IPhonePlayer platform doesn't sound like it handles all IOS mobile cases
    /// 
    /// This class gives you Editor and iOS enums for simpler handling
    /// </summary>
    public static class PlatformChecker
    {
        public static Platform GetPlatform()
        {
            if (IsEditor())
            {
                return Platform.Editor;
            }
            else if (IsIOS())
            {
                return Platform.iOS;
            }
            else if (IsDesktop())
            {
                return Platform.Desktop;
            } else 
            {
                throw new Exception("Platform not handled: " + Application.platform);
            }
        }

        private static bool IsDesktop()
        {
            return Application.platform == RuntimePlatform.WindowsPlayer
                || Application.platform == RuntimePlatform.LinuxPlayer
                || Application.platform == RuntimePlatform.OSXPlayer;
        }

        private static bool IsEditor()
        {
            return Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXEditor
            || Application.platform == RuntimePlatform.LinuxEditor;
        }

        private static bool IsIOS()
        {
            return Application.platform == RuntimePlatform.IPhonePlayer;
        }


        public enum Platform
        {
            Editor, iOS, Desktop
        }
    }
}