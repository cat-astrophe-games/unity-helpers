using System;
using UnityEngine;

namespace CatAstropheGames
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("Instance is null before the mono behaviour awokes in the scene. Maybe you didn't even add it to the scene.");
                }

                return instance;
            }
        }

        private void Awake()
        {
            instance = this as T;
        }
    }
}