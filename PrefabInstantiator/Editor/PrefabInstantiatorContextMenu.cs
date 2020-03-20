using UnityEditor;
using UnityEngine;

namespace CatAstropheGames
{
    public static class PrefabInstantiatorContextMenu
    {

        [MenuItem("GameObject/Prefab Instantiator/Instantiate", false, 1)]
        public static void Instantiate()
        {
            Instantiate(Selection.activeObject);
        }

        private static void Instantiate(UnityEngine.Object selectedObject)
        {

            foreach (PrefabInstantiator instantiator in (selectedObject as GameObject).transform.GetComponentsInChildren<PrefabInstantiator>(true))
            {
                Instantiate(instantiator.prefabToInstantiate, instantiator.gameObject);
            }
        }

        private static void Instantiate(GameObject prefab, GameObject parent)
        {
            (PrefabUtility.InstantiatePrefab(prefab) as GameObject).transform.SetParent(parent.transform, false);
        }
    }
}