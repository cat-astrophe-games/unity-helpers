using UnityEngine;
using System.Collections;

namespace CatAstropheGames
{
    public static class RectTransformExtensions
    {
        public static Vector2 GetDimensions(this RectTransform rt)
        {
            return new Vector2(rt.rect.width, rt.rect.height);
        }
        public static void FitDimensions(this RectTransform rt, Vector2 dimensions, bool fitHeight, bool fitWidth)
        {
            if (!fitHeight && !fitWidth)
            {
                return;
            }

            rt.sizeDelta = new Vector2(fitWidth ? dimensions.x : rt.sizeDelta.x, fitHeight ? dimensions.y : rt.sizeDelta.y);
        }

        public static void SetSize(this RectTransform trans, Vector2 newSize)
        {
            Vector2 oldSize = trans.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
            trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
        }
    }

}