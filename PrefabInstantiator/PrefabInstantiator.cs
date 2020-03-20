using System;
using System.Collections.Generic;
using UnityEngine;

namespace CatAstropheGames
{
    public class PrefabInstantiator : MonoBehaviour
    {
        [SerializeField] public GameObject prefabToInstantiate;
        [SerializeField] private bool InstantiateOnAwake;

        private List<GameObject> InstantiatedObjects = new List<GameObject>();

        private void Awake()
        {
            if (InstantiateOnAwake)
            {
                Instantiate();
            }
        }

        public T Instantiate<T>()
        {
            return this.Instantiate().GetComponent<T>();
        }

        public GameObject Instantiate()
        {
            GameObject newObject = Instantiate(prefabToInstantiate, this.transform, false);
            this.InstantiatedObjects.Add(newObject);
            return newObject;
        }

        internal void DeleteAllCreatedInstances()
        {
            foreach(GameObject instance in this.InstantiatedObjects)
            {
                Destroy(instance);
            }
            this.InstantiatedObjects.Clear();
        }
    }
}