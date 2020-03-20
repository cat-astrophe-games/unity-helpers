using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

namespace CatAstropheGames
{
    public class IOSPermissionsToPlist
    {

        [PostProcessBuild]
        public static void ChangeXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                // Get plist
                string plistPath = pathToBuiltProject + "/Info.plist";
                PlistDocument plist = new PlistDocument();
                plist.ReadFromString(File.ReadAllText(plistPath));

                // Get root
                PlistElementDict rootDict = plist.root;

                // background location useage key (new in iOS 8)
                rootDict.SetString("NSLocationAlwaysUsageDescription", "The challenge will be update your progress after you turn off the screen.");
                rootDict.SetString("NSLocationWhenInUseUsageDescription", "Know in which district you are.");

                //First of all you have to activate the background processing in 'Background Mode'.

                // background modes
                // https://developer.apple.com/documentation/bundleresources/information_property_list/uibackgroundmodes
                PlistElementArray bgModes = rootDict.CreateArray("UIBackgroundModes");
                bgModes.AddString("location");
                bgModes.AddString("fetch");
                //audio
                //processing

                // Write to file
                File.WriteAllText(plistPath, plist.WriteToString());
            }
        }
    }
}