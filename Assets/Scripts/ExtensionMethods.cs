using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Klid
{
    static class ExtensionMethods
    {
        public static GameObject InstantiateIntoRectTransform(this GameObject prefab, GameObject parent, Vector3 pos)
        {
            GameObject gameObject = GameObject.Instantiate<GameObject>(prefab);
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.SetParent(parent.transform, false);
            rectTransform.anchoredPosition = pos;
            return gameObject;
        }

        public static void InstantiateEnumerable<T>(this GameObject prefab, IEnumerable<T> source, GameObject parent, Vector2 beginningOffset, RectTransform.Axis stackDirection, Vector2 stackItemOffset, UnityAction<GameObject, T> setValuesMethod)
        {
            RectTransform rectTransform = prefab.GetComponent<RectTransform>();
            float prefabWidth = rectTransform.rect.width;
            float prefabHeight = rectTransform.rect.height;

            Vector2 offset = beginningOffset;
            foreach(T item in source)
            {
                setValuesMethod(prefab.InstantiateIntoRectTransform(parent, offset), item);

                offset += stackItemOffset;
                if (stackDirection == RectTransform.Axis.Horizontal)
                    offset.x += prefabWidth;
                else
                    offset.y += prefabHeight;
            }
        }
    }
}
