using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace PostXcodeBuilder
{
    public class PostXcodeBuild
    {
        [PostProcessBuild]
        public static void SetXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget != BuildTarget.iOS) return;

            var plistPath = pathToBuiltProject + "/Info.plist";
            var plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            var rootDict = plist.root;
            // ここに記載したKey-ValueがXcodeのinfo.plistに反映されます
            rootDict.SetBoolean("UIFileSharingEnabled", true);
            rootDict.SetBoolean("LSSupportsOpeningDocumentsInPlace", true);
            rootDict.SetString("UILaunchStoryboardName~iphone", "LaunchScreen-iPhone");
            rootDict.SetString("UILaunchStoryboardName~ipod", "LaunchScreen-iPhone");

            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}