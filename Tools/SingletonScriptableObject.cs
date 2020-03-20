using System.Linq;
using UnityEngine;

namespace CatAstropheGames
{
    /// <summary>
    /// Abstract class for making reload-proof singletons out of ScriptableObjects
    /// Returns the asset created on the editor, or null if there is none
    /// Based on https://www.youtube.com/watch?v=VBA1QCoEAX4
    /// </summary>
    /// <author>Bruno Araújo</author>
    /// <see cref="https://baraujo.net/unity3d-making-singletons-from-scriptableobjects-automatically/"/>
    /// <typeparam name="T">Singleton type</typeparam>

    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        static T _instance = null;
        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    T[] allOfTypeT = Resources.FindObjectsOfTypeAll<T>();
                    if (allOfTypeT.Length != 1)
                    {
                        throw new System.Exception("There could be only instance of singleton scriptable object " + typeof(T).Name + ", but are: " + allOfTypeT.Length);
                    }

                    _instance = allOfTypeT.FirstOrDefault();
                }
                return _instance;
            }
        }
    }
}