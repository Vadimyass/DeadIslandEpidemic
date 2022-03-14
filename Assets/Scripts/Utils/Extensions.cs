using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class Extensions
    {
        public static ValType GetSafe<KeyType, ValType>(this IDictionary<KeyType, ValType> self, KeyType key)
        {
            if (self.ContainsKey(key))
                return self[key];

            return default(ValType);
        }

        public static void ClearHierarchy(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i).gameObject;

                if (Application.isPlaying)
                {
                    Object.Destroy(child);
                }
                else
                {
                    Object.DestroyImmediate(child);
                }
            }
        }

        public static bool IsNull(this UnityEngine.Object o) => o == null || !o;

        public static string ToSceneInfo<T>(this T component) where T : Component
        {
            var sb = new StringBuilder();

            sb.Append(component.name);

            var iterator = component.transform.parent;

            while (iterator)
            {
                sb.Insert(0, $"{iterator.name}\\");
                iterator = iterator.parent;
            }

            sb.Insert(0, $"{component.gameObject.scene.name}\\");

            sb.Insert(0, $"[{component.GetType().Name}] - ");

            return sb.ToString();
        }

        /// <summary>
        /// Enbable or disable gameObject of given component.
        /// </summary>
        public static TComponent SetActive<TComponent>(this TComponent component, bool isActive) where TComponent : Component
        {
            if (component == false) throw new ArgumentNullException(nameof(component), $"Reference of component \'{typeof(TComponent)}\' is null.");

            component.gameObject.SetActive(isActive);
            return component;
        }
    }
}