using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace CatAstropheGames
{
    public class CreateScriptableObject : MonoBehaviour
    {

#if UNITY_EDITOR

        public static void CreateMyAsset<T>() where T : ScriptableObject
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (CheckDir(path))
            {
                T asset = ScriptableObject.CreateInstance<T>();
                ProjectWindowUtil.CreateAsset(asset, path + "/New Scriptable Object.asset");
            }
        }

        private static bool CheckDir(string path)
        {
            if (path.Length > 0)
            {
                if (Directory.Exists(path))
                {
                    return true;
                } else
                {
                    Debug.LogError("Don't select a file, clik in a directory");
                }
            } else
            {
                Debug.LogError("Not in assets folder");
            }
            return false;
        }

#endif
    }

}